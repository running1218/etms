using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Configuration;
namespace ETMS.Utility
{
    public static class HrefUtility
    {
        private static string[] S_ExcludeSafeUrlRules = null;
        
        /// <summary>
        /// 排除url安全规则（不参与url校验）
        /// </summary>
        public static string[] ExcludeSafeUrlRules
        {
            get
            {
                if (S_ExcludeSafeUrlRules == null)
                {
                    NameValueCollection excludeSafeUrlSettings = (ConfigurationManager.GetSection("excludeSafeUrlSettings") as NameValueCollection);
                    if (excludeSafeUrlSettings == null)
                    {
                        S_ExcludeSafeUrlRules = new string[0];
                    }
                    else
                    {
                        S_ExcludeSafeUrlRules = new string[excludeSafeUrlSettings.Count];
                        excludeSafeUrlSettings.CopyTo(S_ExcludeSafeUrlRules, 0);
                    }
                }
                return S_ExcludeSafeUrlRules;
            }
        }

      


        /// <summary>
        /// md5加密密码
        /// </summary>
        public static string UrlPassword
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Security.DefaultPassword"] ?? "1234567890";
            }
        }

        /// <summary>
        /// 是否启用安全的url
        /// </summary>
        public static bool IsEnableSafeUrl
        {
            get
            {
                string enableFlag = System.Configuration.ConfigurationManager.AppSettings["Security.IsEnableSafeUrl"] ?? "false";
                return bool.Parse(enableFlag);
            }
        }

        /// <summary>
        /// url中令牌参数标识
        /// </summary>
        public static string UrlTokenKeyName = "token";

        /// <summary>
        /// 获取安全的url串
        /// </summary>
        /// <param name="pathAndQueryString">路径+url参数，如~/xxx.aspx?a=11&b=22</param>
        /// <returns>安全的url串</returns>
        public static string ActionHref(string pathAndQueryString)
        {
            Control control = (System.Web.HttpContext.Current.Handler as System.Web.UI.Page);
            return ActionHref(control, pathAndQueryString);
        }
        /// <summary>
        ///  获取安全的url串
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="pathAndQueryString">路径+url参数，如~/xxx.aspx?a=11&b=22</param>
        /// <returns>安全的url串</returns>
        public static string ActionHref(this Control control, string pathAndQueryString)
        {
            if (string.IsNullOrEmpty(pathAndQueryString))
            {
                throw new ArgumentNullException("pathAndQueryString");
            }
            //翻译~成客户端路径
            pathAndQueryString = control.ResolveUrl(pathAndQueryString);

            int index = pathAndQueryString.IndexOf("?");
            if (index == -1)//如果没有任何url参数，则直接返回
                return pathAndQueryString;
            string path = pathAndQueryString.Substring(0, index);
            string querystring = pathAndQueryString.Substring(index + 1);

            string[] keyValues = querystring.Split('&');

            StringBuilder writer = new StringBuilder();
            StringBuilder md5ParamsWriter = new StringBuilder();          
            //解析匿名类所有属性 
            foreach (string item in keyValues)
            {
                string[] strs = item.Split('=');
                if (strs.Length != 2)
                {
                    continue;
                }
                string propertyName = strs[0];
                object propertyValue = strs.Length > 0 ? strs[1] : "";

                writer.AppendFormat("&{0}={1}", propertyName, propertyValue);
                md5ParamsWriter.AppendFormat("{0},", propertyValue);
            }

            if (writer.Length == 0)//如果没有参数，则直接返回
            {
                return path;
            }
            path = path + "?" + writer.ToString().TrimStart('&');
            //是否启用加密功能
            if (IsEnableSafeUrl)
            {
                string md5key = CreateToken(md5ParamsWriter.ToString());
                path += string.Format("&{0}={1}", UrlTokenKeyName, md5key);
            }
            return path;
        }

        public static string ActionHrefNoControl(string pathAndQueryString)
        {
            if (string.IsNullOrEmpty(pathAndQueryString))
            {
                throw new ArgumentNullException("pathAndQueryString");
            }
            //翻译~成客户端路径
            pathAndQueryString = ResolveUrl(pathAndQueryString);

            int index = pathAndQueryString.IndexOf("?");
            if (index == -1)//如果没有任何url参数，则直接返回
                return pathAndQueryString;
            string path = pathAndQueryString.Substring(0, index);
            string querystring = pathAndQueryString.Substring(index + 1);

            string[] keyValues = querystring.Split('&');

            StringBuilder writer = new StringBuilder();
            StringBuilder md5ParamsWriter = new StringBuilder();
            //解析匿名类所有属性 
            foreach (string item in keyValues)
            {
                string[] strs = item.Split('=');
                if (strs.Length != 2)
                {
                    continue;
                }
                string propertyName = strs[0];
                object propertyValue = strs.Length > 0 ? strs[1] : "";

                writer.AppendFormat("&{0}={1}", propertyName, propertyValue);
                md5ParamsWriter.AppendFormat("{0},", propertyValue);
            }

            if (writer.Length == 0)//如果没有参数，则直接返回
            {
                return path;
            }
            path = path + "?" + writer.ToString().TrimStart('&');
            //是否启用加密功能
            if (IsEnableSafeUrl)
            {
                string md5key = CreateToken(md5ParamsWriter.ToString());
                path += string.Format("&{0}={1}", UrlTokenKeyName, md5key);
            }
            return path;
        }

        /// <summary>
        /// 获取安全的url串
        /// </summary>
        /// <param name="pathAndQueryString">路径+url参数，如~/xxx.aspx?a=11&b=22</param>
        /// <returns>安全的url串</returns>
        public static string ActionHref(string path, object routeValues)
        {
            Control control = (System.Web.HttpContext.Current.Handler as System.Web.UI.Page);
            return ActionHref(control, path, routeValues);
        }
        /// <summary>
        /// 获取安全的url串
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="path">路径</param>
        /// <param name="routeValues">通过匿名类传递url参数键值对，如new {a="11",b="22"}</param>
        /// <returns>安全的url串</returns>
        public static string ActionHref(this Control control, string path, object routeValues)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (routeValues == null)
            {
                throw new ArgumentNullException("routeValues");
            }
            StringBuilder writer = new StringBuilder();
            StringBuilder md5ParamsWriter = new StringBuilder();
            //解析匿名类所有属性 

            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(routeValues))
            {
                string propertyName = propertyDescriptor.Name;
                object propertyValue = propertyDescriptor.GetValue(routeValues);

                writer.AppendFormat("&{0}={1}", propertyName, propertyValue);
                md5ParamsWriter.AppendFormat("{0},", propertyValue);
            }

            path = control.ResolveUrl(path);
            if (writer.Length == 0)//如果没有参数，则直接返回
            {
                return path;
            }
            path = path + "?" + writer.ToString().TrimStart('&');
            //是否启用加密功能
            if (IsEnableSafeUrl)
            {
                string md5key = CreateToken(md5ParamsWriter.ToString());
                path += string.Format("&{0}={1}", UrlTokenKeyName, md5key);
            }
            return path;
        }

        /// <summary>
        /// 创建安全Token
        /// </summary>
        /// <param name="parms">url参数传，按照顺序多个直接用“,”分割</param>
        /// <returns></returns>
        public static string CreateToken(string parms)
        {
            string userID = ETMS.AppContext.UserContext.Current.UserID.ToString();
            if (!parms.EndsWith(","))
            {
                parms += ",";
            }
            return ETMS.Utility.Cryptography.MD5Utility.MD516(string.Format("{0}{1},{2}", parms, UrlPassword, userID));

        }

        public static string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");

            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' ||
                relativeUrl[0] == '\\') return relativeUrl;

            int idxOfScheme =
              relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
            if (idxOfScheme != -1)
            {
                int idxOfQM = relativeUrl.IndexOf('?');
                if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
            }

            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
            if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

            // found question mark already? query string, do not touch!
            bool foundQM = false;
            bool foundSlash; // the latest char was a slash?
            if (relativeUrl.Length > 1
                && relativeUrl[0] == '~'
                && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
            {
                relativeUrl = relativeUrl.Substring(2);
                foundSlash = true;
            }
            else foundSlash = false;
            foreach (char c in relativeUrl)
            {
                if (!foundQM)
                {
                    if (c == '?') foundQM = true;
                    else
                    {
                        if (c == '/' || c == '\\')
                        {
                            if (foundSlash) continue;
                            else
                            {
                                sbUrl.Append('/');
                                foundSlash = true;
                                continue;
                            }
                        }
                        else if (foundSlash) foundSlash = false;
                    }
                }
                sbUrl.Append(c);
            }

            return sbUrl.ToString();
        }
    }
}
