// File:    ExtendedTextQuestionLogic.cs
// Author:  Administrator
// Created: 2011��12��17�� 10:07:11
// Purpose: Definition of Class ExtendedTextQuestionLogic

using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    /// <summary>
    /// �ʴ����߼�ʵ��
    /// </summary>
    public class ExtendedTextQuestionLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
   {
       /// <summary>
       /// ͨ����������ӿ�
       /// </summary>
       public ICommonQuestionLogic CommonQuestionLogic { get; set; }

       #region IMessageSourceAware ��Ա
       public IMessageSource MessageSource
       {
           get;
           set;
       }
       #endregion
       #region IInitializingObject ��Ա
       public void AfterPropertiesSet()
       {
           if(this.CommonQuestionLogic == null)
               throw new Exception("please set CommonQuestionLogic Property First!");
       }
       #endregion

       /// <summary>
       /// ���һ������
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       [Transaction]
       public Guid AddQuestion(QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("�����ʴ�������ݴ���!");

           //����������Ϣ(����Ĵ��ѱ�Json��)
           ExtendedTextQuestion tmp=(ExtendedTextQuestion)question;
           ExtendedTextAnswer answer = tmp.Answer;
           tmp.CommonQuestion.QuestionType = QuestionType.ExtendedText;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.AddQuestion(tmp.CommonQuestion);
           if(tmp.CommonQuestion == null || tmp.CommonQuestion.Question == null)
               return Guid.Empty;

           //�õ��ѱ��������ID
           Guid questionID = tmp.CommonQuestion.Question.QuestionID;
           return questionID;
       }

       /// <summary>
       /// �ж����������Ƿ���Ч
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       private bool IsValid(QuestionBase question)
       {
           bool flag = true;
           if (question == null)
               flag = false;
           else if (!(question is ExtendedTextQuestion))
               flag = false;
           else if (question.CommonQuestion == null)
               flag = false;
           else if (question.CommonQuestion.Question == null)
               flag = false;

           return flag;
       }


       /// <summary>
       /// �޸�������Ϣ
       /// </summary>
       /// <param name="questionID"></param>
       /// <param name="question"></param>
       [Transaction]
       public void Update(Guid questionID, QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("�޸��ʴ�������ݴ���!");

           //���ô�
           //string str = question.CommonQuestion.Question.Answers;
           //ExtendedTextAnswer answer = new ExtendedTextAnswer(str);

           //����������Ϣ
           ExtendedTextQuestion tmp = (ExtendedTextQuestion)question;
           ExtendedTextAnswer answer = tmp.Answer;
           tmp.CommonQuestion.QuestionType = QuestionType.ExtendedText;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.Update(questionID, tmp.CommonQuestion);
       }

       /// <summary>
       /// ɾ��һ������
       /// </summary>
       /// <param name="questionID"></param>
       [Transaction]
       public void Delete(Guid questionID)
       {
           this.CommonQuestionLogic.Delete(questionID);
       }
       public void DeleteBatch(IList<Guid> questionIDs)
       {
       }

       /// <summary>
       /// �õ�ָ������
       /// </summary>
       /// <param name="questionID"></param>
       /// <returns></returns>
       [Transaction]
       public QuestionBase GetByID(Guid questionID)
       {
           ExtendedTextQuestion question = null;
           CommonQuestion CommonQuestion = this.CommonQuestionLogic.GetByID(questionID);
           if(CommonQuestion != null)
           {
               if (CommonQuestion.Question.QuestionType == QuestionType.ExtendedText)
               {
                   question = new ExtendedTextQuestion();
                   question.CommonQuestion = CommonQuestion;

                   //���ô�(��db��ȡ������Json����)
                   ExtendedTextAnswer answer = new ExtendedTextAnswer();
                   answer.Answer = question.CommonQuestion.Question.Answers;
                   question.Answer = answer;
               }
           }
           return question;
       }
   }
}