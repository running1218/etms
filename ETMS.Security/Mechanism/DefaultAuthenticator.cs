
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using ETMS.AppContext;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.API.Entity.Security;
using System.Data;

namespace ETMS.Security
{
    /// <summary>
    /// ���ڵ����¼������֤ʵ��
    /// </summary>
    public abstract class DefaultAuthenticator
    {
        #region ��¼
        /// <summary>
        /// ͨ���û����������¼
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="bSaveUserName">�Ƿ��ס�û���</param>
        /// <param name="bAutoSignIn">�Ƿ��Զ���¼</param>
        /// <returns>��֤ͨ���������Ӧ��url����ticket)</returns>
        public static string SignIn(string userName, string password, bool bSaveUserName, bool bAutoSignIn)
        {
            string ssid = Guid.NewGuid().ToString();
            IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();

            try
            {
                IUser userInfo = Authenticator.Authenticate(userName, password, ssid);

                //(1)ͳһ�û���֤��Ϣ->(2)�־û�->(3)Ӧ����֤��Ϣ->(4)�ض�������ҳ��
                //ͳһ�û���֤��Ϣ����
                XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, !bSaveUserName, bAutoSignIn, ssid);
                SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                //Ӧ����֤Url����
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ͨ���û����������¼
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="bSaveUserName">�Ƿ��ס�û���</param>
        /// <param name="bAutoSignIn">�Ƿ��Զ���¼</param>
        /// <param name="isEncrypt">�����Ƿ����</param>
        /// <returns>��֤ͨ���󷵻��û�ID</returns>
        public static IUser MobileSignIn(string userName, string password, bool bSaveUserName, bool bAutoSignIn,bool isEncrypt = false)
        {
            string ssid = Guid.NewGuid().ToString();
            try
            {
                IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
                IUser userInfo = Authenticator.Authenticate(userName, password, ssid, isEncrypt);

                //(1)ͳһ�û���֤��Ϣ->(2)Ӧ����֤��Ϣ�洢->(3)�����û�ID

                //ͳһ�û���֤��Ϣ����
                //XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, !bSaveUserName, bAutoSignIn, ssid);
                //SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                ////ͳһ�û���֤cookie�洢
                //signInInfo.SaveToCookie();

                //�û�ID
                return userInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ģ���û���¼
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <returns>��֤ͨ���������Ӧ��url����ticket)</returns>
        public static string ImitateSignIn(int userID)
        {
            IPassportFacade Service = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            try
            {
                IUser userInfo = Service.GetUserInfoByID(userID);

                //(1)ͳһ�û���֤��Ϣ->(2)�־û�->(3)Ӧ����֤��Ϣ->(4)�ض�������ҳ��
                //ͳһ�û���֤��Ϣ����
                XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, true, false, Guid.NewGuid().ToString());
                SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                //Ӧ����֤Url����
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �Զ���¼����
        /// </summary>
        /// <returns>����û���֤��Ϣ���ڣ��򷵻���֤ͨ���������Ӧ��url����ticket)�����򷵻�""</returns>
        public static string AutoSignIn()
        {

            //��Cookie����ȡ�û���֤��Ϣ
            ISignInInfo signInInfo = SignInInfo.LoadFromCookie();

            //����û���֤��ϢʧЧ
            if (signInInfo == null || !signInInfo.IsValid())
            {
                return "";//�û���֤cookieʧЧ���޷��Զ���¼�������µ�¼��                
            }
            else //����û���֤��Ϣ��Ч
            {
                //����������ʱ���ȥ���ԣ���ʱ����󻬶�
                if (PassportSignInSettings.GetConfig().HasSlidingExpiration)
                    signInInfo.SignInTime = DateTime.Now;
                IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
                IUser userInfo = Authenticator.GetUserInfoByID(int.Parse(signInInfo.UserID));
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
        }

        /// <summary>
        /// ��֤��¼������Url�ڲ���        
        ///1��ru ��¼����ת���ĵ�ַ
        ///2��to Ӧ����Ч����
        ///3��aid Ӧ��id
        ///4��ip �û�ip
        ///5��lou Ӧ��ע��ʱ�ص���ַ��ȫ·����
        ///6��sf 16λMD5У���룬��ֹ�۸�
        /// </summary>
        /// <param name="request"></param>
        public static void ValidSignInParams(HttpRequest request)
        {
            if (
                  string.IsNullOrEmpty(request.QueryString["ru"]) ||
                  string.IsNullOrEmpty(request.QueryString["to"]) ||
                  string.IsNullOrEmpty(request.QueryString["aid"]) ||
                  string.IsNullOrEmpty(request.QueryString["ip"]) ||
                  string.IsNullOrEmpty(request.QueryString["lou"]) ||
                  string.IsNullOrEmpty(request.QueryString["sf"])
                )
            {
                //��֤������Ч��
                throw new BusinessException("Authenticate.InValidParams");
            }
            else
            {
                //���ݴ����Ĳ�������16λmd5�����롰sf���������Ƚ�
                string realMD5 = ETMS.Utility.Cryptography.MD5Utility.MD516(string.Format("{0},{1},{2},{3},{4}@{5}",
                    new string[] 
                { 
                    HttpUtility.UrlEncode(request.QueryString["ru"]),
                    request.QueryString["to"],
                    request.QueryString["aid"],
                    request.QueryString["ip"],
                    HttpUtility.UrlEncode( request.QueryString["lou"]),
                    Common.C_URL_PASSWORD
                }));
                if (!realMD5.Equals(request.QueryString["sf"], StringComparison.InvariantCultureIgnoreCase))
                {
                    //��֤������Ч��
                    throw new BusinessException("Authenticate.InValidParams");
                }
            }
        }
        #endregion

        #region ע��
        /// <summary>
        /// ע��ͳһ�û���֤����ע���û����е�¼��Ӧ�ã�
        /// �ڴ˽�ע��ͳһ��֤����Ҫ�ص�����Ӧ��ע��ҳ������ע������Ӧ�á�
        /// </summary>     
        /// <returns>�û��Ѿ���֤��¼��Ӧ��ע����ַ�б�</returns>
        public static IList<string> LogOffAllAPP()
        {
            ISignInInfo signInInfo = SignInInfo.LoadFromCookie();

            if (signInInfo == null || !signInInfo.IsValid())//��ЧӦ����֤��Ϣ
            {
                return new List<string>();
            }
            
            string sessionID = signInInfo.SignInSessionID;
            IPassportFacade passportAuthenticateService = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            IList<string> appLogOffUrls = passportAuthenticateService.GetAllRelativeAppsLogOffCallBackUrl(sessionID);
            //1���Ƴ����ݿ���֤��Ϣ(�Ȳ����ǣ�
            //passportAuthenticateService.DeleteRelativeSignInInfo(sessionID);
            //2���Ƴ�ͳһ�û���֤cookie
            PassportManager.ClearSignInCookie();
            //3�������û��ѵ�¼Ӧ���б���ǰ��չ�֣��ԣ�Ƕ��Image��ʽ���ص����Ƴ�Ӧ����֤cookie
            //4������û�����״̬
            //ServiceRepository.OnlineUserStateService.ClearUserOnlineState(Guid.Parse(signInInfo.UserID));
            return appLogOffUrls;
        }

        /// <summary>
        /// �û��ǳ�״̬��0���ǳ�
        /// </summary>
        /// <param name="userID"></param>
        public static void LogoffUserAccessLog(int userID)
        {
            IPassportFacade Service = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            Service.LogoffUserAccessSuccessLog(userID);
        }

        /// <summary>
        /// ��ȡ�û���½��Ϣ
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetPassportAccessLog(int userID)
        { 
            return ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>().GetPassportAccessLog(userID);
        }

        /// <summary>
        /// ǿ���������Ӧ��Ticket���Է�ֹ֮ǰ�û�Ӧ����֤�Ժ��¼��Ӱ��
        /// ���ڵ�ǰ�ù�������Ӧ��Ticket��Ӧ��Cookie�������ͬ��Domain�£�
        /// </summary>
        public static void ForceClearAllAppTicket()
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            for (int i = 0; i < request.Cookies.Keys.Count; i++)
            {
                string cookieKey = request.Cookies.Keys[i];
                if (cookieKey.StartsWith(Common.C_TICKET_COOKIE))
                {
                    HttpCookie cookie = request.Cookies[cookieKey];
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    response.Cookies.Set(cookie);
                }
            }
        }

        #endregion

        #region Helper
        /// <summary>
        /// ����Ӧ����֤��ϢTicket�������ص�¼Ӧ�õ�Url
        /// </summary>
        /// <param name="signInInfo"></param>     
        /// <param name="userInfo">�û���Ϣ</param>
        /// <returns></returns>
        private static string GetTicketForAppUrl(ISignInInfo signInInfo, IUser userInfo)
        {
            HttpRequest Request = HttpContext.Current.Request;

            string strIP = Request.QueryString["ip"] ?? Request.UserHostAddress;
            //1������Ticket��Ϣ
            ITicket ticket = new Ticket(Common.GenerateTicketString(signInInfo, strIP));
            ticket.AppEnvironment = userInfo.OrganizationID.ToString();
            //2���־û�����Ticket
            //����url
            string strReturnUrl = HttpUtility.UrlDecode(Request.QueryString["ru"]);
            //ע��url
            string strLogOffUrl = Request.QueryString["lou"] ?? "#";
            //Ӧ��ID
            string strAppID = Request.QueryString["aid"] ?? PassportClientSettings.GetConfig().AppID;
            //����uri����
            System.Uri uri = string.IsNullOrEmpty(strReturnUrl) ? Request.Url : new Uri(strReturnUrl);
            IPassportFacade handler = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            handler.SaveTicket(new PassportTicket()
            {
                APP_SIGNIN_ID = ticket.AppSignInSessionID,
                APP_ID = ticket.AppID,
                SIGNIN_ID = ticket.SignInInfo.SignInSessionID,
                APP_SIGNIN_TIME = ticket.AppSignInTime,
                APP_SIGNIN_TIMEOUT = ticket.AppSignInTimeout,
                APP_SIGNIN_IP = ticket.AppSignInIP,
                APP_SIGNIN_URL = uri.ToString(),
                APP_LOGOFF_URL = new Uri(strLogOffUrl, UriKind.RelativeOrAbsolute).ToString()
            });
            //ticket��Ϣ����
            string strBase64 = ETMS.Security.Common.EncryptTicket(ticket);
            //���췵��Ӧ�õ�¼url
            string targetParams = string.Format("{0}={1}",
                                    PassportManager.TicketParamName,
                                    HttpUtility.UrlEncode(strBase64));
            if (uri.Query.Length > 0)
                return (uri.ToString() + "&" + targetParams);
            else
                return (uri.ToString() + "?" + targetParams);
        }

        /// <summary>
        /// ����SignInInfo��Ϣ�����û�Ӧ����֤Url
        /// </summary>
        /// <param name="signInInfo">ͳһ�û���֤��Ϣ</param>
        /// <param name="userInfo">�û���Ϣ</param>
        /// <returns>�û�Ӧ����֤Url</returns>
        private static string GenerateTicketUrlBySignInInfo(ISignInInfo signInInfo, IUser userInfo)
        {
            IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            //ͳһ�û���֤��Ϣ�־û�
            Authenticator.SaveSignInInfo(new PassportSignInInfo()
            {
                SIGNIN_ID = signInInfo.SignInSessionID,
                USER_ID = int.Parse(signInInfo.UserID),
                USER_NAME = signInInfo.UserName,
                AUTHENTICATE_SERVER = signInInfo.AuthenticateServer,
                SIGNIN_TIME = signInInfo.SignInTime,
                SIGNIN_TIMEOUT = signInInfo.SignInTimeout
            });
            //ͳһ�û���֤cookie�洢
            signInInfo.SaveToCookie();

            //Ӧ����֤��Ϣ����
            return GetTicketForAppUrl(signInInfo, userInfo);
        }
        #endregion
    }


}
