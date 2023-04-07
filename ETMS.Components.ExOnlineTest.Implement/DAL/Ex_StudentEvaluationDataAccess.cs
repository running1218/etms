using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.ExOnlineTest.Implement.DAL
{
    public class Ex_StudentEvaluationDataAccess
    {
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataSet GetStudentEvaluationListByUserID(int UserID, Guid ItemCourseID)
        {
            string commandName = "[dbo].[Pr_Ex_StudentEvaluation_GetByStudentID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            parms[1].Value = ItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

        }
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetStudentEvaluationListByUserID(int UserID)
        {
            string commandName = "[dbo].[Pr_Ex_StudentEvaluation_GetByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

        }
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentEvaluationListByItemCourseIDAndUserID(int UserID, Guid ItemCourseID)
        {
            string commandName = "[dbo].[Pr_Ex_StudentEvaluation_GetByItemCourseIDAndUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            parms[1].Value = ItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];

        }
    }
}
