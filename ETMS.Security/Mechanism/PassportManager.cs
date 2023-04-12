
using System;
using System.Web;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using MCS.Library.Core;
using ETMS.Security.Properties;

namespace ETMS.Security
{
    /// <summary>
    /// Passport管理类。
    /// </summary>
    public sealed class PassportManager
    {
        /// <summary>
        /// Ticket在url中的参数名称
        /// </summary>
        public const string TicketParamName = "s_t";

        private static readonly string[] ReservedParams = { PassportManager.TicketParamName, "ru", "to", "aid", "ip", "lou" };

        private PassportManager()
        {
        }

        #region 静态方法
        /// <summary>
        /// 清除认证服务的Cookie
        /// </summary>
        public static void ClearSignInCookie()
        {
            Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpCookie cookie = request.Cookies[Common.C_SIGNIN_COOKIE_KEY];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);

                cookie.Value = null;
                response.SetCookie(cookie);
            }
        }

        /// <summary>
        /// 清除应用的Cookie
        /// </summary>
        public static void ClearAppSignInCookie()
        {
            Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpCookie cookie = request.Cookies[Common.C_TICKET_COOKIE_KEY];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);

                cookie.Value = null;
                response.SetCookie(cookie);
            }
        }

        #endregion
        /// <summary>
        /// 获取注销后重定向地址
        /// </summary>
        /// <returns>注销后重定向url</returns>
        public static Uri GetLogOffCallBackUrl()
        {
            string strLouUrl = string.Empty;
            string locu = PassportClientSettings.GetConfig().LogOffCallBackUrl.ToString();

            HttpRequest request = HttpContext.Current.Request;

            if (locu == string.Empty)
                strLouUrl = request.Url.GetComponents(
                                UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) +
                                (request.ApplicationPath == "/" ?
                                    request.ApplicationPath + Common.C_LOGOFF_CALLBACK_VIRTUAL_PATH :
                                    request.ApplicationPath + "/" + Common.C_LOGOFF_CALLBACK_VIRTUAL_PATH);
            else
                strLouUrl = ChangeUrlToCurrentServer(locu);

            return new Uri(strLouUrl, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// 从Cookie中得到Ticket
        /// </summary>
        /// <returns><see cref="ITicket"/> 对象。</returns>
        public static ITicket GetTicket(out bool fromCookie)
        {
            fromCookie = false;

            Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;

            ITicket ticket = Ticket.LoadFromUrl();

            if (IsTicketValid(ticket) == false)
            {
                ticket = Ticket.LoadFromCookie();	//从Cookie中加载Ticket

                if (ticket != null)
                {
                    fromCookie = true;
                    Trace.WriteLine(string.Format("从cookie中找到用户{0}的ticket", ticket.SignInInfo.UserID), "PassportSDK");
                }
            }

            if (IsTicketValid(ticket) == true)
                AdjustSignInTimeout(ticket);

            return ticket;
        }
        /// <summary>
        /// 得到认证页面的URL
        /// </summary>
        /// <param name="strReturlUrl">返回的URL</param>
        /// <returns>得到认证页面的URL</returns>
        public static string GetAjaxSignInPageUrl(string strReturlUrl)
        {
            Common.CheckHttpContext();
            //如果站点启用了自定义的认证页面
            return PassportClientSettings.GetConfig().AjaxSignInUrl.ToString() + GetExtraRequestParams(strReturlUrl);
        }
        /// <summary>
        /// 得到认证页面的URL
        /// </summary>
        /// <param name="strReturlUrl">返回的URL</param>
        /// <returns>得到认证页面的URL</returns>
        public static string GetSignInPageUrl(string strReturlUrl)
        {
            Common.CheckHttpContext();
            //如果站点启用了自定义的认证页面
            if (PassportClientSettings.GetConfig().CustomSignInUrl != null)
            {
                return PassportClientSettings.GetConfig().CustomSignInUrl.ToString() + GetExtraRequestParams(strReturlUrl);
            }
            else
            {
                return PassportClientSettings.GetConfig().SignInUrl.ToString() + GetExtraRequestParams(strReturlUrl);
            }
        }

        /// <summary>
        /// 获取登录或注销的url，设置url中的认证后重定向的returnUrl
        /// </summary>
        /// <param name="returnUrl">认证通过后重定向地址</param>
        /// <returns>登录或是注销url</returns>
        public static string GetLogOnOrLogOffUrl(string returnUrl)
        {
            return GetLogOnOrLogOffUrl(returnUrl, true);
        }

        /// <summary>
        /// 获取登录或注销的url，设置url中的认证后重定向的returnUrl，设置注销后重定向的logOffAutoRedirect
        /// </summary>
        /// <param name="returnUrl">认证后重定向的地址</param>
        /// <param name="logOffAutoRedirect">是否注销后重定向</param>
        /// <returns>登录或是注销url</returns>
        public static string GetLogOnOrLogOffUrl(string returnUrl, bool logOffAutoRedirect)
        {
            Common.CheckHttpContext();
            string strResult = string.Empty;

            bool fromCookie = false;
            ITicket ticket = GetTicket(out fromCookie);

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            PassportClientSettings settings = PassportClientSettings.GetConfig();
            string strPassportPath = settings.SignInUrl.ToString();

            int nSplit = strPassportPath.LastIndexOf("/");
            strPassportPath = strPassportPath.Substring(0, nSplit + 1);

            if (IsTicketValid(ticket) == true)
            {
                StringBuilder strB = new StringBuilder(1024);

                strB.Append(settings.LogOffUrl);
                strB.AppendFormat("?asid={0}&ru={1}&lar={2}&appID={3}&lou={4}&loa=y&wi={5}&lu={6}",
                    ticket.SignInInfo.SignInSessionID,
                    HttpUtility.UrlEncode(returnUrl),
                    logOffAutoRedirect.ToString().ToLower(),
                    ticket.AppID,
                    HttpUtility.UrlEncode(GetLogOffCallBackUrl().ToString()),
                    ticket.SignInInfo.WindowsIntegrated.ToString().ToLower(),
                    ticket.SignInInfo.OriginalUserID
                    );

                strResult = strB.ToString();
            }
            else
                strResult = GetSignInPageUrl(returnUrl);

            return strResult;
        }

        /// <summary>
        /// 根据当前的Web请求，得到认证后需要重定向的url。在此过程中检查"t"参数是否存在，如果存在，则抛出异常
        /// </summary>
        /// <returns>认证后需要重定向的url</returns>
        public static string GetReturnUrl()
        {
            Common.CheckHttpContext();
            HttpRequest request = HttpContext.Current.Request;

            StringBuilder strB = new StringBuilder(2048);

            strB.Append(request.Url.GetLeftPart(UriPartial.Path));

            bool bFirstParam = true;

            foreach (string strKey in request.QueryString)
            {
                if (strKey != PassportManager.TicketParamName)
                {
                    ExceptionHelper.TrueThrow<AuthenticateException>(
                        Array.Exists<string>(PassportManager.ReservedParams, delegate(string data)
                        {
                            return string.Compare(strKey, data, true) == 0;
                        }),
                        Resource.ParamIsReserved, strKey);

                    if (bFirstParam)
                    {
                        strB.Append("?");
                        bFirstParam = false;
                    }
                    else
                        strB.Append("&");

                    strB.Append(strKey + "=" + request.QueryString[strKey]);
                }
            }

            return strB.ToString();
        }

        /// <summary>
        /// 检查应用的认证Cookie是否有效。如果失效，会自动转到认证页面
        /// </summary>
        public static void CheckAuthenticated()
        {
            CheckAuthenticated(true);
        }

        /// <summary>
        /// 检查应用的认证Cookie是否有效。如果失效，根据autoRedirect参数来决定是否转到认证页面
        /// </summary>
        /// <param name="autoRedirect">是否自动转到认证页面</param>
        public static ITicket CheckAuthenticated(bool autoRedirect)
        {
            Common.CheckHttpContext();
            HttpContext context = HttpContext.Current;

            bool fromCookie = false;
            ITicket ticket = GetTicket(out fromCookie);

            if (IsTicketValid(ticket) == false)//无效安全认证令牌
            {
                //if (autoRedirect)
                context.Response.Redirect(GetSignInPageUrl(GetReturnUrl()));
            }
            else
            {
                if (IsOthersLoginOnTime(ticket))
                {
                    PassportManager.ClearSignInCookie();
                    context.Response.Redirect(GetSignInPageUrl(GetReturnUrl()));                     
                }
                else
                {
                    ticket.SaveToCookie();
                }
            }
            return ticket;
        }

        /// <summary>
        /// 注销（HTML输出）
        /// </summary>
        /// <param name="Request"></param>
        private static void LogOff(HttpRequest Request)
        {
            //System.Collections.Specialized.NameValueCollection QueryString = FillFromString(Request.RawUrl, true, System.Text.Encoding.Default);
            //Request.QueryString.Add(QueryString);
            IList<string> logOffUrls = DefaultAuthenticator.LogOffAllAPP();
            string ReturnUrl = Request.QueryString["RedirectUrl"] ?? System.Configuration.ConfigurationManager.AppSettings["DefaultHomeUrl"];
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Type", "text/html");
            Response.Write(@"
<div>正在注销。。。</div>
    <div style='display:none'>");
            foreach (string item in logOffUrls)
            {
                Response.Write(string.Format("<img src='{0}' />", item));
            }
            Response.Write("</div>");
            Response.Write(@"
<script language='javascript'>
    window.onload = function () {
       window.location.href = '");
            Response.Write(ReturnUrl);
            Response.Write(@"';
    }
</script>");


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        static bool IsOthersLoginOnTime(ITicket ticket)
        {
            //int userID = int.Parse(ticket.SignInInfo.UserID);
            //var source = DefaultAuthenticator.GetPassportAccessLog(userID);

            //if (null != source && source.Rows.Count > 0)
            //{
            //    if (ticket.SignInInfo.SignInSessionID.Equals(source.Rows[0]["LastLoginIP"].ToString()) && source.Rows[0]["LoginStatus"].ToString().ToLower() == "true")
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}

            //return true;

            return false;
        }

        /// <summary>
        /// 设置当前应用上下文（当前机构编码）
        /// 修改：薛永波
        /// 目的：供应用设置当前用户所处的机构环境
        /// 时间：2011-6-8
        /// </summary>
        /// <param name="appEnvironment">应用上下文参数（机构编码</param>
        public static void SetAppEnvironment(string appEnvironment)
        {
            bool fromCookie;
            ITicket ticket = PassportManager.GetTicket(out fromCookie);
            ticket.AppEnvironment = appEnvironment;
            ticket.SaveToCookie();
        }

        /// <summary>
        /// 获取应用上下文参数（当前机构编码）
        /// 修改：薛永波
        /// 目的：供应用获取当前用户所处的机构环境
        /// 时间：2011-6-8
        /// </summary>
        /// <returns>（当前机构编码）</returns>
        public static string GetAppEnvironment()
        {
            bool fromCookie;
            ITicket ticket = PassportManager.GetTicket(out fromCookie);
            return ticket.AppEnvironment;
        }
        #region 私有方法
        private static bool IsTicketValid(ITicket ticket)
        {
            return ticket != null && ticket.IsValid();
        }

        private static void AdjustSignInTimeout(ITicket ticket)
        {
            if (PassportClientSettings.GetConfig().HasSlidingExpiration)
                ticket.AppSignInTime = DateTime.Now;
        }

        /// <summary>
        /// 将指向相对路径的Url映射到当前服务器，生成一个绝对路径
        /// </summary>
        /// <param name="strUrl">相对路径的Url</param>
        /// <returns>绝对路径的Url</returns>
        private static string ChangeUrlToCurrentServer(string strUrl)
        {
            string result = string.Empty;
            Uri url = new Uri(strUrl, UriKind.RelativeOrAbsolute);

            if (url.IsAbsoluteUri == false)
                result = HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped)
                    + "/" + url.ToString();
            else
                result = url.ToString();

            return result;
        }

        private static string GetExtraRequestParams(string strReturlUrl)
        {
            HttpRequest request = HttpContext.Current.Request;
            string ru = HttpUtility.UrlEncode(strReturlUrl);
            string to = PassportClientSettings.GetConfig().AppSignInTimeout.ToString();
            string aid = PassportClientSettings.GetConfig().AppID;
            string ip = request.UserHostAddress;
            string lou = HttpUtility.UrlEncode(GetLogOffCallBackUrl().ToString());
            string sf = ETMS.Utility.Cryptography.MD5Utility.MD516(string.Format("{0},{1},{2},{3},{4}@{5}", new string[] { ru, to, aid, ip, lou, Common.C_URL_PASSWORD }));
            return "?ru=" + ru
                    + "&to=" + to
                    + "&aid=" + aid
                    + "&ip=" + request.UserHostAddress
                    + "&lou=" + lou
                    + "&sf=" + sf;
        }
        #endregion 私有方法
    }
}
