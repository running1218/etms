using System;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using System.Data;

namespace ETMS.Components.QS.Implement.DAL
{
    public partial class Import_SurveyAreaDataAccess
    {
        public int DoValid(int taskid)
        {
            string commandName = "dbo.Pr_Import_QueryArea_Valid";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) ,
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = taskid;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            return ((int)parms[1].Value);
        }



        /// <summary>
        /// 导入主表数据
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="QueryAreaID"></param>
        /// <param name="creator"></param>
        /// <param name="errorNum"></param>
        /// <returns></returns>
        public int ImportStudentSurveyArea(int queryAreaID, int taskID, string creator, out int errorNum)
        {
            string commandName = "dbo.Pr_Import_QueryArea_Student";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) ,
                    new SqlParameter("@QueryAreaID", SqlDbType.Int) ,
                    new SqlParameter("@Creator",SqlDbType.NVarChar, 50),
                    new SqlParameter("@ErrorNum",SqlDbType.Int),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = taskID;
            parms[1].Value = queryAreaID;
            parms[2].Value = creator;
            parms[3].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            errorNum = (int)parms[3].Value;
            return ((int)parms[4].Value);
        }

        /// <summary>
        /// 根据任务编码查询导入信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public DataSet GetPollImportStudentList(int taskID)
        {
            string commandName = "dbo.Pr_Poll_ImportStudent_TaskInfo";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) 
										};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = taskID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }
    }
}
