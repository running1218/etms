// File:    SingleChoiceQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 14:24:05
// Purpose: Definition of Class SingleChoiceQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// ��ѡ��ʵ����
   ///</summary>
    [Serializable]
    public class SingleChoiceQuestion : QuestionBase
   {
       public SingleChoiceQuestion()
       { }
       public SingleChoiceQuestion(CommonQuestion commQuestion)
           :base(commQuestion)
       {
       }
       /// <summary>
       /// ѡ�����.
       /// </summary>
       public SingleChoiceAnswer Answer { get; set; }

      ///<summary>
      /// ��ѡ�����ѡ��
      ///</summary>
      public IList<QuestionOption> Options
      {
         get;
         set;
      }
   }
}