
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
        //用户登录信息的Cookie键名称
        internal static string C_SIGNIN_User_COOKIE_KEY = "UserSignIn";
        /// <summary>
        /// 用户应用上的认证信息（每个应用有独立的认证cookie键）
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
        /// 应用令牌Cookie存储的域，默认为空标识当前域
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
        /// 生成的应用认证URL加密口令
        /// </summary>
        internal const string C_URL_PASSWORD = "BE599B44-8B13-40CF-844A-36C8743AE79F";


        /// <summary>
        /// 检查Http的上下文是否存在
        /// </summary>
        public static void CheckHttpContext()
        {
            ExceptionHelper.FalseThrow(HttpContext.Current != null,
                "无法取得HttpContext。代码必须运行在Web请求的处理过程中");
        }

        /// <summary>
        /// 加密字符串，返回加密后的Base64的字符串
        /// </summary>
        /// <param name="strData">原始字符串</param>
        /// <returns>加密后的Base64的字符串</returns>
        public static string EncryptString(string strData)
        {
            return Convert.ToBase64String(PassportEncryptionSettings.GetConfig().StringEncryption.EncryptString(strData));
        }

        /// <summary>
        /// 解密字符串，将加密后的Base64的字符串解密为原始字符串
        /// </summary>
        /// <param name="strEncText">加密后的Base64</param>
        /// <returns>解密后的原始字符串</returns>
        public static string DecryptString(string strEncText)
        {
            byte[] data = Convert.FromBase64String(strEncText);

            return PassportEncryptionSettings.GetConfig().StringEncryption.DecryptString(data);
        }
        /// <summary>
        /// 生成SignInInfo的Xml格式数据
        /// </summary>
        /// <param name="userInfo">用户登录信息</param>
        /// <param name="bDontSaveUserID">是否保存用户名</param>
        /// <param name="bAutoSignIn">是否自动登录</param>
        /// <returns>SignInfo的xml格式数据</returns>
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
        /// 生成Ticket的字符串
        /// </summary>
        /// <param name="signInInfo">登录信息</param>
        /// <param name="strIP">客户端ip</param>
        /// <returns>Ticket的字符串</returns>
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
        /// 加密Ticket
        /// </summary>
        /// <param name="ticket">ticket</param>
        /// <returns>加密后的Ticket并且使用Base64编码</returns>
        public static string EncryptTicket(ITicket ticket)
        {
            ITicketEncryption et = PassportEncryptionSettings.GetConfig().TicketEncryption;

            //byte[] data = et.EncryptTicket(ticket, PassportClientSettings.GetConfig().RsaKeyValue); /del by yuanyong 20090416
            //原来是PassportClientSettings，不正确的。加密ticket是PassportService的事情。虽然Client和Service都配置了密钥，但是应该使用Service方的

            //RSAKeyContainer容器与RSAKey二选一
            string key = string.IsNullOrEmpty(PassportSignInSettings.GetConfig().RSAKeyContainerName) ?
                PassportSignInSettings.GetConfig().RsaKeyValue : PassportSignInSettings.GetConfig().RSAKeyContainerName;
            byte[] data = et.EncryptTicket(ticket, key);

            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 将加密之后的ticket对应字符转换回来
        /// </summary>
        /// <param name="strTicketEncoded">待解密的字符串原始数据</param>
        /// <returns>解密之后的ticket对象</returns>
        /// <remarks>
        /// 解密的源数据要求采用相同加密格式要求，配置于PassportEncryptionSettings中
        /// </remarks>
        public static ITicket DecryptTicket(string strTicketEncoded)
        {
            ITicketEncryption et = PassportEncryptionSettings.GetConfig().TicketEncryption;

            byte[] data = Convert.FromBase64String(strTicketEncoded);
            //RSAKeyContainer容器与RSAKey二选一
            string key = string.IsNullOrEmpty(PassportClientSettings.GetConfig().RSAKeyContainerName) ?
              PassportClientSettings.GetConfig().RsaKeyValue : PassportClientSettings.GetConfig().RSAKeyContainerName;
            return et.DecryptTicket(data, key);
        }

        /// <summary>
        /// 计算缓存Principal所使用的Session Key
        /// </summary>
        /// <returns>Session Key值</returns>
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
        /// 获取Session超时时间
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
