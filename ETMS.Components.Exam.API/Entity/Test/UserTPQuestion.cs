// File:    UserTPQuestion.cs
// Author:  Administrator
// Created: 2011年12月15日 15:12:29
// Purpose: Definition of Class UserTPQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 用户试卷试题实体
    ///</summary>
    [Serializable]
   public class UserTPQuestion
   {
      
      ///<summary>
      /// 考生试卷试题ID
      ///</summary>
      public System.Guid UserTPQuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生ID
      ///</summary>
      public System.Guid UserID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 所对应的考生试卷ID
      ///</summary>
      public System.Guid ExamID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题所对应的ID
      ///</summary>
      public System.Guid QuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 所考试的ID
      ///</summary>
      public System.Guid TestPaperID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题类型
      ///</summary>
      public ETMS.Components.Exam.API.Entity.ItemBank.QuestionType QuestionType
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题标准答案
      ///</summary>
      public string Answers
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题用户答案
      ///</summary>
      public string UserAnswer
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题分数
      ///</summary>
      public decimal QuestionScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试题得分
      ///</summary>
      public decimal ExamScore
      {
         get;
         set;
      }
   
   }
}