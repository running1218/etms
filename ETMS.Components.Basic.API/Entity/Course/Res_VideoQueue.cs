using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 视频转码等待队列 
    /// </summary>
    [Serializable]
    public partial class Res_VideoQueue : AbstractObject
    {
        public Res_VideoQueue() { }

        #region Properties
        /// <summary>
		/// 主键
		/// <summary>
		public Guid VideoQueueID { get; set; }

        /// <summary>
		/// 资源ID
		/// <summary>
		public Guid ContentID { get; set; }

        /// <summary>
		/// 码率
		/// <summary>
		public String StreamCode { get; set; }

        /// <summary>
		/// 
		/// <summary>
		public DateTime CreateTime { get; set; }

        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "VideoQueueID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.VideoQueueID;
            }
            set
            {
                this.VideoQueueID = (Guid)value;
            }
        }

        #endregion override
    }
}
