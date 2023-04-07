using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Utility;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement.DAL;
using ETMS.AppContext;
using System.Data;

namespace ETMS.Components.Reporting.Implement.BLL
{
    public partial class OrgnizationTrainingSummaryLogic
    {
        private static readonly OrgnizationTrainingSummaryDataAccess DAL = new OrgnizationTrainingSummaryDataAccess();

        public List<OrgnizationTrainingSummary> GetOrgnizationTrainingSummary(bool isSingleOrganizationVersion, DateTime itemBeginTime, DateTime itemEndTime, string orgnizationName)
        {
            var source = DAL.GetOrgnizationTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, orgnizationName).ToList<OrgnizationTrainingSummary>();

            if (isSingleOrganizationVersion)
                source = source.Where(s => s.OrganizationID.Equals(UserContext.Current.OrganizationID)).ToList();
            return source;
        }

        public DataTable GetOrgnizationTrainingSummaryTab(bool isSingleOrganizationVersion, DateTime itemBeginTime, DateTime itemEndTime, string orgnizationName)
        {
            DataTable source = DAL.GetOrgnizationTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, orgnizationName);

            if (isSingleOrganizationVersion)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (row["OrganizationID"].ToInt() != UserContext.Current.OrganizationID)
                    {
                        source.Rows.Remove(row);
                    }
                }
            }
            return source;
        }

        public List<OrgnizationTrainingSummary> GetOrgnizationTrainingSummary(bool isSingleOrganizationVersion, DateTime itemBeginTime, DateTime itemEndTime, string orgnizationName, int pageIndex, int pageSize, out int totalRecords)
        {
            var source = DAL.GetOrgnizationTrainingSummary(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, orgnizationName).ToList<OrgnizationTrainingSummary>();
            if (isSingleOrganizationVersion)
                source = source.Where(s => s.OrganizationID.Equals(UserContext.Current.OrganizationID)).ToList();
            return source.PageList<OrgnizationTrainingSummary>(pageIndex, pageSize, out totalRecords);
        }
    }
}
