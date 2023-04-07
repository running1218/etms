// File:    JudgementQuestionLogic.cs
// Author:  Administrator
// Created: 2011��12��16�� 10:27:24
// Purpose: Definition of Class JudgementQuestionLogic

using System;
using System.Collections.Generic;
using Autumn.Context;
using Autumn.Objects.Factory;
using Autumn.Transaction.Interceptor;
using System.Linq;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    internal class JudgementQuestionLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
   {
       private static string Err_JudgementQuestion_Invalid_Type = "ItemBank.JudgementQuestion.Invalid.Type";
       private static string Err_JudgementQuestion_Invalid_Data = "ItemBank.JudgementQuestion.Invalid.Data";
       private static string Err_JudgementQuestion_Not_Found = "ItemBank.JudgementQuestion.Not.Found";

       #region --����--
       /// <summary>
       /// �����������Ϣ�Ľӿ�
       /// </summary>
       public ICommonQuestionLogic CommonQuestionLogic { get; set; }
       /// <summary>
       /// ѡ��ķ���ӿ�
       /// </summary>
       public IOptionServiceLogic OptionService { get; set; }
       #endregion

       #region IInitializingObject ��Ա

       public void AfterPropertiesSet()
       {
           
       }

       #endregion

       #region IMessageSourceAware ��Ա

       public IMessageSource MessageSource
       {
           get;
           set;
       }

       #endregion

       #region IQuestionLogic ��Ա

       [Transaction]
       public Guid AddQuestion(QuestionBase question)
       {
           ThrowNotValid(question, System.Guid.Empty);
           JudgementQuestion MyQuestion = (JudgementQuestion)question;

           MyQuestion.CommonQuestion = this.CommonQuestionLogic.AddQuestion(question.CommonQuestion);
           if (MyQuestion.CommonQuestion == null || MyQuestion.CommonQuestion.Question == null)
               return Guid.Empty;

           //�������ѡ��
           if (MyQuestion.Options != null && MyQuestion.Options.Count > 0)
           {
               Array.ForEach<QuestionOption>(MyQuestion.Options.ToArray<QuestionOption>(),
                   x =>
                   {
                       x.QuestionID = MyQuestion.CommonQuestion.Question.QuestionID;
                       x.OptionID = this.OptionService.AddOption(x);
                   });
               //��һ�ַ���
               //MyQuestion.Options.Select(
               //    x => 
               //    { 
               //        x.OptionID =this.OptionService.AddOption(x);
               //        return x;
               //    }).ToList<QuestionOption>();
           }
           //���ô�JSON�ִ����������
           MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
           //�����,���а�����ѡ���ID��������Ҫ��󱣴�
           this.CommonQuestionLogic.UpdateAnswers(MyQuestion.CommonQuestion.Question.QuestionID,
               MyQuestion.Answer.Answer);

           return MyQuestion.CommonQuestion.Question.QuestionID;
       }
       private int ValidAnswer(JudgementQuestion MyQuestion, out string sErrMsg)
       {
           sErrMsg = "";
           if (MyQuestion.Answer == null ||
               MyQuestion.Answer.AnswerOption == null)
           {
               sErrMsg = "δ����ָ����";
               return 1;
           }

           //������ѡ���Ƿ���������ѡ��
           if (!MyQuestion.Options.Contains(MyQuestion.Answer.AnswerOption))
           {
               sErrMsg = "����ѡ��δ����������ѡ����";
               return 2;
           }
           return 0;
       }
       /// <summary>
       /// ��֤ѡ������ѡ���Ƿ����Ҫ��
       /// </summary>
       /// <param name="LstOptions"></param>
       /// <param name="QuestionID"></param>
       /// <param name="sErrMsg"></param>
       /// <returns></returns>
       private int ValidQuestionOptions(IList<QuestionOption> LstOptions,
           System.Guid QuestionID, out string sErrMsg)
       {
           sErrMsg = "";
           if (LstOptions == null || LstOptions.Count <= 0)
               return 0;

           //���ѡ������Ƿ�Ψһ
           var LstTmp = from x in
                            (from item in LstOptions
                             group item by item.OptionCode)
                        where x.Count() > 1
                        select x;
           if (LstTmp.Count() > 0)
           {
               sErrMsg = "ѡ����ⲻΨһ";
               return 1;
           }

           //���ѡ��������ID�Ƿ�һ��
           var LstTmp2 = from item in LstOptions
                         where item.OptionID != Guid.Empty && item.QuestionID != QuestionID
                         select item;
           if (LstTmp2.Count() > 0)
           {
               sErrMsg = "ѡ��������ID�����Ȿ��ID��һ��";
               return 2;
           }
           return 0;
       }
       /// <summary>
       /// ������ʵ�������֤��δͨ����ֱ֤���׳��쳣
       /// </summary>
       /// <param name="question"></param>
       /// <param name="questionID"></param>
       private void ThrowNotValid(QuestionBase question, System.Guid questionID)
       {
           if (question == null || question.CommonQuestion == null || question.CommonQuestion.Question == null)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception("�������ݲ�����"));
           }
           //�������
           if (!(question is JudgementQuestion))
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Type, new Exception("��������ͷǵ�ѡ��"));
           }
           JudgementQuestion MyQuestion = (JudgementQuestion)question;
           //�������ѡ��
           string sErrMsg = "";
           int nValid = this.ValidQuestionOptions(MyQuestion.Options, questionID, out sErrMsg);
           if (nValid != 0)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception(sErrMsg));
           }
           //��֤���Ƿ�Ϸ�
           nValid = this.ValidAnswer(MyQuestion, out sErrMsg);
           if (nValid != 0)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception(sErrMsg));
           }
       }
       public void Update(Guid questionID, QuestionBase newQuestion)
       {
           ThrowNotValid(newQuestion, questionID);
           //�õ�ԭ����
           JudgementQuestion MyOldQuestion = (JudgementQuestion)this.GetByID(questionID);
           if (MyOldQuestion == null || MyOldQuestion.CommonQuestion == null
               || MyOldQuestion.CommonQuestion.Question == null)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Not_Found, new Exception("�����Ѳ����ڣ��޷�����"));
           }

           JudgementQuestion MyQuestion = (JudgementQuestion)newQuestion;

           //���и���
           this.CommonQuestionLogic.Update(questionID, newQuestion.CommonQuestion);
           #region --//��������ѡ���
           //�õ�ɾ����
           IList<QuestionOption> LstTemps = QuestionUtils.GetListDeleted(MyOldQuestion.Options, MyQuestion.Options);
           if (LstTemps != null && LstTemps.Count > 0)
           {
               LstTemps.Select(
                   x =>
                   {
                       this.OptionService.Delete(x.OptionID);
                       return x;
                   }).ToList<QuestionOption>();
           }
           //�õ�������
           LstTemps = QuestionUtils.GetListNew(MyOldQuestion.Options, MyQuestion.Options);
           if (LstTemps != null && LstTemps.Count > 0)
           {
               Array.ForEach<QuestionOption>(LstTemps.ToArray<QuestionOption>(),
                   x =>
                   {
                        x.QuestionID = questionID;
                       System.Guid OptionID = this.OptionService.AddOption(x);
                       x.OptionID = OptionID;
                   }
                   );
           }

           //�õ����µ�
           LstTemps = QuestionUtils.GetListUpdated(MyOldQuestion.Options, MyQuestion.Options);
           if (LstTemps != null && LstTemps.Count > 0)
           {
               LstTemps.Select(
                   x =>
                   {
                       this.OptionService.Update(x);
                       return x;
                   }).ToList<QuestionOption>();
           }
           #endregion

           #region --//���±����--
           //���ô�JSON�ִ����������
           MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
           //�����,���а�����ѡ���ID��������Ҫ��󱣴�
           this.CommonQuestionLogic.UpdateAnswers(MyQuestion.CommonQuestion.Question.QuestionID,
               MyQuestion.Answer.Answer);
           #endregion
       }

       public void Delete(Guid questionID)
       {
           //ɾ������
           this.CommonQuestionLogic.Delete(questionID);
           //ɾ��ѡ��
           this.OptionService.DeleteByQuestionID(questionID);
       }

       public void DeleteBatch(IList<Guid> questionIDs)
       {
           throw new NotImplementedException();
       }

       public QuestionBase GetByID(Guid questionID)
       {
           //�õ�һ����ѡ��
           JudgementQuestion question = new JudgementQuestion();
           //�õ����������Ϣ
           CommonQuestion CommonQuestion = this.CommonQuestionLogic.GetByID(questionID);
           if (CommonQuestion == null || CommonQuestion.Question == null)
               return null;
           //������������Ƿ���ȷ
           if (CommonQuestion.Question.QuestionType != QuestionType.Judgement)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Type, new Exception("�������ͷǵ�ѡ��"));
           }

           question.CommonQuestion = CommonQuestion;
           //�õ������ѡ��
           IList<QuestionOption> LstOptions = this.OptionService.LoadAllInQuestion(questionID);
           question.Options = LstOptions;

           //����𰸵Ľ���
           string sAnswer = CommonQuestion.Question.Answers;
           OptionAnswer OptionAnswer = AnswerBase.Deserialize<OptionAnswer>(sAnswer);
           if (OptionAnswer != null)
           {

               var LstAnswerOptions = LstOptions.Where(
                   x =>
                   {
                       return x.OptionID == OptionAnswer.OptionID;
                   }).ToList<QuestionOption>();
               if (LstAnswerOptions != null && LstAnswerOptions.Count > 0)
               {
                   JudgementAnswer oAnswer = new JudgementAnswer(sAnswer, LstAnswerOptions[0]);
                   question.Answer = oAnswer;
               }
           }
           return question;
       }

       #endregion
   }
}