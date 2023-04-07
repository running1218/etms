using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    [Serializable]
	public partial class Sty_UserStudyProgress : AbstractObject
	{
		public Sty_UserStudyProgress() {}

		#region Properties

		/// <summary>
		/// 学习进度ID
		/// <summary>
		public Guid UserStudyProgressID { get; set; }

		/// <summary>
		/// 用户ID
		/// <summary>
		public Guid UserID { get; set; }

		/// <summary>
		/// 资源ID
		/// <summary>
		public Guid ChapterResourceID { get; set; }

		/// <summary>
		/// 学习状态
		/// <summary>
		public Int16 StudyStatus { get; set; }

		/// <summary>
		/// 学习进度
		/// <summary>
		public Int32? StudyProgress { get; set; }

        /// <summary>
        /// 视频学习时间
        /// </summary>
        public decimal? StudyTime { get; set; }


        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime CreateTime { get; set; }

		/// <summary>
		/// 修改时间
		/// <summary>
		public DateTime ModifyTime { get; set; }
        
        #endregion Properties

        #region Override

        public override string DefaultKeyName
		{
			get { return "UserStudyProgressID"; }
		}

		public override object KeyValue
		{
			get
			{
				return this.UserStudyProgressID;
			}
			set
			{
				this.UserStudyProgressID = (Guid)value;
			}
		}

		#endregion override
	}
}