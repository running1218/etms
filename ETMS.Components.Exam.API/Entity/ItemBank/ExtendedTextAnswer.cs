// File:    ExtendedTextAnswer.cs
// Author:  Administrator
// Created: 2011年12月15日 17:49:07
// Purpose: Definition of Class ExtendedTextAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// 问题题答案
    ///</summary>
    [Serializable]
   public class ExtendedTextAnswer : AnswerBase
   {
       //序列化后的答案
       private string answer;

       /*
       /// <summary>
       /// 所属试题ID
       /// </summary>
       public Guid QuestionID { get; set; }
       */

       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="answers"></param>
       public ExtendedTextAnswer(string answers)
       {
           //if(String.IsNullOrEmpty(answers))
           //    throw new ETMS.AppContext.BusinessException("答案不能为Empty");

           this.answer = answers;
       }
       /// <summary>
       /// 构造函数
       /// </summary>
       public ExtendedTextAnswer()
       {
           this.Answer = string.Empty; 
       }
       ///<summary>
       /// 答案的Json字串
       ///</summary>
       public override string Answer
       {
           get { return this.answer; }
           set { this.answer = value; }
       }
   }
}