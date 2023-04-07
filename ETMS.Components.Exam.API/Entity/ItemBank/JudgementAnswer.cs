// File:    JudgementAnswer.cs
// Author:  Administrator
// Created: 2011��12��15�� 19:10:20
// Purpose: Definition of Class JudgementAnswer

using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    ///<summary>
    /// �ж����
    ///</summary>
    ///<remarks>
    /// �ж�����һ���������͵ĵ�ѡ�⣬���Դ˴�ֱ�Ӵӵ�ѡ��𰸼̳ж���
    ///</remarks>
    [Serializable]
   public class JudgementAnswer : SingleChoiceAnswer
   {
        public JudgementAnswer(string sAnswer)
            : base(sAnswer)
        { }
       /// <summary>
       /// ����һ���ж����ʵ��.
       /// </summary>
       /// <remarks>
       /// 1,ǰ�˽ӿ����������캯��; <br></br>
       /// 2,���ڹ�������ʱ�������ַ������ѡ��һ�����
       /// </remarks>
       /// <param name="sAnswer">�𰸵��ַ�����ʾ</param>
       /// <param name="AnswerOption">��ѡ��</param>
       public JudgementAnswer(string sAnswer, QuestionOption AnswerOption)
           :base(sAnswer,AnswerOption)
       { }
       /// <summary>
       /// ����һ���ж����ʵ��
       /// </summary>
       /// <param name="AnswerOption">��ѡ��</param>
       public JudgementAnswer(QuestionOption AnswerOption)
           : base(AnswerOption)
       { }
   }
}