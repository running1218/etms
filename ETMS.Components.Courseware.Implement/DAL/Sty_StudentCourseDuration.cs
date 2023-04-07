using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ETMS.Utility.Data;
using ETMS.Components.Courseware.API.Entity;

namespace ETMS.Components.Courseware.Implement.DAL
{
    public class Sty_StudentCourseDuration
    {

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Sty_StudentCourseDurationModel courseDurationModel)
        {
            string commandName = "[dbo].[Pr_Sty_StudentCourseDuration_Insert]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@StudentCourseDurationID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@CourseWareID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CourseResTypeID", SqlDbType.Int,4),
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier,32),
                    new SqlParameter("@NumCount", SqlDbType.Int,4),
                    new SqlParameter("@CountTime ", SqlDbType.Int,4),

                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = courseDurationModel.StudentCourseDurationID;
            parms[1].Value = courseDurationModel.ResourceID;
            parms[2].Value = courseDurationModel.ItemCourseResID;
            parms[3].Value = courseDurationModel.UserID;
            parms[4].Value = courseDurationModel.CreateTime;
            parms[5].Value = courseDurationModel.CourseResTypeID;
            parms[6].Value = courseDurationModel.CourseID;
            parms[7].Value = courseDurationModel.NumCount;
            parms[8].Value = courseDurationModel.CountTime;

            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }


        /// <summary>
        /// 课件插入数据和修改时间
        /// </summary>
        public void SetSessionTime(Guid ResourceID, Guid ItemCourseResID, Guid CourseWareID, int SessionTime, DateTime EndTime, int UserID)
        {
            string commandName = "[dbo].[PR_Scorm_SetSessionTime]";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ResourceID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@CourseWareID", SqlDbType.UniqueIdentifier,32),
					new SqlParameter("@SessionTime", SqlDbType.Int,4),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.Int,4)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = ResourceID;
            parms[1].Value = ItemCourseResID;
            parms[2].Value = CourseWareID;
            parms[3].Value = SessionTime;
            parms[4].Value = EndTime;
            parms[5].Value = UserID;
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }




        /// <summary>
        /// 查询用户是浏览过此课件
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public string GetStudentCourseDuration(string UserID, string ResourceID, string ItemCourseResID)
        {
            string sql = string.Format("select StudentCourseDurationID  from dbo.Sty_StudentCourseDuration where userid={0} and CourseWareID='{1}' and  ItemCourseResID='{2}'", UserID, ResourceID, ItemCourseResID);
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["StudentCourseDurationID"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        ///根据StudentCourseDurationID获取SCORE详情ID
        /// </summary> 
        public string GetScoreID(string StudentCourseDurationID)
        {
            string sql = string.Format("SELECT TOP 1 ID FROM dbo.Sty_StudentCourseDurationInfo WHERE StudentCourseDurationID='{0}' ORDER BY id desc", StudentCourseDurationID);
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql).Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["ID"].ToString();
                }
            }
            return null;
        }


        /// <summary>
        /// 插入课件详情数据
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public decimal InsertStudentCourseDurationInfo(string StartBeginTime, string EndTime, string SessionTime, string StudentCourseDurationID)
        {
            string sql = string.Format("INSERT INTO [dbo].[Sty_StudentCourseDurationInfo]([StartBeginTime],[EndTime],[SessionTime],CreateTime,StudentCourseDurationID)VALUES('{0}','{1}','{2}',getdate(),'{3}');Select @@Identity", StartBeginTime, EndTime, SessionTime, StudentCourseDurationID);
            return (decimal)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, sql);
        }

        /// <summary>
        /// 修改结束时间
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public int UpdateStudentCourseDurationInfo(string EndTime, string SessionTime, string ID)
        {
            string sql = string.Format("UPDATE [dbo].[Sty_StudentCourseDurationInfo] set [EndTime] = '{0}',[SessionTime] ='{1}' where ID='{2}'", EndTime, SessionTime, ID);
            return (int)SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.Text, sql);
        }
        /// <summary>
        ///修改浏览次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public int UpdateStudentCourseDurationCount(string CountTime, string StudentCourseDurationID)
        {
            string sql = string.Format("update [Sty_StudentCourseDuration] set NumCount=NumCount+1 where StudentCourseDurationID='{1}'", CountTime, StudentCourseDurationID);
            return (int)SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.Text, sql);
        }

        /// <summary>
        ///修改浏览次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public void UpdateStudentCourseDurationCount(string CountTime, string StudentCourseDurationID, int type)
        {
            string sql = string.Format("update [Sty_StudentCourseDuration] set CountTime+={0} where StudentCourseDurationID='{1}'", CountTime, StudentCourseDurationID);
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.Text, sql);
        }

        /// <summary>
        /// 增加一条笔记数据
        /// </summary>
        public int AddSty_StudentNotes(Sty_StudentNotes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sty_StudentNotes(");
            strSql.Append("NotesID,NotesContent,UserID,RealName,ResourceID,CourseWareID,ItemCourseResID,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@NotesID,@NotesContent,@UserID,@RealName,@ResourceID,@CourseWareID,@ItemCourseResID,@CreateTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@NotesID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@NotesContent", SqlDbType.Text),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RealName", SqlDbType.VarChar,250),
					new SqlParameter("@ResourceID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CourseWareID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.NotesID;
            parameters[1].Value = model.NotesContent;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.RealName;
            parameters[4].Value = model.ResourceID;
            parameters[5].Value = model.CourseWareID;
            parameters[6].Value = model.ItemCourseResID;
            parameters[7].Value = model.CreateTime;

            object obj = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 增加一条讨论数据
        /// </summary>
        public int AddSty_StudentTalk(Sty_StudentTalk model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sty_StudentTalk(");
            strSql.Append("TalkID,UserID,RealName,TalkContent,ResourceID,CourseWareID,ItemCourseResID,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@TalkID,@UserID,@RealName,@TalkContent,@ResourceID,@CourseWareID,@ItemCourseResID,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@TalkID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RealName", SqlDbType.VarChar,250),
					new SqlParameter("@TalkContent", SqlDbType.Text),
					new SqlParameter("@ResourceID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CourseWareID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ItemCourseResID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.TalkID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.RealName;
            parameters[3].Value = model.TalkContent;
            parameters[4].Value = model.ResourceID;
            parameters[5].Value = model.CourseWareID;
            parameters[6].Value = model.ItemCourseResID;
            parameters[7].Value = model.CreateTime;
            object obj = SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        ///根据用户ID和资源ID获取同学讨论的记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable Pr_GetSty_studentTalk(int pageIndex, int pageSize, string sortExpression, string UserID, string CourseWareID, out int totalRecords)
        {
            string commandName = "dbo.Pr_GetSty_studentTalk";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@pageIndex", SqlDbType.Int),
					new SqlParameter("@pageSize", SqlDbType.Int),
					new SqlParameter("@sortExpression", SqlDbType.VarChar), 
					new SqlParameter("@CourseWareID", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = UserID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            parms[4].Value = CourseWareID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }


        /// <summary>
        ///根据用户ID和资源ID获取同学笔记的记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable Pr_GetSty_StudentNotes(int pageIndex, int pageSize, string sortExpression, string UserID, string ResourceID, out int totalRecords)
        {
            string commandName = "dbo.Pr_GetSty_StudentNotes";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@pageIndex", SqlDbType.Int),
					new SqlParameter("@pageSize", SqlDbType.Int),
					new SqlParameter("@sortExpression", SqlDbType.VarChar), 
					new SqlParameter("@CourseWareID", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = UserID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            parms[4].Value = ResourceID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }
    }
}
