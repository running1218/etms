
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
    /// Ʊ����Ϣ��
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
        /// ��Cookie�ж�ȡITicket��Ϣ
        /// </summary>
        /// <returns><see cref="ITicket"/> ����</returns>
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
        /// ��Url�еĲ�����ȡTicket��Ϣ
        /// </summary>
        /// <returns>����</returns>
        public static ITicket LoadFromUrl()
        {
            return LoadFromUrl(PassportManager.TicketParamName);
        }

        /// <summary>
        /// ��Url�еĲ�����ȡTicket��Ϣ
        /// </summary>
        /// <param name="reqParamName">url�ж�Ӧticket�Ĳ�������</param>
        /// <returns>����</returns>
        public static ITicket LoadFromUrl(string reqParamName)
        {
            ITicket ticket = null;

            HttpRequest request = HttpContext.Current.Request;

            string strTicket = request.QueryString[reqParamName];

            try
            {
                if (strTicket != null)
                    ticket = Common.DecryptTicket(strTicket);	//��URL�м���Ticket

                if (ticket != null)
                    Trace.WriteLine(string.Format("��url���ҵ��û�{0}��ticket", ticket.SignInInfo.UserID), "PassportSDK");
            }
            catch (CryptographicException ex)
            {
                throw new Exception("ticket ����ʧ�ܡ�", ex);
            }

            return ticket;
        }

        /// <summary>
        /// ������
        /// </summary>
        public Ticket()
        {
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strXml">xml�ṹ��Ticket����</param>
        /// <remarks>
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="TicketTest" lang="cs" title="Ticket�����Xml������ת��" />
        /// </remarks>
        public Ticket(string strXml)
        {
            InitFromXml(strXml);
        }

        #region ITicket ��Ա
        /// <summary>
        /// �û���¼��Ϣ
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
        /// Ӧ�õ�¼��SessionID
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
        /// Ӧ��ID
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
        /// Ӧ�õ�¼ʱ��
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
        /// Ӧ�õ�¼��ʱʱ��
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
        /// Ӧ�õ�¼IP
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
        /// ����Ӧ��ҳ��
        /// </summary>
        public string AppSignInURL
        {
            get;
            set;
        }
        /// <summary>
        /// Ӧ��ע��ҳ��
        /// </summary>
        public string AppLogoOffURL
        {
            get;
            set;
        }

        #region Ѧ�����޸ģ�Ŀ�ģ�����Ӧ�ý�ɫ�б���������
        private string appRoles;
        /// <summary>
        /// Ӧ�ý�ɫ�б������ɫ��";"�ָ�
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
        /// Ӧ�û�������ԵĻ������룩
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
                this.appRoles = "";//�л�Ӧ�û�������Ӧ�Ľ�ɫ�б���Ҫ���
            }
        }

        #endregion
        /// <summary>
        /// Ticket��Ϣ������Cookie��
        /// </summary>
        public void SaveToCookie()
        {
            Common.CheckHttpContext();

            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;

            string strData = SaveToXml().InnerXml;

            HttpCookie cookie = new HttpCookie(Common.C_TICKET_COOKIE_KEY, Common.EncryptString(strData));
            //����趨��Ӧ������Cookie��Ӧ��������cookie.Domain����
            if (!string.IsNullOrEmpty(Common.C_TICKET_COOKIE_DOMAIN))
            {
                cookie.Domain = Common.C_TICKET_COOKIE_DOMAIN;
            }
            cookie.Expires = this.appSignInTimeout;

            response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Ticket��Ϣ���Xml�ṹ
        /// </summary>
        /// <returns>Xml�ṹ��Ticket��Ϣ</returns>
        /// <remarks>
        /// <code source="..\Framework\TestProjects\DeluxeWorks.Library.Passport.Test\DataObjectsTest.cs" region="TicketTest" lang="cs" title="Ticket�����Xml������ת��" />
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
            #region Ѧ�����޸ģ�Ŀ�ģ�����Ӧ�ý�ɫ�б�Ӧ�û��������ڵĻ�����ţ�
            XmlHelper.AppendNode(root, "AppRoles", this.appRoles);
            XmlHelper.AppendNode(root, "AppEnv", this.appEnvironment);
            #endregion
            return xmlDoc;
        }
        /// <summary>
        /// �ж�Ticket�Ƿ�Ϸ�
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
                //TODO:���������Լ���Trace
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

            return bExpired;		//����ʱ���Ƿ����
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
                bExpired = (DateTime.Now >= dtTO);		//���ʱ�����
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

            #region Ѧ�����޸ģ�Ŀ�ģ�����Ӧ�ý�ɫ�б�Ӧ�û��������ڵĻ�����ţ�
            this.appRoles = XmlHelper.GetSingleNodeText(root, "AppRoles");
            this.appEnvironment = XmlHelper.GetSingleNodeText(root, "AppEnv");
            #endregion
        }
    }
}
