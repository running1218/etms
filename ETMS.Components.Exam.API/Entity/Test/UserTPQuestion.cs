// File:    UserTPQuestion.cs
// Author:  Administrator
// Created: 2011��12��15�� 15:12:29
// Purpose: Definition of Class UserTPQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �û��Ծ�����ʵ��
    ///</summary>
    [Serializable]
   public class UserTPQuestion
   {
      
      ///<summary>
      /// �����Ծ�����ID
      ///</summary>
      public System.Guid UserTPQuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ����ID
      ///</summary>
      public System.Guid UserID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ����Ӧ�Ŀ����Ծ�ID
      ///</summary>
      public System.Guid ExamID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ��������Ӧ��ID
      ///</summary>
      public System.Guid QuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �����Ե�ID
      ///</summary>
      public System.Guid TestPaperID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ��������
      ///</summary>
      public ETMS.Components.Exam.API.Entity.ItemBank.QuestionType QuestionType
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �����׼��
      ///</summary>
      public string Answers
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �����û���
      ///</summary>
      public string UserAnswer
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �������
      ///</summary>
      public decimal QuestionScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ����÷�
      ///</summary>
      public decimal ExamScore
      {
         get;
         set;
      }
   
   }
}