namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 提醒消息的来源服务接口
    /// 本职工作：将业务数据转换为标准的消息格式
    /// </summary>
    public interface INotifyMessageSourceService
    {
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="obj">格式化消息需要的参数</param>
        NotifyMessage GetMessage(string messageType, object context);
    }
}
