using Autumn.Objects.Factory;
using Common.Logging;
namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 默认的消息提醒服务实现
    /// </summary>
    public class DefaultNotifyService : INotifyService, IInitializingObject
    {
        private ILog Logger = LogManager.GetLogger(typeof(DefaultNotifyService));
        /// <summary>
        /// 依赖注入属性：消息队列
        /// </summary>
        public INotifyQueueLogic MessageQueue { get; set; }
        /// <summary>
        /// 消息源名称（如应用系统名称）
        /// </summary>
        public string MessageSourceName { get; set; }
        /// <summary>
        /// 应用上下文加载，对象属性应用后，检查属性是否完全初始化！
        /// </summary>
        public void AfterPropertiesSet()
        {
            if (this.MessageQueue == null)
            {
                Logger.Info("当前提醒服务没有配置属性【MessageQueue】！");
            }
        }


        public void Notify(NotifyMessage message)
        {
            if (this.MessageQueue != null)
            {
                //将消息写入队列
                //默认规则：如果未设定消息来源，则默认将消息Title属性做为消息源！         
                 MessageQueue.Send(message.Receiver, message.Title, message.Body, (int)message.Level, MessageSourceName ?? message.Title);
            }
        }
    }
}
