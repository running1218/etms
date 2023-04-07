// File:    UserTestPaper.cs
// Author:  Administrator
// Created: 2011��12��15�� 14:54:50
// Purpose: Definition of Class UserTestPaper

using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �������
    ///</summary>
    [Serializable]
   public class UserTestPaper
   {
      
      ///<summary>
      /// �����Ծ�ID
      ///</summary>
      public System.Guid ExamID
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
      /// ������ʹ�õ��Ծ�ID
      ///</summary>
      public System.Guid TestPaperID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ������ʹ�õ��Ծ�����
      ///</summary>
      public string TestPaperName
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �Ծ��ܷ�
      ///</summary>
      public decimal TestPaperScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �Ծ������
      ///</summary>
      public decimal PassingScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ��������
      ///</summary>
      public decimal AdjustedScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �����÷�
      ///</summary>
      public decimal ExamScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ���շ���
      ///</summary>
      public decimal FinalScore
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ����������ʱ�䡣����λ���룩
      ///</summary>
      public int TimeLimit
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �������Կ�ʼʱ��
      ///</summary>
      public DateTime? BeginExamTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �������Խ���ʱ��
      ///</summary>
      public DateTime? EndExamTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ������ʹ�õ�ʱ�䣨��λ���룩��
      ///</summary>
      public int TotalTime
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// �Ծ�����ļ���
      ///</summary>
      public string TestPapertlFileName
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ��������״̬
      ///</summary>
      public UserTestStatusType Status
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ������Ӧ�Ŀγ�ID
      ///</summary>
      public System.Guid? StudentSourceID
      {
         get;
         set;
      }
   
   }
}