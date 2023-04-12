using ETMS.Utility;
using Mobile.Web.App_Start;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class MyTrainController : Controller
    {
        // GET: MyTrain
        [AuthAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [AuthAttribute]
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult LivingStudy()
        {
            return View();
        }
        [AuthAttribute]
        public ActionResult DocumentPlay()
        {
            return View();
        }


        [HttpGet]
        public string GetTrainCourseList()
        {
            var UserID = Request["UserID"];
            var module = Request["Module"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="module",
                    Value=module,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Train/Course/{UserID}/{module}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetTrainEvaluationList()
        {
            var UserID = Request["UserID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Train/Evaluation/{UserID}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetResourceByTrainingItemCourse()
        {
            var UserID = Request["UserID"];
            var TrainingItemCourseID = Request["TrainingItemCourseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TrainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Train/Course/Resource/{TrainingItemCourseID}/{UserID}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetTrainCourseEvaluationList()
        {
            var UserID = Request["UserID"];
            var TrainingItemCourseID = Request["TrainingItemCourseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TrainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Train/Evaluation/{TrainingItemCourseID}/{UserID}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetUserCourseLivings()
        {
            var UserID = Request["UserID"];
            var TrainingItemCourseID = Request["TrainingItemCourseID"];
            var CourseID = Request["CourseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="userID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="trainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="courseID",
                    Value=CourseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Train/Living/{courseID}/{trainingItemCourseID}/{userID}";
            return request.Get(resource);
        }

        #region 问答

        public ActionResult AnswerList(Guid QuestionID, int UserID)
        {
            ViewBag.QuestionID = QuestionID;
            ViewBag.UserID = UserID;
            return View();
        }

        [HttpGet]
        public string GetQuestionList()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];
            var UserID = Request["UserID"];
            var PageSize = Request["PageSize"];
            var ContentID = Request["ContentID"];
            var LastQuestionID = Request["LastQuestionID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "TrainingItemCourseID",
                    Value = TrainingItemCourseID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "UserID",
                    Value = UserID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "PageSize",
                    Value = PageSize,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "ContentID",
                    Value = ContentID.ToGuid(),
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "LastQuestionID",
                    Value = string.IsNullOrWhiteSpace(LastQuestionID) ? Guid.Empty : LastQuestionID.ToGuid(),
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Question/QuestionList/{TrainingItemCourseID}/{UserID}/{PageSize}/{ContentID}/{LastQuestionID}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetAnswerList()
        {
            var QuestionID = Request["QuestionID"];
            var PageSize = Request["PageSize"];
            var LastAnswerID = Request["LastAnswerID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "QuestionID",
                    Value = QuestionID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "PageSize",
                    Value = PageSize,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "LastAnswerID",
                    Value = string.IsNullOrWhiteSpace(LastAnswerID) ? Guid.Empty : LastAnswerID.ToGuid(),
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Question/AnswerList/{QuestionID}/{PageSize}/{LastAnswerID}";
            return request.Get(resource);
        }

        [HttpPost]
        public string PostInsertAnswer()
        {
            var QuestionID = Request["QuestionID"];
            var UserID = Request["UserID"];
            var AnswerContent = Request["AnswerContent"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "QuestionID",
                    Value = QuestionID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "UserID",
                    Value = UserID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "AnswerContent",
                    Value = AnswerContent,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Question/InsertAnswer/{QuestionID}/{UserID}/{AnswerContent}";
            return request.Post(resource);
        }

        [HttpPost]
        public string PostInsertQuestion()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];
            var ContentID = Request["ContentID"];
            var UserID = Request["UserID"];
            var QuestionTitle = Request["QuestionTitle"];
            var QuestionContent = Request["QuestionContent"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "TrainingItemCourseID",
                    Value = TrainingItemCourseID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "ContentID",
                    Value = ContentID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "UserID",
                    Value = UserID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "QuestionTitle",
                    Value = QuestionTitle,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "QuestionContent",
                    Value = QuestionContent,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Question/InsertQuestion/{TrainingItemCourseID}/{ContentID}/{UserID}/{QuestionTitle}/{QuestionContent}";
            return request.Post(resource);
        }

        [HttpPost]
        public string PostRemoveQuestion()
        {
            var QuestionID = Request["QuestionID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "QuestionID",
                    Value = QuestionID,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Question/RemoveQuestion/{QuestionID}";
            return request.Post(resource);
        }


        #endregion


        #region 笔记

        public ActionResult Note(Guid NoteID)
        {
            ViewBag.NoteID = NoteID;
            return View();
        }

        [HttpGet]
        public string GetNoteList()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];
            var UserID = Request["UserID"];
            var PageSize = Request["PageSize"];
            var ContentID = Request["ContentID"];
            var LastNoteID = Request["LastNoteID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "TrainingItemCourseID",
                    Value = TrainingItemCourseID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "UserID",
                    Value = UserID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "PageSize",
                    Value = PageSize,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "ContentID",
                    Value = ContentID.ToGuid(),
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "LastNoteID",
                    Value = string.IsNullOrWhiteSpace(LastNoteID) ? Guid.Empty : LastNoteID.ToGuid(),
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Note/List/{TrainingItemCourseID}/{UserID}/{PageSize}/{ContentID}/{LastNoteID}";
            return request.Get(resource);
        }

        [HttpPost]
        public string PostInsertNote()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];
            var ContentID = Request["ContentID"];
            var UserID = Request["UserID"];
            var Title = Request["Title"];
            var NoteContent = Request["NoteContent"];
            var IsPublic = Request["IsPublic"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "TrainingItemCourseID",
                    Value = TrainingItemCourseID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "ContentID",
                    Value = ContentID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "UserID",
                    Value = UserID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "Title",
                    Value = Title,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "NoteContent",
                    Value = NoteContent,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "IsPublic",
                    Value = IsPublic,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Note/Insert/{TrainingItemCourseID}/{ContentID}/{UserID}/{Title}/{NoteContent}/{IsPublic}";
            return request.Post(resource);
        }
        
        [HttpPost]
        public string PostUpdateNote()
        {
            var NoteID = Request["NoteID"];
            var Title = Request["Title"];
            var NoteContent = Request["NoteContent"];
            var IsPublic = Request["IsPublic"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "NoteID",
                    Value = NoteID,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "Title",
                    Value = Title,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "NoteContent",
                    Value = NoteContent,
                    Type = ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key = "IsPublic",
                    Value = IsPublic,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Note/Update/{NoteID}/{Title}/{NoteContent}/{IsPublic}";
            return request.Post(resource);
        }

        [HttpPost]
        public string PostRemoveNote()
        {
            var NoteID = Request["NoteID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "NoteID",
                    Value = NoteID,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Note/Remove/{NoteID}";
            return request.Post(resource);
        }

        [HttpGet]
        public string GetNote()
        {
            var NoteID = Request["NoteID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key = "NoteID",
                    Value = NoteID,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Note/Note/{NoteID}";
            return request.Get(resource);
        }

        #endregion
    }
}