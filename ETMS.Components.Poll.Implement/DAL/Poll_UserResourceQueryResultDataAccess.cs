//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:40.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using System.Text;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// 数据访问
    /// </summary>
    public partial class Poll_UserResourceQueryResultDataAccess
    {
        public bool IsHasJoined(Int32 queryID, string userName, string userType, string resourceType, string resourceCode)
        {
            string whereSql = string.Format(" SELECT COUNT(*) FROM Poll_UserResourceQueryResult WHERE QueryID={0} AND userName='{1}' AND userType='{2}' AND ResourceTypeCode='{3}' AND ResourceCode='{4}'", new object[] { queryID, userName, userType, resourceType, resourceCode });
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, whereSql);
            return (count > 0);
        }

        /// <summary>
        /// 根据标识问卷的AnswerXML文档
        /// </summary>
        public String CreateAnswerXMLOfUserByQueryID(Int32 batchID)
        {
            return CreateXMLByQueryID(batchID, 5, null, null);
        }
        private String CreateXMLByQueryID(Int32 queryID, int requstType, string resourceType, string resourceCode)
        {
            StringBuilder XmlContent = new StringBuilder();
            SqlParameter[] parms = null;
            string commandName = "";
            if (requstType == 5)
            {
                commandName = "dbo.Pr_Poll_Query_CreateAnswerXMLOfUserByBatchID";
                XmlContent.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
                if (parms == null)
                {
                    parms = new SqlParameter[] { new SqlParameter("@BatchID", SqlDbType.Int) };
                    SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
                }
                parms[0].Value = queryID;

            }
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    XmlContent.Append(dataReader.GetString(0));
                }
            }

            return XmlContent.ToString();
        }




        /// <summary>
        /// 获取用户下的所有调查问卷 已发布 启用 未结束 未作答
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="orgID">用户所在的组织机构ID</param>
        /// <returns></returns>
        public DataTable GetQueryListNoAnswerByUserID(int userID, int orgID)
        {
            string commandName = "dbo.[pr_Poll_QueryListNoAnswerByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }


        /// <summary>
        /// 获取用户下的所有调查问卷 已发布 启用 未结束
        /// </summary>
        public DataTable GetQueryListForUserPagedList(int userID, int orgID)
        {
            string commandName = "dbo.pr_Poll_QueryListForUser";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

            return dt;
        }

        /// <summary>
        /// 根据试卷ID查询答卷人数和选择人数
        /// </summary>
        /// <param name="queryID"></param>
        /// <returns></returns>
        public int GetQueryUserCount(int queryID)
        {

            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_Count";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@TotalRecords", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

            return (int)parms[1].Value;
        }

        /// <summary>
        /// 查询已提交试卷的用户信息add 2013-1-8 hjy 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetEntityListByQueryID(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "dbo.Pr_Poll_UserResourceQueryResultByQueryID_GetPagedList";
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
        /// 查询未提交试卷的用户信息 add 2013-1-9 hjy 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryID"></param>
        /// <param name="resourceTypeCode"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetUnSubmitUserByQueryID(int pageIndex, int pageSize, int queryID, string resourceTypeCode, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Poll_QueryAreaNotSubmitUser_GetPagedList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@QueryID",SqlDbType.Int),
                    new SqlParameter("@ResourceTypeCode",SqlDbType.VarChar),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = resourceTypeCode;
            parms[2].Value = pageIndex;
            parms[3].Value = pageSize;
            parms[4].Value = "";
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }


        /// <summary>
        /// 查询调查试卷的明细数据(包括提交调查人员和未提交人员)add 2013-1-9 hjy 
        /// </summary>
        /// <param name="queryID"></param>
        /// <param name="resourceTypeCode"></param>
        /// <returns></returns>
        public DataTable GetSubmitTotalUserByQuerID(int queryID, string resourceTypeCode)
        {

            string commandName = "dbo.Pr_Poll_QueryAreaTotalByQueryID_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar)
									};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }


            parms[0].Value = queryID;
            parms[1].Value = resourceTypeCode;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 查询未提交答卷的人员并且发送邮件 add 2013-1-10 hjy
        /// </summary>
        /// <param name="queryID"></param>
        /// <returns></returns>
        public DataTable GetUnSubmitPaperUserByQuerID(int queryID, string resourceTypeCode)
        {
            string commandName = "dbo.Pr_Poll_InvestPaperUnSubmitUserSendMessage";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
                    new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar)
									};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }


            parms[0].Value = queryID;
            parms[1].Value = resourceTypeCode;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

    }
}
