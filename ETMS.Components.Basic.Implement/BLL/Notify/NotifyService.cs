using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using Common.Logging;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Utility.BizCache;
using ETMS.Utility.Service.Notify;
using ETMS.Components.Basic.API.Entity.Notify;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// ETMS提醒服务（DataBase支持）
    /// </summary>
    public class NotifyService : INotifyMessageSourceService, INotifyStrategy, INotifyService
    {
        #region 内部成员

        private Notify_MessageConfigLogic MessageConfigLogic = new Notify_MessageConfigLogic();

        private Notify_MessageLogic MessageLogic = new Notify_MessageLogic();

        private ILog Logger = LogManager.GetLogger(typeof(NotifyService));

        #endregion

        #region 公开属性

        /// <summary>
        /// 消息类型
        /// 1：邮件
        /// 2：短信
        /// 3：站内消息
        /// </summary>
        public short MessageType { get; set; }

        /// <summary>
        /// 消息队列
        /// </summary>
        public INotifyQueueLogic MessageQueue { get; set; }

        #endregion

        #region INotifyMessageSourceService

        public NotifyMessage GetMessage(string messageClass, object context)
        {
            #region 1、获取消息配置项
            Notify_MessageConfig messageConfigItem = GetMessageConfigItem(messageClass);
            string subjectTemplate = "";
            string bodyTemplate = "";
            if (MessageType == 1)//邮件
            {
                subjectTemplate = messageConfigItem.EmailSubjectTemplate;
                bodyTemplate = messageConfigItem.EmailBodyTemplate;
            }
            else if (MessageType == 2)//短信
            {
                subjectTemplate = messageConfigItem.SMSSubjectTemplate;
                bodyTemplate = messageConfigItem.SMSBodyTemplate;
            }
            else if (MessageType == 3)//站内信
            {
                subjectTemplate = messageConfigItem.SiteInfoSubjectTemplate;
                bodyTemplate = messageConfigItem.SiteInfoBodyTemplate;
            }
            else
            {
                throw new Exception("不支持此消息类型!MessageType=" + MessageType.ToString());
            }
            #endregion

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(string.Format("获取消息模板定义：消息类别={0},消息类型={1}，标题模板={2}，内容模板={3}", messageClass, (MessageType == 1 ? "邮件" : (MessageType == 2 ? "短信" : "站内信")), subjectTemplate, bodyTemplate));
            }
            #region 2、消息发送上下文设置(模板变量）

            Hashtable templateVariables = new Hashtable();
            //接收者上下文
            System.Reflection.PropertyInfo property = context.GetType().GetProperty("UserInfo");
            object userContext = property.GetValue(context, null);
            this.FillTemplateVariable(templateVariables, userContext);

            //业务参数上下文
            property = context.GetType().GetProperty("Context");
            object bizContext = property.GetValue(context, null);
            this.FillTemplateVariable(templateVariables, bizContext);

            if (Logger.IsDebugEnabled)
            {
                StringBuilder writer = new StringBuilder();
                foreach (string key in templateVariables.Keys)
                {
                    writer.AppendFormat("\r\n{0}={1}", key, templateVariables[key]);
                }
                Logger.Debug("模板变量如下：" + writer.ToString());
            }
            #endregion

            #region 3、构造消息
            property = context.GetType().GetProperty("Receiver");//接收者
            object receiver = property.GetValue(context, null);

            NotifyMessage messageObject = new NotifyMessage();
            messageObject.Title = ReplaceTemplateVariables(subjectTemplate, templateVariables);//替换标题
            messageObject.Body = ReplaceTemplateVariables(bodyTemplate, templateVariables);//替换正文
            messageObject.Receiver = receiver.ToString();
            messageObject.MessageClassID = messageConfigItem.MessageClassID;

            if (templateVariables["${BeginTime}"] != null)
                messageObject.MessageEndTime = templateVariables["${BeginTime}"].ToDateTime();
            else
                messageObject.MessageEndTime = default(DateTime);

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(string.Format("格式化后消息：\r\n接收者={0}\r\n标题={1}\r\n内容={2}", messageObject.Receiver, messageObject.Title, messageObject.Body));
            }

            return messageObject;
            #endregion

        }

        #endregion

        #region INotifyStrategy

        public char[] GetStrategy(string messageClass)
        {
            Notify_MessageConfig messageConfigItem = GetMessageConfigItem(messageClass);
            string sendStrategy = string.Format("{0}{1}{2}", (messageConfigItem.IsEnableEmail ? 1 : 0)
                 , (messageConfigItem.IsEnableSMS ? 1 : 0)
                 , (messageConfigItem.IsEnableSiteInfo ? 1 : 0));
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(string.Format("获取消息发送策略：消息类别={0},发送策略=发送邮件{1}，发送短信{2}，发送站内信{3}"
                    , messageClass
                    , messageConfigItem.IsEnableEmail
                    , messageConfigItem.IsEnableSMS
                    , messageConfigItem.IsEnableSiteInfo));
            }
            return sendStrategy.ToCharArray();
        }

        #endregion

        #region INotifyService

        public void Notify(NotifyMessage message)
        {
            //1、消息入库
            Notify_Message messageObject = new Notify_Message();
            try
            {
                //2、发送
                switch (MessageType)
                {
                    case 1://邮件 
                    case 2://短信（本身不发送，仅记录，然后又后台服务发送）
                        messageObject.Status = 0;
                        break;
                    case 3://站内信（无需发送）
                        messageObject.Status = 3;
                        break;
                    default:
                        throw new Exception("不支持此消息类型发送!MessageType=" + MessageType.ToString());
                }
            }
            catch (Exception ex)
            {
                messageObject.Status = 2;//发送失败
                messageObject.Remark = ex.ToString();
                throw new ETMS.AppContext.BusinessException("Notify.SendEmailFailed", ex);
            }
            finally
            {
                //消息入库
                messageObject.MessageTypeID = MessageType;
                messageObject.MessageClassID = message.MessageClassID;
                messageObject.OrganizationID = UserContext.Current.OrganizationID;
                messageObject.CreatorID = UserContext.Current.UserID;
                messageObject.Subject = message.Title;
                messageObject.Body = message.Body;
                messageObject.Receiver = message.Receiver;
                messageObject.CreateTime = DateTime.Now;
                messageObject.MessageEndTime = message.MessageEndTime;
                MessageLogic.Save(messageObject);
            }
        }

        #endregion

        #region helper

        /// <summary>
        /// 将模板内容中的模板变量替换
        /// </summary>
        /// <param name="templeteContent">模板内容</param>
        /// <param name="templateVariables">模板变量</param>
        /// <returns>替换后的模板内容</returns>
        private string ReplaceTemplateVariables(string templeteContent, Hashtable templateVariables)
        {
            foreach (string templateVariableName in templateVariables.Keys)
            {
                object value = templateVariables[templateVariableName];
                if (value != null)//如果未设置，则不替换
                {
                    //替换
                    templeteContent = templeteContent.Replace(templateVariableName, value.ToString());
                }
            }
            return templeteContent;
        }

        /// <summary>
        /// 填充模板变量
        /// </summary>
        /// <param name="templateVariables">模板变量</param>
        /// <param name="Context">上下文</param>
        private void FillTemplateVariable(Hashtable templateVariables, object Context)
        {
            foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(Context))
            {
                string propertyName = propertyDescriptor.Name;
                object propertyValue = propertyDescriptor.GetValue(Context);
                //模板变量"${变量名称},如：${UserName}
                templateVariables.Add("${" + propertyName + "}", propertyValue);
            }

        }

        /// <summary>
        /// 根据消息类别获取消息配置项
        /// </summary>
        /// <param name="messageClass"></param>
        /// <returns></returns>
        private Notify_MessageConfig GetMessageConfigItem(string messageClass)
        {
            //1、获取本机构的业务消息配置,如果没有找到，则取默认业务消息配置
            int currentOrgID = UserContext.Current.OrganizationID;

            //缓存键
            //config/BizCache.config中定义缓存过期策略
            string key = "Notify_MessageTemplateConfig";
            string cacheItemKey = string.Format("{0}/{1}", messageClass, currentOrgID);
            IList<Notify_MessageConfig> list = BizCacheHelper.GetOrInsertItem<IList<Notify_MessageConfig>>(key, cacheItemKey, () =>
            {
                int totalRecords = 0;
                IList<Notify_MessageConfig> result = MessageConfigLogic.GetEntityList(1, 1, " OrganizationID desc", string.Format(@" 
AND MessageClassID in(select MessageClassID from Notify_MessageClass where MessageClassName='{0}')
AND Status=1 
AND OrganizationID in (0,{1})", messageClass, currentOrgID), out totalRecords);
                if (totalRecords == 0)
                {
                    throw new BusinessException("Notify.NotFoundMessageConfigItem", new object[] { messageClass });
                }
                else
                {
                    return result;
                }
            });
            return list[0];
        }

        #endregion

    }
}
