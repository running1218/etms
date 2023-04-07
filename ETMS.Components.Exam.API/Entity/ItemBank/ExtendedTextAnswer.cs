// File:    ExtendedTextAnswer.cs
// Author:  Administrator
// Created: 2011��12��15�� 17:49:07
// Purpose: Definition of Class ExtendedTextAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// �������
    ///</summary>
    [Serializable]
   public class ExtendedTextAnswer : AnswerBase
   {
       //���л���Ĵ�
       private string answer;

       /*
       /// <summary>
       /// ��������ID
       /// </summary>
       public Guid QuestionID { get; set; }
       */

       /// <summary>
       /// ���캯��
       /// </summary>
       /// <param name="answers"></param>
       public ExtendedTextAnswer(string answers)
       {
           //if(String.IsNullOrEmpty(answers))
           //    throw new ETMS.AppContext.BusinessException("�𰸲���ΪEmpty");

           this.answer = answers;
       }
       /// <summary>
       /// ���캯��
       /// </summary>
       public ExtendedTextAnswer()
       {
           this.Answer = string.Empty; 
       }
       ///<summary>
       /// �𰸵�Json�ִ�
       ///</summary>
       public override string Answer
       {
           get { return this.answer; }
           set { this.answer = value; }
       }
   }
}