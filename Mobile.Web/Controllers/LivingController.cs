using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class LivingController : Controller
    {
        // GET: Living
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string ValidList()
        {
            var PageIndex = Request["PageIndex"];
            var PageSize = Request["PageSize"];

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
                    Key="OrgID",
                    Value=BaseUtility.SiteOrganizationID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/Valid/{OrgID}/{PageIndex}/{PageSize}";
            return request.Get(resource);
        }

        [HttpGet]
        public string PlayBackList()
        {
            var PageIndex = Request["PageIndex"];
            var PageSize = Request["PageSize"];

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
                    Key="OrgID",
                    Value=BaseUtility.SiteOrganizationID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/PlayBack/{OrgID}/{PageIndex}/{PageSize}";
            return request.Get(resource);
        }

        [HttpGet]
        public string LivingUrl()
        {
            var LivingID = Request["LivingID"];
            var UserID = Request["UserID"];
            var NikeName = Request["NikeName"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="LivingID",
                    Value=LivingID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="NikeName",
                    Value=NikeName,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/Address/{LivingID}/{UserID}/{NikeName}";
            return request.Get(resource);
        }

        [HttpGet]
        public string PlayBackUrl()
        {
            var LivingID = Request["LivingID"];
            var UserID = Request["UserID"];
            var NikeName = Request["NikeName"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="LivingID",
                    Value=LivingID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="UserID",
                    Value=UserID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="NikeName",
                    Value=NikeName,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/PlayBackUrl/{LivingID}/{UserID}/{NikeName}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetCourseLivings()
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
                },
                new ResourceParameter()
                {
                    Key="orgID",
                    Value=BaseUtility.SiteOrganizationID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/ItemsOfCourse/{courseID}/{orgID}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetLivingUrl()
        {
            var livingID = Request["livingID"];
            var userID = Request["userID"];
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="livingID",
                    Value=livingID,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="userID",
                    Value=userID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Living/LivingUrl/{livingID}/{userID}";
            return request.Get(resource);
        }
    }
}