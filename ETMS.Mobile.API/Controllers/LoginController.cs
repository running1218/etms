using ETMS.AppContext;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Security;
using ETMS.Utility;
using ETMS.Utility.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ETMS.Mobile.API.Controllers
{
    [RoutePrefix("Api/User/Login")]
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        [Route("{loginName}/{psw}", Name = "用户根据用户名和密码登录")]
        public HttpResponseMessage Post(string loginName, string psw)
        {
            try
            {
                IUser UserInfo = DefaultAuthenticator.MobileSignIn(loginName, psw, false, false);
                User user = new UserLogic().GetUserBaseData(UserInfo.UserID);
                if (user != null)
                {
                    UserInfo.PhotoUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(user.PhotoUrl) ? "default.gif" : user.PhotoUrl);
                }
                return ResponseJson.GetSuccessJson(UserInfo);
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }

        }

        [Route("{uid}", Name = "用户根据用户ID登录")]
        public HttpResponseMessage PostUserID(string uid)
        {
            try
            {
                //解密
                int UserID = TripleDesCryptographyUtility.DecryptTextFromString(uid).ToInt();
                User user = new UserLogic().GetUserBaseData(UserID);
                IUser UserInfo = null;
                if (user != null)
                {
                    UserInfo = DefaultAuthenticator.MobileSignIn(user.LoginName, user.PassWord, false, false, true);
                    UserInfo.PhotoUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(user.PhotoUrl) ? "default.gif" : user.PhotoUrl);
                }
                return ResponseJson.GetSuccessJson(UserInfo);
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }

        }


        [Route("Quit/{UserID}", Name = "用户退出")]
        public HttpResponseMessage Post(int UserID)
        {
            try
            {
                //强制清除存放在相同Cookie.Domain下各应用Ticket，防止多个用户登录之间互相影响！
                //ISignInInfo signInInfo = SignInInfo.LoadFromCookie();
                //int userID = string.IsNullOrEmpty(signInInfo.UserID) ? 0 : int.Parse(signInInfo.UserID);
                new PassportAuthenticateLogic().LogoffUserAccessSuccessLog(UserID);
                //ETMS.Security.DefaultAuthenticator.ForceClearAllAppTicket();
                return ResponseJson.GetSuccessJson();
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }
        }
    }
}
