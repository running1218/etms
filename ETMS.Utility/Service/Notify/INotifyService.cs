namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 消息提醒服务接口
    /// </summary>
    public interface INotifyService
    {
        /// <summary>
        /// 消息提醒
        /// </summary>
        /// <param name="message">消息内容</param>
        void Notify(NotifyMessage message);   
    }
}
