using System;

namespace ETMS.Utility.RequestAuth
{
    /// <summary>
    /// ������֤
    /// ֻ��������Resources.RequestAllowedHost�а���������������
    /// </summary>
    internal static class AuthUtility
    {
        /// <summary>
        /// ������Դ��֤
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
