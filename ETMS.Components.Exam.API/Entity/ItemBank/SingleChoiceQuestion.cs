// File:    SingleChoiceQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 14:24:05
// Purpose: Definition of Class SingleChoiceQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// 单选题实体类
   ///</summary>
    [Serializable]
    public class SingleChoiceQuestion : QuestionBase
   {
       public SingleChoiceQuestion()
       { }
       public SingleChoiceQuestion(CommonQuestion commQuestion)
           :base(commQuestion)
       {
       }
       /// <summary>
       /// 选择题答案.
       /// </summary>
       public SingleChoiceAnswer Answer { get; set; }

      ///<summary>
      /// 单选题各个选项
      ///</summary>
      public IList<QuestionOption> Options
      {
         get;
         set;
      }
   }
}