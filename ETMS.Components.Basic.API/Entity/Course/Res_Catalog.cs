using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 记录课程目录
    /// </summary>
    [Serializable]
    public partial class Res_Catalog : AbstractObject
    {
        public Res_Catalog() { }

        #region Properties
        /// <summary>
		/// 主键
		/// <summary>
		public Guid CatalogID { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Guid CoursewareID { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Guid ParentID { get; set; }

        /// <summary>
		/// 
		/// <summary>
		public String Name { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Int32 Level { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Int32 Sort { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Int32 Status { get; set; }

        /// <summary>
		/// 
		/// <summary>
		public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// <summary>
        public DateTime ModifyTime { get; set; }
        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "CatalogID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.CatalogID;
            }
            set
            {
                this.CatalogID = (Guid)value;
            }
        }

        #endregion override
    }
}
