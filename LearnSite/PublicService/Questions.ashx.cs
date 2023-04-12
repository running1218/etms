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
    /// Question 的摘要说明
    /// </summary>
    public class Questions : IHttpHandler
    {

        private HttpContext currentContext = null;

        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            var Method = context.Request["Method"];
            var pageIndex = context.Request["PageIndex"].ToInt();
            var pageSize = context.Request["PageSize"].ToInt();
            var sortExpression = context.Request["SortExpression"];
            var TrainingItemCourseID = context.Request["TrainingItemCourseID"].ToGuid();
            var ContentID = context.Request["ContentID"].ToGuid();
            var isPersonalQuestion = context.Request["IsPersonalQuestion"].ToInt();
            var questionContent = context.Request["QuestionContent"];
            var questionTitle = context.Request["QuestionTitle"];
            var questionID = context.Request["QuestionID"].ToGuid();

            switch (Method)
            {
                case "QuestionList"://问题列表
                    ReturnResponseContent(GetQuestionList(isPersonalQuestion, sortExpression, TrainingItemCourseID, ContentID));
                    break;
                case "QuestionDel"://删除问题
                    ReturnResponseContent(QuestionDel(questionID));
                    break;
                case "QuestionTmpl"://问题列表 模版
                    ReturnResponseContent(GetQuestionListTmpl(isPersonalQuestion, pageIndex, pageSize, sortExpression, TrainingItemCourseID, ContentID));
                    break;
                case "QuestionAdd"://添加问题
                    ReturnResponseContent(QuestionAdd(questionTitle, questionContent, TrainingItemCourseID, ContentID));
                    break;
                case "":
                    ReturnResponseContent(QuestionUpdate(questionTitle, questionContent, questionID));
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


        /// <summary>
        /// 问题列表 返回JSON
        /// </summary>
        private string GetQuestionList(int isPersonalQuestion, string sortExpression, Guid TrainingItemCourseID, Guid ContentID)
        {
            int totalRecordCount = 0;
            DataTable tab = new QA_QuestionLogic().GetPagedList(1, int.MaxValue - 1, UserContext.Current.UserID, isPersonalQuestion, sortExpression, TrainingItemCourseID, ContentID, out totalRecordCount);
            return JsonHelper.GetInvokeSuccessJson(tab);
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        private string QuestionDel(Guid questionID)
        {
            string msgStr = "";
            try
            {
                msgStr = new QA_QuestionLogic().Remove(questionID);
            }
            catch (BusinessException bizEx)
            {
                msgStr = JsonHelper.GetInvokeFailedJson(0, ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
            return msgStr;
        }
        /// <summary>
        /// 问题列表 返回JSON
        /// </summary>
        private string GetQuestionListTmpl(int isPersonalQuestion, int pageIndex, int pageSize, string sortExpression, Guid TrainingItemCourseID, Guid ContentID)
        {
            string questionStr = "{\"msg\":\"没有更多问题了！\"}";
            int totalRecordCount = 0;
            DataTable tab = new QA_QuestionLogic().GetPagedList(pageIndex, pageSize, UserContext.Current.UserID, isPersonalQuestion, sortExpression, TrainingItemCourseID, ContentID, out totalRecordCount);
            tab.Columns.Add("TotalRecordCount", typeof(int));
            if (totalRecordCount > 0 && tab.Rows.Count > 0)
            {
                string questionIDs = string.Empty;
                foreach (DataRow row in tab.Rows)
                {
                    questionIDs += row["QuestionID"].ToString() + ",";
                    row["TotalRecordCount"] = totalRecordCount;
                }
                int totalRecordCounta = 0;
                DataTable tabAnswer = new QA_AnswerLogic().GetPagedListTmpl(1, int.MaxValue - 1, questionIDs.TrimEnd(','), out totalRecordCounta);

                ArrayList qList = new ArrayList();
                foreach (DataRow qRow in tab.Rows)
                {
                    QAQuestionNew q = qRow.ToEntity<QAQuestionNew>();
                    ArrayList qaList = new ArrayList();
                    foreach (DataRow qaRow in tabAnswer.Select("QuestionID='" + q.QuestionID + "'"))
                    {
                        QAAnswerNew qaNew = qaRow.ToEntity<QAAnswerNew>();
                        qaNew.QuestionCreateID = q.UserID;
                        qaList.Add(qaNew);
                    }
                    q.QAAnswers = qaList;
                    qList.Add(q);
                }
                questionStr = JsonHelper.GetInvokeSuccessJson(qList);
            }
            return questionStr;
        }

        /// <summary>
        /// 添加问题 并返回当前对象JSON
        /// </summary>
        private string QuestionAdd(string questionTitle, string questionContent, Guid TrainingItemCourseID, Guid ContentID)
        {
            string questionStr = "{\"msg\":\"没有更多问题了！\"}";
            QA_QuestionLogic qlogic = new QA_QuestionLogic();
            QA_Question question = new QA_Question();
            question.QuestionID = Guid.NewGuid();
            question.TrainingItemCourseID = TrainingItemCourseID;
            question.ContentID = ContentID;
            question.UserID = UserContext.Current.UserID;
            question.QuestionTitle = questionTitle;
            question.QuestionContent = questionContent;
            question.CreateTime = DateTime.Now;
            question.AnswerCount = 0;
            qlogic.Insert(question);

            ArrayList qList = new ArrayList();
            DataTable tab = qlogic.GetByID(question.QuestionID);
            if (tab.Rows.Count > 0)
            {
                DataRow row = tab.Rows[0];
                QAQuestionNew q = new QAQuestionNew();
                q.QuestionID = row["QuestionID"].ToGuid();
                q.UserID = row["UserID"] != null ? (string.IsNullOrWhiteSpace(row["UserID"].ToString()) ? 0 : Convert.ToInt32(row["UserID"])) : 0;
                q.QuestionTitle = row["QuestionTitle"].ToString();
                q.QuestionContent = row["QuestionContent"].ToString();
                q.CreateTime = row["CreateTime"].ToDateTime();
                q.AnswerCount = row["AnswerCount"].ToInt();
                qList.Add(q);
                questionStr = JsonHelper.SerializeObject(qList);
            }
            return questionStr;
        }

        private string QuestionUpdate(string questionTitle, string questionContent, Guid questionID)
        {
            QA_QuestionLogic qlogic = new QA_QuestionLogic();
            var question = new QA_Question();
            question.QuestionTitle = questionTitle;
            question.QuestionContent = questionContent;
            qlogic.Update(question);
            return JsonHelper.GetInvokeSuccessJson();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    //问题扩展类
    public class QAQuestionNew : QA_Question
    {
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int PersonalUserID { get { return UserContext.Current.UserID; } }

        public string RealName { get; set; }

        public string LoginName { get; set; }

        /// <summary>
        /// 总问题数
        /// </summary>
        public int TotalRecordCount { get; set; }
    }

    //问题回答扩展类
    public class QAAnswerNew : QA_Answer
    {

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int PersonalUserID { get { return UserContext.Current.UserID; } }

        public string RealName { get; set; }

        public string LoginName { get; set; }


        /// <summary>
        /// 问题创建用户ID
        /// <summary>
        public int QuestionCreateID { get; set; }
        /// <summary>
        /// 是否教师
        /// <summary>
        public int IsTeacher { get; set; }
    }
}