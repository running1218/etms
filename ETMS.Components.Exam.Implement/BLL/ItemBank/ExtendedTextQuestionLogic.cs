// File:    ExtendedTextQuestionLogic.cs
// Author:  Administrator
// Created: 2011年12月17日 10:07:11
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
    /// 问答题逻辑实现
    /// </summary>
    public class ExtendedTextQuestionLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
   {
       /// <summary>
       /// 通用试题操作接口
       /// </summary>
       public ICommonQuestionLogic CommonQuestionLogic { get; set; }

       #region IMessageSourceAware 成员
       public IMessageSource MessageSource
       {
           get;
           set;
       }
       #endregion
       #region IInitializingObject 成员
       public void AfterPropertiesSet()
       {
           if(this.CommonQuestionLogic == null)
               throw new Exception("please set CommonQuestionLogic Property First!");
       }
       #endregion

       /// <summary>
       /// 填加一个试题
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       [Transaction]
       public Guid AddQuestion(QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("创建问答题的数据错误!");

           //保存试题信息(输入的答案已被Json化)
           ExtendedTextQuestion tmp=(ExtendedTextQuestion)question;
           ExtendedTextAnswer answer = tmp.Answer;
           tmp.CommonQuestion.QuestionType = QuestionType.ExtendedText;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.AddQuestion(tmp.CommonQuestion);
           if(tmp.CommonQuestion == null || tmp.CommonQuestion.Question == null)
               return Guid.Empty;

           //得到已保存的试题ID
           Guid questionID = tmp.CommonQuestion.Question.QuestionID;
           return questionID;
       }

       /// <summary>
       /// 判断输入数据是否有效
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
       /// 修改试题信息
       /// </summary>
       /// <param name="questionID"></param>
       /// <param name="question"></param>
       [Transaction]
       public void Update(Guid questionID, QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("修改问答题的数据错误!");

           //设置答案
           //string str = question.CommonQuestion.Question.Answers;
           //ExtendedTextAnswer answer = new ExtendedTextAnswer(str);

           //更新试题信息
           ExtendedTextQuestion tmp = (ExtendedTextQuestion)question;
           ExtendedTextAnswer answer = tmp.Answer;
           tmp.CommonQuestion.QuestionType = QuestionType.ExtendedText;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.Update(questionID, tmp.CommonQuestion);
       }

       /// <summary>
       /// 删除一个试题
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
       /// 得到指定试题
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

                   //设置答案(从db中取出的是Json数据)
                   ExtendedTextAnswer answer = new ExtendedTextAnswer();
                   answer.Answer = question.CommonQuestion.Question.Answers;
                   question.Answer = answer;
               }
           }
           return question;
       }
   }
}