// File:    JudgementQuestionLogic.cs
// Author:  Administrator
// Created: 2011年12月16日 10:27:24
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

       #region --属性--
       /// <summary>
       /// 对试题基本信息的接口
       /// </summary>
       public ICommonQuestionLogic CommonQuestionLogic { get; set; }
       /// <summary>
       /// 选项的服务接口
       /// </summary>
       public IOptionServiceLogic OptionService { get; set; }
       #endregion

       #region IInitializingObject 成员

       public void AfterPropertiesSet()
       {
           
       }

       #endregion

       #region IMessageSourceAware 成员

       public IMessageSource MessageSource
       {
           get;
           set;
       }

       #endregion

       #region IQuestionLogic 成员

       [Transaction]
       public Guid AddQuestion(QuestionBase question)
       {
           ThrowNotValid(question, System.Guid.Empty);
           JudgementQuestion MyQuestion = (JudgementQuestion)question;

           MyQuestion.CommonQuestion = this.CommonQuestionLogic.AddQuestion(question.CommonQuestion);
           if (MyQuestion.CommonQuestion == null || MyQuestion.CommonQuestion.Question == null)
               return Guid.Empty;

           //添加试题选项
           if (MyQuestion.Options != null && MyQuestion.Options.Count > 0)
           {
               Array.ForEach<QuestionOption>(MyQuestion.Options.ToArray<QuestionOption>(),
                   x =>
                   {
                       x.QuestionID = MyQuestion.CommonQuestion.Question.QuestionID;
                       x.OptionID = this.OptionService.AddOption(x);
                   });
               //另一种方法
               //MyQuestion.Options.Select(
               //    x => 
               //    { 
               //        x.OptionID =this.OptionService.AddOption(x);
               //        return x;
               //    }).ToList<QuestionOption>();
           }
           //设置答案JSON字串，并保存答案
           MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
           //保存答案,答案中包含了选项的ID，所以需要最后保存
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
               sErrMsg = "未试题指定答案";
               return 1;
           }

           //检查答案中选项是否来自试题选项
           if (!MyQuestion.Options.Contains(MyQuestion.Answer.AnswerOption))
           {
               sErrMsg = "答案中选项未包含在试题选项中";
               return 2;
           }
           return 0;
       }
       /// <summary>
       /// 验证选择题中选项是否符合要求
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

           //检查选项标题是否唯一
           var LstTmp = from x in
                            (from item in LstOptions
                             group item by item.OptionCode)
                        where x.Count() > 1
                        select x;
           if (LstTmp.Count() > 0)
           {
               sErrMsg = "选项标题不唯一";
               return 1;
           }

           //检查选项中试题ID是否一致
           var LstTmp2 = from item in LstOptions
                         where item.OptionID != Guid.Empty && item.QuestionID != QuestionID
                         select item;
           if (LstTmp2.Count() > 0)
           {
               sErrMsg = "选项中试题ID与试题本身ID不一致";
               return 2;
           }
           return 0;
       }
       /// <summary>
       /// 对试题实体进行验证，未通过验证直接抛出异常
       /// </summary>
       /// <param name="question"></param>
       /// <param name="questionID"></param>
       private void ThrowNotValid(QuestionBase question, System.Guid questionID)
       {
           if (question == null || question.CommonQuestion == null || question.CommonQuestion.Question == null)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception("试题数据不完整"));
           }
           //检查类型
           if (!(question is JudgementQuestion))
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Type, new Exception("试题的类型非单选题"));
           }
           JudgementQuestion MyQuestion = (JudgementQuestion)question;
           //检查试题选项
           string sErrMsg = "";
           int nValid = this.ValidQuestionOptions(MyQuestion.Options, questionID, out sErrMsg);
           if (nValid != 0)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception(sErrMsg));
           }
           //验证答案是否合法
           nValid = this.ValidAnswer(MyQuestion, out sErrMsg);
           if (nValid != 0)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Data, new Exception(sErrMsg));
           }
       }
       public void Update(Guid questionID, QuestionBase newQuestion)
       {
           ThrowNotValid(newQuestion, questionID);
           //得到原试题
           JudgementQuestion MyOldQuestion = (JudgementQuestion)this.GetByID(questionID);
           if (MyOldQuestion == null || MyOldQuestion.CommonQuestion == null
               || MyOldQuestion.CommonQuestion.Question == null)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Not_Found, new Exception("试题已不存在，无法更新"));
           }

           JudgementQuestion MyQuestion = (JudgementQuestion)newQuestion;

           //进行更新
           this.CommonQuestionLogic.Update(questionID, newQuestion.CommonQuestion);
           #region --//处理试题选项部分
           //得到删除的
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
           //得到新增的
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

           //得到更新的
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

           #region --//重新保存答案--
           //设置答案JSON字串，并保存答案
           MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
           //保存答案,答案中包含了选项的ID，所以需要最后保存
           this.CommonQuestionLogic.UpdateAnswers(MyQuestion.CommonQuestion.Question.QuestionID,
               MyQuestion.Answer.Answer);
           #endregion
       }

       public void Delete(Guid questionID)
       {
           //删除试题
           this.CommonQuestionLogic.Delete(questionID);
           //删除选项
           this.OptionService.DeleteByQuestionID(questionID);
       }

       public void DeleteBatch(IList<Guid> questionIDs)
       {
           throw new NotImplementedException();
       }

       public QuestionBase GetByID(Guid questionID)
       {
           //得到一个单选题
           JudgementQuestion question = new JudgementQuestion();
           //得到试题基本信息
           CommonQuestion CommonQuestion = this.CommonQuestionLogic.GetByID(questionID);
           if (CommonQuestion == null || CommonQuestion.Question == null)
               return null;
           //检查试题类型是否正确
           if (CommonQuestion.Question.QuestionType != QuestionType.Judgement)
           {
               throw new ETMS.AppContext.BusinessException(Err_JudgementQuestion_Invalid_Type, new Exception("试题类型非单选题"));
           }

           question.CommonQuestion = CommonQuestion;
           //得到试题的选项
           IList<QuestionOption> LstOptions = this.OptionService.LoadAllInQuestion(questionID);
           question.Options = LstOptions;

           //试题答案的解析
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