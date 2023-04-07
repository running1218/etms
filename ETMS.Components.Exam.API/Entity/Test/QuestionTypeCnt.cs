// File:    QuestionTypeCnt.cs
// Author:  Administrator
// Created: 2012年1月12日 17:26:09
// Purpose: Definition of Class QuestionTypeCnt

using System;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 试卷中每种试题类型的试题数信息
    ///</summary>
    [Serializable]
   public class QuestionTypeCnt
   {
      ///<summary>
      /// 试题类型
      ///</summary>
      public QuestionType QuestionType
      {
         get;
         set;
      }
      
      ///<summary>
      /// 试题题数
      ///</summary>
      public  int Count
      {
         get;
         set;
      }
   
   }
}