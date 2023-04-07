// File:    JudgementQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 14:24:06
// Purpose: Definition of Class JudgementQuestion

using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// �ж��������ʵ����
    /// </summary>
   [Serializable]
   public class JudgementQuestion:QuestionBase
   {
       /// <summary>
       /// �ж����.
       /// </summary>
       public JudgementAnswer Answer { get; set; }

       ///<summary>
       /// �ж������ѡ�ֻ�������ѡ�
       ///</summary>
       public IList<QuestionOption> Options
       {
           get;
           set;
       }
   }
}