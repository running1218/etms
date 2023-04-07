using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course.Resources
{
    /// <summary>
    /// 视频转码队列表的实体
    /// </summary>
    [Serializable]
    public partial class Res_TranscodingQueue : AbstractObject
    {
        public Res_TranscodingQueue() { }

        /// <summary>
        /// 
        /// <summary>
        public Guid TranscodingQueueID { get; set; }

        /// <summary>
        /// 资源ID
        /// <summary>
        public Guid? ContentID { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public DateTime CreateTime { get; set; }

        /// <summary>
		/// 码率
		/// <summary>
		public String StreamCode { get; set; }

        /// <summary>
		/// 转码次数
		/// <summary>
		public int? TranscodingCount { get; set; }

        /// <summary>
        /// 
        /// <summary>
        //public int Status { get; set; }

        #region Override

        public override string DefaultKeyName
        {
            get { return "TranscodingQueueID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.TranscodingQueueID;
            }
            set
            {
                this.TranscodingQueueID = (Guid)value;
            }
        }

        #endregion override
    }
}
