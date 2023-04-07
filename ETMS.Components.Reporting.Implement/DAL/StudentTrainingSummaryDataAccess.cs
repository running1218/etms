using System;
using ETMS.Utility.Data;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Reporting.Implement.DAL
{
    public class StudentTrainingSummaryDataAccess
    {
        /// <summary>
        /// 学员培训汇总表
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetStudentTrainingSummary(int organizationID, DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            string commandName = "[dbo].[Pr_Reporting_GetStudentTrainingSummary]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
				    new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
                    new SqlParameter("@WorkerNo", SqlDbType.NVarChar),
                    new SqlParameter("@PostName", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = itemBeginTime;
            parms[2].Value = itemEndTime;
            parms[3].Value = realName;
            parms[4].Value = departmentName;
            parms[5].Value = workerNo;
            parms[6].Value = postName;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetAllOrderList(int isCheck, string OrderNo, int OrderStatus, string LoginName, string RealName, int OrganizationID, DateTime BeginTime, DateTime EndTime, int pageIndex, int PageSize, string SortExpression, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Order_GetAllOrderList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PageIndex", SqlDbType.Int),
				    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@SortExpression", SqlDbType.NVarChar),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar),
                    new SqlParameter("@OrderStatus", SqlDbType.Int),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@LoginName", SqlDbType.NVarChar),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@isCheck", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = PageSize;
            parms[2].Value = SortExpression;
            parms[3].Value = OrderNo;
            parms[4].Value = OrderStatus;
            parms[5].Value = OrganizationID;
            parms[6].Value = LoginName;
            parms[7].Value = RealName;
            parms[8].Value = BeginTime;
            parms[9].Value = EndTime;
            parms[10].Value = isCheck;
            #endregion
            DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[11].Value;
            return dt;
        }
    }
}
