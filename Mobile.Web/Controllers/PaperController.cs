using ETMS.Components.Exam.API.Entity.NewTestPaper;
using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class PaperController : Controller
    {
        // GET: Paper
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult HomeWork()
        {
            return View();
        }
        public ActionResult TestResult()
        {
            return View();
        }
        public ActionResult AnswerSheet()
        {
            return View();
        }
        [HttpGet]
        public string GetStudentPaperData()
        {

            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var TestPaperID = Request["TestPaperID"];//试卷ID
            var OnlineTestID = Request["OnlineTestID"];//测评【考试ID或者作业ID】 
            var StudentCourseID = Request["StudentCourseID"];//学生选课ID 
            var TestType = Request["TestType"];//试卷测试类型：2-在线作业；5-在线测试
            var StudentID = Request["StudentID"];//用户ID 

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="StudentID",
                    Value=StudentID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TrainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TestPaperID",
                    Value=TestPaperID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="OnlineTestID",
                    Value=OnlineTestID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="StudentCourseID",
                    Value=StudentCourseID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TestType",
                    Value=TestType,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Paper/{StudentID}/{TestPaperID}/{TrainingItemCourseID}/{OnlineTestID}/{StudentCourseID}/{TestType}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetStudentPaperResult()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var TestPaperID = Request["TestPaperID"];//试卷ID
            var UserExamID = Request["UserExamID"];//用户考试ID 
            var TestType = Request["TestType"];//试卷测试类型：2-在线作业；5-在线测试
            var StudentID = Request["StudentID"];//用户ID 
            if (string.IsNullOrEmpty(UserExamID))
            {
                UserExamID = Guid.Empty.ToString();
            }
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="StudentID",
                    Value=StudentID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TrainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TestPaperID",
                    Value=TestPaperID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="UserExamID",
                    Value=UserExamID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="TestType",
                    Value=TestType,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Paper/{StudentID}/{TestPaperID}/{TrainingItemCourseID}/{UserExamID}/{TestType}";
            return request.Get(resource);
        }
        [HttpPost]
        public string PostSubmitStudentPaper()
        {
            var jsonPaperData = Request["JsonPaperData"];//学生提交的试卷信息
            var StudentID = Request["StudentID"];//用户ID
            Paper paperData = JsonHelper.DeserializeObject<Paper>(jsonPaperData);

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="StudentID",
                    Value=StudentID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="paperData",
                    Value=paperData,
                    Type=ParameterType.RequestBody
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Paper/Submit/{StudentID}";
            return request.Post(resource);
        }
        [HttpPost]
        public string PostSaveStudentPaper()
        {
            string jsonPaperData = Request["JsonPaperData"];//学生保存的试卷信息
            int StudentID = Request["StudentID"].ToInt();//用户ID
            Paper paperData = JsonHelper.DeserializeObject<Paper>(jsonPaperData);

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="StudentID",
                    Value=StudentID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="paperData",
                    Value=paperData,
                    Type=ParameterType.RequestBody
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Paper/Save/{StudentID}";
            return request.Post(resource);
        }
    }
}