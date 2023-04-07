using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Import;


namespace ETMS.Components.Basic.Implement.DAL.Import
{


    /// <summary>
    /// 培训项目学员导入业务扩展类（导入到学员报名表）
    /// </summary>
    public partial class Import_StudentSignupDataAccess
    {


        /// <summary>
        /// 验证要导入学员到培训项目（学员报名）
        /// </summary>
        /// <param name="task"></param>
        /// <returns>失败数量</returns>
        public int DoValid(Import_Task task)
        {
            string commandName = "dbo.[Pr_Import_StudentSignup_ImportValid]";
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
        /// 导入学员到培训项目（学员报名）
        /// </summary>
        /// <param name="task">导入任务实体</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="createUserID">导入人ID</param>
        /// <param name="createUser">导入人</param>
        /// <param name="ErrorNum">返回不能导入的学员数</param>
        /// <returns>通过总数</returns>
        public int DoImport(Import_Task task, Guid trainingItemID, int createUserID, string createUser, out int errorNum)
        {
            string commandName = "dbo.[Pr_Import_StudentSignup_Import]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) ,
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier) ,
					new SqlParameter("@CreateUserID", SqlDbType.Int) ,
					new SqlParameter("@CreateUser", SqlDbType.VarChar),
					new SqlParameter("@ErrorNum", SqlDbType.Int) ,
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = task.TaskID;
            parms[1].Value = trainingItemID;
            parms[2].Value = createUserID;
            parms[3].Value = createUser;
            parms[4].Direction = ParameterDirection.Output;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            errorNum = (int)parms[4].Value;
            return (int)parms[5].Value;
        }




    }
}
