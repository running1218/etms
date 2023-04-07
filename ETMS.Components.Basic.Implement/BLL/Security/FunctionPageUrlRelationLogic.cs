using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class FunctionPageUrlRelationLogic : DefaultManager
    {
        public FunctionPageUrlRelationLogic(ETMS.AppContext.IObject manager)
            : base(manager, new PageUrlDataAccess())
        {
           
        }

        protected override void doAssociate(ETMS.AppContext.IObject member)
        {
            PageUrl entity = (PageUrl)member;
            entity.FunctionID = (int)this.Manager.PK.Value;
        }
    }
}
