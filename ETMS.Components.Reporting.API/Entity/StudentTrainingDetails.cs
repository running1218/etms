using System;
using System.Collections.Generic;

namespace ETMS.Components.Reporting.API.Entity
{
    public partial class StudentTrainingDetails
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string WorkerNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 员工部门
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 员工职级
        /// </summary>
        public int RankID { get; set; }
        /// <summary>
        /// 员工岗位
        /// </summary>
        public int PostID { get; set; }
        /// <summary>
        /// 员工职务
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 学员培训项目
        /// </summary>
        public List<StudentTrainingItems> TrainingItems { get; set; }
    }

    public partial class StudentTrainingItems
    {
        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }
        /// <summary>
        /// 培训项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 培训项目开始时间
        /// </summary>
        public DateTime ItemBeginTime { get; set; }
        /// <summary>
        /// 培训项目结束时间
        /// </summary>
        public DateTime ItemEndTime { get; set; }

        /// <summary>
        /// 培训项目课程
        /// </summary>
        public List<StudentTrainingItemCourses> TrainingItemCourses { get; set; }
    }

    public partial class StudentTrainingItemCourses
    {
        /// <summary>
        /// 学员选课ID
        /// </summary>
        public Guid StudentCourse { get; set; }
        /// <summary>
        /// 课程编码
        /// </summary>
        public string CourseCode { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseTypeID { get; set; }
        /// <summary>
        /// 课时
        /// </summary>
        public decimal CourseHours { get; set; }
        /// <summary>
        /// 学员项目选课成绩
        /// </summary>
        public decimal SumGrade { get; set; }
        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// 授课方式
        /// </summary>
        public Int32 TeachModelID { get; set; }

        /// <summary>
        /// 课程状态
        /// </summary>
        public Int32 CourseStatus { get; set; }

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime CourseBeginTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime CourseEndTime { get; set; }

        /// <summary>
        /// 培训方式
        /// </summary>
        public Int32 TrainingModelID { get; set; }
        /// <summary>
        /// 课程属性
        /// </summary>
        public Int32 CourseAttrID { get; set; }

        /// <summary>
        /// 培训项目课程课时
        /// </summary>
        public List<StudentTrainingItemCourseHours> TrainingItemCourseHours { get; set; }
    }

    public partial class StudentTrainingItemCourseHours
    {
        /// <summary>
        /// 培训项目课程ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }
        /// <summary>
        /// 培训项目课时ID
        /// </summary>
        public Guid ItemCourseHoursID { get; set; }
        /// <summary>
        /// 培训日期
        /// </summary>
        public DateTime TrainingDate { get; set; }
        /// <summary>
        /// 培训开始日期
        /// </summary>
        public DateTime TrainingBeginTime { get; set; }
        /// <summary>
        /// 培训结束日期
        /// </summary>
        public DateTime TrainingEndTime { get; set; }
        /// <summary>
        /// 教室ID
        /// </summary>
        public Guid ClassRoomID { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public int TeacherID { get; set; }
        /// <summary>
        /// 讲师姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 教室地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 教室名称
        /// </summary>
        public string ClassRoomName { get; set; }
        /// <summary>
        /// 签到信息
        /// </summary>
        public string SigninTypeName { get; set; }
    }
}
