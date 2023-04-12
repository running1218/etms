namespace ETMS.Security
{
    /// <summary>
    /// 授权异常
    /// </summary>
    public class AuthorizationException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// 请求URL
        /// </summary>
        public string RequestUrl { get; set; }


        /// <summary>
        /// 授权异常
        /// </summary>
        /// <param name="requestUrl">请求页面</param>
        public AuthorizationException(string requestUrl)
            : base("您没有此页面的操作权限！")
        {
            this.RequestUrl = requestUrl;
        }
        /// <summary>
        /// 授权异常
        /// </summary>
        /// <param name="message">异常描述</param>
        /// <param name="requestUrl">请求页面</param>
        public AuthorizationException(string message, string requestUrl)
            : base(message)
        {
            this.RequestUrl = requestUrl;
        }
    }
}
