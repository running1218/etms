using System;

using Common.Logging;
using ETMS.Utility.Service.Notify;
namespace ETMS.Utility
{
    [Serializable]
    public class NotifyReceiver
    {
        public string UserID { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
    }
    /// <summary>
    /// 消息提醒辅助器
    /// 内部有完整的策略支持
    /// </summary>
    public class NotifyUtility
    {
        private static ILog Logger = LogManager.GetLogger(typeof(NotifyUtility));
 
        /// <summary>
        /// 消息提醒
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="receiver">消息接收者</param>
        /// <param name="context">附带消息参数（建议通过匿名类来传递）要求：以实际配置中NotifyMessageConfig需要的参数为准！</param>
        public static void Notify(string messageType, NotifyReceiver receiver, object context)
        {
            string UserID = receiver.UserID;
            string LoginName = receiver.LoginName;
            string UserName = receiver.UserName;
            string MobilePhone = receiver.MobilePhone;
            string Email = receiver.Email;
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(string.Format("消息提醒开始，消息类型={0},用户信息={1}|{6}|{2}|{3}|{4},业务参数={5}", messageType, UserID, UserName, MobilePhone, Email, context, LoginName));
            }
            //1、根据消息类型获取消息发送策略（是否支持发邮件、是否支持发短信、是否支持发站内信）
            INotifyStrategy strategyService = ETMS.Utility.Service.ServiceRepository.NotifyStrategyService;
            char[] strategy = strategyService.GetStrategy(messageType);//邮件、短信、站内信
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(string.Format("获取消息提醒策略配置：发邮件({0})发短信({1})发站内信({2})", (strategy[0] == '1' ? "Y" : "N"), (strategy[1] == '1' ? "Y" : "N"), (strategy[2] == '1' ? "Y" : "N")));
            }
            //2、已经策略执行对应消息发送
            INotifyMessageSourceService messageSourceService = null;
            INotifyService notifyService = null;
            //发送Email
            if (strategy[0] == '1')
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("进入发【邮件】环节...");
                }
                messageSourceService = ETMS.Utility.Service.ServiceRepository.EmailNotifyMessageSourceService;
                notifyService = ETMS.Utility.Service.ServiceRepository.EmailNotifyService;

                NotifyMessage message = messageSourceService.GetMessage(messageType, new { UserInfo = receiver, Context = context, Receiver = Email });
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(string.Format("获取标准的【邮件】内容：接收者={0} 标题={1} 正文={2} 优先级={3})", message.Receiver, message.Title, message.Body, message.Level));
                }
                notifyService.Notify(message);

                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("完成【邮件】发送！");
                }
            }
            //发送SMS
            if (strategy[1] == '1')
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("进入发【短信】环节...");
                }
                messageSourceService = ETMS.Utility.Service.ServiceRepository.SMSNotifyMessageSourceService;
                notifyService = ETMS.Utility.Service.ServiceRepository.SMSNotifyService;
                NotifyMessage message = messageSourceService.GetMessage(messageType, new { UserInfo = receiver, Context = context, Receiver = MobilePhone });
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(string.Format("获取标准的【短信】内容：接收者={0} 标题={1} 正文={2} 优先级={3})", message.Receiver, message.Title, message.Body, message.Level));
                }
                notifyService.Notify(message);
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("完成【短信】发送！");
                }
            }
            //发送SiteInfo
            if (strategy[2] == '1')
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("进入发【站内信】环节...");
                }
                messageSourceService = ETMS.Utility.Service.ServiceRepository.SiteInfoNotifyMessageSourceService;
                notifyService = ETMS.Utility.Service.ServiceRepository.SiteInfoNotifyService;
                NotifyMessage message = messageSourceService.GetMessage(messageType, new { UserInfo = receiver, Context = context, Receiver = UserID });
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(string.Format("获取标准的【站内信】内容：接收者={0} 标题={1} 正文={2} 优先级={3})", message.Receiver, message.Title, message.Body, message.Level));
                }
                notifyService.Notify(message);
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("完成【站内信】发送！");
                }
            }
        }
    }
}
