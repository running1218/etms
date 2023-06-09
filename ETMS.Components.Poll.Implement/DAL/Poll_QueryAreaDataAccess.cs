//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Poll.API.Entity;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// 数据访问
    /// </summary>
    public partial class Poll_QueryAreaDataAccess
    {
        /// <summary>
        /// 获取首页问卷区域分配表(针对普通用户)
        /// </summary>
        /// <param name="resourceType">资源类型</param>
        /// <returns>首页问卷区域分配表</returns>
        public DataTable GetQueryAreaForUserMode(string resourceType)
        {
            string commandName = "";
            if (resourceType == "R4")
            {
                commandName = "select * from vw_IndexQueryArea_UserMode where ResourceTypeCode='" + resourceType + "' order by Createtime desc";
            }
            else
            {
                commandName = "select * from vw_IndexQueryArea_UserMode where ResourceTypeCode='" + resourceType + "' order by AreaType";
            }

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName).Tables[0];
        }
        /// <summary>
        /// 获取首页问卷的区域分配表(针对管理者用户)
        /// </summary>
        /// <param name="queryID">资源编号</param>
        /// <param name="resourceType">资源类型</param>
        /// <returns>首页问卷的区域分配表</returns>
        public List<Poll_QueryArea> GetQueryAreaByQueryIDForManagerMode(Int32 queryID, string resourceType)
        {
            string commandName = "dbo.Pr_tb_r_QueryArea_GetByQueryID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					 new SqlParameter("@QueryID", SqlDbType.Int),
                     new SqlParameter("@ResourceType", SqlDbType.VarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryID;
            parms[1].Value = resourceType;

            #endregion

            List<Poll_QueryArea> tb_r_QueryAreas = new List<Poll_QueryArea>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    tb_r_QueryAreas.Add(PopulatePoll_QueryAreaFromDataReader(dataReader));
                }
            }

            return tb_r_QueryAreas;
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
        public DataTable GetNoSelectInfoByOrg(int queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetNoSelectInfoByOrg]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
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

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetSelectInfoByOrg]";
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
        public DataTable GetNoSelectInfoByStudent(int queryID, int orgID, int orgType, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetNoSelectInfoByStudent]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.Int),
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

            string commandName = "[dbo].[Pr_Poll_QueryArea_GetSelectInfoByStudent]";
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
        /// 查询某个问卷调查已经设置调查范围的学员数
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <returns></returns>
        public int GetStudentNumFromQueryArea(int queryID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'";
            string sql = string.Format(sqlModal, queryID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }


        /// <summary>
        /// 查询某个问卷调查在某个组织机构下设置调查范围的学员数
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public int GetStudentNumFromQueryAreaByOrg(int queryID, int orgID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'
                                    AND u.OrganizationID = '{1}'";
            string sql = string.Format(sqlModal, queryID, orgID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }


        /// <summary>
        /// 查询某个问卷调查不属于某组织机构下的已经设置调查范围的学员数
        /// </summary>
        /// <param name="queryID">问卷调查ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public int GetNoStudentNumFromQueryAreaByOrg(int queryID, int orgID)
        {
            string sqlModal = @"SELECT COUNT(*)
                                FROM [Poll_QueryAreaDetail] qad
                                    INNER JOIN Poll_QueryArea qa ON qa.QueryAreaID=qad.QueryAreaID
                                    INNER JOIN Poll_QueryPublishObject qpo ON qpo.QueryPublishID = qa.QueryPublishID
                                    INNER JOIN Poll_Query q ON q.QueryID= qpo.QueryID
                                    INNER JOIN dbo.Site_User u ON u.UserID=qad.DetailCode
                                WHERE qad.DetailType='Student'
                                    AND q.QueryID = '{0}'
                                    AND u.OrganizationID != '{1}'";
            string sql = string.Format(sqlModal, queryID, orgID);
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
            return count;
        }




    }
}
