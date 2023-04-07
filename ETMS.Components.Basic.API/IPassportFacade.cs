using System.Collections.Generic;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API.Entity.Security;
using System.Data;
namespace ETMS.Components.Basic.API
{
    /// <summary>
    /// 认证与授权门面模式
    /// </summary>
    public interface IPassportFacade : IComponent
    {
        #region 认证相关
        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果用户名密码正确则返回用户信息，否则返回null</returns>        
        IUser Authenticate(string userName, string password, string ssid, bool isEncrypt = false);

        /// <summary>
        /// 根据用户ID获取用户基本信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户基本信息</returns>
        IUser GetUserInfoByID(int userID);

        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="passportSignInInfo">用户认证后生成的登录信息</param>        
        void SaveSignInInfo(PassportSignInInfo passportSignInInfo);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="SIGNIN_ID">认证ID</param>
        /// <returns>登录信息</returns>        
        PassportSignInInfo GetSignInInfo(string SIGNIN_ID);

        /// <summary>
        /// 保存应用认证后生成的PassportTicket信息
        /// </summary>
        /// <param name="passportTicket"></param>        
        void SaveTicket(PassportTicket passportTicket);

        /// <summary>
        /// 获取应用登录信息
        /// </summary>
        /// <param name="ticketID">应用认证ID</param>
        /// <returns>应用登录信息</returns>        
        PassportTicket GetTicket(string ticketID);

        /// <summary>
        /// 删除相关的认证信息
        /// </summary>
        /// <param name="signInID">认证的SessionID</param>        
        void DeleteRelativeSignInInfo(string signInID);

        /// <summary>
        /// 得到某个登录Session的所有注销的回调Url
        /// </summary>
        /// <param name="sessionID">登录SessionID</param>
        /// <returns>某个登录Session的所有注销的回调Url</returns>
        IList<string> GetAllRelativeAppsLogOffCallBackUrl(string sessionID);

        /// <summary>
        /// 用户登出状态重置0
        /// </summary>
        /// <param name="userID"></param>
        void LogoffUserAccessSuccessLog(int userID);

        /// <summary>
        /// 登陆用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        DataTable GetPassportAccessLog(int userID);

        #endregion

        #region 授权相关
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="appID">应用ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>角色编码列表,多个间用","分割</returns>
        string GetUserRoles(string appID, int userID);
        /// <summary>
        /// 用户是否拥有功能操作权限
        /// </summary>
        /// <param name="appID">应用ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="funCode">功能编码（目前：url)</param>
        /// <returns>true:已授权 false:未授权</returns>
        bool DoesUserHasPermission(string appID, int userID, string funCode);

        /// <summary>
        /// 用户是否拥有功能操作权限
        /// 可以有外部传入用户在当前应用的角色列表（通过缓存或cookie方式暂存），以提供性能。
        /// </summary>
        /// <param name="appID">应用ID</param>
        /// <param name="userRoles">用户拥有的应用角色列表，多个角色间用","分割</param>
        /// <param name="funCode">功能编号</param>
        /// <returns>true:已授权 false:未授权</returns> 
        bool DoesUserHasPermissionWithAppRoles(string appID, int userID, string userRoles, string funCode);
        #endregion
    }
}
