using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Basic.Implement.DAL.Course
{
    public class Sty_UserStudyProgressDataAccess
    {
        /// <summary>
        /// 根据项目课程关系ID查询课程资源学习进度
        /// </summary>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public DataTableCollection GetCourseProgressByTrainingItemCourseID(Guid TrainingItemCourseID,int UserID)
        {
            string commandName = "[dbo].[Pr_GetCourseProgressByTrainingItemCourseID]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, TrainingItemCourseID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, UserID)
            };

            var collection = SqlHelper.ExecuteDataset(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters).Tables;
            return collection;
        }

        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TrainingItemCourseID"></param>
        /// <returns></returns>
        public DataSet GetTestAndPaperProgress(int UserID, Guid TrainingItemCourseID)
        {
            string commandName = "[dbo].[Pr_GetTestAndPaperProgress]";
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
            parms[1].Value = TrainingItemCourseID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);

        }
    }
}
