// File:    TextEntryQuestionLogic.cs
// Author:  Administrator
// Created: 2011��12��16�� 10:27:25
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
           if (this.CommonQuestionLogic == null)
               throw new Exception("please set CommonQuestionLogic Property First!");
       }
       #endregion


       /// <summary>
       /// �õ�����Ϣ
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       private TextEntryAnswer ProcessQuestion(QuestionBase question)
       {
           /*
           //(ע)ʹ���������
           //�����Ĵ������������õ�.
           string content = question.CommonQuestion.Question.QuestionTitle;
           //string regex = @"<input[^>]*entryid='(?<id>.+?)'\s+entryvalue='(?<value>.+?)'/>";
           //string regex = @"<input[^>]*entryid=[\""|'](?<id>.+?)[\""|']\s+entryvalue=[\""|'](?<value>.+?)[\""|'][^>]* />";
           string regex = @"<input[^>]*value=[\""|'](?<value>.+?)[\""|']\s+entryid=[\""|'](?<id>.+?)[\""|'][^>]* />";
           Regex reg = new Regex(regex, RegexOptions.IgnoreCase);
           Match match = reg.Match(content);

           //�õ����б�
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

           //���ô�,���õ�ȥ���𰸵������Ϣ
           TextEntryAnswer answer = new TextEntryAnswer(answerList);
           //string title = Regex.Replace(content, @"entryvalue='[^>]*'", "");
           string title = Regex.Replace(content, @"value=[\""|'][^>]*[\""|']", "");
           question.CommonQuestion.Question.QuestionTitle = title;
           return answer;
           */
           //ʹ��Dom����
           string content = question.CommonQuestion.Question.QuestionTitle;
           List<TextEntry> answerList = new List<TextEntry>();
           IHTMLDocument2 doc = (IHTMLDocument2)new mshtml.HTMLDocument();
           doc.write(content);
           if (doc.body == null)
               throw new ETMS.AppContext.BusinessException("���������ʱ���ݽ�������!");

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

           //���ô�,��������ɵĴ���Ϣ
           TextEntryAnswer answer = new TextEntryAnswer(answerList);
           return answer;
       }

       /// <summary>
       /// ���һ������
       /// </summary>
       /// <param name="question"></param>
       /// <returns></returns>
       public Guid AddQuestion(QuestionBase question)
       {
           if (!IsValid(question))
               throw new ETMS.AppContext.BusinessException("�������������ݴ���!");

           //�õ���
           TextEntryAnswer answer = ProcessQuestion(question);

           //����������Ϣ
           TextEntryQuestion tmp = (TextEntryQuestion)question;
           tmp.CommonQuestion.QuestionType = QuestionType.TextEntry;
           tmp.CommonQuestion.Question.Answers = answer.Answer;
           tmp.CommonQuestion.QuestionFeedback = null;
           tmp.CommonQuestion.LstOptionFeedbacks = null;
           this.CommonQuestionLogic.AddQuestion(tmp.CommonQuestion);
           if (tmp.CommonQuestion == null || tmp.CommonQuestion.Question == null)
               return Guid.Empty;

           //�õ��ѱ��������ID
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
           TextEntryAnswer answer = ProcessQuestion(question);

           //����������Ϣ
           TextEntryQuestion tmp = (TextEntryQuestion)question;
           tmp.CommonQuestion.QuestionType = QuestionType.TextEntry;
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
       /// �õ�ָ����������Ϣ
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

                   //���ô�(��db��ȡ������Json����)
                   string str = question.CommonQuestion.Question.Answers;
                   TextEntryAnswer answer = new TextEntryAnswer(str);
                   question.Answer = answer;
               }
           }
           return question;
       }

   }
}