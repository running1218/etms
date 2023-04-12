using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
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
        public string GetAnnouncementDetail()
        {
            var ArticleID = Request["ArticleID"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="ArticleID",
                    Value=ArticleID,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Announcement/{ArticleID}";
            return request.Get(resource);
        }
    }
}