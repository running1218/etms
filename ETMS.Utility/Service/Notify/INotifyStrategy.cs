namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 消息提醒策略
    /// </summary>
    public interface INotifyStrategy
    {
        /// <summary>
        /// 获取特定类型消息对应的提醒策略
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <returns>返回此消息对应的提醒策略（支持email？支持sms？支持SiteInfo?)以{‘1','0','0'}格式返回</returns>
        char[] GetStrategy(string messageType);
    }
}
