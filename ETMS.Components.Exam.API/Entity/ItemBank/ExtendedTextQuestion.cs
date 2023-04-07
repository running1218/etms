// File:    ExtendedTextQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 14:24:08
// Purpose: Definition of Class ExtendedTextQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// �ʴ���ʵ����
    /// </summary>
    [Serializable]
    public class ExtendedTextQuestion : QuestionBase
   {
        /// <summary>
       /// ���캯��
        /// </summary>
        public ExtendedTextQuestion()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="commQuestion"></param>
        public ExtendedTextQuestion(CommonQuestion commQuestion)
            : base(commQuestion){

        }

        /// <summary>
        /// �ʴ����.
        /// </summary>
        public ExtendedTextAnswer Answer { get; set; }

       /*
       /// <summary>
       /// ���캯��
       /// </summary>
       public ExtendedTextQuestion()
       {
           this.CommonQuestion = new CommonQuestion(QuestionType.ExtendedText);
       }
       /// <summary>
       /// ���������ͨ��������Ϣ
       /// </summary>
       //public CommonQuestion CommonQuestion { get; set; }
       */
   }
}