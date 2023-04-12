namespace ETMS.Security
{
    /// <summary>
    /// 定义了应用LogOff时回调的Url
    /// </summary>
    public class AppLogOffCallBackUrl
    {
        private string appID = string.Empty;
        private string logOffCallBackUrl = string.Empty;

        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppID
        {
            get { return this.appID; }
            set { this.appID = value; }
        }

        /// <summary>
        /// 应用注销回调的Url
        /// </summary>
        public string LogOffCallBackUrl
        {
            get { return this.logOffCallBackUrl; }
            set { this.logOffCallBackUrl = value; }
        }
    }
}
