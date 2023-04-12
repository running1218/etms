using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Operation;
using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetBannerList()
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
            string resource = "Api/Banner/{OrgID}";
            return request.Get(resource);
        }
        
        [HttpGet]
        public string GetAnnouncementList()
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
            string resource = "Api/Announcement/{OrgID}/{PageSize}/{PageIndex}";
            return request.Get(resource);
        }

        [HttpGet]
        public string GetCourseRecommendList()
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
                    Key="SortRule",
                    Value="top",
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
            string resource = "Api/Course/{PageSize}/{PageIndex}/{SortRule}/{OrgID}";
            return request.Get(resource);
        }
    }
}