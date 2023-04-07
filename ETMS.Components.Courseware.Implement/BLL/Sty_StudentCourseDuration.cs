using System;
using ETMS.Components.Courseware.API.Entity;
using System.Data;


namespace ETMS.Components.Courseware.Implement.BLL
{
    public class Sty_StudentCourseDuration
    {
        DAL.Sty_StudentCourseDuration dal = new DAL.Sty_StudentCourseDuration();
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Sty_StudentCourseDurationModel courseDurationModel)
        {
            dal.Add(courseDurationModel);
        }

        /// <summary>
        /// 查询用户是浏览过此课件
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public string GetStudentCourseDuration(string UserID, string ResourceID, string ItemCourseResID)
        {
            return dal.GetStudentCourseDuration(UserID, ResourceID, ItemCourseResID);
        }
        /// <summary>
        ///根据StudentCourseDurationID获取SCORE详情ID
        /// </summary> 
        public string GetScoreID(string StudentCourseDurationID)
        {
            return dal.GetScoreID(StudentCourseDurationID);
        }

        /// <summary>
        /// 课件插入数据和修改时间
        /// </summary>
        public void SetSessionTime(Guid ResourceID, Guid ItemCourseResID, Guid CourseWareID, int SessionTime, DateTime EndTime, int UserID)
        {
            dal.SetSessionTime(ResourceID, ItemCourseResID, CourseWareID, SessionTime, EndTime, UserID);
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
            return dal.InsertStudentCourseDurationInfo(StartBeginTime, EndTime, SessionTime, StudentCourseDurationID);
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
            return dal.UpdateStudentCourseDurationInfo(EndTime, SessionTime, ID);
        }

        /// <summary>
        ///修改浏览次数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ItemCourseResID"></param>
        /// <returns></returns>
        public int UpdateStudentCourseDurationCount(string SessionTime, string StudentCourseDurationID)
        {
            return dal.UpdateStudentCourseDurationCount(SessionTime, StudentCourseDurationID);
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
            dal.UpdateStudentCourseDurationCount(CountTime, StudentCourseDurationID, 0);
        }

        /// <summary>
        /// 增加一条笔记数据
        /// </summary>
        public int AddSty_StudentNotes(Sty_StudentNotes model)
        {
            return dal.AddSty_StudentNotes(model);
        }
        /// <summary>
        /// 增加一条讨论数据
        /// </summary>
        public int AddSty_StudentTalk(Sty_StudentTalk model)
        {
            return dal.AddSty_StudentTalk(model);
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
            return dal.Pr_GetSty_studentTalk(pageIndex, pageSize, sortExpression, UserID, CourseWareID, out totalRecords);
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
            return dal.Pr_GetSty_StudentNotes(pageIndex, pageSize, sortExpression, UserID, ResourceID, out totalRecords);
        }
    }
}
