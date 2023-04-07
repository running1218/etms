using System;
using System.Data;

using ETMS.Components.Point.Implement.DAL;

namespace ETMS.Components.Point.Implement.BLL
{

    /// <summary>
    /// 学生积分明细扩展类
    /// 黄中福：2012－05－17
    /// </summary>
    public partial class Point_Student_IssueDetailLogic
    {
        private static readonly Point_Student_IssueDetailDataAccess DAL = new Point_Student_IssueDetailDataAccess();

        /// <summary>
        /// 统计学员所有已经发布的积分明细列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable StatStudentPointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.StatStudentPointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
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
            return DAL.GetStudentPointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个学员已经发布的积分明细列表
        /// FROM Point_Student_IssueDetail a
        ///     INNER JOIN Site_User u on u.UserID=a.StudentID
        ///     INNER JOIN Site_Student s on s.UserID=u.UserID
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentPointAllInfoListByStudentID(int studentID,int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND a.StudentID='{0}'",studentID);
            if ((sortExpression == "") || (sortExpression == null))
            {
                sortExpression = " a.IssueTime desc ";
            }
            return GetStudentPointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 统计某个组织机构下的学员所有已经发布的积分明细列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable StatStudentPointAllInfoListByOrgID(int orgID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND u.OrganizationID = '{0}'", orgID);
            return StatStudentPointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 统计某个学生在某个时间前的积分总和，用来做为“期初积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="endTime">截止时间，不不含当天（即指定日期以前）</param>
        /// <returns></returns>
        public int StatStudentPointByBeforeDateTime(int studentID, DateTime endTime)
        {
            return DAL.StatStudentPointByBeforeDateTime(studentID, endTime);
        }



        /// <summary>
        /// 统计某个学生在某个时间段消费的积分总和，用来做为“本期积分消费”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="beginTime">开始时间（含）</param>
        /// <param name="endTime">截止时间（含）</param>
        /// <returns></returns>
        public int StatStudentExpensePointBetweenTwoDate(int studentID, DateTime beginTime, DateTime endTime)
        {
            return DAL.StatStudentExpensePointBetweenTwoDate(studentID, beginTime, endTime);

        }



        /// <summary>
        /// 统计某个学生已经发布的“课程积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentCoursePointByStudentID(int studentID)
        {
            return DAL.StatStudentCoursePointByStudentID(studentID);
        }



        /// <summary>
        /// 统计某个学生已经发布的“积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentAllPointByStudentID(int studentID)
        {
            return DAL.StatStudentAllPointByStudentID(studentID);
        }




        /// <summary>
        /// 统计某个学生已经发布的“学习过程积分”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public int StatStudentInputPointByStudentID(int studentID)
        {
            return DAL.StatStudentInputPointByStudentID(studentID);
        }


        /// <summary>
        /// 获取已经发布的“课程积分”的所有学员的积分明细列表，带课程、项目的基本信息等
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListFromIssueDetail(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCoursePointAllInfoListFromIssueDetail(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 从“积分发布明细表”获取某个学员的所有课程获得积分的列表（“个人积分查询”），带课程、项目的基本信息等
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListFromIssueDetailByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND u.UserID='{0}'", studentID); //学员ID
            if ((sortExpression == "") || (sortExpression == null))
                sortExpression = " c.ItemName,a.IssueTime desc "; //按项目名称和发布积分的时间倒序排序
            return GetStudentCoursePointAllInfoListFromIssueDetail(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }




        /// <summary>
        /// 获取学员所有已经发布的”学习过程“积分明细列表，带项目的基本信息等
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentInputPointAllInfoListFromIssueDetail(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentInputPointAllInfoListFromIssueDetail(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 从“积分发布明细表”获取某个学员的所有“学习过程积分”的列表（“个人积分查询”），带项目的基本信息等
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentInputPointAllInfoListFromIssueDetailByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND u.UserID='{0}'", studentID); //学员ID
            if ((sortExpression == "") || (sortExpression == null))
                sortExpression = " c.ItemName,a.IssueTime desc "; //按项目名称和发布积分的时间倒序排序
            return GetStudentInputPointAllInfoListFromIssueDetail(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }




    }
}
