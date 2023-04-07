using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 记录课程目录
    /// </summary>
    [Serializable]
    public partial class Rec_Course : AbstractObject
    {
        public Rec_Course() { }

        #region Properties
        public Guid RecommendID { get; set; }
        /// <summary>
        /// 主键，课程ID
        /// <summary>
        public Guid CourseID { get; set; }
        /// <summary>
		/// 是否置顶
		/// <summary>
		public Boolean IsTop { get; set; }
        /// <summary>
		/// 排序
		/// <summary>
		public Int32 Sort { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

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

        #endregion Properties

        #region Override

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

        #endregion override
    }
    public class DemandCourse
    {
        public Guid CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal CourseHours { get; set; }
        public string ThumbnailURL { get; set; }
        public int FocusCount { get; set; }
        public int CourseTypeID { get; set; }
        public string TeacherName { get; set; }

        public string TeacherNameLimit
        {
            get {
                string[] arr = this.TeacherName.ToString().Split(new char[] { ',' });
                if (arr.Length > 3)
                {
                    return string.Format("{0}...", TeacherName.Substring(0, TeacherName.IndexOf(",", TeacherName.IndexOf(",", TeacherName.IndexOf(",") + 1) + 1)));
                }
                else
                    return TeacherName;
            }
        }
        public string TeacherOneLimit
        {
            get
            {
                string[] arr = this.TeacherName.ToString().Split(new char[] { ',' });
                if (arr.Length > 1)
                {
                    return string.Format("{0}...", TeacherName.Substring(0, TeacherName.IndexOf(",")));
                }
                else
                    return TeacherName;
            }
        }
        public int LivingStatus {
            get{
                if (this.StartDate != DateTime.MinValue && this.EndDate != DateTime.MinValue)
                {
                    if (DateTime.Now >= this.StartDate && this.EndDate > DateTime.Now)
                    {
                        //正在直播
                        return 1;
                    }
                    else if (this.StartDate > DateTime.Now)
                    {
                        //即将直播
                        return 2;
                    }
                    else if (this.EndDate <= DateTime.Now)
                    {
                        //直播结束
                        return 3;
                    }
                    else {
                        return 2;
                    }
                }
                else {
                    return 2;
                }
            } }
        public int IsLiving
        {
            get
            {
                if (this.CourseModel == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        public DateTime CreateTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseModel { get; set; }
        public string TimeString
        {
            get
            {
                if (this.StartDate.HasValue && this.EndDate.HasValue)
                {
                    return string.Format("{0} ~ {1}", this.StartDate.Value.ToString("yyyy-MM-dd"), this.EndDate.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
