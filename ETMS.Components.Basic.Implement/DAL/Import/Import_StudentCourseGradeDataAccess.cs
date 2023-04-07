using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Import;

namespace ETMS.Components.Basic.Implement.DAL.Import
{
    /// <summary>
    /// 学员课程成绩导入扩展类
    /// 黄中福：2012－05－20
    /// </summary>
    public partial class Import_StudentCourseGradeDataAccess
    {
        /// <summary>
        /// 验证要导入的学员成绩（到学员选课表）
        /// </summary>
        /// <param name="task"></param>
        /// <returns>失败数量</returns>
        public int DoValid(Import_Task task)
        {
            string commandName = "dbo.[Pr_Import_StudentCourseGrade_ImportValid]";
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
        /// 导入学员的成绩（到学员选课表）
        /// </summary>
        /// <param name="task">导入任务实体</param>
        /// <param name="modifyUser">导入人</param>
        /// <returns>通过总数</returns>
        public int DoImport(Import_Task task,  string modifyUser)
        {
            string commandName = "dbo.[Pr_Import_StudentCourseGrade_Import]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	 
					new SqlParameter("@TaskID", SqlDbType.Int) ,
					new SqlParameter("@ModifyUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = task.TaskID;
            parms[1].Value = modifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
            return (int)parms[2].Value;
        }





    }
}
