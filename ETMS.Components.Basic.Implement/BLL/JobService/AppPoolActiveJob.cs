using System;

using System.Net;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 应用程序池激活Job
    /// </summary>
    public class AppPoolActiveJob : IJobService
    {
        /// <summary>
        /// 默认Url
        /// </summary>
        public string DefaultUrl { get; set; }

        public AppPoolActiveJob(string defaultUrl)
        {
            this.DefaultUrl = defaultUrl;
        }
        public int DoJob()
        {
            if (!string.IsNullOrEmpty(this.DefaultUrl))
            {
                RequestUrl(this.DefaultUrl);
                return 1;
            }
            else
            {
                if (this.Logger != null && this.Logger.IsDebug)
                {
                    this.Logger.Debug("请求URL为空！应用程序池激活任务不启用！ ");
                }
                return 0;
            }
        }

        public OES.Logger.ILog Logger { get; set; }

        /// <summary>
        /// URL请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>URL请求成功或失败</returns>
        private bool RequestUrl(string url)
        {
            HttpWebRequest webRequest = null;
            HttpWebResponse webResponse = null;
            bool isOk = false;
            try
            {
                webRequest = (HttpWebRequest)HttpWebRequest.CreateDefault(new Uri(url));
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                isOk = webResponse.StatusCode == HttpStatusCode.OK;

                if (this.Logger != null && this.Logger.IsDebug)
                {
                    this.Logger.Debug(string.Format("请求URL={0}，请求结果={1}", url, webResponse.StatusCode));
                }
            }
            catch (Exception ex)
            {
                if (this.Logger != null && this.Logger.IsError)
                {
                    this.Logger.Error(string.Format("请求URL={0}，请求失败！原因：{1}", url, ex.ToString()));
                }
                isOk = false;
            }
            finally
            {
                try
                {
                    if (webResponse != null)
                        webResponse.Close();
                    if (webRequest != null)
                        webRequest.Abort();
                }
                catch
                {
                }
            }
            return isOk;
        }

    }
}
