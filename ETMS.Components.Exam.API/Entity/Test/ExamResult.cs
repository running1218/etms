// File:    ExamResult.cs
// Author:  Administrator
// Created: 2012年1月13日 14:00:26
// Purpose: Definition of Class ExamResult

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
   ///<summary>
   /// 试题的考试结果信息，包括：试题ID、考生答案、标准答案、试题反馈、试卷反馈等信息
   ///</summary>
   [Serializable]
   public class ExamResult
   {
       public Guid QuestionID { get; set; }
       public decimal UserScore { get; set; }
       public decimal QuestionScore { get; set; }
       public AnswerBase UserAnswer { get; set; }
       public AnswerBase QuestionAnswer { get; set; }
       public QuestionType QuestionType { get; set; }

       /// <summary>
       /// 适合于试题考生结果的试题反馈
       /// </summary>
       public string QuestionFeedback { get; set; }
       /// <summary>
       /// 适合于试题考生结果的选项反馈
       /// </summary>
       public IList<OptionFeedback> OptionFeedbacks { get; set; }
   }
}