namespace ETMS.Security
{
    /// <summary>
    /// ��Ч�����쳣
    /// </summary>
    public class InValidRequestException : ETMS.AppContext.BusinessException
    {
        /// <summary>
        /// ����URL
        /// </summary>
        public string RequestUrl { get; set; }


        /// <summary>
        /// ��Ȩ�쳣
        /// </summary>
        /// <param name="requestUrl">����ҳ��</param>
        public InValidRequestException(string requestUrl)
            : base("��Ч����url���۸ģ�")
        {
            this.RequestUrl = requestUrl;
        }
        /// <summary>
        /// ��Ȩ�쳣
        /// </summary>
        /// <param name="message">�쳣����</param>
        /// <param name="requestUrl">����ҳ��</param>
        public InValidRequestException(string message, string requestUrl)
            : base(message)
        {
            this.RequestUrl = requestUrl;
        }
    }
}
