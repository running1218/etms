// File:    UserTestResult.cs
// Author:  Administrator
// Created: 2011��12��17�� 11:49:26
// Purpose: Definition of Class UserTestResult

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.Test
{
   ///<summary>
   /// �������Խ��
   ///</summary>
   [Serializable]
   public class UserTestResult
   {
      
      ///<summary>
      /// �Ծ�ķ���
      ///</summary>
      public IList<TestFeedback> TestFeedbacks
      {
         get;
         set;
      }
   
   }
}