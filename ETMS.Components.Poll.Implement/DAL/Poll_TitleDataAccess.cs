using System;
using ETMS.Components.Poll.API.Entity;
using ETMS.Utility.Data;
using System.Data.SqlClient;
using System.Data;

namespace ETMS.Components.Poll.Implement.DAL
{

    public partial class Poll_TitleDataAccess
    {
        public void UpdateQSTitleSort(Poll_Title title)
        {

            string commandName = "dbo.Pr_Poll_QueryTitle_TitleNoSort";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TitleID", SqlDbType.Int),
					new SqlParameter("@TitleNo", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = title.TitleID;
            parms[1].Value = title.TitleNo;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 获取某个调查问卷的当前最大题目序号
        /// </summary>
        /// <param name="queryID">调查问卷ID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleNo(int columnID)
        {
            string commandName = string.Format("SELECT ISNULL(MAX(TitleNo),0) FROM Poll_Title WHERE ColumnID='{0}'", columnID);
            return (Int32)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }
    }
}
