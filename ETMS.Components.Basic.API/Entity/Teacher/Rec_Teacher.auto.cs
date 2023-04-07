using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Teacher
{
    /// <summary>
    /// 记录课程目录
    /// </summary>
    [Serializable]
    public partial class Rec_Teacher : AbstractObject
    {
        public Rec_Teacher() { }

        #region Properties
        /// <summary>
		/// 主键，讲师ID
		/// <summary>
		public Int32 TeacherID { get; set; }
        /// <summary>
		/// 是否置顶
		/// <summary>
		public Boolean IsTop { get; set; }
        /// <summary>
		/// 排序
		/// <summary>
		public Int32 Sort { get; set; }

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
            get { return "TeacherID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.TeacherID;
            }
            set
            {
                this.TeacherID = (int)value;
            }
        }

        #endregion override
    }
}
