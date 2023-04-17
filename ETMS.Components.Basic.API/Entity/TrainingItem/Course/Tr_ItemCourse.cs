
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course
{
    /// <summary>
    /// 培训项目课程表业务实体
    /// </summary>
    public partial class Tr_ItemCourse:AbstractObject
	{
        /// <summary>
        /// 课程缩略图
        /// </summary>
        public string ThumbnailURL
        {
            get;
            set;
        }
        /// <summary>
        /// 课程编码
        /// </summary>
        public string CourseCode
        {
            get;
            set;
        }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName
        {
            get;
            set;
        }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员项目选课成绩
        /// </summary>
        public decimal SumGrade
        {
            get;
            set;
        }

        /// <summary>
        /// 课程-学员报名状态
        /// 0: 未报名；1：此项目下报名； 2：其它项目下报名
        /// </summary>
        public int SignStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 学员报名ID
        /// </summary>
        public Guid StudentSignupID { get; set; }
        /// <summary>
        /// 课程学习次数
        /// </summary>
        public int StudyTimes { get; set; }
        /// <summary>
        /// 培训项目课程讲师数
        /// </summary>
        public int TeacherNum { get; set; }
	}
    public class TrainItemCourse
    {
        public Guid TrainingItemID { get; set; }
        public string TrainingItemName { get; set; }
        public Guid CourseID { get; set; }
        public string CourseName { get; set; }
        public string ThumbnailURL { get; set; }
        public string CourseBeginTime { get; set; }
        public string CourseEndTime { get; set; }
        public Guid TrainingItemCourseID { get; set; }
        public decimal StudyProcessPercent { get; set; }
        public int CourseModel { get; set; }
        public int LivingType { get; set; }
    }
}
