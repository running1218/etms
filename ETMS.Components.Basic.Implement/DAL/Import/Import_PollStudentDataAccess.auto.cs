/*
 -- =============================================
-- Author:		<胡俊义>
-- Create date: <2013-1-4>
-- Description:	<导入调查范围的学生>
-- =============================================
 */

using System;
using System.Data;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Import;
using System.Data.SqlClient;
using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.Basic.Implement.DAL.Import
{
    public class Import_PollStudentDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Import_SurveyArea import_SurveyArea)
        {
            string commandName = "dbo.Pr_Poll_ImportStudent_Add";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@DetailID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@TaskID", SqlDbType.Int),
                    new SqlParameter("@QueryID", SqlDbType.Int),
                   
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 50),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@WorkNo", SqlDbType.NVarChar, 50),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar,100),
                    new SqlParameter("@PostName", SqlDbType.NVarChar, 50),
					new SqlParameter("@RankName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Email", SqlDbType.NVarChar, 50),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 50),
                    new SqlParameter("@QueryPublishID",SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[1].Value = import_SurveyArea.TaskID;
            parms[2].Value = import_SurveyArea.QueryID;
            parms[3].Value = import_SurveyArea.Status;
            if (import_SurveyArea.Remark != null) { parms[4].Value = import_SurveyArea.Remark; } else { parms[4].Value = DBNull.Value; }

            if (import_SurveyArea.LoginName != null) { parms[5].Value = import_SurveyArea.LoginName; } else { parms[5].Value = DBNull.Value; }
            if (import_SurveyArea.RealName != null) { parms[6].Value = import_SurveyArea.RealName; } else { parms[6].Value = DBNull.Value; }
            if (import_SurveyArea.WorkNo != null) { parms[7].Value = import_SurveyArea.WorkNo; } else { parms[7].Value = DBNull.Value; }
            parms[8].Value = import_SurveyArea.OrganizationID;
            if (import_SurveyArea.DepartmentName != null) { parms[9].Value = import_SurveyArea.DepartmentName; } else { parms[9].Value = DBNull.Value; }
            if (import_SurveyArea.PostName != null) { parms[10].Value = import_SurveyArea.PostName; } else { parms[10].Value = DBNull.Value; }
            if (import_SurveyArea.RankName != null) { parms[11].Value = import_SurveyArea.RankName; } else { parms[11].Value = DBNull.Value; }
            if (import_SurveyArea.Email != null) { parms[12].Value = import_SurveyArea.Email; } else { parms[12].Value = DBNull.Value; }
            if (import_SurveyArea.DisplayPath != null) { parms[13].Value = import_SurveyArea.DisplayPath; } else { parms[13].Value = DBNull.Value; }
            //parms[14].Value = import_SurveyArea.QueryPublishID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            import_SurveyArea.DetailID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="task"></param>
        /// <returns>失败数量</returns>
        public int DoValid(Import_Task task)
        {
            string commandName = "dbo.Pr_Poll_ImportStudent_Valid";
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

            parms[0].Value = task.TaskID;
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
        public int ImportStudentSurveyArea(int taskID, int queryPublishID, string creator, out int errorNum)
        {
            string commandName = "dbo.Pr_Import_StudentSurveyArea";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) ,
                    new SqlParameter("@QueryPublishID",SqlDbType.Int),
                    new SqlParameter("@Creator",SqlDbType.NVarChar, 50),
                    new SqlParameter("@ErrorNum",SqlDbType.Int),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = taskID;
            parms[1].Value = queryPublishID;
            parms[2].Value = creator;
            parms[3].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            errorNum = (int)parms[3].Value;
            return ((int)parms[4].Value);
        }

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
