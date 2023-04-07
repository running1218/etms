using ETMS.AppContext;
using System;

namespace ETMS.Components.NoteQuestion.API.Entity
{
    [Serializable]
	public partial class QA_Answer : AbstractObject
	{
		public QA_Answer() {}

		#region Properties

		/// <summary>
		/// 回答ID
		/// <summary>
		public Guid AnswerID { get; set; }

		/// <summary>
		/// 回答用户ID
		/// <summary>
		public int UserID { get; set; }

		/// <summary>
		/// 回答内容
		/// <summary>
		public String AnswerContent { get; set; }

		/// <summary>
		/// 回答时间
		/// <summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 问题id
		/// <summary>
		public Guid QuestionID { get; set; }
        

		#endregion Properties

		#region Override

		public override string DefaultKeyName
		{
			get { return "AnswerID"; }
		}

		public override object KeyValue
		{
			get
			{
				return this.AnswerID;
			}
			set
			{
				this.AnswerID = (Guid)value;
			}
		}

		#endregion override
	}
}