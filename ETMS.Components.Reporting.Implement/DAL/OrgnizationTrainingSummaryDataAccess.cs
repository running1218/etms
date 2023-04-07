using System;
using ETMS.Utility.Data;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Reporting.Implement.DAL
{
    public class OrgnizationTrainingSummaryDataAccess
    {
        /// <summary>
        /// 机构培训汇总表
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetOrgnizationTrainingSummary(int organizationID, DateTime itemBeginTime, DateTime itemEndTime, string orgnizationName)
        {
            string commandName = "[dbo].[Pr_Reporting_GetOrgnizationTrainingSummary]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
				    new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemEndTime", SqlDbType.DateTime),                   
                    new SqlParameter("@OrganizationName", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = itemBeginTime;
            parms[2].Value = itemEndTime;
            parms[3].Value = orgnizationName;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
