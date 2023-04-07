
using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// 问卷调查题目表数据访问
    /// </summary>
    public partial class QS_QueryTitleDataAccess
    {


        /// <summary>
        /// 获取某个调查问卷的当前最大题目序号
        /// </summary>
        /// <param name="queryID">调查问卷ID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleNo(Guid queryID)
        {
            string commandName = string.Format("SELECT ISNULL(MAX(TitleNo),0) FROM QS_QueryTitle WHERE QueryID='{0}'", queryID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 获取问卷调查的所有标题详细信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GetQueryTitleAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_QS_QueryTitle_GetALLInfoList]";
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
        /// 修改试题序号
        /// </summary>
        /// <param name="titleEntity"></param>
        public void UpdateQSTitleSort(QS_QueryTitle titleEntity)
        {
            string commandName = "dbo.[Pr_QS_QueryTitleSort_Update]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TitleNo", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = titleEntity.TitleID;
            parms[1].Value = titleEntity.TitleNo;

            #endregion
            int i = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

        }


    }
}
