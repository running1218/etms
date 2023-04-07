using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 所有试题类型的基类
    /// </summary>
    [Serializable]
    public class QuestionBase
    {
        public QuestionBase()
        { }

        public QuestionBase(CommonQuestion commQuestion)
        {
            this.CommonQuestion = commQuestion;
        }

        /// <summary>
        /// 所有试题的通用试题信息
        /// </summary>
        public CommonQuestion CommonQuestion { get; set; }
    }
}
