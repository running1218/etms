namespace ETMS.Security
{
    /// <summary>
    /// ��֤�쳣
    /// </summary>
    public class AuthenticateException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// ��֤�쳣
        /// </summary>
        public AuthenticateException()
            : base("��֤ʧ��")
        {
        }
        /// <summary>
        /// ��֤�쳣
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        public AuthenticateException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// ��֤�쳣
        /// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="ex">�ڲ��쳣</param>
        public AuthenticateException(string message, System.Exception ex)
            : base(message, ex)
        {
        }
    }
}
