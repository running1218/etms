using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    public partial class Sty_UserStudyProgressDataAccess
    {
        public CourseContentStudyProgress GetUserStudyProgress(int UserID, Guid ResourceID, Guid TrainingItemCourseID, string code)
        {
            CourseContentStudyProgress progress = null;
            string commandName = "Pr_GetUserContentStudyProgress";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StreamCode", SqlDbType.NVarChar),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            parms[1].Value = ResourceID;
            parms[2].Value = code;
            parms[3].Value = TrainingItemCourseID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    progress = PopulateSty_ContentStudyProgressDataReader(dataReader);
                }
            }
            return progress;
            //return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, commandName, parms).Tables[0];
        }

        public Sty_UserStudyProgress GetUserStudyProgress(Guid ResourceID, Guid TrainingItemCourseID, int UserID) {
            Sty_UserStudyProgress progress = null;
            string commandName = "Pr_GetUserStudyProgress";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = UserID;
            parms[1].Value = ResourceID;
            parms[2].Value = TrainingItemCourseID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    progress = PopulateSty_UserStudyProgressDataReader(dataReader);
                }
            }
            return progress;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="courseUserStudyProgress">业务实体</param>
		public void Insert(Sty_UserStudyProgress courseUserStudyProgress)
        {
            string commandName = "[dbo].[Pr_Sty_UserStudyProgress_Insert]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@UserStudyProgressID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.UserStudyProgressID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, courseUserStudyProgress.UserID),
                SqlHelper.CreateInputSqlParameter("@ChapterResourceID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.ChapterResourceID),
                SqlHelper.CreateInputSqlParameter("@StudyStatus", SqlDbType.SmallInt, courseUserStudyProgress.StudyStatus),
                SqlHelper.CreateInputSqlParameter("@StudyProgress", SqlDbType.Int, courseUserStudyProgress.StudyProgress, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, courseUserStudyProgress.CreateTime),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, courseUserStudyProgress.ModifyTime),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.TrainingItemCourseID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="courseUserStudyProgress">业务实体</param>
		public void Update(Sty_UserStudyProgress courseUserStudyProgress)
        {
            string commandName = "[dbo].[Pr_Sty_UserStudyProgress_Update]";
            SqlParameter[] parameters = new SqlParameter[]
            {
                SqlHelper.CreateInputSqlParameter("@UserStudyProgressID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.UserStudyProgressID),
                SqlHelper.CreateInputSqlParameter("@UserID", SqlDbType.Int, courseUserStudyProgress.UserID),
                SqlHelper.CreateInputSqlParameter("@ChapterResourceID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.ChapterResourceID),
                SqlHelper.CreateInputSqlParameter("@StudyStatus", SqlDbType.SmallInt, courseUserStudyProgress.StudyStatus),
                SqlHelper.CreateInputSqlParameter("@StudyProgress", SqlDbType.Int, courseUserStudyProgress.StudyProgress, true),
                SqlHelper.CreateInputSqlParameter("@CreateTime", SqlDbType.DateTime, courseUserStudyProgress.CreateTime),
                SqlHelper.CreateInputSqlParameter("@ModifyTime", SqlDbType.DateTime, courseUserStudyProgress.ModifyTime),
                SqlHelper.CreateInputSqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier, courseUserStudyProgress.TrainingItemCourseID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parameters);
        }
        /// <summary>
        /// 获取用户最后学习的资源信息
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public DataTable GetUserStudyLastContent(Guid TrainingItemCourseID, int UserID)
        {
            string commandName = "[dbo].[Pr_GetUserStudyLastContent]";
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
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        private Sty_UserStudyProgress PopulateSty_UserStudyProgressDataReader(SqlDataReader reader)
        {
            Sty_UserStudyProgress progress = new Sty_UserStudyProgress();
            int userStudyProgressIDIndex = reader.GetOrdinal("UserStudyProgressID");
            if (!reader.IsDBNull(userStudyProgressIDIndex))
            {
                progress.UserStudyProgressID = reader.GetGuid(userStudyProgressIDIndex);
            }
            int UserIDIndex = reader.GetOrdinal("UserID");
            if (!reader.IsDBNull(UserIDIndex))
            {
                progress.UserID = reader.GetInt32(UserIDIndex);
            }
            int ChapterResourceIDIndex = reader.GetOrdinal("ChapterResourceID");
            if (!reader.IsDBNull(ChapterResourceIDIndex))
            {
                progress.ChapterResourceID = reader.GetGuid(ChapterResourceIDIndex);
            }
            int StudyStatusIndex = reader.GetOrdinal("StudyStatus");
            if (!reader.IsDBNull(StudyStatusIndex))
            {
                progress.StudyStatus = reader.GetInt16(StudyStatusIndex);
            }
            int StudyProgressIndex = reader.GetOrdinal("StudyProgress");
            if (!reader.IsDBNull(StudyProgressIndex))
            {
                progress.StudyProgress = reader.GetInt32(StudyProgressIndex);
            }
            int StudyTimeIndex = reader.GetOrdinal("StudyTime");
            if (!reader.IsDBNull(StudyTimeIndex))
            {
                progress.StudyTime = reader.GetDecimal(StudyTimeIndex);
            }
            int CreateTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(CreateTimeIndex))
            {
                progress.CreateTime = reader.GetDateTime(CreateTimeIndex);
            }
            int ModifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(ModifyTimeIndex))
            {
                progress.ModifyTime = reader.GetDateTime(ModifyTimeIndex);
            }
            int TrainingItemCourseIDIndex = reader.GetOrdinal("TrainingItemCourseID");
            if (!reader.IsDBNull(TrainingItemCourseIDIndex))
            {
                progress.TrainingItemCourseID = reader.GetGuid(TrainingItemCourseIDIndex);
            }
            return progress;
        }

        private CourseContentStudyProgress PopulateSty_ContentStudyProgressDataReader(SqlDataReader reader)
        {
            CourseContentStudyProgress progress = new CourseContentStudyProgress();

            int contentIDIndex = reader.GetOrdinal("ContentID");
            if (!reader.IsDBNull(contentIDIndex))
            {
                progress.ContentID = reader.GetGuid(contentIDIndex);
            }

            int nameIndex = reader.GetOrdinal("Name");
            if (!reader.IsDBNull(nameIndex))
            {
                progress.Name = reader.GetString(nameIndex);
            }

            int typeIndex = reader.GetOrdinal("Type");
            if (!reader.IsDBNull(typeIndex))
            {
                progress.Type = reader.GetInt32(typeIndex);
            }

            int dataInfoIndex = reader.GetOrdinal("DataInfo");
            if (!reader.IsDBNull(dataInfoIndex))
            {
                progress.DataInfo = reader.GetString(dataInfoIndex);
            }

            int teacherNameIndex = reader.GetOrdinal("teacherName");
            if (!reader.IsDBNull(teacherNameIndex))
            {
                progress.TeacherName = reader.GetString(teacherNameIndex);
            }

            //int videoTimeIndex = reader.GetOrdinal("videoTime");
            //if (!reader.IsDBNull(videoTimeIndex))
            //{
            //    progress.VideoTime = reader.GetInt32(videoTimeIndex);
            //}

            int playTimeIndex = reader.GetOrdinal("PlayTime");
            if (!reader.IsDBNull(playTimeIndex))
            {
                progress.PlayTime = reader.GetInt32(playTimeIndex);
            }

            int studyStatusIndex = reader.GetOrdinal("StudyStatus");
            if (!reader.IsDBNull(studyStatusIndex))
            {
                progress.StudyStatus = reader.GetInt16(studyStatusIndex);
            }

            int studyProgressIndex = reader.GetOrdinal("StudyProgress");
            if (!reader.IsDBNull(studyProgressIndex))
            {
                progress.StudyProgress = reader.GetInt32(studyProgressIndex);
            }
            int thumbnailURLIndex = reader.GetOrdinal("ThumbnailURL");
            if (!reader.IsDBNull(thumbnailURLIndex))
            {
                progress.ThumbnailURL = reader.GetString(thumbnailURLIndex);
            }
            return progress;

        }
    }
}
