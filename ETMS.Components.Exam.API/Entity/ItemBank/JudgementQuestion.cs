// File:    JudgementQuestion.cs
// Author:  Administrator
// Created: 2011年12月16日 14:24:06
// Purpose: Definition of Class JudgementQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 判断题试题的实体类
    /// </summary>
   [Serializable]
   public class JudgementQuestion:QuestionBase
   {
       /// <summary>
       /// 判断题答案.
       /// </summary>
       public JudgementAnswer Answer { get; set; }

       ///<summary>
       /// 判断题各个选项（只允许二个选项）
       ///</summary>
       public IList<QuestionOption> Options
       {
           get;
           set;
       }
   }
}