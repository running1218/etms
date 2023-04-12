namespace ETMS.Security
{
    /// <summary>
    /// ��Ȩ�쳣
    /// </summary>
    public class AuthorizationException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// ����URL
        /// </summary>
        public string RequestUrl { get; set; }


        /// <summary>
        /// ��Ȩ�쳣
        /// </summary>
        /// <param name="requestUrl">����ҳ��</param>
        public AuthorizationException(string requestUrl)
            : base("��û�д�ҳ��Ĳ���Ȩ�ޣ�")
        {
            this.RequestUrl = requestUrl;
        }
        /// <summary>
        /// ��Ȩ�쳣
        /// </summary>
        /// <param name="message">�쳣����</param>
        /// <param name="requestUrl">����ҳ��</param>
        public AuthorizationException(string message, string requestUrl)
            : base(message)
        {
            this.RequestUrl = requestUrl;
        }
    }
}
