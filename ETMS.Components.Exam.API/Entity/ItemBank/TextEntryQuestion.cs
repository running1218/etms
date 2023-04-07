// File:    TextEntryQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 14:24:07
// Purpose: Definition of Class TextEntryQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 填空题实体类
    /// </summary>
    [Serializable]
    public class TextEntryQuestion : QuestionBase
   {
        /// <summary>
       /// 构造函数
        /// </summary>
        public TextEntryQuestion()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commQuestion"></param>
        public TextEntryQuestion(CommonQuestion commQuestion)
            : base(commQuestion)
        {
        }

        /// <summary>
        /// 问答题答案.
        /// </summary>
        public TextEntryAnswer Answer { get; set; }

       /*
       /// <summary>
       /// 构造函数
       /// </summary>
       public TextEntryQuestion()
       {
           this.CommonQuestion = new CommonQuestion(QuestionType.TextEntry);
       }
       /// <summary>
       /// 所有试题的通用试题信息
       /// </summary>
       public CommonQuestion CommonQuestion { get; set; }
       */
   }
}