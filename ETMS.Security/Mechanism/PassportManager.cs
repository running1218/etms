
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
    /// Passport�����ࡣ
    /// </summary>
    public sealed class PassportManager
    {
        /// <summary>
        /// Ticket��url�еĲ�������
        /// </summary>
        public const string TicketParamName = "s_t";

        private static readonly string[] ReservedParams = { PassportManager.TicketParamName, "ru", "to", "aid", "ip", "lou" };

        private PassportManager()
        {
        }

        #region ��̬����
        /// <summary>
        /// �����֤�����Cookie
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
        /// ���Ӧ�õ�Cookie
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
        /// ��ȡע�����ض����ַ
        /// </summary>
        /// <returns>ע�����ض���url</returns>
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
        /// ��Cookie�еõ�Ticket
        /// </summary>
        /// <returns><see cref="ITicket"/> ����</returns>
        public static ITicket GetTicket(out bool fromCookie)
        {
            fromCookie = false;

            Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;

            ITicket ticket = Ticket.LoadFromUrl();

            if (IsTicketValid(ticket) == false)
            {
                ticket = Ticket.LoadFromCookie();	//��Cookie�м���Ticket

                if (ticket != null)
                {
                    fromCookie = true;
                    Trace.WriteLine(string.Format("��cookie���ҵ��û�{0}��ticket", ticket.SignInInfo.UserID), "PassportSDK");
                }
            }

            if (IsTicketValid(ticket) == true)
                AdjustSignInTimeout(ticket);

            return ticket;
        }
        /// <summary>
        /// �õ���֤ҳ���URL
        /// </summary>
        /// <param name="strReturlUrl">���ص�URL</param>
        /// <returns>�õ���֤ҳ���URL</returns>
        public static string GetAjaxSignInPageUrl(string strReturlUrl)
        {
            Common.CheckHttpContext();
            //���վ���������Զ������֤ҳ��
            return PassportClientSettings.GetConfig().AjaxSignInUrl.ToString() + GetExtraRequestParams(strReturlUrl);
        }
        /// <summary>
        /// �õ���֤ҳ���URL
        /// </summary>
        /// <param name="strReturlUrl">���ص�URL</param>
        /// <returns>�õ���֤ҳ���URL</returns>
        public static string GetSignInPageUrl(string strReturlUrl)
        {
            Common.CheckHttpContext();
            //���վ���������Զ������֤ҳ��
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
        /// ��ȡ��¼��ע����url������url�е���֤���ض����returnUrl
        /// </summary>
        /// <param name="returnUrl">��֤ͨ�����ض����ַ</param>
        /// <returns>��¼����ע��url</returns>
        public static string GetLogOnOrLogOffUrl(string returnUrl)
        {
            return GetLogOnOrLogOffUrl(returnUrl, true);
        }

        /// <summary>
        /// ��ȡ��¼��ע����url������url�е���֤���ض����returnUrl������ע�����ض����logOffAutoRedirect
        /// </summary>
        /// <param name="returnUrl">��֤���ض���ĵ�ַ</param>
        /// <param name="logOffAutoRedirect">�Ƿ�ע�����ض���</param>
        /// <returns>��¼����ע��url</returns>
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
        /// ���ݵ�ǰ��Web���󣬵õ���֤����Ҫ�ض����url���ڴ˹����м��"t"�����Ƿ���ڣ�������ڣ����׳��쳣
        /// </summary>
        /// <returns>��֤����Ҫ�ض����url</returns>
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
        /// ���Ӧ�õ���֤Cookie�Ƿ���Ч�����ʧЧ�����Զ�ת����֤ҳ��
        /// </summary>
        public static void CheckAuthenticated()
        {
            CheckAuthenticated(true);
        }

        /// <summary>
        /// ���Ӧ�õ���֤Cookie�Ƿ���Ч�����ʧЧ������autoRedirect�����������Ƿ�ת����֤ҳ��
        /// </summary>
        /// <param name="autoRedirect">�Ƿ��Զ�ת����֤ҳ��</param>
        public static ITicket CheckAuthenticated(bool autoRedirect)
        {
            Common.CheckHttpContext();
            HttpContext context = HttpContext.Current;

            bool fromCookie = false;
            ITicket ticket = GetTicket(out fromCookie);

            if (IsTicketValid(ticket) == false)//��Ч��ȫ��֤����
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
        /// ע����HTML�����
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
<div>����ע��������</div>
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
        /// ���õ�ǰӦ�������ģ���ǰ�������룩
        /// �޸ģ�Ѧ����
        /// Ŀ�ģ���Ӧ�����õ�ǰ�û������Ļ�������
        /// ʱ�䣺2011-6-8
        /// </summary>
        /// <param name="appEnvironment">Ӧ�������Ĳ�������������</param>
        public static void SetAppEnvironment(string appEnvironment)
        {
            bool fromCookie;
            ITicket ticket = PassportManager.GetTicket(out fromCookie);
            ticket.AppEnvironment = appEnvironment;
            ticket.SaveToCookie();
        }

        /// <summary>
        /// ��ȡӦ�������Ĳ�������ǰ�������룩
        /// �޸ģ�Ѧ����
        /// Ŀ�ģ���Ӧ�û�ȡ��ǰ�û������Ļ�������
        /// ʱ�䣺2011-6-8
        /// </summary>
        /// <returns>����ǰ�������룩</returns>
        public static string GetAppEnvironment()
        {
            bool fromCookie;
            ITicket ticket = PassportManager.GetTicket(out fromCookie);
            return ticket.AppEnvironment;
        }
        #region ˽�з���
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
        /// ��ָ�����·����Urlӳ�䵽��ǰ������������һ������·��
        /// </summary>
        /// <param name="strUrl">���·����Url</param>
        /// <returns>����·����Url</returns>
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
        #endregion ˽�з���
    }
}
