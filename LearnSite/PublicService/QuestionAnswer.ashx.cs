using University.Mooc.AppContext;
using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.BLL;
using ETMS.Utility;
using System;
using System.Collections;
using System.Data;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// QuestionAnswer 的摘要说明
    /// </summary>
    public class QuestionAnswer : IHttpHandler
    {
        private HttpContext currentContext = null;

        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            var Method = context.Request["Method"];
            var questionID = context.Request["QuestionID"].ToGuid();
            var answerID = context.Request["AnswerID"].ToGuid();
            string answerContent = context.Request["AnswerContent"];
            
            switch (Method)
            {
                case "QuestionAnswerList"://回答列表
                    ReturnResponseContent(GetQuestionAnswerList(questionID));
                    break;
                case "QuestionAnswerAdd"://添加回答
                    ReturnResponseContent(QuestionAnswerAdd(questionID, answerContent));
                    break;
                case "QuestionAnswerDel"://删除问题 回复
                    ReturnResponseContent(QuestionAnswerDel(answerID));
                    break;
            }
        }

        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }


        //回答列表 返回JSON
        private string GetQuestionAnswerList(Guid questionID)
        {
            QA_Question q = new QAQuestionNew();
            DataTable tabq = new QA_QuestionLogic().GetByID(questionID);
            if (tabq != null && tabq.Rows.Count > 0)
            {
                q = tabq.Rows[0].ToEntity<QA_Question>();
            }

            int totalRecordCount = 0;
            DataTable tab = new QA_AnswerLogic().GetPagedList(1, int.MaxValue - 1, new QA_Answer() { QuestionID = questionID }, out totalRecordCount);
            ArrayList qaList = new ArrayList();

            foreach (DataRow row in tab.Rows)
            {
                QAAnswerNew qaNew = row.ToEntity<QAAnswerNew>();
                qaNew.QuestionCreateID = q.UserID;
                qaList.Add(qaNew);
            }
            q.QAAnswers = qaList;
            return JsonHelper.GetInvokeSuccessJson(q);
        }

        //添加回复
        private string QuestionAnswerAdd(Guid questionID, string answerContent)
        {
            QA_AnswerLogic logic = new QA_AnswerLogic();
            QA_Answer answer = new QA_Answer();
            answer.AnswerID = Guid.NewGuid();
            answer.UserID = UserContext.Current.UserID;
            answer.AnswerContent = answerContent;
            answer.CreateTime = DateTime.Now;
            answer.QuestionID = questionID;
            logic.Insert(answer);

            QA_Answer answerTab = logic.GetByID(answer.AnswerID);
            QAQuestionNew q = new QAQuestionNew();
            ArrayList qaList = new ArrayList();
            qaList.Add(answerTab);
            q.QAAnswers = qaList;

            return JsonHelper.GetInvokeSuccessJson(q);
        }
        

        /// <summary>
        /// 删除问题 回复
        /// </summary>
        private string QuestionAnswerDel(Guid answerID)
        {
            string msgStr = "";
            try
            {
                new QA_AnswerLogic().Remove(answerID);
                msgStr = JsonHelper.GetInvokeSuccessJson(new
                {
                    Status = true,
                    Code = 1,
                    Message = "删除成功！"
                });
            }
            catch (AppContext.BusinessException bizEx)
            {
                msgStr = JsonHelper.GetInvokeFailedJson(0, Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
            return msgStr;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}