// File:    UserTestPaper.cs
// Author:  Administrator
// Created: 2011年12月15日 14:54:50
// Purpose: Definition of Class UserTestPaper

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 考生答卷
    ///</summary>
    [Serializable]
   public class UserTestPaper
   {
      
      ///<summary>
      /// 考生试卷ID
      ///</summary>
      public System.Guid ExamID
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
      /// 考生所使用的试卷ID
      ///</summary>
      public System.Guid TestPaperID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生所使用的试卷名称
      ///</summary>
      public string TestPaperName
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试卷总分
      ///</summary>
      public decimal TestPaperScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试卷及格分数
      ///</summary>
      public decimal PassingScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 调整分数
      ///</summary>
      public decimal AdjustedScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生得分
      ///</summary>
      public decimal ExamScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 最终分数
      ///</summary>
      public decimal FinalScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考试限制总时间。（单位：秒）
      ///</summary>
      public int TimeLimit
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生考试开始时间
      ///</summary>
      public DateTime? BeginExamTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生考试结束时间
      ///</summary>
      public DateTime? EndExamTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生已使用的时间（单位：秒）。
      ///</summary>
      public int TotalTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 试卷快照文件名
      ///</summary>
      public string TestPapertlFileName
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生考试状态
      ///</summary>
      public UserTestStatusType Status
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// 考生对应的课程ID
      ///</summary>
      public System.Guid? StudentSourceID
      {
         get;
         set;
      }
   
   }
}