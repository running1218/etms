
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
    /// 用于单点登录服务认证实现
    /// </summary>
    public abstract class DefaultAuthenticator
    {
        #region 登录
        /// <summary>
        /// 通过用户名、密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="bSaveUserName">是否记住用户名</param>
        /// <param name="bAutoSignIn">是否自动登录</param>
        /// <returns>认证通过后调整到应用url（含ticket)</returns>
        public static string SignIn(string userName, string password, bool bSaveUserName, bool bAutoSignIn)
        {
            string ssid = Guid.NewGuid().ToString();
            IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();

            try
            {
                IUser userInfo = Authenticator.Authenticate(userName, password, ssid);

                //(1)统一用户认证信息->(2)持久化->(3)应用认证信息->(4)重定向到请求页面
                //统一用户认证信息构造
                XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, !bSaveUserName, bAutoSignIn, ssid);
                SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                //应用认证Url构造
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过用户名、密码登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="bSaveUserName">是否记住用户名</param>
        /// <param name="bAutoSignIn">是否自动登录</param>
        /// <param name="isEncrypt">密码是否加密</param>
        /// <returns>认证通过后返回用户ID</returns>
        public static IUser MobileSignIn(string userName, string password, bool bSaveUserName, bool bAutoSignIn,bool isEncrypt = false)
        {
            string ssid = Guid.NewGuid().ToString();
            try
            {
                IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
                IUser userInfo = Authenticator.Authenticate(userName, password, ssid, isEncrypt);

                //(1)统一用户认证信息->(2)应用认证信息存储->(3)返回用户ID

                //统一用户认证信息构造
                //XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, !bSaveUserName, bAutoSignIn, ssid);
                //SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                ////统一用户认证cookie存储
                //signInInfo.SaveToCookie();

                //用户ID
                return userInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 模拟用户登录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>认证通过后调整到应用url（含ticket)</returns>
        public static string ImitateSignIn(int userID)
        {
            IPassportFacade Service = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            try
            {
                IUser userInfo = Service.GetUserInfoByID(userID);

                //(1)统一用户认证信息->(2)持久化->(3)应用认证信息->(4)重定向到请求页面
                //统一用户认证信息构造
                XmlDocument signInXml = Common.GenerateSignInInfo(userInfo, true, false, Guid.NewGuid().ToString());
                SignInInfo signInInfo = new SignInInfo(signInXml.InnerXml);

                //应用认证Url构造
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 自动登录过程
        /// </summary>
        /// <returns>如果用户认证信息存在，则返回认证通过后调整到应用url（含ticket)，否则返回""</returns>
        public static string AutoSignIn()
        {

            //从Cookie中提取用户认证信息
            ISignInInfo signInInfo = SignInInfo.LoadFromCookie();

            //如果用户认证信息失效
            if (signInInfo == null || !signInInfo.IsValid())
            {
                return "";//用户认证cookie失效，无法自动登录，请重新登录！                
            }
            else //如果用户认证信息有效
            {
                //如果采用相对时间过去策略，则时间向后滑动
                if (PassportSignInSettings.GetConfig().HasSlidingExpiration)
                    signInInfo.SignInTime = DateTime.Now;
                IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
                IUser userInfo = Authenticator.GetUserInfoByID(int.Parse(signInInfo.UserID));
                return GenerateTicketUrlBySignInInfo(signInInfo, userInfo);
            }
        }

        /// <summary>
        /// 验证登录参数，Url内参数        
        ///1、ru 登录后跳转到的地址
        ///2、to 应用有效周期
        ///3、aid 应用id
        ///4、ip 用户ip
        ///5、lou 应用注销时回调地址（全路径）
        ///6、sf 16位MD5校验码，防止篡改
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
                //认证参数无效！
                throw new BusinessException("Authenticate.InValidParams");
            }
            else
            {
                //根据传进的参数构造16位md5，并与“sf”参数做比较
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
                    //认证参数无效！
                    throw new BusinessException("Authenticate.InValidParams");
                }
            }
        }
        #endregion

        #region 注销
        /// <summary>
        /// 注销统一用户认证，并注销用户所有登录的应用！
        /// 在此仅注销统一认证，需要回调各个应用注销页来单独注销各个应用。
        /// </summary>     
        /// <returns>用户已经认证登录的应用注销地址列表</returns>
        public static IList<string> LogOffAllAPP()
        {
            ISignInInfo signInInfo = SignInInfo.LoadFromCookie();

            if (signInInfo == null || !signInInfo.IsValid())//无效应用认证信息
            {
                return new List<string>();
            }
            
            string sessionID = signInInfo.SignInSessionID;
            IPassportFacade passportAuthenticateService = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            IList<string> appLogOffUrls = passportAuthenticateService.GetAllRelativeAppsLogOffCallBackUrl(sessionID);
            //1、移除数据库认证信息(先不考虑）
            //passportAuthenticateService.DeleteRelativeSignInInfo(sessionID);
            //2、移除统一用户认证cookie
            PassportManager.ClearSignInCookie();
            //3、返回用户已登录应用列表，需前段展现，以（嵌入Image方式）回调，移除应用认证cookie
            //4、清除用户在线状态
            //ServiceRepository.OnlineUserStateService.ClearUserOnlineState(Guid.Parse(signInInfo.UserID));
            return appLogOffUrls;
        }

        /// <summary>
        /// 用户登出状态置0：登出
        /// </summary>
        /// <param name="userID"></param>
        public static void LogoffUserAccessLog(int userID)
        {
            IPassportFacade Service = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            Service.LogoffUserAccessSuccessLog(userID);
        }

        /// <summary>
        /// 获取用户登陆信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetPassportAccessLog(int userID)
        { 
            return ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>().GetPassportAccessLog(userID);
        }

        /// <summary>
        /// 强制清除所有应用Ticket，以防止之前用户应用认证对后登录的影响
        /// 内在的前置规则：所有应用Ticket对应的Cookie存放在相同的Domain下！
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
        /// 创建应用认证信息Ticket，并返回登录应用的Url
        /// </summary>
        /// <param name="signInInfo"></param>     
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        private static string GetTicketForAppUrl(ISignInInfo signInInfo, IUser userInfo)
        {
            HttpRequest Request = HttpContext.Current.Request;

            string strIP = Request.QueryString["ip"] ?? Request.UserHostAddress;
            //1、构造Ticket信息
            ITicket ticket = new Ticket(Common.GenerateTicketString(signInInfo, strIP));
            ticket.AppEnvironment = userInfo.OrganizationID.ToString();
            //2、持久化保存Ticket
            //返回url
            string strReturnUrl = HttpUtility.UrlDecode(Request.QueryString["ru"]);
            //注销url
            string strLogOffUrl = Request.QueryString["lou"] ?? "#";
            //应用ID
            string strAppID = Request.QueryString["aid"] ?? PassportClientSettings.GetConfig().AppID;
            //返回uri类型
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
            //ticket信息加密
            string strBase64 = ETMS.Security.Common.EncryptTicket(ticket);
            //构造返回应用登录url
            string targetParams = string.Format("{0}={1}",
                                    PassportManager.TicketParamName,
                                    HttpUtility.UrlEncode(strBase64));
            if (uri.Query.Length > 0)
                return (uri.ToString() + "&" + targetParams);
            else
                return (uri.ToString() + "?" + targetParams);
        }

        /// <summary>
        /// 基于SignInInfo信息创建用户应用认证Url
        /// </summary>
        /// <param name="signInInfo">统一用户认证信息</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>用户应用认证Url</returns>
        private static string GenerateTicketUrlBySignInInfo(ISignInInfo signInInfo, IUser userInfo)
        {
            IPassportFacade Authenticator = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            //统一用户认证信息持久化
            Authenticator.SaveSignInInfo(new PassportSignInInfo()
            {
                SIGNIN_ID = signInInfo.SignInSessionID,
                USER_ID = int.Parse(signInInfo.UserID),
                USER_NAME = signInInfo.UserName,
                AUTHENTICATE_SERVER = signInInfo.AuthenticateServer,
                SIGNIN_TIME = signInInfo.SignInTime,
                SIGNIN_TIMEOUT = signInInfo.SignInTimeout
            });
            //统一用户认证cookie存储
            signInInfo.SaveToCookie();

            //应用认证信息构造
            return GetTicketForAppUrl(signInInfo, userInfo);
        }
        #endregion
    }


}
