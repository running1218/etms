using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Hours
{
    /// <summary>
    /// 培训项目课程课时扩展类
    /// 黄中福2012－04－22
    /// </summary>
    public partial class Tr_ItemCourseHoursDataAccess
    {

        /// <summary>
        /// 根据培训项目课程ID获取其课时总和
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回:课时总和</returns>
        public int GetItemCourseHourseTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select count(*) num from Tr_ItemCourseHours a where a.TrainingItemCourseID='{0}'", trainingItemCourseID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 根据培训项目课程ID获取其课时总和
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回:课时总和</returns>
        public decimal GetItemCourseHourseSumTotal(Guid trainingItemCourseID)
        {
            string commandName = string.Format("select isnull(SUM(a.CourseHours),0) sumHours from Tr_ItemCourseHours a where a.TrainingItemCourseID='{0}'", trainingItemCourseID);
            return (decimal)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 验证某个教室是否已经被别的课时使用:“添加”时候使用
        /// </summary>
        /// <param name="classRoomID">教室ID</param>
        /// <param name="beginTime">时间</param>
        /// <returns></returns>
        public bool CheckHoursIsUsedByClassRoom(Guid classRoomID, DateTime beginTime)
        {
            bool isUsed = false;
            string sqlModal = @"select COUNT(*) num from Tr_ItemCourseHours a
                                where a.ClassRoomID ='{0}'
                                AND '{1}' between a.TrainingBeginTime AND a.TrainingEndTime";
            string commandName = string.Format(sqlModal, classRoomID, beginTime.AddMinutes(1));
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (count > 0)
                isUsed = true;
            return isUsed;
        }


        /// <summary>
        /// 验证某个教室是否已经被别的课时使用:“修改”时候使用
        /// </summary>
        /// <param name="itemCourseHoursID">当前修改的课时ID</param>
        /// <param name="classRoomID">教室ID</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public bool CheckHoursIsUsedByClassRoom(Guid itemCourseHoursID, Guid classRoomID, DateTime beginTime, DateTime endTime)
        {
            bool isUsed = false;
            string sqlModal = @"select COUNT(*) num from Tr_ItemCourseHours a
                                where a.ItemCourseHoursID != '{0}' AND a.ClassRoomID ='{1}' 
                                AND ('{2}' between a.TrainingBeginTime AND a.TrainingEndTime OR '{3}' between a.TrainingBeginTime AND a.TrainingEndTime)";
            string commandName = string.Format(sqlModal, itemCourseHoursID, classRoomID, beginTime.AddMinutes(1), endTime.AddMinutes(-1));
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (count > 0)
                isUsed = true;
            return isUsed;
        }



        /// <summary>
        /// 验证某个讲师是否已经被别的课时使用:“添加”时候使用
        /// </summary>
        /// <param name="teacherID">讲师ID</param>
        /// <param name="beginTime">时间</param>
        /// <returns></returns>
        public bool CheckHoursIsUsedByTeacher(int teacherID, DateTime beginTime)
        {
            bool isUsed = false;
            string sqlModal = @"select COUNT(*) num from Tr_ItemCourseHours a
                                where a.TeacherID ='{0}'
                                AND '{1}' between a.TrainingBeginTime AND a.TrainingEndTime";
            string commandName = string.Format(sqlModal, teacherID, beginTime.AddMinutes(1));
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (count > 0)
                isUsed = true;
            return isUsed;
        }

        /// <summary>
        /// 验证某个讲师是否已经被别的课时使用:“修改”时候使用
        /// </summary>
        /// <param name="itemCourseHoursID">当前修改的课时ID</param>
        /// <param name="teacherID">讲师ID</param>
        /// <param name="beginTime">时间</param>
        /// <returns></returns>
        public bool CheckHoursIsUsedByTeacher(Guid itemCourseHoursID, int teacherID, DateTime beginTime, DateTime endTime)
        {
            bool isUsed = false;
            string sqlModal = @"select COUNT(*) num from Tr_ItemCourseHours a
                                where a.ItemCourseHoursID != '{0}' AND a.TeacherID ='{1}' 
                                AND ('{2}' between a.TrainingBeginTime AND a.TrainingEndTime OR '{3}' between a.TrainingBeginTime AND a.TrainingEndTime)";
            string commandName = string.Format(sqlModal, itemCourseHoursID, teacherID, beginTime.AddMinutes(1), endTime.AddMinutes(-1));
            int count = (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
            if (count > 0)
                isUsed = true;
            return isUsed;
        }





        /// <summary>
        /// 获取培训项目课程的课时安排列表
        ///FROM Tr_ItemCourseHours a 
        /// INNER JOIN Res_ClassRoom b on b.ClassRoomID = a.ClassRoomID
        /// INNER JOIN Site_Teacher c on c.TeacherID = a.TeacherID
        /// INNER JOIN Site_User d on d.UserID = c.TeacherID
        /// INNER JOIN Tr_ItemCourse e on e.TrainingItemCourseID = a.TrainingItemCourseID
        /// INNER JOIN Tr_Item f on f.TrainingItemID = e.TrainingItemID
        /// INNER JOIN Res_Course g on g.CourseID = e.CourseID
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetItemCourseHoursALLInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourseHours_GetALLInfoList]";
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
        /// 获取培训项目课程课时的没有选择的学员列表
        /// 	from dbo.Sty_StudentCourse a 
        /// 	inner join dbo.Sty_StudentSignup b on a.StudentSignupID=b.StudentSignupID
        /// 	inner join dbo.vw_ValidStudent u on u.UserID=b.UserID
        /// 	left join Sty_ClassStudent c on c.StudentSignupID = b.StudentSignupID
        /// 	left join Sty_Class d on d.ClassID = c.ClassID
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetItemCourseHours_GetNoSelectStudentLis(Guid itemCourseHoursID,int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "[dbo].[Pr_Tr_ItemCourseHours_GetNoSelectStudentList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = itemCourseHoursID;
            parms[1].Value = pageIndex;
            parms[2].Value = pageSize;
            parms[3].Value = sortExpression;
            parms[4].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[5].Value;
            return dt;
        }




        /// <summary>
        /// 审核某个课时的状态，即“课时执行结果”
        /// </summary>
        /// <param name="itemCourseHoursID">培训项目课程课时ID</param>
        /// <param name="courseHoursStatusID">课时状态（执行结果）（0：未设置，1：已执行，2：未执行）</param>
        /// <param name="modifyUser">操作人</param>
        /// <param name="courseHoursStatusDesc">课时执行说明</param>
        public void ItemCourseHours_HoursAudit(Guid itemCourseHoursID, int courseHoursStatusID, string modifyUser, string courseHoursStatusDesc)
        {
            string commandName = "dbo.[Pr_Tr_ItemCourseHours_HoursAudit]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@CourseHoursStatusID", SqlDbType.Int),
                    new SqlParameter("@ModifyUser", SqlDbType.NVarChar),
                    new SqlParameter("@CourseHoursStatusDesc", SqlDbType.NVarChar)
                    };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = itemCourseHoursID;
            parms[1].Value = courseHoursStatusID;
            parms[2].Value = modifyUser;
            parms[3].Value = courseHoursStatusDesc;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }






    }
}
