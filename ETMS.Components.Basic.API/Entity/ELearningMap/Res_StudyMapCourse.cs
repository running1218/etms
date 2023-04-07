using System;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// 学习地图表业务实体
    /// </summary>
    public partial class Res_StudyMapCourse : Res_StudyMap
	{
        #region 课程信息 Fields, Properties
        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid CourseID { get; set; }

        /// <summary>
        /// 课程编码
        /// </summary>
        public String CourseCode { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public String CourseName { get; set; }

        /// <summary>
        /// 课程等级
        /// </summary>
        public Int32 CourseLevelID { get; set; }

        /// <summary>
        /// 课程类型
        /// </summary>
        public Int32 CourseTypeID { get; set; }

        /// <summary>
        /// 课程状态
        /// </summary>
        public Int32 CourseStatus { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public Boolean IsPublic { get; set; }

        /// <summary>
        /// 课时
        /// </summary>
        public Decimal CourseHours { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public String ThumbnailURL { get; set; }

        /// <summary>
        /// 适用对象
        /// </summary>
        public String ForObject { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        public String CourseIntroduction { get; set; }

        /// <summary>
        /// 课程大纲
        /// </summary>
        public String CourseOutline { get; set; }
        /// <summary>
        /// 学习方式
        /// </summary>
        public int StudyModelID { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ChargeMan { get; set; }
        #endregion
    }
}
