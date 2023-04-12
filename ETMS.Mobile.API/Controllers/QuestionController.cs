using ETMS.Components.NoteQuestion.API.Entity;
using ETMS.Components.NoteQuestion.Implement.BLL;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Question")]
    public class QuestionController : ApiController
    {
        [Route("QuestionList/{TrainingItemCourseID}/{UserID}/{PageSize}/{ContentID}/{LastQuestionID}", Name = "获取培训项目课程的问题列表信息")]
        public HttpResponseMessage GetQuestionList(Guid TrainingItemCourseID, int UserID, int PageSize, Guid ContentID, Guid? LastQuestionID)
        {
            if (LastQuestionID == Guid.Empty)
                LastQuestionID = null;
            var data = new QA_QuestionLogic().GetList(UserID, TrainingItemCourseID, PageSize, ContentID, LastQuestionID);
            return ResponseJson.GetSuccessJson(data);
        }

        [Route("AnswerList/{QuestionID}/{PageSize}/{LastAnswerID}", Name = "获取培训项目课程的回复列表信息")]
        public HttpResponseMessage GetAnswerList(Guid QuestionID, int PageSize, Guid? LastAnswerID)
        {
            if (LastAnswerID == Guid.Empty)
                LastAnswerID = null;
            var ds = new QA_AnswerLogic().GetList(QuestionID, PageSize, LastAnswerID);
            return ResponseJson.GetSuccessJson(new
            {
                Question = ds.Tables[0],
                Answer = ds.Tables[1]
            });
        }

        [Route("InsertAnswer/{QuestionID}/{UserID}/{AnswerContent}", Name = "新增回复")]
        public HttpResponseMessage PostInsertAnswer(Guid QuestionID, int UserID, string AnswerContent)
        {
            var answer = new QA_Answer();
            answer.AnswerID = Guid.NewGuid();
            answer.CreateTime = DateTime.Now;
            answer.AnswerContent = AnswerContent;
            answer.QuestionID = QuestionID;
            answer.UserID = UserID;
            new QA_AnswerLogic().Insert(answer);
            return ResponseJson.GetSuccessJson();
        }

        [Route("InsertQuestion/{TrainingItemCourseID}/{ContentID}/{UserID}/{QuestionTitle}/{QuestionContent}", Name = "新增问答")]
        public HttpResponseMessage PostInsertQuestion(Guid TrainingItemCourseID, Guid ContentID, int UserID, string QuestionTitle, string QuestionContent)
        {
            var question = new QA_Question();
            question.QuestionID = Guid.NewGuid();
            question.TrainingItemCourseID = TrainingItemCourseID;
            question.ContentID = ContentID;
            question.UserID = UserID;
            question.QuestionTitle = QuestionTitle;
            question.QuestionContent = QuestionContent;
            question.CreateTime = DateTime.Now;
            question.AnswerCount = 0;
            new QA_QuestionLogic().Insert(question);
            return ResponseJson.GetSuccessJson();
        }

        [Route("RemoveQuestion/{QuestionID}", Name = "删除问答")]
        public HttpResponseMessage PostRemoveQuestion(Guid QuestionID)
        {
            var result = new QA_QuestionLogic().Remove(QuestionID);
            return ResponseJson.GetSuccessJson(result);
        }
    }
}
