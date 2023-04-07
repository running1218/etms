using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.Basic.Implement.BLL.Course.Teacher
{
    public partial class Res_TeacherCourseLogic
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public List<Site_Teacher> GetPageList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            List<Res_TeacherCourse> courseTeacher = DAL.GetPagedList(1, int.MaxValue - 100, sortExpression, criteria, out totalRecords).ToList<Res_TeacherCourse>();            
            List<Site_Teacher> teacherList = new Site_TeacherLogic().GetTeachersByOrganization();

            var list = (from t in teacherList
                        join ct in courseTeacher
                        on t.TeacherID equals ct.TeacherID
                        orderby t.RealName ascending
                        select t).ToList();

            totalRecords = list.Count;
            return list.PageList<Site_Teacher>(pageIndex, pageSize);
        }


        public List<Site_Teacher> GetCourseTeachers(Guid courseID)
        {
            return DAL.GetCourseTeacher(courseID).ToList<Site_Teacher>();
        }

        public List<Site_Teacher> GetTeacherChooseList(int pageIndex, int pageSize, string teacherName, string criteria, out int totalRecords)
        {
            List<Res_TeacherCourse> courseTeacher = DAL.GetPagedList(1, int.MaxValue - 100, string.Empty, criteria, out totalRecords).ToList<Res_TeacherCourse>();
            List<Site_Teacher> teacherList = new Site_TeacherLogic().GetTeachersByOrganization();

            var list = (from t in teacherList
                        where !(from ct in courseTeacher
                                select ct.TeacherID).Contains(t.TeacherID)
                        orderby t.RealName ascending
                        select t).ToList();
            list = list.Where(t => t.RealName.Contains(teacherName)).ToList();
            totalRecords = list.Count;
            return list.PageList<Site_Teacher>(pageIndex, pageSize);
        }

        public void Save(int[] teachers, Guid courseID)
        {
            foreach (int teacherID in teachers)
            {
                Add(new Res_TeacherCourse(){ 
                        TeacherCourseID = Guid.NewGuid(), 
                        CourseID = courseID ,
                        TeacherID = teacherID,
                        CreateUser = UserContext.Current.RealName,
                        CreateUserID = UserContext.Current.UserID,
                        CreateTime = DateTime.Now
                });
            }
        }

        /// <summary>
        /// 新增讲师课程
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="courseID"></param>
        public void SaveTeacherCourse(int teacherID, Guid[] courseID)
        {
            foreach (Guid key in courseID)
            {
                Add(new Res_TeacherCourse()
                {
                    TeacherCourseID = Guid.NewGuid(),
                    CourseID = key,
                    TeacherID = teacherID,
                    CreateUser = UserContext.Current.RealName,
                    CreateUserID = UserContext.Current.UserID,
                    CreateTime = DateTime.Now
                });
            }
        }

        private User[] GetUsers()
        {
            return  new UserLogic().GetUsers(string.Format(" And OrganizationID={0} ", UserContext.Current.OrganizationID));
        }

        /// <summary>
        /// 删除课程讲师
        /// </summary>
        /// <param name="teacherIDs"></param>
        /// <param name="courseID"></param>
        public void Delete(int[] teacherIDs, Guid courseID)
        {
            foreach (int teacherID in teacherIDs)
            {
                DAL.Remove(teacherID, courseID);
                BizLogHelper.Operate(string.Format("课程ID={0} & 讲师ID={1}", courseID, teacherID), "删除");
            }
        }

        /// <summary>
        /// 删除课程讲师
        /// </summary>
        /// <param name="teacherIDs"></param>
        /// <param name="courseID"></param>
        public void DeleteTeacherCourse(int teacherID, Guid[] courseID)
        {
            foreach (Guid key in courseID)
            {
                DAL.Remove(teacherID, key);
                BizLogHelper.Operate(string.Format("课程ID={0} & 讲师ID={1}", courseID, teacherID), "删除");
            }
        }

        /// <summary>
        /// 获取讲师教授课程列表
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Res_Course> GetTeacherTeachCourse(int teacherID, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetTeacherTeachCourse(teacherID).Where(f=>f.CourseStatus.Equals(1)).ToList().PageList<Res_Course>(pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// 获取课程讲师数
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public int GetCourseTeacherNum(Guid courseID)
        {
            int totalRecords = 0;
            var list = DAL.GetPagedList(1, 10, string.Empty, string.Format(" And CourseID = '{0}' ", courseID), out totalRecords);
            return totalRecords;
        }

        public DataTable GetCourseTeacher(Guid courseID) {
            return DAL.GetTeachersByCourseID(courseID);
        }

        /// <summary>
        /// 获取有效状态的课程讲师
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="courseID"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Site_Teacher> GetTeachersByCourseID(int pageIndex, int pageSize, Guid courseID, out int totalRecords)
        {
            return DAL.GetTeachersByCourseID(courseID).PageList<Site_Teacher>(pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// 获取讲师教授课程列表
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Res_Course> GetTeacherTeachCourse(int teacherID)
        {
            return DAL.GetTeacherTeachCourse(teacherID).ToList<Res_Course>().Where(f=>f.CourseStatus.Equals(1)).ToList();
        }

        /// <summary>
        /// 获取待选课程
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="courseCode"></param>
        /// <param name="courseName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Res_Course> ChooseTeacherTeachCourse(int teacherID, string courseCode, string courseName, int pageIndex, int pageSize, out int totalRecords)
        {
            // 讲师已选取课程
            var teacherCourseList = GetTeacherTeachCourse(teacherID);
            
            // 机构下的所有启动的课程
            List<Res_Course> courseList = CacheHelper.Get(string.Format("ChooseTeacherTeachCourse_{0}",UserContext.Current.OrganizationID)) as List<Res_Course>;
            if (null == courseList)
            {
                courseList = new Res_CourseLogic().GetCourseByOrgID();
                CacheHelper.Add(string.Format("ChooseTeacherTeachCourse_{0}", UserContext.Current.OrganizationID), courseList, TimeSpan.FromMinutes(10));
            }

            var list = courseList.Where(f => f.CourseCode.Contains(courseCode)
                                            & f.CourseName.Contains(courseName)
                                            & f.CourseStatus.Equals(1)).ToList();

            // 获取讲师可选的课程
            list = (from t in list
                        where !(from ct in teacherCourseList
                                select ct.CourseID).Contains(t.CourseID)
                        orderby t.CourseCode ascending, t.CourseName ascending
                        select t).ToList();

            return list.PageList<Res_Course>(pageIndex, pageSize, out totalRecords);
        }
    }
}
