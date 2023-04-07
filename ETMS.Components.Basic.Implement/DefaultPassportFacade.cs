using System.Collections.Generic;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Data;
namespace ETMS.Components.Basic.Implement
{
    /// <summary>
    /// 默认的Passport门面模式实现
    /// </summary>
    public class DefaultPassportFacade : DefaultComponent, IPassportFacade
    {
        private PassportAuthenticateLogic PassportAuthenticateLogic = new PassportAuthenticateLogic();
        private RoleLogic RoleService = new RoleLogic();
        private UserRoleRelationLogic UserRoleService = new UserRoleRelationLogic();
        /// <summary>
        /// 设置组件基本信息（默认）
        /// </summary>
        public DefaultPassportFacade()
        {
            this.ID = typeof(IPassportFacade).ToString();
            this.Name = "统一认证与授权";
            this.Description = "向Security模块提供对：统一认证与授权的基本操作支持！";
        }

        public IUser Authenticate(string userName, string password, string ssid, bool isEncrypt = false)
        {
            return PassportAuthenticateLogic.Authenticate(userName, password, ssid, isEncrypt);
        }

        public IUser GetUserInfoByID(int userID)
        {
            return PassportAuthenticateLogic.GetUserInfoByID(userID);
        }

        public void SaveSignInInfo(PassportSignInInfo passportSignInInfo)
        {
            PassportAuthenticateLogic.SaveSignInInfo(passportSignInInfo);
        }

        public PassportSignInInfo GetSignInInfo(string SIGNIN_ID)
        {
            return PassportAuthenticateLogic.GetSignInInfo(SIGNIN_ID);
        }

        public void SaveTicket(PassportTicket passportTicket)
        {
            PassportAuthenticateLogic.SaveTicket(passportTicket);
        }

        public PassportTicket GetTicket(string ticketID)
        {
            return PassportAuthenticateLogic.GetTicket(ticketID);
        }

        public void DeleteRelativeSignInInfo(string signInID)
        {
            PassportAuthenticateLogic.DeleteRelativeSignInInfo(signInID);
        }

        public IList<string> GetAllRelativeAppsLogOffCallBackUrl(string sessionID)
        {
            return PassportAuthenticateLogic.GetAllRelativeAppsLogOffCallBackUrl(sessionID);
        }

        public bool DoesUserHasPermission(string appID, int userID, string funCode)
        {
            //1、获取用户角色
            string roleCodes = GetUserRoles(appID, userID);
            return RoleService.IsAllowManageUserAccessURL(roleCodes, userID, funCode);
        }

        public bool DoesUserHasPermissionWithAppRoles(string appID, int userID, string userRoles, string funCode)
        {
            return RoleService.IsAllowManageUserAccessURL(userRoles, userID, funCode);

        }

        /// <summary>
        /// 获取用户角色，“，”分割多个角色编码
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserRoles(string appID, int userID)
        {
            string result = "";
            IList<Role> roles = UserRoleService.Query(userID);
            foreach (Role role in roles)
            {
                result += role.RoleCode + ",";
            }
            return result;
        }

        public void LogoffUserAccessSuccessLog(int userID)
        {
            PassportAuthenticateLogic.LogoffUserAccessSuccessLog(userID);
        }

        public DataTable GetPassportAccessLog(int userID)
        {
            return PassportAuthenticateLogic.GetPassportAccessLog(userID);
        }
    }
}
