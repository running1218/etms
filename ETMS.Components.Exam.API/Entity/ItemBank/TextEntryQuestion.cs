// File:    TextEntryQuestion.cs
// Author:  Administrator
// Created: 2011��12��16�� 14:24:07
// Purpose: Definition of Class TextEntryQuestion

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// �����ʵ����
    /// </summary>
    [Serializable]
    public class TextEntryQuestion : QuestionBase
   {
        /// <summary>
       /// ���캯��
        /// </summary>
        public TextEntryQuestion()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="commQuestion"></param>
        public TextEntryQuestion(CommonQuestion commQuestion)
            : base(commQuestion)
        {
        }

        /// <summary>
        /// �ʴ����.
        /// </summary>
        public TextEntryAnswer Answer { get; set; }

       /*
       /// <summary>
       /// ���캯��
       /// </summary>
       public TextEntryQuestion()
       {
           this.CommonQuestion = new CommonQuestion(QuestionType.TextEntry);
       }
       /// <summary>
       /// ���������ͨ��������Ϣ
       /// </summary>
       public CommonQuestion CommonQuestion { get; set; }
       */
   }
}