using System;
using ETMS.AppContext;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 课程表业务实体
    /// </summary>
    [Serializable]
    public partial class Res_Course : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "CourseID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.CourseID;
            }
            set
            {
                this.CourseID = (Guid)value;
            }
        }
        #endregion

        #region Fields, Properties
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
        /// 课程上课模式
        /// 1：在线课程（默认）
        /// 2：直播课程
        /// </summary>
        public Int32 CourseModel { get; set; }
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
        /// 所属机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

        /// <summary>
        /// 课程关注人数
        /// </summary>
        public Int32 FocusCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 CreateUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String ModifyUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public Boolean DelFlag { get; set; }

        public bool IsPay { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 直播类型
        /// </summary>
        public int LivingType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string StarDateString { get; set; }
        public string EndDateString { get; set; }
        public string TeacherInfo { get; set; }
        #endregion Fields, Properties

    }
}
