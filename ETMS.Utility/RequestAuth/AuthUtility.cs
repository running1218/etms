using System;

namespace ETMS.Utility.RequestAuth
{
    /// <summary>
    /// 请求验证
    /// 只允许来自Resources.RequestAllowedHost中包含的域名的请求
    /// </summary>
    internal static class AuthUtility
    {
        /// <summary>
        /// 请求来源验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool Authenticate(System.Web.HttpContext context)
        {
            if (context.Request != null && context.Request.UrlReferrer != null)
            {
                string requestHost = context.Request.UrlReferrer.Host.ToLower();
                string[] allowedHosts = Properties.Resources.RequestAllowedHost.ToLower().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < allowedHosts.Length; i++)
                {
                    string allowedHost = allowedHosts[i];
                    if (requestHost.Contains(allowedHost))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        
    }
}
