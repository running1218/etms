using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// Portal模型依赖的角色接口
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        string RoleCode { get; set; }
        /// <summary>
        /// 角色映射编码
        /// </summary>
        string RoleMapCode { get; set; }
        /// <summary>
        /// 是否系统管理员角色
        /// </summary>
        bool IsSysAdminRole { get;}
        /// <summary>
        /// 父角色ID
        /// </summary>
        [Obsolete("此方法已经废弃，请使用IsSysAdminRole来判断！")]
        int ParentNodeID { get; set; }
    }
}
