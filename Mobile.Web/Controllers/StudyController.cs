using ETMS.Utility;
using ETMS.Utility.Cryptography;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class StudyController : Controller
    {
        // GET: Study
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string GetStudyResourceContent()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var ResourceType = Request["ResourceType"];//资源类型
            var ResourceID = Request["ResourceID"];//资源ID 
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
                    Key="ResourceType",
                    Value=ResourceType,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="ResourceID",
                    Value=ResourceID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Resource/{TrainingItemCourseID}/{ResourceID}/{ResourceType}/{StudentID}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetUserStudyLastContent()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
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
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Resource/{TrainingItemCourseID}/{StudentID}";
            return request.Get(resource);
        }
        [HttpPost]
        public string PostInitializeStudyProgress()
        {
            var StudentID = Request["StudentID"];//用户ID
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var ResourceID = Request["ResourceID"];//资源ID

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
                    Key="ResourceID",
                    Value=ResourceID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Initialize/{TrainingItemCourseID}/{ResourceID}/{StudentID}";
            return request.Post(resource);
        }

        [HttpPost]
        public string PostTerminateStudyProgress()
        {
            var StudentID = Request["StudentID"];//用户ID
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var ResourceID = Request["ResourceID"];//资源ID
            var StudyProgress = Request["StudyProgress"];//学习进度
            var StartTime = Request["StartTime"];//学习的开始时间

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
                    Key="ResourceID",
                    Value=ResourceID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="StudyProgress",
                    Value=StudyProgress,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="StartTime",
                    Value=Base64Utility.Base64Encode(StartTime),
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Terminate/{TrainingItemCourseID}/{ResourceID}/{StudyProgress}/{StartTime}/{StudentID}";
            return request.Post(resource);
        }

        [HttpPost]
        public string PostCompletedStudyProgress()
        {
            var StudentID = Request["StudentID"];//用户ID
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var ResourceID = Request["ResourceID"];//资源ID
            var StudyProgress = Request["StudyProgress"];//学习进度

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
                    Key="ResourceID",
                    Value=ResourceID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="StudyProgress",
                    Value=StudyProgress,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Completed/{TrainingItemCourseID}/{ResourceID}/{StudyProgress}/{StudentID}";
            return request.Post(resource);
        }

        [HttpGet]
        public string GetUserStudyLiving()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];//培训项目课程ID
            var courseID = Request["courseID"];//
            var userID = Request["userID"];//用户ID 

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="userID",
                    Value=userID,
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
                    Key="courseID",
                    Value=courseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Study/Livings/{courseID}/{userID}/{trainingItemCourseID}";
            return request.Get(resource);
        }
    }
}