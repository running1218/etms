
using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Text;
using System.Diagnostics;
using MCS.Library.Core;

namespace University.Mooc.Security
{
    /// <summary>
    /// 登录页面的配置信息
    /// </summary>
    public class SignInPageData
    {
        private bool isSaveUserName = true;
        private bool isAutoSignIn = false;
        private string userName = string.Empty;
        /// <summary>
        /// 是否保存用户名
        /// </summary>
        public bool IsSaveUserName
        {
            get
            {
                return this.isSaveUserName;
            }
            set
            {
                this.isSaveUserName = value;
            }
        }
        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool IsAutoSignIn
        {
            get
            {
                return this.isAutoSignIn;
            }
            set
            {
                this.isAutoSignIn = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
        /// <summary>
        /// 从Cookie中装载页面配置信息
        /// </summary>
        public void LoadFromCookie()
        {
            HttpContext context = HttpContext.Current;

            HttpCookie cookie = context.Request.Cookies["SignInPageData"];

            if (cookie != null)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(Base64StringToCookieValue(cookie.Value));

                    this.userName = XmlHelper.GetSingleNodeText(xmlDoc.DocumentElement, "UNM");
                    this.isSaveUserName = XmlHelper.GetSingleNodeValue(xmlDoc.DocumentElement, "SUID", true);
                    this.isAutoSignIn = XmlHelper.GetSingleNodeValue(xmlDoc.DocumentElement, "ASI", false);
                }
                catch (System.Exception)
                {
                    //忽略cookie中的内容错误
                }
            }
        }
        /// <summary>
        /// 存入Cookie中
        /// </summary>
        public void SaveToCookie()
        {
            HttpContext context = HttpContext.Current;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<Data/>");

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UNM", this.userName);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "SUID", this.isSaveUserName.ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "ASI", this.isAutoSignIn.ToString());

            HttpCookie cookie = new HttpCookie("SignInPageData");
            cookie.Value = CookieValueToBase64String(xmlDoc.InnerXml);
            cookie.Expires = DateTime.MaxValue;//cookie永不过期

            context.Response.Cookies.Add(cookie);
        }

        private string CookieValueToBase64String(string v)
        {
            byte[] data = Encoding.UTF8.GetBytes(v);

            return Convert.ToBase64String(data);
        }

        private string Base64StringToCookieValue(string v)
        {
            string cookieValue = v;

            try
            {
                byte[] data = Convert.FromBase64String(v);

                MemoryStream ms = new MemoryStream(data);
                try
                {
                    StreamReader sr = new StreamReader(ms, Encoding.UTF8);

                    cookieValue = sr.ReadToEnd();
                }
                finally
                {
                    ms.Close();
                }
            }
            catch (System.FormatException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return cookieValue;
        }
        /// <summary>
        /// 清除应用的Cookie
        /// </summary>
        public void ClearSignInPageDataCookie()
        {

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpCookie cookie = request.Cookies["SignInPageData"];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = null;
                response.SetCookie(cookie);
            }
        }
    }
}
