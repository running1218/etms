using System;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;

namespace ETMS.Components.Poll.Implement.DAL
{
    public partial class Poll_QueryAreaDetailDataAccess
    {
        /// <summary>
        /// 设置调查范围增加全部调查学员的方法 add 2013-1-31 hjy
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="criteria">查询条件</param>
        /// <param name="queryAreaID">区域ID</param>
        /// <param name="creator">增加明细人</param>
        public void AddAllStudent(string sortExpression, string criteria, int queryAreaID, string creator)
        {
            string commandName = "[dbo].[Pr_Poll_QueryAreaDetail_AddAll]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.VarChar),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryAreaID;
            parms[1].Value = creator;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            int i = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }
        /// <summary>
        /// 设置调查范围删除全部学员的方法
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <param name="queryAreaID">区域ID</param>
        public int DeleteAllStudent(string criteria, int queryAreaID, out int totalCount)
        {
            string commandName = "[dbo].[Pr_Poll_QueryAreaDetailAll_Delete]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
                    new SqlParameter("@totalCount", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryAreaID;
            parms[1].Value = criteria;
            parms[2].Direction = ParameterDirection.Output;
            #endregion
            int i = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
            totalCount = (int)parms[2].Value;
            return i;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int RemoveItem(Int32 queryAreaDetailID)
        {
            string commandName = "dbo.Pr_Poll_QueryAreaDetail_Delete_item";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaDetailID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryAreaDetailID;

            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int Remove(Int32 queryAreaDetailID,int queryID)
        {
            string commandName = "dbo.Pr_Poll_QueryAreaDetail_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaDetailID", SqlDbType.Int),
                    new SqlParameter("@QueryID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryAreaDetailID;
            parms[1].Value = queryID;
            #endregion
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
    }
}
