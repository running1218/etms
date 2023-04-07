using System;
using System.Web;
using System.Configuration;
using ETMS.Utility.Service;

namespace ETMS.Utility
{
    public class WebUtility
    { 
        /// <summary>
        /// 站点目录路径
        /// </summary>
        public static string AppPath
        {
            get
            {
                string path = HttpContext.Current.Request.ApplicationPath;
                return ("/" == path) ? "" : path;
            }
        }
        /// <summary>
        /// WebApi地址
        /// </summary>
        public static string WebApiPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["WebApiPath"] != null ? ConfigurationManager.AppSettings["WebApiPath"].ToString() : "";
                return ("/" == path) ? "" : path;
            }
        }
        /// <summary>
        /// 移动端访问地址
        /// </summary>
        public static string MobileAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["MobileCallAddress"] ?? string.Empty;
            }
        }
        
        /// <summary>
        /// file physical path
        /// </summary>
        public static string FileRoot
        {
            get
            {
                return (ServiceRepository.FileUploadStrategyService as  ETMS.Utility.Service.FileUpload.DefaultFileUploadStrategyService).Root;
            }
        }


        /// <summary>
        /// file domain and root
        /// </summary>
        public static string FileUrlRoot
        {
            get
            {
                return (ServiceRepository.FileUploadStrategyService as ETMS.Utility.Service.FileUpload.DefaultFileUploadStrategyService).UrlRoot;
            }
        }

        public static string PlatTitle
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["PlatTitle"]) ? string.Empty : ConfigurationManager.AppSettings["PlatTitle"];
            }
        }

        public static string CopyRight
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["CopyRight"]) ? string.Empty : ConfigurationManager.AppSettings["CopyRight"];
            }
        }

        public static int DefaultVistorNum
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultVistorNum"]) ? 0 : ConfigurationManager.AppSettings["DefaultVistorNum"].ToInt();
            }
        }
        public static int OrgMaxExamNum
        {
            get
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrgMaxExamNum"]) ? 0 : ConfigurationManager.AppSettings["OrgMaxExamNum"].ToInt();
            }
        }

        public static string Version
        {
            get {
                return DateTime.Now.ToShortTimeString();
            }
        }
    }    
}
