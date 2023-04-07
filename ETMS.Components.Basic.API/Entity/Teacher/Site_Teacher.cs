using System;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
namespace ETMS.Components.Basic.API.Entity.Teacher
{
    /// <summary>
    /// 讲师表业务实体
    /// </summary>
    public partial class Site_Teacher
    {
        public User UserInfo
        {
            get;
            set;
        }

        public string RealName { get; set; }
        public int DepartmentID { get; set; }
        public int OrganizationID { get; set; }
        public string BelongOrgName { get; set; }
        public string PhotoUrl { get; set; }
        public Site_Student StudentInfo
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 讲师综合查询实体
    /// </summary>
    [Serializable]
    public partial class TeacherCourseMuiltyInfo : Site_Teacher
    {
        /// <summary>
        /// 讲师课程数
        /// </summary>
        public int TeachNum { get; set; }
        /// <summary>
        /// 参与项目数
        /// </summary>
        public int ItemNum { get; set; }
        /// <summary>
        /// 讲师项目教授课程数
        /// </summary>
        public int TeachCourseNum { get; set; }
        /// <summary>
        /// 教授项目课程课时数
        /// </summary>
        public decimal CourseHours { get; set; }
        /// <summary>
        /// 教授项目课程课时安排数
        /// </summary>
        public int CourseHoursNum { get; set; }
    }

    /// <summary>
    /// 讲师培训项目课程
    /// </summary>
    [Serializable]
    public partial class TeacherTraniningItemCourseInfo : Tr_ItemCourse
    {
        /// <summary>
        /// 培训项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 培训项目课程名称
        /// </summary>
        public string CourseName { get; set; }
    }

    /// <summary>
    /// 讲师培训项目课程课时
    /// </summary>
    [Serializable]
    public partial class TeacherTrainingItemCourseHoursInfo : Tr_ItemCourseHours
    {
        /// <summary>
        /// 培训项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 培训项目课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }
        /// <summary>
        /// 教室地址
        /// </summary>
        public string Address {get;set;}
        /// <summary>
        /// 教室名称
        /// </summary>
        public string ClassRoomName { get; set; }
        /// <summary>
        /// 培训课时学生数
        /// </summary>
        public int ItemCourseHoursStudentNum { get; set; }
    }
}
