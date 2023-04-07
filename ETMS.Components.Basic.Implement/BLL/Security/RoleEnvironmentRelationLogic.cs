using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 角色环境关系
    /// </summary>
    public class RoleEnvironmentRelationLogic : DefaultManager
    {
        public RoleEnvironmentRelationLogic(ETMS.AppContext.IObject manager)
            : base(manager, new RoleEnvironmentDataAccess())
        {

        }
        protected override void doAssociate(ETMS.AppContext.IObject member)
        {
            RoleEnvironment entity = (RoleEnvironment)member;
            entity.RoleID = (int)this.Manager.PK.Value;
        }

        protected override void doReleaseAssociate(ETMS.AppContext.IObject member)
        {
            RoleEnvironmentDataAccess roleEnvironmentDataAccess=new RoleEnvironmentDataAccess();
            //1、移除当前角色下所有子角色分配的此功能
            RoleEnvironment roleEnvironment = (RoleEnvironment)member;
            Role role = (Role)new RoleLogic().GetNodeByID(roleEnvironment.RoleID);
            foreach (RoleEnvironment item in roleEnvironmentDataAccess.Query(string.Format(" AND EnvironmentID={0} AND RoleCode like '{1}%'", roleEnvironment.EnvironmentID, role.RoleCode)))
            {
                roleEnvironmentDataAccess.Delete(item);
            }
            //2、移除当前角色下分配的此功能
            base.doReleaseAssociate(member);
        }
    }
}
