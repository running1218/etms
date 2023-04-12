using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string PostUserLoginQuit()
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
            string resource = "Api/User/Login/Quit/{UserID}";
            return request.Post(resource);
        }
        [HttpPost]
        public string PostUserLogin()
        {
            var UserName = Request["UserName"];
            var Password= Request["Password"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="UserName",
                    Value=UserName,
                    Type=ParameterType.UrlSegment
                },
                new ResourceParameter()
                {
                    Key="Password",
                    Value=Password,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/User/Login/{UserName}/{Password}";
            return request.Post(resource);
        }
        [HttpPost]
        public string PostUserLoginByID()
        {
            var uid = Request["uid"];

            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="uid",
                    Value=uid,
                    Type=ParameterType.UrlSegment
                }
            };
            request.Parameters = parameters;
            string resource = "Api/User/Login/{uid}";
            return request.Post(resource);
        }
    }
}