using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class RoleFunctionRelationLogic : DefaultManager
    {
        public RoleFunctionRelationLogic(ETMS.AppContext.IObject manager)
            : base(manager, new RoleFunctionDataAccess())
        {

        }
        protected override void doAssociate(ETMS.AppContext.IObject member)
        {
            RoleFunction entity = (RoleFunction)member;
            entity.RoleID = (int)this.Manager.PK.Value;
        }

        protected override void doReleaseAssociate(ETMS.AppContext.IObject member)
        {
            RoleFunctionDataAccess roleFunctionDataAccess=new RoleFunctionDataAccess();
            //1、移除当前角色下所有子角色分配的此功能
            RoleFunction roleFunction = (RoleFunction)member;
            Role role = (Role)new RoleLogic().GetNodeByID(roleFunction.RoleID);
            foreach (RoleFunction item in roleFunctionDataAccess.Query(string.Format(" AND FunctionID={0} AND RoleCode like '{1}%'", roleFunction.FunctionID, role.RoleCode)))
            {
                roleFunctionDataAccess.Delete(item);
            }
            //2、移除当前角色下分配的此功能
            base.doReleaseAssociate(member);
        }
    }
}
