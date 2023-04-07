namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 提醒队列服务 
    /// </summary>   
    public interface INotifyQueueLogic
    {
        /// <summary>
        /// 发送提醒
        /// </summary>
        /// <param name="reciever">接收者</param>
        /// <param name="subject">标题</param>
        /// <param name="body">正文</param>    
        /// <param name="level">优先级别</param>
        /// <param name="appMan">申请者（系统名称，例如：ITOMS）</param>
        /// <returns>返回此提醒对应任务ID，用于业务系统记录</returns> 
        int Send(string reciever, string subject, string body, int level, string appMan);
    }
}
