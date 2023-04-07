using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class FunctionGroupFunctionRelationLogic : DefaultManager
    {
        public FunctionGroupFunctionRelationLogic(ETMS.AppContext.IObject manager)
            : base(manager, new FunctionDataAccess())
        {

        }

        protected override void doAssociate(ETMS.AppContext.IObject member)
        {
            Function entity = (Function)member;
            entity.FunctionGroupID = (int)this.Manager.PK.Value;
        }
    }
}
