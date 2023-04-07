// File:    MultipleChoiceQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 14:24:06
// Purpose: Definition of Class MultipleChoiceQuestion

using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// ��ѡ�������ʵ����
    /// </summary>
    public class MultipleChoiceQuestion:QuestionBase
   {
       public MultipleChoiceQuestion()
       { }
       public MultipleChoiceQuestion(CommonQuestion commQuestion)
           :base(commQuestion)
       {
       }
       /// <summary>
       /// �ж����.
       /// </summary>
       public MultipleChoiceAnswer Answer { get; set; }

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