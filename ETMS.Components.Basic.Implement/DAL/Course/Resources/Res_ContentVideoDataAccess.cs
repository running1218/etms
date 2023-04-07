using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Basic.Implement.DAL.Course.Resources
{
    public partial class Res_ContentVideoDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="resContent">业务实体</param>
        //public void Add(Res_ContentVideo resContentVideo)
        //{
        //    string commandName = "dbo.Pr_Res_ContentVideo_Insert";
        //    #region Parameters
        //    SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
        //    if (parms == null)
        //    {
        //        parms = new SqlParameter[] {
        //                  new SqlParameter("@ContentVideoID", SqlDbType.UniqueIdentifier),
        //                  new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
        //                  new SqlParameter("@StreamCode", SqlDbType.NVarChar),
        //                  new SqlParameter("@DataInfo", SqlDbType.NVarChar),
        //                  new SqlParameter("@CreateTime", SqlDbType.DateTime)
        //              };
        //        SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
        //    }
        //    parms[0].Value = resContentVideo.ContentVideoID;
        //    parms[1].Value = resContentVideo.ContentID;
        //    parms[2].Value = resContentVideo.StreamCode;
        //    parms[3].Value = resContentVideo.DataInfo;
        //    parms[4].Value = resContentVideo.CreateTime;
        //    #endregion
        //    SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        //}

        public void Insert(Transcoding transcoding)
        {
            string commandName = "dbo.Pr_Res_ContentVideo_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TranscodingQueueID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Duration", SqlDbType.Int),
                    new SqlParameter("@Outpath", SqlDbType.NVarChar)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = transcoding.TaskID;
            parms[1].Value = transcoding.Duration;
            parms[2].Value = transcoding.Outpath;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 查询指定编码的视频，未转码完成，返回原视频地址
        /// </summary>
        /// <param name="ContentID"></param>
        /// <param name="streamCode"></param>
        /// <returns></returns>
        public ResContent GetTranscodingById(Guid ContentID,string streamCode) {
            ResContent res_Content = null;

            string commandName = "dbo.Pr_Res_Content_Transcoding_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StreamCode", SqlDbType.NVarChar)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = ContentID;
            parms[1].Value = streamCode;
            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    res_Content = PopulateRes_ContentResFromDataReader(dataReader);
                }
            }

            return res_Content;
        }

        public DataTable GetResourceByCourse(Guid trainingItemCourseID,int userID)
        {
            string commandName = "dbo.Pr_GetResourceByCourse";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = userID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据课程ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        public DataTable GetResourceByCourseID(Guid CourseID)
        {
            string commandName = "dbo.Pr_GetResourceByCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据课程ID和资源ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="ContentID"></param> 
        /// <returns></returns>
        public DataTable GetResourceByCourseID(Guid CourseID,Guid ContentID)
        {
            string commandName = "dbo.Pr_GetResourceByContentID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                     new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier)
                     
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseID;
            parms[1].Value = ContentID;
         
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据课程ID和资源ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="ContentID"></param> 
        /// <returns></returns>
        public DataTable GetResourceByCourseAndContentID(Guid CourseID, Guid ContentID,string Code)
        {
            string commandName = "dbo.Pr_GetResourceByCourseAndContentID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                     new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                     new SqlParameter("@StreamCode", SqlDbType.NVarChar)

                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseID;
            parms[1].Value = ContentID;
            parms[2].Value = Code;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public int GetContentVideoTotalByCourse(Guid trainingItemCourseID) {
            string commandName = "dbo.Pr_GetContentVideoTotalByCourse";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0]["VideoTime"] == DBNull.Value?0: Convert.ToInt32(dt.Rows[0]["VideoTime"].ToString());
            }
            else {
                return 0;
            }
        }

        /// <summary>
        /// 学生一门课总学习时长
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetStudentStudyTimeByCourse(Guid trainingItemCourseID,int userID)
        {
            string commandName = "dbo.Pr_GetStudentStudyTimeByCourse";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@UserID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = userID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            if (dt.Rows.Count == 1)
            {
                return Convert.ToInt32(Math.Round(Convert.ToDouble(dt.Rows[0]["StudyTime"]),0));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private ResContent PopulateRes_ContentResFromDataReader(SqlDataReader reader)
        {
            ResContent rec_Course = new ResContent();

            int courseIDIndex = reader.GetOrdinal("ContentID");
            if (!reader.IsDBNull(courseIDIndex))
            {
                rec_Course.ContentID = reader.GetGuid(courseIDIndex);
            }
            int nameIndex = reader.GetOrdinal("Name");
            if (!reader.IsDBNull(nameIndex))
            {
                rec_Course.Name = reader.GetString(nameIndex);
            }

            int typeIndex = reader.GetOrdinal("Type");
            if (!reader.IsDBNull(typeIndex))
            {
                rec_Course.Type = reader.GetInt32(typeIndex);
            }

            int dataInfoIndex = reader.GetOrdinal("DataInfo");
            if (!reader.IsDBNull(dataInfoIndex))
            {
                rec_Course.DataInfo = reader.GetString(dataInfoIndex);
            }

            int teacherNameIndex = reader.GetOrdinal("TeacherName");
            if (!reader.IsDBNull(teacherNameIndex))
            {
                rec_Course.TeacherName = reader.GetString(teacherNameIndex);
            }

            int videoTimeIndex = reader.GetOrdinal("PlayTime");
            if (!reader.IsDBNull(videoTimeIndex))
            {
                rec_Course.PlayTime = reader.GetInt32(videoTimeIndex);
            }

            int remarkIndex = reader.GetOrdinal("Remark");
            if (!reader.IsDBNull(remarkIndex))
            {
                rec_Course.Remark = reader.GetString(remarkIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                rec_Course.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int modifyTimeIndex = reader.GetOrdinal("ModifyTime");
            if (!reader.IsDBNull(modifyTimeIndex))
            {
                rec_Course.ModifyTime = reader.GetDateTime(modifyTimeIndex);
            }

            return rec_Course;
        }
    }
}
