
using System;
using System.Xml;
using System.Web;
using System.Configuration;
using MCS.Library.Core;
using System.Web.Configuration;

namespace University.Mooc.Security
{
    /// <summary>
    /// Common
    /// </summary>
    public class Common
    {
        internal static string UserContext_COOKIE = ConfigurationManager.AppSettings["UserCookie"];
        internal const string C_SIGNIN_COOKIE_KEY = "HPassportSignIn";
        internal static string C_TICKET_COOKIE = ConfigurationManager.AppSettings["UserCookie"];
        //�û���¼��Ϣ��Cookie������
        internal static string C_SIGNIN_User_COOKIE_KEY = "UserSignIn";
        /// <summary>
        /// �û�Ӧ���ϵ���֤��Ϣ��ÿ��Ӧ���ж�������֤cookie����
        /// </summary>
        internal static string C_TICKET_COOKIE_KEY
        {
            get
            {
                //return string.Format("{0}_{1}", C_TICKET_COOKIE, PassportClientSettings.GetConfig().AppID ?? "Default");
                return string.Format("{0}_{1}", C_TICKET_COOKIE, PassportClientSettings.GetConfig().AppID ?? "Default");
            }
        }
        /// <summary>
        /// Ӧ������Cookie�洢����Ĭ��Ϊ�ձ�ʶ��ǰ��
        /// </summary>
        internal static string C_TICKET_COOKIE_DOMAIN
        {
            get
            {
                return PassportClientSettings.GetConfig().TicketCookieDomain;
            }
        }
        internal const string C_SESSION_KEY_NAME = "DeluxeWorksPassport_SignInInfo";
        internal const string C_USER_SETTINGS_KEY_NAME = "User_Settings_Key_Name";
        internal const string C_USER_DELEGATION_KEY_NAME = "User_Delegation_Key_Name";

        internal const string C_LOGOFF_CALLBACK_VIRTUAL_PATH = "MCSAuthenticateLogOff.axd";
        /// <summary>
        /// ���ɵ�Ӧ����֤URL���ܿ���
        /// </summary>
        internal const string C_URL_PASSWORD = "BE599B44-8B13-40CF-844A-36C8743AE79F";


        /// <summary>
        /// ���Http���������Ƿ����
        /// </summary>
        public static void CheckHttpContext()
        {
            ExceptionHelper.FalseThrow(HttpContext.Current != null,
                "�޷�ȡ��HttpContext���������������Web����Ĵ��������");
        }

        /// <summary>
        /// �����ַ��������ؼ��ܺ��Base64���ַ���
        /// </summary>
        /// <param name="strData">ԭʼ�ַ���</param>
        /// <returns>���ܺ��Base64���ַ���</returns>
        public static string EncryptString(string strData)
        {
            return Convert.ToBase64String(PassportEncryptionSettings.GetConfig().StringEncryption.EncryptString(strData));
        }

        /// <summary>
        /// �����ַ����������ܺ��Base64���ַ�������Ϊԭʼ�ַ���
        /// </summary>
        /// <param name="strEncText">���ܺ��Base64</param>
        /// <returns>���ܺ��ԭʼ�ַ���</returns>
        public static string DecryptString(string strEncText)
        {
            byte[] data = Convert.FromBase64String(strEncText);

            return PassportEncryptionSettings.GetConfig().StringEncryption.DecryptString(data);
        }
        /// <summary>
        /// ����SignInInfo��Xml��ʽ����
        /// </summary>
        /// <param name="userInfo">�û���¼��Ϣ</param>
        /// <param name="bDontSaveUserID">�Ƿ񱣴��û���</param>
        /// <param name="bAutoSignIn">�Ƿ��Զ���¼</param>
        /// <returns>SignInfo��xml��ʽ����</returns>
        public static XmlDocument GenerateSignInInfo(ETMS.Components.Basic.API.Entity.Security.IUser userInfo, bool bDontSaveUserID, bool bAutoSignIn)
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            XmlDocument xmlDoc = XmlHelper.CreateDomDocument("<SignInInfo/>");

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "SSID", Guid.NewGuid().ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UID", userInfo.UserID.ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UName", userInfo.RealName);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UNM", userInfo.LoginName);
            //XmlHelper.AppendNode(xmlDoc.DocumentElement, "NickName", userInfo.NickName);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "OUID", userInfo.UserID.ToString());
            //XmlHelper.AppendNode(xmlDoc.DocumentElement, "UserType", userInfo.UserType.ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "UserOrgs", userInfo.OrganizationID);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "DSUID", bDontSaveUserID);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "ASI", bAutoSignIn);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "STime", DateTime.Now);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "AS", request.Url.Host + ":" + request.Url.Port);
            
            DateTime dtExpireTime = DateTime.Now.AddDays(1);

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "STimeout", dtExpireTime);

            return xmlDoc;
        }
        /// <summary>
        /// ����Ticket���ַ���
        /// </summary>
        /// <param name="signInInfo">��¼��Ϣ</param>
        /// <param name="strIP">�ͻ���ip</param>
        /// <returns>Ticket���ַ���</returns>
        public static string GenerateTicketString(ISignInInfo signInInfo, string strIP)
        {
            HttpContext context = HttpContext.Current;

            HttpRequest request = context.Request;

            XmlDocument xmlDoc = XmlHelper.CreateDomDocument("<Ticket/>");

            XmlDocument xmlSignInInfo = signInInfo.SaveToXml();

            XmlNode SignInNode = XmlHelper.AppendNode(xmlDoc.DocumentElement, "SignInInfo");
            SignInNode.InnerXml = xmlSignInInfo.DocumentElement.InnerXml;

            string strTimeout = request.QueryString["to"];
            int nTimeout = -1;

            if (strTimeout != null)
            {
                try
                {
                    nTimeout = int.Parse(strTimeout);
                }
                catch (System.Exception)
                {
                }
            }
            //else
            //    nTimeout = (int)(PassportSignInSettings.GetConfig().DefaultTimeout.TotalSeconds);

            string strAppID = PassportClientSettings.GetConfig().AppID ?? "Default";

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "AppSSID", Guid.NewGuid().ToString());
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "AppID", strAppID);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "AppSTime", DateTime.Now);
            XmlHelper.AppendNode(xmlDoc.DocumentElement, "IP", strIP);
#if DELUXEWORKSTEST
            Debug.WriteLine(context.Request.UserHostAddress);
#endif
            DateTime dtExpireTime = DateTime.Now.AddDays(1);

            //if (nTimeout >= 0)
            //    dtExpireTime = DateTime.Now.AddSeconds(nTimeout);
            //else
            //    if (nTimeout < -1)
            //        dtExpireTime = DateTime.MinValue;
            //    else
            //        if (nTimeout == -1)
            //            dtExpireTime = DateTime.MaxValue;

            XmlHelper.AppendNode(xmlDoc.DocumentElement, "AppSTimeout", dtExpireTime);

            return xmlDoc.OuterXml;
        }

        /// <summary>
        /// ����Ticket
        /// </summary>
        /// <param name="ticket">ticket</param>
        /// <returns>���ܺ��Ticket����ʹ��Base64����</returns>
        public static string EncryptTicket(ITicket ticket)
        {
            ITicketEncryption et = PassportEncryptionSettings.GetConfig().TicketEncryption;

            //byte[] data = et.EncryptTicket(ticket, PassportClientSettings.GetConfig().RsaKeyValue); /del by yuanyong 20090416
            //ԭ����PassportClientSettings������ȷ�ġ�����ticket��PassportService�����顣��ȻClient��Service����������Կ������Ӧ��ʹ��Service����

            //RSAKeyContainer������RSAKey��ѡһ
            string key = string.IsNullOrEmpty(PassportSignInSettings.GetConfig().RSAKeyContainerName) ?
                PassportSignInSettings.GetConfig().RsaKeyValue : PassportSignInSettings.GetConfig().RSAKeyContainerName;
            byte[] data = et.EncryptTicket(ticket, key);

            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// ������֮���ticket��Ӧ�ַ�ת������
        /// </summary>
        /// <param name="strTicketEncoded">�����ܵ��ַ���ԭʼ����</param>
        /// <returns>����֮���ticket����</returns>
        /// <remarks>
        /// ���ܵ�Դ����Ҫ�������ͬ���ܸ�ʽҪ��������PassportEncryptionSettings��
        /// </remarks>
        public static ITicket DecryptTicket(string strTicketEncoded)
        {
            ITicketEncryption et = PassportEncryptionSettings.GetConfig().TicketEncryption;

            byte[] data = Convert.FromBase64String(strTicketEncoded);
            //RSAKeyContainer������RSAKey��ѡһ
            string key = string.IsNullOrEmpty(PassportClientSettings.GetConfig().RSAKeyContainerName) ?
              PassportClientSettings.GetConfig().RsaKeyValue : PassportClientSettings.GetConfig().RSAKeyContainerName;
            return et.DecryptTicket(data, key);
        }

        /// <summary>
        /// ���㻺��Principal��ʹ�õ�Session Key
        /// </summary>
        /// <returns>Session Keyֵ</returns>
        public static string GetPrincipalSessionKey()
        {
            HttpRequest request = HttpContext.Current.Request;

            HttpCookie cookie = request.Cookies[C_SESSION_KEY_NAME];

            string result;

            if (cookie != null)
                result = cookie.Value;
            else
            {
                result = UuidHelper.NewUuidString() + "-UserPrincipal";

                cookie = new HttpCookie(C_SESSION_KEY_NAME);
                cookie.Value = result;
                cookie.Expires = DateTime.MinValue;

                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            return result;
        }
        /// <summary>
        /// ��ȡSession��ʱʱ��
        /// </summary>
        /// <returns>SessionTimeOut</returns>
        public static TimeSpan GetSessionTimeOut()
        {
            TimeSpan timeout = TimeSpan.FromMinutes(20);

            SessionStateSection section = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");

            if (section != null)
                timeout = section.Timeout;

            return timeout;
        }
    }
}
