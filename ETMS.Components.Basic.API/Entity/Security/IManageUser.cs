using System;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// Portal模型依赖的管理用户接口
    /// </summary>
    public interface IManageUser : IUser, ETMS.AppContext.IObject
    {
        string Telphone { get; set; }
        string Email { get; set; }
        string Description { get; set; }
        bool IsSysAdmin { get; }
        string Creator { get; set; }
        DateTime CreateTime { get; set; }
        string Modifier { get; set; }
        DateTime ModifyTime { get; set; }
        int Status { get; set; }
    }
}
