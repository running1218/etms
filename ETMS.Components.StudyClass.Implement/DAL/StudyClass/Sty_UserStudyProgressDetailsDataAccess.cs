using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility.Data;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    public class Sty_UserStudyProgressDetailsDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">业务实体</param>
        public void Insert(Sty_UserStudyProgressDetails entity)
        {
            string commandName = "Pr_Sty_UserStudyProgressDetails_Insert";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@UserStudyProgressDetailsID", SqlDbType.UniqueIdentifier, entity.UserStudyProgressDetailsID),
                SqlHelper.CreateInputSqlParameter("@ChapterResourceID", SqlDbType.UniqueIdentifier, entity.ChapterResourceID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, entity.UserID),
                SqlHelper.CreateInputSqlParameter("@StartTime", SqlDbType.DateTime, entity.StartTime, true),
                SqlHelper.CreateInputSqlParameter("@EndTime", SqlDbType.DateTime, entity.EndTime, true),
                SqlHelper.CreateInputSqlParameter("@StudyProgress", SqlDbType.Int, entity.StudyProgress, true),
                SqlHelper.CreateInputSqlParameter("@StudyTime", SqlDbType.Decimal, entity.StudyTime, true),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, entity.TrainingItemCourseID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }
    }
}
