using Autumn.Context.Support;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility.Service.Notify;
namespace ETMS.Utility.Service
{
    /// <summary>
    /// 服务仓库
    /// 每个API服务提供一个单例，供外部应用调用
    /// </summary>
    public abstract class ServiceRepository
    { 
        #region 基础服务

        #region 消息发送服务，支持：邮件:Email、短信:SMS、站内信:SiteInfo三种类型推送

        /// <summary>
        /// 邮件-消息提醒服务
        /// </summary>
        public static INotifyService EmailNotifyService
        {
            get
            {
                return (INotifyService)ContextRegistry.GetContext().GetObject("EmailNotifyService");
            }
        }
        /// <summary>
        /// 短信-消息提醒服务
        /// </summary>
        public static INotifyService SMSNotifyService
        {
            get
            {
                return (INotifyService)ContextRegistry.GetContext().GetObject("SMSNotifyService");
            }
        }

        #endregion

        #region 消息源服务，支持：邮件:Email、短信:SMS、站内信:SiteInfo三种类型消息配置
        /// <summary>
        /// 站内信-消息提醒服务
        /// </summary>
        public static INotifyService SiteInfoNotifyService
        {
            get
            {
                return (INotifyService)ContextRegistry.GetContext().GetObject("SiteInfoNotifyService");
            }
        }
        /// <summary>
        ///  邮件-消息源服务
        /// </summary>
        public static INotifyMessageSourceService EmailNotifyMessageSourceService
        {
            get
            {
                return (INotifyMessageSourceService)ContextRegistry.GetContext().GetObject("EmailNotifyMessageSourceService");
            }
        }
        /// <summary>
        ///  短信-消息源服务
        /// </summary>
        public static INotifyMessageSourceService SMSNotifyMessageSourceService
        {
            get
            {
                return (INotifyMessageSourceService)ContextRegistry.GetContext().GetObject("SMSNotifyMessageSourceService");
            }
        }
        /// <summary>
        ///  站内信-消息源服务
        /// </summary>
        public static INotifyMessageSourceService SiteInfoNotifyMessageSourceService
        {
            get
            {
                return (INotifyMessageSourceService)ContextRegistry.GetContext().GetObject("SiteInfoNotifyMessageSourceService");
            }
        }
        #endregion

        #region 消息提醒提醒策略服务

        /// <summary>
        ///  消息提醒策略服务
        /// </summary>
        public static INotifyStrategy NotifyStrategyService
        {
            get
            {
                return (INotifyStrategy)ContextRegistry.GetContext().GetObject("NotifyStrategyService");
            }
        }

        #endregion

        #region 统一文件上传服务
        /// <summary>
        /// 文件上传服务
        /// </summary>
        public static IFileUploadService FileUploadService
        {
            get
            {
                return (IFileUploadService)ContextRegistry.GetContext().GetObject("FileUploadService");
            }
        }
        /// <summary>
        /// 文件上传策略服务
        /// </summary>
        public static IFileUploadStrategyService FileUploadStrategyService
        {
            get
            {
                return (IFileUploadStrategyService)ContextRegistry.GetContext().GetObject("FileUploadStrategyService");
            }
        }
        #endregion 
        #endregion

      
    }
}
