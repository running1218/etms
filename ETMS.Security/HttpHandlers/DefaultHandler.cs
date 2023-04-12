using System;
using System.Collections.Generic;
using System.Web;
namespace ETMS.Security
{
    /// <summary>
    ///DefaultHandler 的摘要说明
    /// </summary>
    public class DefaultHandler : IHttpHandler
    {
        /// <summary>
        /// 是否重用
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">请求上下文</param>
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            if (action.Equals("AjaxLogin", StringComparison.InvariantCultureIgnoreCase))
            {
                AjaxLogin(context.Request);
            }
            else if (action.Equals("LogOff", StringComparison.InvariantCultureIgnoreCase))
            {
                ISignInInfo signInInfo = SignInInfo.LoadFromCookie();
                int userID = string.IsNullOrEmpty(signInInfo.UserID) ? 0 : int.Parse(signInInfo.UserID);
                DefaultAuthenticator.LogoffUserAccessLog(userID);

                LogOff(context.Request);
            }
            else
            {
                context.Response.Write("非法访问！");
                context.Response.End();
            }

        }

        /// <summary>
        /// 向应用系统提供自定义登录的异步调用
        /// </summary>
        /// <param name="Request">请求上下文</param>
        public void AjaxLogin(HttpRequest Request)
        {
            //System.Collections.Specialized.NameValueCollection QueryString = FillFromString(Request.RawUrl, true, System.Text.Encoding.Default);
            //Request.QueryString.Add(QueryString);
            string username = Request.QueryString["lu"];
            string password = Request.QueryString["lp"];
            bool isSaveUserName = false;
            bool isAutoSignIn = false;

            //调用登录
            try
            {
                //0、验证url参数是否有效
                DefaultAuthenticator.ValidSignInParams(Request);

                //1、自动检测统一用户是否已经认证，如果认证，则自动登录
                string appsignInUrl = DefaultAuthenticator.AutoSignIn();
                if (!string.IsNullOrEmpty(appsignInUrl))
                {
                    AjaxResponse(true, appsignInUrl);
                }
                //2、验证用户输出参数：用户名、密码未空时，提示
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    AjaxResponse(false, "用户名或密码错误！");
                    return;
                }
                //剔除前后空白字符
                username = username.Trim();
                password = password.Trim();
                //登录验证
                appsignInUrl = DefaultAuthenticator.SignIn(username, password, isSaveUserName, isAutoSignIn);
                //跳转到相应的应用Url
                AjaxResponse(true, appsignInUrl);
            }
            //登录出错时处理
            catch (Exception ex)
            {
                //载入登录页(将用户的默认设置传入）
                AjaxResponse(false, ex.Message);
            }
        }

        /// <summary>
        /// Ajax内容输出
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="parm"></param>
        private void AjaxResponse(bool isValid, string parm)
        {
            string js = "var loginResult={\"IsValid\":$0,\"parm\":\"$1\"};".Replace("$0", isValid ? "true" : "false").Replace("$1", parm.Replace("\"", "'"));
            HttpResponse Response = HttpContext.Current.Response;
            //js输出
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Type", "application/x-javascript");
            Response.Write(js);
        }

        /// <summary>
        /// 注销（HTML输出）
        /// </summary>
        /// <param name="Request"></param>
        public void LogOff(HttpRequest Request)
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


        private static System.Collections.Specialized.NameValueCollection FillFromString(string s, bool urlencoded, System.Text.Encoding encoding)
        {
            System.Collections.Specialized.NameValueCollection queryString = new System.Collections.Specialized.NameValueCollection();
            int num = (s != null) ? s.Length : 0;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = s[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string str = null;
                string str2 = null;
                if (num4 >= 0)
                {
                    str = s.Substring(startIndex, num4 - startIndex);
                    str2 = s.Substring(num4 + 1, (i - num4) - 1);
                }
                else
                {
                    str2 = s.Substring(startIndex, i - startIndex);
                }
                if (urlencoded)
                {
                    queryString.Add(HttpUtility.UrlDecode(str, encoding), HttpUtility.UrlDecode(str2, encoding));
                }
                else
                {
                    queryString.Add(str, str2);
                }
                if ((i == (num - 1)) && (s[i] == '&'))
                {
                    queryString.Add(null, string.Empty);
                }
            }
            return queryString;
        }

    }
}