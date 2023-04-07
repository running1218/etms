using System.Collections.Generic;

using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 角色-用户关系 管理器
    /// </summary>
    public class UserRoleRelationLogic
    {
        UserRoleDataAccess DAL = new UserRoleDataAccess();
        /// <summary>
        /// 保存用户角色列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleIDs">授予的角色</param>
        /// <param name="creator">操作人</param>
        public void Save(int userID, string roleIDs, string creator)
        {
            DAL.Save(userID, roleIDs, creator);
        }

        /// <summary>
        /// 获取用户被授予的角色列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>被授予的角色列表</returns>
        public IList<Role> Query(int userID)
        {
            return DAL.Query(userID);
        }
    }
}
