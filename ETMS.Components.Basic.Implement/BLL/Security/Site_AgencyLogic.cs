using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.Security
{
    public class Site_AgencyLogic
    {
        private static readonly Site_AgencyDataAccess DAL = new Site_AgencyDataAccess();

        public int Save(Site_Agency entity)
        {
            if (entity.AgencyID == 0)
            {
                return this.Insert(entity);
            }
            else
            {
                return this.Update(entity);
            }
        }
        public int Insert(Site_Agency entity)
        {
            return DAL.Insert(entity);
        }

        public int Update(Site_Agency entity)
        {
            return DAL.Update(entity);
        }

        public int Delete(int agencyID)
        {
            return DAL.Delete(agencyID);
        }

        public List<Site_Agency> GetPageList(int orgID)
        {
            return DAL.GetPageList(orgID).ToList<Site_Agency>();
        }
    }
}
