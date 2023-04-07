// File:    MultipleChoiceQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 14:24:06
// Purpose: Definition of Class MultipleChoiceQuestion

using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 多选题试题的实体类
    /// </summary>
    public class MultipleChoiceQuestion:QuestionBase
   {
       public MultipleChoiceQuestion()
       { }
       public MultipleChoiceQuestion(CommonQuestion commQuestion)
           :base(commQuestion)
       {
       }
       /// <summary>
       /// 判断题答案.
       /// </summary>
       public MultipleChoiceAnswer Answer { get; set; }

       ///<summary>
       /// 多选题各个选项
       ///</summary>
       public IList<QuestionOption> Options
       {
           get;
           set;
       }
   }
}