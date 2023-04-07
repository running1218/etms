using System;
using ETMS.AppContext;

namespace ETMS.Components.Basic.API.Entity.Course
{
    /// <summary>
    /// 课程资源表的实体
    /// </summary>
	[Serializable]
	public partial class ResContent : AbstractObject
	{
		public ResContent() {}

		#region Properties

		/// <summary>
		/// 
		/// <summary>
		public Guid ContentID { get; set; }

		/// <summary>
		/// 课件ID
		/// <summary>
		//public Guid? CoursewareID { get; set; }

		/// <summary>
		/// 
		/// <summary>
		public String Name { get; set; }

		/// <summary>
		/// 资源类型，1：视频；2: Office(ppt,pdf,word,excel); 3:练习;4:Text（文字或者富文本）;5：URL
		/// <summary>
		public Int32 Type { get; set; }

		/// <summary>
		/// 根据资源类型存储不同实体
		/// <summary>
		public String DataInfo { get; set; }

		/// <summary>
		/// 
		/// <summary>
		//public Int32 Sort { get; set; }

		/// <summary>
		/// 状态，0：停用；1：启用。
		/// <summary>
		//public Int32 Status { get; set; }

		/// <summary>
		/// 
		/// <summary>
		public String TeacherName { get; set; }

		/// <summary>
		/// 视频时长（秒）
		/// <summary>
		public Decimal? PlayTime { get; set; }

		/// <summary>
		/// 
		/// <summary>
		public String Remark { get; set; }

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
			get { return "ContentID"; }
		}

		public override object KeyValue
		{
			get
			{
				return this.ContentID;
			}
			set
			{
				this.ContentID = (Guid)value;
			}
		}

		#endregion override
	}
}