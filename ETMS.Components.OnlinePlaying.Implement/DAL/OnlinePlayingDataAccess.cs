using System;
using System.Data.SqlClient;
using System.Data;
using ETMS.Utility.Data;
using ETMS.Components.OnlinePlaying.API.Entity;

namespace ETMS.Components.OnlinePlaying.Implement.DAL
{
    public partial class OnlinePlayingDataAccess
    {
        /// <summary>
        /// 新增直播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateOnlinePlaying(Tr_CourseOnlinePlaying entity)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_insert";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OnlinePlayingID", SqlDbType.NVarChar),
					new SqlParameter("@PlayingNo", SqlDbType.NVarChar),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PlayingSubject", SqlDbType.NVarChar),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@PlayingTime", SqlDbType.Int),
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@TeacherName", SqlDbType.NVarChar),
                    new SqlParameter("@WindowSize", SqlDbType.Int),
                    new SqlParameter("@Skin", SqlDbType.NVarChar),
                    new SqlParameter("@OrganizerJoinUrl", SqlDbType.NVarChar),
                    new SqlParameter("@PanelistJoinUrl", SqlDbType.NVarChar),
                    new SqlParameter("@AttendeeJoinUrl", SqlDbType.NVarChar),
                    new SqlParameter("@OrganizerToken", SqlDbType.NVarChar),
                    new SqlParameter("@PanelistToken", SqlDbType.NVarChar),
                    new SqlParameter("@AttendeeToken", SqlDbType.NVarChar),
                    new SqlParameter("@OnlineStatus", SqlDbType.Bit),
                    new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.OnlinePlayingID;
            parms[1].Value = entity.PlayingNo;
            parms[2].Value = entity.TrainingItemCourseID;
            parms[3].Value = entity.PlayingSubject;
            parms[4].Value = entity.StartTime;
            parms[5].Value = entity.EndTime;
            parms[6].Value = entity.PlayingTime;
            parms[7].Value = entity.TeacherID;
            parms[8].Value = entity.TeacherName;
            parms[9].Value = entity.WindowSize;
            parms[10].Value = entity.Skin;
            parms[11].Value = entity.OrganizerJoinUrl;
            parms[12].Value = entity.PanelistJoinUrl;
            parms[13].Value = entity.AttendeeJoinUrl;
            parms[14].Value = entity.OrganizerToken;
            parms[15].Value = entity.PanelistToken;
            parms[16].Value = entity.AttendeeToken;
            parms[17].Value = entity.OnlineStatus;
            parms[18].Value = entity.Description;
            parms[19].Value = entity.CreateTime;
            parms[20].Value = entity.CreateUser;
            parms[21].Value = entity.CreateUserID;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 修改直播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateOnlinePlaying(Tr_CourseOnlinePlaying entity)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_Update";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OnlinePlayingID", SqlDbType.NVarChar),
               		new SqlParameter("@PlayingSubject", SqlDbType.NVarChar),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@PlayingTime", SqlDbType.Int),
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@TeacherName", SqlDbType.NVarChar),
                    new SqlParameter("@WindowSize", SqlDbType.Int),
                    new SqlParameter("@Skin", SqlDbType.NVarChar),                   
                    new SqlParameter("@OnlineStatus", SqlDbType.Bit),
                    new SqlParameter("@Description", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.OnlinePlayingID;
            parms[1].Value = entity.PlayingSubject;
            parms[2].Value = entity.StartTime;
            parms[3].Value = entity.EndTime;
            parms[4].Value = entity.PlayingTime;
            parms[5].Value = entity.TeacherID;
            parms[6].Value = entity.TeacherName;
            parms[7].Value = entity.WindowSize;
            parms[8].Value = entity.Skin;
            parms[9].Value = entity.OnlineStatus;
            parms[10].Value = entity.Description;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 删除直播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteOnlinePlaying(string onlinePllayingID)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_Delete";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OnlinePlayingID", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = onlinePllayingID;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 查询直播（根据直播ID）
        /// </summary>
        /// <param name="onlinePlayingID"></param>
        /// <returns></returns>
        public DataTable GetOnlinePlaying(string onlinePlayingID)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_Select";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OnlinePlayingID", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = onlinePlayingID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询直播（根据直播ID）
        /// </summary>
        /// <param name="onlinePlayingID"></param>
        /// <returns></returns>
        public DataTable QueryOnlinePlaying(OnlinePlayingInfo entity)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_GetByUser";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemName", SqlDbType.NVarChar)
                    ,new SqlParameter("@CourseName", SqlDbType.NVarChar)
                    ,new SqlParameter("@PlayingSubject", SqlDbType.NVarChar)
                    ,new SqlParameter("@StartTime", SqlDbType.DateTime)
                    ,new SqlParameter("@EndTime", SqlDbType.DateTime)
                    ,new SqlParameter("@OnlineStatus", SqlDbType.Bit)
                    ,new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.ItemName;
            parms[1].Value = entity.CourseName;
            parms[2].Value = entity.PlayingSubject;
            parms[3].Value = entity.StartTime;
            parms[4].Value = entity.EndTime;
            parms[5].Value = entity.OnlineStatus;
            parms[6].Value = entity.UserID;
            
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据项目课程ID查询直播
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetOnlinePlayingByItemCourseID(Guid trainingItemCourseID)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_GetByItemCourseID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询学员参与项目课程的所有有效的直播
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetOnlinePlayingByUserID(int userID)
        {
            string commandName = "Pr_Tr_ItemCourseOnlinePlaying_GetAllValid";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 学员进入直播后，记录一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateStudentOnlinePlaying(Sty_StudentOnlinePlaying entity)
        {
            string commandName = "Pr_Sty_StudentOnlinePlaying_insert";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OnlinePlayingID", SqlDbType.NVarChar),
					new SqlParameter("@StudentID", SqlDbType.Int),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),		
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.OnlinePlayingID;
            parms[1].Value = entity.StudentID;
            parms[2].Value = entity.JoinTime;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        #region 直播课程2017
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateLiving(Res_Living entity)
        {
            string commandName = "Pr_Res_Living_Insert";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@LivingID", SqlDbType.NVarChar),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@LivingName", SqlDbType.NVarChar),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@PartnerID", SqlDbType.NVarChar),
                    new SqlParameter("@BID", SqlDbType.NVarChar),
                    new SqlParameter("@AnchorKey", SqlDbType.NVarChar),
                    new SqlParameter("@AssistantKey", SqlDbType.NVarChar),
                    new SqlParameter("@StudentKey", SqlDbType.NVarChar),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar),
                    new SqlParameter("@CreateUserID", SqlDbType.Int),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar),
                    new SqlParameter("@LivingType", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.LivingID;
            parms[1].Value = entity.CourseID;
            parms[2].Value = entity.LivingName;
            parms[3].Value = entity.StartTime;
            parms[4].Value = entity.EndTime;
            parms[5].Value = entity.TeacherID;
            parms[6].Value = entity.PartnerID;
            parms[7].Value = entity.BID;
            parms[8].Value = entity.AnchorKey;
            parms[9].Value = entity.AssistantKey;
            parms[10].Value = entity.StudentKey;
            parms[11].Value = entity.OrgID;
            parms[12].Value = entity.CreateTime;
            parms[13].Value = entity.CreateUser;
            parms[14].Value = entity.CreateUserID;
            parms[15].Value = entity.ModifyTime;
            parms[16].Value = entity.ModifyUser;
            parms[17].Value = entity.LivingType;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public int UpdateLiving(Res_Living entity)
        {
            string commandName = "Pr_Res_Living_Update";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@LivingID", SqlDbType.NVarChar),
                    new SqlParameter("@LivingName", SqlDbType.NVarChar),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@ModifyTime", SqlDbType.DateTime),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = entity.LivingID;
            parms[1].Value = entity.LivingName;
            parms[2].Value = entity.StartTime;
            parms[3].Value = entity.EndTime;
            parms[4].Value = entity.TeacherID;            
            parms[5].Value = entity.ModifyTime;
            parms[6].Value = entity.ModifyUser;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public int DeleteLiving(string livingID)
        {
            string commandName = "Pr_Res_Living_Delete";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@LivingID", SqlDbType.NVarChar),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = livingID;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public int DeleteLivingByCourse(Guid courseID)
        {
            string commandName = "Pr_Res_Living_DeleteByCourseID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = courseID;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        public DataTable GetLiving(string livingID)
        {
            string commandName = "Pr_Res_Living_Get";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@LivingID", SqlDbType.NVarChar)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = livingID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetLivingPageList(Guid courseID)
        {
            string commandName = "Pr_Res_Living_PageList";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = courseID;
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetLivingsByTeacher(int teacherID, DateTime startTime, DateTime endTime, string livingName, string courseName, int livingType)
        {
            string commandName = "Pr_Res_Living_ByTeacher";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TeacherID", SqlDbType.Int),
                    new SqlParameter("@StartTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@LivingName", SqlDbType.NVarChar),
                    new SqlParameter("@CourseName", SqlDbType.NVarChar),
                    new SqlParameter("@LivingType", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = teacherID;
            parms[1].Value = startTime;
            parms[2].Value = endTime;
            parms[3].Value = livingName;
            parms[4].Value = courseName;
            parms[5].Value = livingType;

            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetNowValidLivings(int orgID)
        {
            string commandName = @"SELECT [LivingID]
      ,A.[CourseID]
      ,[LivingName]
      ,[StartTime]
      ,[EndTime]
      ,[TeacherID]
      ,[PartnerID]
      ,[BID]
      ,[AnchorKey]
      ,[AssistantKey]
      ,[StudentKey]
      ,A.[OrgID]
      ,A.[CreateTime]
      ,A.[CreateUserID]
      ,A.[CreateUser]
      ,A.[ModifyTime]
      ,A.[ModifyUser]
	  ,B.RealName AS TeacherName
	  ,C.CourseName
	  ,C.ThumbnailURL
  FROM [dbo].[Res_Living] A
  INNER JOIN dbo.Res_Course C ON A.CourseID = C.CourseID
  LEFT JOIN dbo.Site_User B ON a.TeacherID = B.UserID  
  WHERE A.StartTime <= GETDATE() and A.EndTime >= GETDATE()
	AND	a.OrgID = @OrgID
	AND C.IsPay = 0
  ORDER BY StartTime DESC";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }
        
        public DataTable GetIndexLivings(int orgID)
        {
            string commandName = @"SELECT TOP 20 B.CourseID, B.CourseName, B.ThumbnailURL,ISNULL(B.FocusCount,0) FocusCount,B.CourseHours,
		                                C.StartDate, C.EndDate, C.TeacherInfo As TeacherName, A.Sort,B.CourseModel
	                                FROM dbo.Recommend_Course A
	                                INNER JOIN dbo.Res_Course B ON B.CourseID = A.CourseID
	                                INNER JOIN dbo.Res_CourseExtensiton C ON C.ExtentionID = B.CourseID
	                                WHERE B.CourseModel = 2 AND A.OrgID = @OrgID
	                                ORDER BY A.Sort";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
            return dt;
        }
        public DataTable GetValidLivings(int orgID)
        {
            string commandName = "Pr_Res_Living_ValidLivings";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetAllLivings(int orgID)
        {
            string commandName = "Pr_Res_Living_AllLivings";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@OrgID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        public DataTable GetHistoryLivings(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            string commandName = "dbo.Pr_Res_Living_HistoryLivings";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@OrgID", SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = orgID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[3].Value;
            return dt;
        }

        /// <summary>
        /// 查询所有精品课程
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable GetCompetitiveCourses(int orgID)
        {
            string commandName = @"SELECT A.CourseID, A.CourseName, A.Price, A.DiscountPrice, A.ForObject, A.CourseHours, A.ThumbnailURL
                                    FROM Res_Course A
                                    WHERE A.CourseStatus = 1
	                                    AND A.IsPay = 1
	                                    AND A.OrgID = @OrgID
                                    ORDER BY A.Sorting DESC";

            SqlParameter[] parms =new SqlParameter[] { new SqlParameter("@OrgID", SqlDbType.Int)};
                
            parms[0].Value = orgID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
        }

        public DataTable GetCompetitiveCourse(Guid courseID)
        {
            string commandName = @"SELECT A.CourseID, A.CourseName, A.Price, A.DiscountPrice, A.ForObject, A.CourseHours, A.ThumbnailURL, A.CourseOutline, A.CourseIntroduction
                                    FROM Res_Course A
                                    WHERE A.CourseID = @CourseID";

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier) };

            parms[0].Value = courseID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
        }

        public DataTable GetSignupNum(Guid courseID)
        {
            string commandName = @"SELECT COUNT(1) AS Num FROM dbo.Sty_StudentLiving WHERE CourseID = @CourseID";

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier) };

            parms[0].Value = courseID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
        }

        public DataTable GetMyCompetitiveCourse(int userID)
        {
            string commandName = @"SELECT A.CourseID, A.CourseName, A.Price, A.DiscountPrice, A.ForObject, A.CourseHours, A.ThumbnailURL, A.CourseOutline, A.CourseIntroduction
                                    FROM Res_Course A
									INNER JOIN dbo.Sty_StudentLiving B ON a.CourseID = B.CourseID
                                    WHERE B.UserID = @UserID
                                    ORDER BY B.SignupTime DESC";

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) };

            parms[0].Value = userID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
        }

        public int CreateUserLiving(string userID, string livingID)
        {
            string commandName = @"DECLARE @Count INT
                                    SELECT @Count=COUNT(1) FROM Sty_UserLiving
                                    WHERE UserID=@UserID
	                                    AND LivingID = @LivingID

                                    IF (@Count = 0)
                                    BEGIN
	                                    INSERT INTO Sty_UserLiving
	                                            ( UserLivingID ,
	                                              UserID ,
	                                              LivingID ,
	                                              CreateTime
	                                            )
	                                    VALUES  ( NEWID(),
	                                              @UserID,
	                                              @LivingID,
	                                              GETDATE()
	                                            )
                                    END";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.NVarChar),
                    new SqlParameter("@LivingID", SqlDbType.NVarChar)
                };

            parms[0].Value = userID;
            parms[1].Value = livingID;

            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, commandName, parms);
        }

        public DataTable GetLivingUserCount(string livingID)
        {
            string sql = @"SELECT COUNT(1) AS RowNum FROM Sty_UserLiving WHERE LivingID = @LivingID";

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@LivingID", SqlDbType.NVarChar) };

            parms[0].Value = livingID;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetStyLivingsByUserCourse(int userID, Guid trainingItemCourseID)
        {
            string commandName = @"SELECT A.*
                                        FROM dbo.Sty_UserLiving A
                                        INNER JOIN dbo.Res_Living B ON A.LivingID = B.LivingID
                                        INNER JOIN dbo.Tr_ItemCourse C ON C.CourseID = B.CourseID 
                                        WHERE A.UserID = @UserID AND C.TrainingItemCourseID = @TrainingItemCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.NVarChar),
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = trainingItemCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, commandName, parms).Tables[0];
            return dt;
        }
        #endregion
    }
}
