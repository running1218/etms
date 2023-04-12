namespace ETMS.Security
{
    /// <summary>
    /// 认证异常
    /// </summary>
    public class AuthenticateException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// 认证异常
        /// </summary>
        public AuthenticateException()
            : base("认证失败")
        {
        }
        /// <summary>
        /// 认证异常
        /// </summary>
        /// <param name="message">异常信息</param>
        public AuthenticateException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 认证异常
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="ex">内部异常</param>
        public AuthenticateException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
