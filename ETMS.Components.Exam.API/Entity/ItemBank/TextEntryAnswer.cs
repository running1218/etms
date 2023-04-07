// File:    TextEntryAnswer.cs
// Author:  Administrator
// Created: 2011��12��15�� 17:49:06
// Purpose: Definition of Class TextEntryAnswer

using System;
using System.Collections.Generic;


namespace ETMS.Components.Exam.API.Entity.ItemBank
{
   ///<summary>
   /// ������
   ///</summary>
   [Serializable]
   public class TextEntryAnswer : AnswerBase
   {
       //���л���Ĵ�
       private string answer;

       //��ʵ���༯��
       private IList<TextEntry> answerList;

       /// <summary>
       /// ���캯��
       /// </summary>
       /// <param name="answers">���༯��</param>
       public TextEntryAnswer(IList<TextEntry> answers)
       {
           if(answers==null)
             throw new ETMS.AppContext.BusinessException("�𰸲���Ϊnull");
           else if(answers.Count==0)
               throw new ETMS.AppContext.BusinessException("�𰸲���ΪEmpty");

           this.answerList = answers;
           this.answer = AnswerBase.Serialize(answers);
       }
       /// <summary>
       /// ���캯��
       /// </summary>
       /// <param name="answers">Json���ִ�</param>
       public TextEntryAnswer(string answers)
       {
           //if(String.IsNullOrEmpty(answers))
           //    throw new ETMS.AppContext.BusinessException("�𰸲���Ϊnull");

           this.answerList = AnswerBase.Deserialize<IList<TextEntry>>(answers);
           this.answer = answers;
       }


       ///<summary>
       /// �𰸵�Json�ִ�
       ///</summary>
       public override string Answer
       {
           get { return this.answer; }
       }

       /// <summary>
       /// ���ش𰸵�ʵ���༯��
       /// </summary>
       public IList<TextEntry> AnswerList {
           get { return this.answerList; }
       }
   }

   /// <summary>
   /// ������
   /// </summary>
   public class TextEntry
   {
       /// <summary>
       /// ��������ֵ
       /// </summary>
       public string EntryValue { get; set; }

       /// <summary>
       /// �������ı��
       /// </summary>
       public string EntryID { get; set; }
   }
}