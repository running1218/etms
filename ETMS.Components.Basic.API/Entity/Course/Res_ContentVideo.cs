using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 各种码流的视频资源
    /// </summary>
    [Serializable]
    public partial class Res_ContentVideo : AbstractObject
    {
        public Res_ContentVideo() { }

        #region Properties
        /// <summary>
		/// 主键
		/// <summary>
		public Guid ContentVideoID { get; set; }

        /// <summary>
		/// 资源ID
		/// <summary>
		public Guid ContentID { get; set; }

        /// <summary>
		/// 
		/// <summary>
		public String StreamCode { get; set; }

        /// <summary>
		/// 
		/// <summary>
		public String DataInfo { get; set; }

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
            get { return "ContentVideoID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.ContentVideoID;
            }
            set
            {
                this.ContentVideoID = (Guid)value;
            }
        }

        #endregion override
    }
}
