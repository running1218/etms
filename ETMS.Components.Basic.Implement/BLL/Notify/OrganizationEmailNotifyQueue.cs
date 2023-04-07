using System.Collections.Generic;
using Common.Logging;

namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    using ETMS.Utility;
    using ETMS.Utility.Service.Notify;
    using ETMS.Components.Basic.API.Entity.Security;
    using ETMS.Components.Basic.Implement.BLL.Security;
    /// <summary>
    /// 各机构邮件发送队列
    /// </summary>
    public class OrganizationEmailNotifyQueue : INotifyQueueLogic
    {
        #region 内部成员
        private ILog Logger = LogManager.GetLogger(typeof(OrganizationEmailNotifyQueue));
        #endregion

        private int m_OrganizationID;

        /// <summary>
        /// 如果没有设置，则默认取当前用户上下文中的机构ID
        /// </summary>
        public int OrganizationID
        {
            get
            {
                if (m_OrganizationID == 0)
                {
                    return ETMS.AppContext.UserContext.Current.OrganizationID;
                }
                return m_OrganizationID;
            }
            set
            {
                m_OrganizationID = value;
            }
        }

        public int Send(string reciever, string subject, string body, int level, string appMan)
        {
            //获取当前机构
            int organizationID = this.OrganizationID;
            //获取机构对应的smtp配置
            SMTPConfig smtpConfig = GetOrganizationSMTPConfig(organizationID);
            //发送邮件
            ETMS.Utility.EmailUtility.SendEmail(new EmailMsg(subject, body, reciever), smtpConfig);
            return 1;
        }

        /// <summary>
        /// 发送邮件 （用于找回密码时无法获得机构ID时传入机构ID）
        /// </summary>
        /// <param name="reciever"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="level"></param>
        /// <param name="appMan"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public int Send(string reciever, string subject, string body, int level, string appMan,int orgID)
        {
            //获取机构对应的smtp配置
            SMTPConfig smtpConfig = GetOrganizationSMTPConfig(orgID);
            //发送邮件
            ETMS.Utility.EmailUtility.SendEmail(new EmailMsg(subject, body, reciever), smtpConfig);
            return 1;
        }

        private SMTPConfig GetOrganizationSMTPConfig(int organizationID)
        {
            Site_SysConfigLogic logic = new Site_SysConfigLogic();
            IList<Site_SysConfig> sysConfigItems = logic.GetOrganizationConfig(organizationID, Site_SysConfigGroup.SMTPConfigGroup.ConfigGroupID);

            SMTPConfig smtpConfig = new SMTPConfig();
            //将数据库中配置项集合转换为配置对象属性赋值
            foreach (Site_SysConfig item in sysConfigItems)
            {
                //获取配置属性并赋值
                System.Reflection.PropertyInfo propertyInfo = smtpConfig.GetType().GetProperty(item.Name);
                if (propertyInfo != null)
                {
                    //如果用户自定义值为空，则取默认值
                    string value = string.IsNullOrEmpty(item.UserValue) ? item.DefaultValue : item.UserValue;
                    propertyInfo.SetValue(smtpConfig, value, null);
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.Debug(string.Format("数据库配置项“{0}”，赋值“{1}”到类型{2}中对应属性!", item.Name, value, smtpConfig.GetType()));
                    }
                }
                else
                {
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.Debug(string.Format("数据库配置项“{0}”，在类型{1}中未找到对应属性!", item.Name, smtpConfig.GetType()));
                    }
                }
            }
            return smtpConfig;

        }
    }

}
