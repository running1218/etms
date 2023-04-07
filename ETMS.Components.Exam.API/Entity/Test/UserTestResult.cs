// File:    UserTestResult.cs
// Author:  Administrator
// Created: 2011年12月17日 11:49:26
// Purpose: Definition of Class UserTestResult

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.Test
{
   ///<summary>
   /// 考生考试结果
   ///</summary>
   [Serializable]
   public class UserTestResult
   {
      
      ///<summary>
      /// 试卷的反馈
      ///</summary>
      public IList<TestFeedback> TestFeedbacks
      {
         get;
         set;
      }
   
   }
}