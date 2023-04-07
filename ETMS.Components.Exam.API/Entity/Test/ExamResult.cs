// File:    ExamResult.cs
// Author:  Administrator
// Created: 2012��1��13�� 14:00:26
// Purpose: Definition of Class ExamResult

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
   ///<summary>
   /// ����Ŀ��Խ����Ϣ������������ID�������𰸡���׼�𰸡����ⷴ�����Ծ�������Ϣ
   ///</summary>
   [Serializable]
   public class ExamResult
   {
       public Guid QuestionID { get; set; }
       public decimal UserScore { get; set; }
       public decimal QuestionScore { get; set; }
       public AnswerBase UserAnswer { get; set; }
       public AnswerBase QuestionAnswer { get; set; }
       public QuestionType QuestionType { get; set; }

       /// <summary>
       /// �ʺ������⿼����������ⷴ��
       /// </summary>
       public string QuestionFeedback { get; set; }
       /// <summary>
       /// �ʺ������⿼�������ѡ���
       /// </summary>
       public IList<OptionFeedback> OptionFeedbacks { get; set; }
   }
}