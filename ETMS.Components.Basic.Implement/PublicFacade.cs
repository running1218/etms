using System;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
//using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.API.Entity.Dictionary;

namespace ETMS.Components.Basic.Implement
{
    public class PublicFacade : DefaultComponent, IPublicFacade
    {
        #region 根据ID获取名称

        /// <summary>
        /// 跟据ID获取部门名称
        /// </summary>
        public string GetDeptNameByID(Int32 DeptID)
        {
            DepartmentLogic departmentLogic = new DepartmentLogic();
            return departmentLogic.GetDeptNameByID(DeptID);
        }

        /// <summary>
        /// 跟据ID获取岗位名称
        /// </summary>
        public string GetPostNameByID(Int32? PostID)
        {
            Dic_PostLogic postLogic=new Dic_PostLogic();
            Dic_Post post = postLogic.GetById(PostID);
            return post.PostName;
        }

        public Site_Student GetStudentInfo()
        {
            User user = new UserLogic().GetUserByID(UserContext.Current.UserID);
            Site_Student student = new Site_StudentLogic().GetStudentById(UserContext.Current.UserID)?? new Site_Student();
            
            return new Site_Student() { 
                DepartmentID = user.DepartmentID,
                RankID = student.RankID,
                PostID = student.PostID,
                RealName = user.RealName,
                PhotoUrl = user.PhotoUrl,
                WorkerNo = student.WorkerNo
            };
        }

        public Site_Student GetStudentInfo(int userID)
        {
            User user = new UserLogic().GetUserByID(userID);
            Site_Student student = new Site_StudentLogic().GetStudentById(userID) ?? new Site_Student();

            return new Site_Student()
            {
                DepartmentID = user.DepartmentID,
                RankID = student.RankID,
                PostID = student.PostID,
                RealName = user.RealName,
                PhotoUrl = user.PhotoUrl,
                WorkerNo = student.WorkerNo
            };
        }

        /// <summary>
        /// 获取讲师（包括用户表、学员表数据）
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public Site_Teacher GetTeacherInfo(int teacherID)
        {
            Site_Teacher teacher = new Site_TeacherLogic().GetTeacherById(teacherID) ?? new Site_Teacher();
            User user = new UserLogic().GetUserByID(teacherID) ?? new User();
            Site_Student student = new Site_StudentLogic().GetStudentById(teacherID) ?? new Site_Student();

            teacher.UserInfo = user;
            teacher.StudentInfo = student;
            return teacher;
        }

        /// <summary>
        /// 跟据ID获取机构名称
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public string GetOrgNameByID(int OrganizationID)
        {
            Organization org = new Organization();
            OrganizationLogic orgLogic = new OrganizationLogic();
            org = (Organization)orgLogic.GetNodeByID(OrganizationID);

            return org.OrganizationName;
        }

        public string GetTeacherNameByID(int teacherID)
        {
            Site_TeacherLogic teacherLogic = new Site_TeacherLogic();
            Site_Teacher entity = teacherLogic.GetById(teacherID);
            return entity != null ? entity.RealName : string.Empty;
        }

        /// <summary>
        /// 跟据ID获取课程名称
        /// </summary>
        public string GetCourseNameByID(Guid CourseID)
        {
            Res_CourseLogic resCourseLogic = new Res_CourseLogic();
            Res_Course resCourse = resCourseLogic.GetById(CourseID);
            if (resCourse != null)
                return resCourse.CourseName;
            else
                return "";
        }
          
        #endregion

        #region 根据ID获取编码

        /// <summary>
        /// 跟据ID获取机构编码
        /// </summary>
        public string GetOrgCodeByID(int OrganizationID)
        {
            Organization org = new Organization();
            OrganizationLogic orgLogic = new OrganizationLogic();
            org = (Organization)orgLogic.GetNodeByID(OrganizationID);

            return org.OrganizationCode;
        }

        /// <summary>
        /// 跟据ID获取课程编码
        /// </summary>
        public string GetCourseCodeByID(Guid CourseID)
        {
            Res_CourseLogic resCourseLogic = new Res_CourseLogic();
            Res_Course resCourse = resCourseLogic.GetById(CourseID);
            if (resCourse != null)
                return resCourse.CourseCode;
            else
                return "";
        }

        #endregion
    }
}
