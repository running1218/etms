using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;


namespace ETMS.Components.QS.Implement.DAL
{
    public partial class QS_QueryAreaDataAccess
    {


        /// <summary>
        /// 查询某个问卷调查在某个组织机构下所有不是其发布围的组织机构信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgType">发布范围:1只是本组织机构,2本组织机构及下级组织机构,3仅所有下级组织机构</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByOrg(Guid queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_QS_QueryArea_GetNoSelectInfoByOrg]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = orgID;
            parms[2].Value = orgType;
            parms[3].Value = pageIndex;
            parms[4].Value = pageSize;
            parms[5].Value = sortExpression;
            parms[6].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[7].Value;
            return dt;

        }





        /// <summary>
        /// 查询某个问卷调查的发布范围是组织机构的组织机构信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByOrg(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_QS_QueryArea_GetSelectInfoByOrg]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;

        }




        /// <summary>
        /// 查询某个问卷调查在某个组织机构下所有不是其发布围的组织机构信息
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgType">发布范围:1只是本组织机构,2本组织机构及下级组织机构,3仅所有下级组织机构</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetNoSelectInfoByStudent(Guid queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_QS_QueryArea_GetNoSelectInfoByStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = orgID;
            parms[2].Value = orgType;
            parms[3].Value = pageIndex;
            parms[4].Value = pageSize;
            parms[5].Value = sortExpression;
            parms[6].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[7].Value;
            return dt;

        }





        /// <summary>
        /// 查询问卷调查的发布对象为学员的所有学员信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="criteria">以AND开头的查询条件</param>
        /// <param name="totalRecords">返回满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetSelectInfoByStudent(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_QS_QueryArea_GetSelectInfoByStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;

        }





    }
}
