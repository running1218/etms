// File:    QuestionTypeCnt.cs
// Author:  Administrator
// Created: 2012��1��12�� 17:26:09
// Purpose: Definition of Class QuestionTypeCnt

using System;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �Ծ���ÿ���������͵���������Ϣ
    ///</summary>
    [Serializable]
   public class QuestionTypeCnt
   {
      ///<summary>
      /// ��������
      ///</summary>
      public QuestionType QuestionType
      {
         get;
         set;
      }
      
      ///<summary>
      /// ��������
      ///</summary>
      public  int Count
      {
         get;
         set;
      }
   
   }
}