
using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Text;
using System.Diagnostics;
using MCS.Library.Core;

namespace ETMS.Security
{
    /// <summary>
    /// ��¼ҳ���������Ϣ
    /// </summary>
    public class SignInPageData
    {
        private bool isSaveUserName = true;
        private bool isAutoSignIn = false;
        private string userName = string.Empty;
        private int loginErrorNum = 0;
        /// <summary>
        /// �Ƿ񱣴��û���
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
        /// �Ƿ��Զ���¼
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
        /// �û���
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
        /// ��¼����Ĵ���
        /// </summary>
        public int LoginErrorNum
        {
            get
            {
                return this.loginErrorNum;
            }
            set
            {
                this.loginErrorNum = value;
            }
        }
        /// <summary>
        /// ��Cookie��װ��ҳ��������Ϣ
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
                    this.LoginErrorNum = XmlHelper.GetSingleNodeValue(xmlDoc.DocumentElement, "LoginErrorNum",0);
                }
                catch (System.Exception)
                {
                    //����cookie�е����ݴ���
                }
            }
        }
        /// <summary>
        /// ����Cookie��
        /// </summary>
        public void SaveToCookie()
        {
            HttpContext context = HttpContext.Current;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<Data/>");

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UNM", this.userName);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "SUID", this.isSaveUserName.ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "ASI", this.isAutoSignIn.ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "LoginErrorNum", this.LoginErrorNum.ToString());
            
            HttpCookie cookie = new HttpCookie("SignInPageData");
            cookie.Value = CookieValueToBase64String(xmlDoc.InnerXml);
            cookie.Expires = DateTime.MaxValue;//cookie��������

            context.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// ���Cookie
        /// </summary>
        public void ClearSignInPageDataCookie()
        {
            HttpContext context = HttpContext.Current;
            HttpCookie cookie = context.Request.Cookies["SignInPageData"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Set(cookie);
            }
        }
        private static string CookieValueToBase64String(string v)
        {
            byte[] data = Encoding.UTF8.GetBytes(v);

            return Convert.ToBase64String(data);
        }

        private static string Base64StringToCookieValue(string v)
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
    }
}
