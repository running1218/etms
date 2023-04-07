// File:    ExtendedTextQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 14:24:08
// Purpose: Definition of Class ExtendedTextQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 问答题实体类
    /// </summary>
    [Serializable]
    public class ExtendedTextQuestion : QuestionBase
   {
        /// <summary>
       /// 构造函数
        /// </summary>
        public ExtendedTextQuestion()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commQuestion"></param>
        public ExtendedTextQuestion(CommonQuestion commQuestion)
            : base(commQuestion){

        }

        /// <summary>
        /// 问答题答案.
        /// </summary>
        public ExtendedTextAnswer Answer { get; set; }

       /*
       /// <summary>
       /// 构造函数
       /// </summary>
       public ExtendedTextQuestion()
       {
           this.CommonQuestion = new CommonQuestion(QuestionType.ExtendedText);
       }
       /// <summary>
       /// 所有试题的通用试题信息
       /// </summary>
       //public CommonQuestion CommonQuestion { get; set; }
       */
   }
}