namespace ETMS.Security
{
    /// <summary>
    /// 无效访问异常
    /// </summary>
    public class InValidRequestException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// 请求URL
        /// </summary>
        public string RequestUrl { get; set; }


        /// <summary>
        /// 授权异常
        /// </summary>
        /// <param name="requestUrl">请求页面</param>
        public InValidRequestException(string requestUrl)
            : base("无效请求，url被篡改！")
        {
            this.RequestUrl = requestUrl;
        }
        /// <summary>
        /// 授权异常
        /// </summary>
        /// <param name="message">异常描述</param>
        /// <param name="requestUrl">请求页面</param>
        public InValidRequestException(string message, string requestUrl)
            : base(message)
        {
            this.RequestUrl = requestUrl;
        }
    }
}
