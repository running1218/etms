namespace ETMS.Security
{
    /// <summary>
    /// ������Ӧ��LogOffʱ�ص���Url
    /// </summary>
    public class AppLogOffCallBackUrl
    {
        private string appID = string.Empty;
        private string logOffCallBackUrl = string.Empty;

        /// <summary>
        /// Ӧ��ID
        /// </summary>
        public string AppID
        {
            get { return this.appID; }
            set { this.appID = value; }
        }

        /// <summary>
        /// Ӧ��ע���ص���Url
        /// </summary>
        public string LogOffCallBackUrl
        {
            get { return this.logOffCallBackUrl; }
            set { this.logOffCallBackUrl = value; }
        }
    }
}
