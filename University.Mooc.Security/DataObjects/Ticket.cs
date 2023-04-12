
using System;
using System.Xml;
using System.Web;
using System.Diagnostics;
using System.Security.Cryptography;

using MCS.Library.Core;
using University.Mooc.Security.Properties;

namespace University.Mooc.Security
{
    /// <summary>
    /// 票据信息类
    /// </summary>
    public class Ticket : ITicket
    {
        private ISignInInfo signInInfo = null;
        private string appID = string.Empty;
        private Guid appSignInSessionID = Guid.Empty;
        private DateTime appSignInTime;
        private DateTime appSignInTimeout = DateTime.MinValue;
        private string appSignInIP = string.Empty;
        /// <summary>
        /// 从Cookie中读取ITicket信息
        /// </summary>
        /// <returns><see cref="ITicket"/> 对象。</returns>
        public static ITicket LoadFromCookie()
        {
            ITicket ticket = null;

            Common.CheckHttpContext();

            HttpRequest request = HttpContext.Current.Request;

            HttpCookie cookie = request.Cookies[Common.UserContext_COOKIE];

            if (cookie != null && cookie.Value != null && cookie.Value != string.Empty)
                ticket = new Ticket(Common.DecryptString(cookie.Value));

            return ticket;
        }

        /// <summary>
        /// 从Url中的参数读取Ticket信息
        /// </summary>
        /// <returns>对象。</returns>
        public static ITicket LoadFromUrl()
        {
            return LoadFromUrl(PassportManager.TicketParamName);
        }

        /// <summary>
        /// 从Url中的参数读取Ticket信息
        /// </summary>
        /// <param name="reqParamName">url中对应ticket的参数名称</param>
        /// <returns>对象。</returns>
        public static ITicket LoadFromUrl(string reqParamName)
        {
            ITicket ticket = null;

            HttpRequest request = HttpContext.Current.Request;

            string strTicket = request.QueryString[reqParamName];

            try
            {
                if (strTicket != null)
                    ticket = Common.DecryptTicket(strTicket);	//从URL中加载Ticket

                if (ticket != null)
                    Trace.WriteLine(string.Format("从url中找到用户{0}的ticket", ticket.SignInInfo.UserID), "PassportSDK");
            }
            catch (CryptographicException ex)
            {
                throw new Exception("ticket 解密失败。", ex);
            }

            return ticket;
        }

        /// <summary>
        /// 构造类
        /// </summary>
        public Ticket()
        {
        }
        /// <summary>
        /// 构造类
        /// </summary>
        /// <param name="strXml">xml结构的Ticket数据</param>
        /// <remarks>
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="TicketTest" lang="cs" title="Ticket对象和Xml对象间的转换" />
        /// </remarks>
        public Ticket(string strXml)
        {
            InitFromXml(strXml);
        }

        #region ITicket 成员
        /// <summary>
        /// 用户登录信息
        /// </summary>
        public ISignInInfo SignInInfo
        {
            get
            {
                return this.signInInfo;
            }
            set
            {
                this.signInInfo = value;
            }
        }
        /// <summary>
        /// 应用登录的SessionID
        /// </summary>
        public Guid AppSignInSessionID
        {
            get
            {
                return this.appSignInSessionID;
            }
            set
            {
                this.appSignInSessionID = value;
            }
        }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppID
        {
            get
            {
                return this.appID;
            }
            set
            {
                this.appID = value;
            }
        }
        /// <summary>
        /// 应用登录时间
        /// </summary>
        public DateTime AppSignInTime
        {
            get
            {
                return this.appSignInTime;
            }
            set
            {
                this.appSignInTime = value;
            }
        }
        /// <summary>
        /// 应用登录超时时间
        /// </summary>
        public DateTime AppSignInTimeout
        {
            get
            {
                return this.appSignInTimeout;
            }
            set
            {
                this.appSignInTimeout = value;
            }
        }
        /// <summary>
        /// 应用登录IP
        /// </summary>
        public string AppSignInIP
        {
            get
            {
                return this.appSignInIP;
            }
            set
            {
                this.appSignInIP = value;
            }
        }
        /// <summary>
        /// 访问应用页面
        /// </summary>
        public string AppSignInURL
        {
            get;
            set;
        }
        /// <summary>
        /// 应用注销页面
        /// </summary>
        public string AppLogoOffURL
        {
            get;
            set;
        }

        #region 薛永波修改，目的：增加应用角色列表、机构编码
        private string appRoles;
        /// <summary>
        /// 应用角色列表，多个角色用";"分割
        /// </summary>
        public string AppRoles
        {
            get
            {
                return this.appRoles;
            }
            set
            {
                this.appRoles = value;
            }
        }

        private string appEnvironment;
        /// <summary>
        /// 应用环境（针对的机构编码）
        /// </summary>
        public string AppEnvironment
        {
            get
            {
                return this.appEnvironment;
            }
            set
            {
                this.appEnvironment = value;
                this.appRoles = "";//切换应用环境，对应的角色列表需要清空
            }
        }

        #endregion
        /// <summary>
        /// Ticket信息保存入Cookie中
        /// </summary>
        public void SaveToCookie()
        {
            Common.CheckHttpContext();

            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;

            string strData = SaveToXml().InnerXml;

            HttpCookie cookie = new HttpCookie(Common.C_TICKET_COOKIE_KEY, Common.EncryptString(strData));
            //如果设定了应用令牌Cookie对应的域，设置cookie.Domain属性
            if (!string.IsNullOrEmpty(Common.C_TICKET_COOKIE_DOMAIN))
            {
                cookie.Domain = Common.C_TICKET_COOKIE_DOMAIN;
            }
            cookie.Expires = this.appSignInTimeout;

            response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Ticket信息存成Xml结构
        /// </summary>
        /// <returns>Xml结构的Ticket信息</returns>
        /// <remarks>
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="TicketTest" lang="cs" title="Ticket对象和Xml对象间的转换" />
        /// </remarks>
        public System.Xml.XmlDocument SaveToXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<Ticket/>");

            XmlElement root = xmlDoc.DocumentElement;

            if (this.signInInfo != null)
            {
                XmlNode nodeSignInInfo = XmlHelper.AppendNode(root, "SignInInfo");
                XmlDocument xmlSignInInfo = this.signInInfo.SaveToXml();

                nodeSignInInfo.InnerXml = xmlSignInInfo.DocumentElement.InnerXml;
            }

            XmlHelper.AppendNode(root, "AppSSID", this.appSignInSessionID);
            XmlHelper.AppendNode(root, "AppID", this.appID);
            XmlHelper.AppendNode(root, "AppSTime", this.appSignInTime);
            XmlHelper.AppendNode(root, "AppSTimeout", this.appSignInTimeout);
            XmlHelper.AppendNode(root, "IP", this.appSignInIP);
            #region 薛永波修改，目的：增加应用角色列表、应用环境（对于的机构编号）
            XmlHelper.AppendNode(root, "AppRoles", this.appRoles);
            XmlHelper.AppendNode(root, "AppEnv", this.appEnvironment);
            #endregion
            return xmlDoc;
        }
        /// <summary>
        /// 判断Ticket是否合法
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {
            bool bValid = false;

            try
            {
                ExceptionHelper.TrueThrow(IsAbsoluteTimeExpired(), Resource.AbsoluteTimeExpired);
                ExceptionHelper.TrueThrow(IsSlidingExpired(), Resource.SlidingTimeExpired);
                ExceptionHelper.TrueThrow(IsIPInvalid(), Resource.IPInvalid);
                ExceptionHelper.TrueThrow(IsDifferentAthenticateServer(), Resource.DifferentAthenticateServer);

                bValid = true;
            }
            catch (System.ApplicationException ex)
            {
                //TODO:增加我们自己的Trace
                Trace.WriteLine(string.Format(Resource.TicketInvalidReason, this.SignInInfo.UserID, ex.Message));
            }

            //Debug.WriteLineIf(bValid == false, "Ticket Invalid", "PassportSDK");

            return bValid;
        }
        #endregion

        private bool IsAbsoluteTimeExpired()
        {
            DateTime newExpireDate = GetConfigExpireDate();

            bool bExpired = DateTime.Now >= newExpireDate;

            //Debug.WriteLineIf(bExpired, "App Ticket Absolute Expired", "PassportSDK");

            return bExpired;		//绝对时间是否过期
        }

        private DateTime GetConfigExpireDate()
        {
            DateTime dt = DateTime.MaxValue;

            PassportClientSettings settings = PassportClientSettings.GetConfig();

            if (settings.AppSignInTimeout >= 0)
                dt = this.AppSignInTime.AddSeconds(settings.AppSignInTimeout);

            return dt;
        }

        private bool IsIPInvalid()
        {
            return HttpContext.Current.Request.UserHostAddress != this.AppSignInIP;
        }

        private bool IsSlidingExpired()
        {
            bool bExpired = false;

            PassportClientSettings settings = PassportClientSettings.GetConfig();

            if (settings.HasSlidingExpiration)
            {
                DateTime dtTO = this.AppSignInTime.Add(settings.AppSlidingExpiration);
                bExpired = (DateTime.Now >= dtTO);		//相对时间过期
            }

            //Debug.WriteLineIf(bExpired, "App Ticket Sliding Expired", "PassportSDK");

            return bExpired;
        }

        private bool IsDifferentAthenticateServer()
        {
            Uri url = PassportClientSettings.GetConfig().SignInUrl;
            return string.Compare(this.SignInInfo.AuthenticateServer, url.Host + ":" + url.Port, true) != 0;
        }

        private void InitFromXml(string strXml)
        {
            XmlDocument xmlDoc = XmlHelper.CreateDomDocument(strXml);

            XmlElement root = xmlDoc.DocumentElement;

            XmlNode nodeSignInInfo = root.SelectSingleNode("SignInInfo");

            if (nodeSignInInfo != null)
                this.signInInfo = new SignInInfo(nodeSignInInfo.OuterXml);

            this.appSignInSessionID =Guid.Parse(XmlHelper.GetSingleNodeText(root, "AppSSID"));

            this.appID = XmlHelper.GetSingleNodeText(root, "AppID");
            this.appSignInTime = XmlHelper.GetSingleNodeValue(root, "AppSTime", DateTime.MinValue);
            this.appSignInTimeout = XmlHelper.GetSingleNodeValue(root, "AppSTimeout", DateTime.MinValue);
            this.appSignInIP = XmlHelper.GetSingleNodeText(root, "IP");

            #region 薛永波修改，目的：增加应用角色列表、应用环境（对于的机构编号）
            this.appRoles = XmlHelper.GetSingleNodeText(root, "AppRoles");
            this.appEnvironment = XmlHelper.GetSingleNodeText(root, "AppEnv");
            #endregion
        }
    }
}
