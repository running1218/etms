using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult LivingDetail()
        {
            return View();
        }
        public ActionResult DocumentPlay()
        {
            return View();
        }

        [HttpGet]
        public string GetCourseTypeList()
        {
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="OrgID",
                    Value=BaseUtility.SiteOrganizationID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Course/CourseType/{OrgID}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetCourseList()
        {
            var PageIndex = Request["PageIndex"];
            var PageSize = Request["PageSize"];
            var CourseName = Request["CourseName"];
            var CourseTypeID = Request["CourseTypeID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="PageIndex",
                    Value=PageIndex,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="PageSize",
                    Value=PageSize,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="SortRule",
                    Value="top",
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="CourseName",
                    Value=CourseName,
                    Type=ParameterType.QueryString
                },
                new ResourceParameter()
                {
                    Key="CourseTypeID",
                    Value=CourseTypeID,
                    Type=ParameterType.QueryString
                },
                new ResourceParameter()
                {
                    Key="OrgID",
                    Value=BaseUtility.SiteOrganizationID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Course/{PageSize}/{PageIndex}/{SortRule}/{OrgID}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetCourseResourceList()
        {
            var CourseID = Request["CourseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="CourseID",
                    Value=CourseID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter() {
                    Key = "OrgID",
                    Value =BaseUtility.SiteOrganizationID,
                    Type = ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Course/Catalog/{CourseID}/{OrgID}";
            return request.Get(resource);
        }
        [HttpGet]
        public string GetCourseResourceContent()
        {
            var CourseID = Request["CourseID"];
            var ResourceID = Request["ResourceID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="CourseID",
                    Value=CourseID,
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
            string resource = "Api/Course/Resource/{CourseID}/{ResourceID}";
            return request.Get(resource);
        }
        
        [HttpGet]
        public string GetCourseNoticeList()
        {
            var TrainingItemCourseID = Request["TrainingItemCourseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="TrainingItemCourseID",
                    Value=TrainingItemCourseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Course/Notice/{TrainingItemCourseID}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetCourseInfo()
        {
            var courseID = Request["courseID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="courseID",
                    Value=courseID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Course/Info/{courseID}";
            return request.Get(resource);
        }
    }
}