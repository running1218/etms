using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;


namespace ETMS.Components.Point.Implement.DAL
{
    /// <summary>
    /// 学生积分明细扩展类
    /// 黄中福：2012－05－17
    /// </summary>
    public partial class Point_Student_IssueDetailDataAccess
    {

        /// <summary>
        /// 统计学员所有已经发布的积分明细列表
        /// FROM Point_Student_IssueDetail a
        ///     INNER JOIN Site_User u on u.UserID=a.StudentID
        ///     INNER JOIN Site_Student s on s.UserID=u.UserID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable StatStudentPointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_IssueDetail_StatStudentPointAllInfoList]";
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
        /// 获取所有学员已经发布的积分明细列表
        /// FROM Point_Student_IssueDetail a
        ///     INNER JOIN Site_User u on u.UserID=a.StudentID
        ///     INNER JOIN Site_Student s on s.UserID=u.UserID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentPointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_IssueDetail_GetStudentPointAllInfoList]";
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
        /// 获取已经发布的“课程积分”的所有学员的积分明细列表，带课程、项目的基本信息等
        /// FROM Point_Student_IssueDetail a
        ///     INNER JOIN Sty_StudentCourse sc on sc.StudentCourse=a.AccessPointSourceID
        ///     INNER JOIN Sty_StudentSignup b on b.StudentSignupID =sc.StudentSignupID
        ///     INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        ///     INNER JOIN Tr_ItemCourse tc on tc.TrainingItemCourseID =sc.TrainingItemCourseID
        ///     INNER JOIN Res_Course e on e.CourseID = tc.CourseID
        ///     INNER JOIN Site_User u on u.UserID=b.UserID
        ///     INNER JOIN Site_Student s on s.UserID=u.UserID
        ///     LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        ///     LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        ///     LEFT JOIN (
        ///         select
        ///             c.ClassStudentID,c.StudentSignupID,c.UserID
        ///             ,b.ClassSubgroupID,b.ClassSubgroupName
        ///             from Sty_ClassSubgroupStudent a
        ///                 inner join Sty_ClassSubgroup b on b.ClassSubgroupID = a.ClassSubgroupID
        ///                 inner join Sty_ClassStudent c on c.ClassStudentID = a.ClassStudentID
        ///                 inner join Sty_Class d on d.ClassID = c.ClassID
        ///                 inner join Sty_StudentSignup e on e.StudentSignupID =c.StudentSignupID
        ///             ) j on j.UserID= s.UserID and j.StudentSignupID=b.StudentSignupID
        ///     LEFT JOIN Dic_Sys_StudentPointIssueType d on d.StudentPointIssueTypeID = a.StudentPointIssueTypeID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListFromIssueDetail(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_IssueDetail_GetCoursePointAllInfoList]";
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
        /// 获取学员所有已经发布的”学习过程“积分明细列表，带项目的基本信息等
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.AccessPointSourceID    
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID    
        /// INNER JOIN Site_User u on u.UserID=b.UserID    
        /// INNER JOIN Site_Student s on s.UserID=u.UserID    
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID    
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID    
        /// LEFT JOIN (    
        ///     select    
        ///         c.ClassStudentID,c.StudentSignupID,c.UserID    
        ///         ,b.ClassSubgroupID,b.ClassSubgroupName    
        ///     from Sty_ClassSubgroupStudent a    
        ///         inner join Sty_ClassSubgroup b on b.ClassSubgroupID = a.ClassSubgroupID    
        ///         inner join Sty_ClassStudent c on c.ClassStudentID = a.ClassStudentID    
        ///         inner join Sty_Class d on d.ClassID = c.ClassID    
        ///         inner join Sty_StudentSignup e on e.StudentSignupID =c.StudentSignupID    
        ///         ) j on j.UserID= s.UserID and j.StudentSignupID=b.StudentSignupID    
        /// LEFT JOIN Dic_Sys_StudentPointIssueType d on d.StudentPointIssueTypeID = a.StudentPointIssueTypeID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentInputPointAllInfoListFromIssueDetail(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Point_Student_IssueDetail_GetInputPointAllInfoList]";
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
        /// 统计某个学生在某个时间前的积分总和，用来做为“期初积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="endTime">截止时间，不不含当天（即指定日期以前）</param>
        /// <returns></returns>
        public int StatStudentPointByBeforeDateTime(int studentID, DateTime endTime)
        {
            string sqlModal = "SELECT ISNULL(SUM(a.AccessPoints),0) AS AccessPoints FROM Point_Student_IssueDetail a WHERE a.StudentID = '{0}' AND a.IssueTime <'{1}'";
            string commandName = string.Format(sqlModal, studentID, endTime.ToString("yyyy-MM-dd"));
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 统计某个学生已经发布的“课程积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentCoursePointByStudentID(int studentID)
        {
            string sqlModal = @"
                SELECT ISNULL(SUM(a.AccessPoints),0) AS AccessPoints 
                FROM Point_Student_IssueDetail a WHERE a.StudentID = '{0}' AND a.StudentPointTypeID='0'";
            string commandName = string.Format(sqlModal, studentID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }


        /// <summary>
        /// 统计某个学生已经发布的“学习过程积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentInputPointByStudentID(int studentID)
        {
            string sqlModal = @"
                SELECT ISNULL(SUM(a.AccessPoints),0) AS AccessPoints 
                FROM Point_Student_IssueDetail a WHERE a.StudentID = '{0}' AND a.StudentPointTypeID='1'";
            string commandName = string.Format(sqlModal, studentID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }



        /// <summary>
        /// 统计某个学生已经发布的“积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentAllPointByStudentID(int studentID)
        {
            string sqlModal = @"
                SELECT ISNULL(SUM(a.AccessPoints),0) AS AccessPoints 
                FROM Point_Student_IssueDetail a WHERE a.StudentID = '{0}'";
            string commandName = string.Format(sqlModal, studentID);
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }




        /// <summary>
        /// 统计某个学生在某个时间前的积分总和，用来做为“期初积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="endTime">截止时间，不不含当天（即指定日期以前）</param>
        /// <returns></returns>
        public int StatStudentExpensePointBetweenTwoDate(int studentID, DateTime beginTime, DateTime endTime)
        {
            string sqlModal = @"
                SELECT ISNULL(SUM(a.BonusPoint),0) AS BonusPoint
                FROM Point_Student_ExpenseDetail a
                WHERE a.StudentID = '{0}' AND a.BonusPointTime >='{1}' AND a.BonusPointTime <'{2}'";
            string commandName = string.Format(sqlModal, studentID, beginTime.ToString("yyyy-MM-dd"), endTime.AddDays(1).ToString("yyyy-MM-dd"));
            return (int)SqlHelper.ExecuteScalar(ConnectionString.ETMSRead, CommandType.Text, commandName, null);
        }





    }
}
