
using System.Web;

namespace ETMS.Utility
{
    public static class UserHelper
    {
        /// <summary>
        /// 获取用户标识
        /// 适用Forms验证、Windows验证
        /// </summary>
        /// <returns>用户标识</returns>
        public static string GetUserIdentity()
        {
            //返回当前登录者标识
            //return ETMS.AppContext.UserContext.Current.UserName;
            if (ETMS.AppContext.UserContext.Current != null)
                return ETMS.AppContext.UserContext.Current.UserName;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns>用户IP地址</returns>
        public static string GetUserIp()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// 获取当前服务器名称
        /// </summary>
        /// <returns></returns>
        public static string GetServerName()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MachineName;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// 获取用户请求页面地址
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.RawUrl;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取用户所处机构ID
        /// </summary>
        /// <returns></returns>
        public static int GetUserInOrganizationID()
        {
            return ETMS.AppContext.UserContext.Current.OrganizationID;
        }
    }
}
