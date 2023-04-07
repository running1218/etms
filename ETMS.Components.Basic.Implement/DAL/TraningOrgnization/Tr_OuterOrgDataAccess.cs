using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.TraningOrgnization
{
    /// <summary>
    /// 外部培训机构表数据访问
    /// </summary>
    public partial class Tr_OuterOrgDataAccess
    {
        /// <summary>
        /// 获取外部培训机构列表
        /// </summary>
        /// <param name="orgID">培训机构编号</param>
        /// <returns></returns>
        public DataTable getList(int orgID)
        {
            string commandName = "dbo.Pr_Tr_OuterOrg_GetList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrgID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;

            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 外部培训组织机构教师统计
        /// </summary>
        /// <param name="orgID">培训机构编号</param>
        /// <returns></returns>
        public DataTable GetOuterOrgTeachers()
        {
            string commandName = "dbo.Pr_Site_Teacher_GetOuterOrgTeachers ";
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, null).Tables[0];
            return dt;
        }
    }
}
