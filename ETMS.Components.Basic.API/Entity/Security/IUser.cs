namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// Portal模型依赖的用户接口
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        int UserID { get; set; }
        /// <summary>
        /// 用户登录名
        /// </summary>
        string LoginName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        string RealName { get; set; }
        /// <summary>
        /// 登录口令
        /// </summary>
        string PassWord { get; set; }
        /// <summary>
        /// 用户授予角色
        /// </summary>
        IRole[] MapRoles { get;set;}

        /// <summary>
        /// 用户所属机构
        /// </summary>
        int OrganizationID { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        string PhotoUrl { get; set; }
    }
}
