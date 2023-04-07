using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// ��ɫ������ϵ
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
            //1���Ƴ���ǰ��ɫ�������ӽ�ɫ����Ĵ˹���
            RoleEnvironment roleEnvironment = (RoleEnvironment)member;
            Role role = (Role)new RoleLogic().GetNodeByID(roleEnvironment.RoleID);
            foreach (RoleEnvironment item in roleEnvironmentDataAccess.Query(string.Format(" AND EnvironmentID={0} AND RoleCode like '{1}%'", roleEnvironment.EnvironmentID, role.RoleCode)))
            {
                roleEnvironmentDataAccess.Delete(item);
            }
            //2���Ƴ���ǰ��ɫ�·���Ĵ˹���
            base.doReleaseAssociate(member);
        }
    }
}
