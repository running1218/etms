using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class UserFunctionRelationLogic : DefaultManager
    {
        public UserFunctionRelationLogic(ETMS.AppContext.IObject manager)
            : base(manager, new UserFunctionDataAccess())
        {

        }
        protected override void doAssociate(ETMS.AppContext.IObject member)
        {
            UserFunction entity = (UserFunction)member;
            entity.UserID = (int)this.Manager.PK.Value;
        }       
    }
}
