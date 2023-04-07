// File:    TextEntryQuestionLogic.cs
// Author:  Administrator
// Created: 2011年12月16日 10:27:25
// Purpose: Definition of Class TextEntryQuestionLogic

using System;
using System.Collections.Generic;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Transaction.Interceptor;
using mshtml;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class TextEntryQuestionLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
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
           if (this.CommonQuestionLogic == null)
               throw new Exception("please set CommonQuestionLogic Property First!");
       }
       #endregion


       /// <summary>
       /// 得到答案信息
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       private TextEntryAnswer ProcessQuestion(QuestionBase question)
       {
           /*
           //(注)使用正则解析
           //填空题的答案需分析题干来得到.
           string content = question.CommonQuestion.Question.QuestionTitle;
           //string regex = @"<input[^>]*entryid='(?<id>.+?)'\s+entryvalue='(?<value>.+?)'/>";
           //string regex = @"<input[^>]*entryid=[\""|'](?<id>.+?)[\""|']\s+entryvalue=[\""|'](?<value>.+?)[\""|'][^>]* />";
           string regex = @"<input[^>]*value=[\""|'](?<value>.+?)[\""|']\s+entryid=[\""|'](?<id>.+?)[\""|'][^>]* />";
           Regex reg = new Regex(regex, RegexOptions.IgnoreCase);
           Match match = reg.Match(content);

           //得到答案列表
           TextEntry result = null;
           List<TextEntry> answerList = new List<TextEntry>();
           while (match.Success)
           {
               result = new TextEntry();
               result.EntryID = match.Groups["id"].Value;
               result.EntryValue = match.Groups["value"].Value;
               answerList.Add(result);
               match = match.NextMatch();
           }

           //设置答案,并得到去掉答案的题干信息
           TextEntryAnswer answer = new TextEntryAnswer(answerList);
           //string title = Regex.Replace(content, @"entryvalue='[^>]*'", "");
           string title = Regex.Replace(content, @"value=[\""|'][^>]*[\""|']", "");
           question.CommonQuestion.Question.QuestionTitle = title;
           return answer;
           */
           //使用Dom解析
           string content = question.CommonQuestion.Question.QuestionTitle;
           List<TextEntry> answerList = new List<TextEntry>();
           IHTMLDocument2 doc = (IHTMLDocument2)new mshtml.HTMLDocument();
           doc.write(content);
           if (doc.body == null)
               throw new ETMS.AppContext.BusinessException("创建填空题时数据解析错误!");

           TextEntry result = null;
           foreach (IHTMLElement tmp in (IHTMLElementCollection)doc.body.all)
           {
               if (tmp.tagName == "INPUT")
               {
                   //string eid = tmp.getAttribute("entryid", 0).ToString();
                   //string vid = tmp.getAttribute("entryvalue", 0).ToString();
                   string eid = String.Empty;
                   if (tmp.getAttribute("entryid", 0) != null)
                       eid = tmp.getAttribute("entryid", 0).ToString();

                   string vid = String.Empty;
                   if (tmp.getAttribute("entryvalue", 0) != null)
                       vid = tmp.getAttribute("entryvalue", 0).ToString();

                   if (!String.IsNullOrEmpty(eid))
                   {
                       IHTMLInputElement t = (IHTMLInputElement)tmp;
                       result = new TextEntry();
                       result.EntryID = eid;
                       result.EntryValue = vid;
                       //result.EntryValue = t.value;
                       answerList.Add(result);
                   }
               }
           }

           //设置答案,并保留题干的答案信息
           TextEntryAnswer answer = new TextEntryAnswer(answerList);
           return answer;
       }

       /// <summary>
       /// 填加一个试题
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       public Guid AddQuestion(QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("创建填空题的数据错误!");

           //得到答案
           TextEntryAnswer answer = ProcessQuestion(question);

           //保存试题信息
           TextEntryQuestion tmp = (TextEntryQuestion)question;
           tmp.CommonQuestion.QuestionType = QuestionType.TextEntry;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.AddQuestion(tmp.CommonQuestion);
           if (tmp.CommonQuestion == null || tmp.CommonQuestion.Question == null)
               return Guid.Empty;

           //得到已保存的试题ID
           Guid questionID = tmp.CommonQuestion.Question.QuestionID;
           return questionID;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       private bool IsValid(QuestionBase question)
       {
           bool flag = true;
           if (question == null)
               flag = false;
           else if (!(question is TextEntryQuestion))
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
           TextEntryAnswer answer = ProcessQuestion(question);

           //更新试题信息
           TextEntryQuestion tmp = (TextEntryQuestion)question;
           tmp.CommonQuestion.QuestionType = QuestionType.TextEntry;
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
       /// 得到指定的试题信息
       /// </summary>
       /// <param name="questionID"></param>
       /// <returns></returns>
       [Transaction]
       public QuestionBase GetByID(Guid questionID)
       {
           TextEntryQuestion question = null;
           CommonQuestion CommonQuestion = this.CommonQuestionLogic.GetByID(questionID);
           if (CommonQuestion != null)
           {
               if (CommonQuestion.Question.QuestionType == QuestionType.TextEntry)
               {
                   question = new TextEntryQuestion();
                   question.CommonQuestion = CommonQuestion;

                   //设置答案(从db中取出的是Json数据)
                   string str = question.CommonQuestion.Question.Answers;
                   TextEntryAnswer answer = new TextEntryAnswer(str);
                   question.Answer = answer;
               }
           }
           return question;
       }

   }
}