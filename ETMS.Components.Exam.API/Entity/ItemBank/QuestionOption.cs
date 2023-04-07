// File:    QuestionOption.cs
// Author:  Administrator
// Created: 2011��12��15�� 9:38:28
// Purpose: Definition of Class QuestionOption

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// �����ѡ��ʵ����
    ///</summary>
    [Serializable]
   public class QuestionOption
   {
      
      ///<summary>
      /// ѡ���ID
      ///</summary>
      public System.Guid OptionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ѡ�����ڵ�����ID
      ///</summary>
      public System.Guid QuestionID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ѡ������ѡ����ID�����ڹ����⡢ƥ������Ч���������Ϳ���ΪNULL
      ///</summary>
      public System.Guid? OptionGroupTitleID
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ѡ���ı��⡣����ѡ����֮��A��B��C��D֮��
      /// �����ƥ���������⣬�������֣���1��2��3��4��
      ///</summary>
      public string OptionCode
      {
         get;
         set;
      }
      
      
      ///<summary>
      /// ѡ�����ݡ���һ�����ı��Ĵ���
      ///</summary>
      public string OptionContent
      {
         get;
         set;
      }
   }
}