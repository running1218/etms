using ETMS.AppContext;
using System;
using System.Collections;

namespace ETMS.Components.NoteQuestion.API.Entity
{
    [Serializable]
    public partial class QA_Question : AbstractObject
    {
        public QA_Question() { }

        #region Properties

        /// <summary>
        /// 问答 问题ID
        /// <summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 项目课程关系ID
        /// <summary>
        public Guid TrainingItemCourseID { get; set; }


        /// <summary>
        /// 资源ID
        /// <summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// 提问者用户ID
        /// <summary>
        public int UserID { get; set; }

        /// <summary>
        /// 问题标题
        /// <summary>
        public String QuestionTitle { get; set; }

        /// <summary>
        /// 问题内容
        /// <summary>
        public String QuestionContent { get; set; }

        /// <summary>
        /// 提问时间
        /// <summary>
        public DateTime CreateTime { get; set; }
        

        /// <summary>
        /// 回复的数量
        /// <summary>
        public int AnswerCount { get; set; }

        /// <summary>
        /// 回复集合
        /// </summary>
        public ArrayList QAAnswers { get; set; }



        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "QuestionID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.QuestionID;
            }
            set
            {
                this.QuestionID = (Guid)value;
            }
        }

        #endregion override
    }
}
