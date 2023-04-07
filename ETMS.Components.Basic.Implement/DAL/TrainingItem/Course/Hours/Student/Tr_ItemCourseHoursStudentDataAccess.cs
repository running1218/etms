using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;


namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Hours.Student
{
    /// <summary>
    /// 培训项目课程课时学员扩展类
    /// 黄中福：2012－04－24
    /// </summary>
    public partial class Tr_ItemCourseHoursStudentDataAccess
    {

        /// <summary>
        /// 某个学员的的所有课时安排列表 
        /// 	FROM Tr_ItemCourseHoursStudent a
        /// 	INNER JOIN Tr_ItemCourseHours b on b.ItemCourseHoursID = a.ItemCourseHoursID
        /// 	INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = b.TrainingItemCourseID
        /// 	INNER JOIN Tr_Item d on d.TrainingItemID = c.TrainingItemID
        /// 	INNER JOIN Res_Course e on e.CourseID = c.CourseID
        /// 	INNER JOIN Sty_StudentCourse f on f.StudentCourse = a.StudentCourse
        /// 	INNER JOIN Sty_StudentSignup g on g.StudentSignupID = f.StudentSignupID
        /// 	INNER JOIN vw_ValidStudent h on h.UserID=g.UserID
        /// 	WHERE 1=1 AND g.UserID = @StudentID
        /// 	ORDER BY b.CreateTime desc,d.ItemName,e.CourseName
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseHoursByStudentID(int studentID)
        {

            string commandName = "dbo.[Pr_Tr_ItemCourseHoursStudent_GetByStudentID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StudentID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }




        /// <summary>
        /// 某个培训项目课程课时安排的学员列表
        /// 	FROM Tr_ItemCourseHoursStudent a
        /// 	INNER JOIN Tr_ItemCourseHours b on b.ItemCourseHoursID = a.ItemCourseHoursID
        /// 	INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = b.TrainingItemCourseID
        /// 	INNER JOIN Tr_Item d on d.TrainingItemID = c.TrainingItemID
        /// 	INNER JOIN Res_Course e on e.CourseID = c.CourseID
        /// 	INNER JOIN Sty_StudentCourse f on f.StudentCourse = a.StudentCourse
        /// 	INNER JOIN Sty_StudentSignup g on g.StudentSignupID = f.StudentSignupID
        /// 	INNER JOIN vw_ValidStudent h on h.UserID=g.UserID
        /// 	WHERE 1=1 AND g.UserID = @StudentID
        /// 	ORDER BY b.CreateTime desc,d.ItemName,e.CourseName,h.RealName
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时安排ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseHoursStudentByItemCourseHoursID(Guid itemCourseHoursID)
        {

            string commandName = "dbo.[Pr_Tr_ItemCourseHoursStudent_GetByItemCourseHoursID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemCourseHoursID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }





        /// <summary>
        /// 查询所有的培训项目课程课时学员
        /// FROM Tr_ItemCourseHoursStudent a
        /// INNER JOIN Tr_ItemCourseHours b on b.ItemCourseHoursID = a.ItemCourseHoursID
        /// INNER JOIN Site_Teacher t on t.TeacherID = b.TeacherID
        /// INNER JOIN Site_User tu on tu.UserID = t.TeacherID
        /// INNER JOIN Res_ClassRoom cr on cr.ClassRoomID = b.ClassRoomID
        /// INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = b.TrainingItemCourseID
        /// INNER JOIN Tr_Item d on d.TrainingItemID = c.TrainingItemID
        /// INNER JOIN Res_Course e on e.CourseID = c.CourseID
        /// INNER JOIN Sty_StudentCourse f on f.StudentCourse = a.StudentCourse
        /// INNER JOIN Sty_StudentSignup g on g.StudentSignupID = f.StudentSignupID
        /// INNER JOIN Site_User u on u.UserID = g.UserID
        /// INNER JOIN Site_Student s on s.UserID = u.UserID
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetItemCourseHoursStudentALLInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourseHoursStudent_GetALLInfoList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;
        }



        /// <summary>
        /// 某个培训项目课程课时安排的学员数
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时安排ID</param>
        /// <returns></returns>
        public int GetItemCourseHoursStudentNumByItemCourseHoursID(Guid itemCourseHoursID)
        {
            string sqlModal = @"select COUNT(*) num from Tr_ItemCourseHoursStudent a
                                where a.ItemCourseHoursID = '{0}' ";
            string commandName = string.Format(sqlModal, itemCourseHoursID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




        #region 学员请假


        /// <summary>
        /// 获取某个学员的的所有课时安排列表 ,为学员请假用
        /// 	FROM Tr_ItemCourseHoursStudent a
        /// 	INNER JOIN Tr_ItemCourseHours b on b.ItemCourseHoursID = a.ItemCourseHoursID
        /// 	INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = b.TrainingItemCourseID
        /// 	INNER JOIN Tr_Item d on d.TrainingItemID = c.TrainingItemID
        /// 	INNER JOIN Res_Course e on e.CourseID = c.CourseID
        /// 	INNER JOIN Sty_StudentCourse f on f.StudentCourse = a.StudentCourse
        /// 	INNER JOIN Sty_StudentSignup g on g.StudentSignupID = f.StudentSignupID
        /// 	INNER JOIN vw_ValidStudent h on h.UserID=g.UserID
        /// 	WHERE 1=1 AND g.UserID = @StudentID
        /// 	ORDER BY b.CreateTime desc,d.ItemName,e.CourseName
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseHoursByStudentIDToLeave(int studentID)
        {

            string commandName = "dbo.[Pr_Tr_ItemCourseHours_GetByStudentID_ToLeave]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@StudentID", SqlDbType.Int),
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = studentID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }


        /// <summary>
        /// 学员课时请假申请
        /// </summary>
        /// <param name="itemCourseHoursStudentID">培训项目课程课时学员ID</param>
        /// <param name="leaveReason">请假原因</param>
        public void ItemCourseHoursStudent_Leave(Guid itemCourseHoursStudentID, string leaveReason)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseHoursStudent_Leave]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@LeaveReason", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = itemCourseHoursStudentID;
            parms[1].Value = leaveReason;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 取消学员课时请假申请
        /// </summary>
        /// <param name="itemCourseHoursStudentID">培训项目课程课时学员ID</param>
        public void ItemCourseHoursStudent_LeaveCancel(Guid itemCourseHoursStudentID)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseHoursStudent_LeaveCancel]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = itemCourseHoursStudentID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }




        /// <summary>
        /// 审核某个学员的课时请假
        /// </summary>
        /// <param name="itemCourseHoursStudentID">培训项目课程课时学员ID</param>
        /// <param name="auditStatus">审核结果（20：审核通过，40：审核不通过）</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void ItemCourseHoursStudent_LeaveAudit(Guid itemCourseHoursStudentID, int auditStatus, string auditUser, string auditOpinion)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseHoursStudent_LeaveAudit]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@AuditStatus", SqlDbType.Int),
                    new SqlParameter("@AuditUser", SqlDbType.NVarChar),
                    new SqlParameter("@AuditOpinion", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = itemCourseHoursStudentID;
            parms[1].Value = auditStatus;
            parms[2].Value = auditUser;
            parms[3].Value = auditOpinion;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }




        #endregion



        #region 学员签到

        /// <summary>
        /// 某个培训项目课程课时的学员统一签到为“正常签到”
        /// 如果有请假则不给他签到
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时ID</param>
        /// <param name="modifyUser">操作员</param>
        /// <returns>统一签到的人数</returns>
        public int ItemCourseHoursStudent_SigninALL(Guid itemCourseHoursID, string modifyUser)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourseHoursStudent_Signin]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@modifyUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemCourseHoursID;
            parms[1].Value = modifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
            return (int)parms[2].Value;
        }


        /// <summary>
        /// 全部取消某个培训项目课程课时的学员签到
        /// 如果有请假则不给他取消签到
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时ID</param>
        /// <param name="modifyUser">操作员</param>
        /// <returns>统一取消签到的人数</returns>
        public int ItemCourseHoursStudent_CancelSigninALL(Guid itemCourseHoursID, string modifyUser)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourseHoursStudent_CancelSignin]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@modifyUser", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemCourseHoursID;
            parms[1].Value = modifyUser;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
            return (int)parms[2].Value;
        }





        #endregion



    }
}
