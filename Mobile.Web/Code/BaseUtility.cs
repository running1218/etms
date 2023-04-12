using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobile.Web
{
    public class BaseUtility
    {
        public static int SiteOrganizationID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["SiteDomain"] == null || HttpContext.Current.Request.Cookies["SiteDomain"].Value.ToString().ToLower() != Domain)
                {
                    string organizationID = GetDomainOrganizationID(Domain);
                    AddCookie("DomainInfo", Domain, organizationID, 1);
                }

                return HttpContext.Current.Request.Cookies["DomainInfo"]["SiteOrganizationID"].ToInt();
            }
        }

        public static string Domain
        {
            get
            {
                return HttpContext.Current.Request.Url.Host.ToLower();
            }
        }

        private static void AddCookie(string name, string domain, string organizationID, int expires)
        {
            System.Web.HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
            {
                cookie = new System.Web.HttpCookie(name);
                DateTime dt = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 12, 0, 0, 0);
                cookie.Expires = dt.Add(ts);
                cookie.Values.Add("SiteDomain", domain);
                cookie.Values.Add("SiteOrganizationID", organizationID);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            else
            {
                cookie.Values["SiteDomain"] = domain;
                cookie.Values["SiteOrganizationID"] = organizationID;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        
        private static string GetDomainOrganizationID(string domain)
        {
            WebRequestHelper request = new WebRequestHelper(WebUtility.WebApiPath);
            List<ResourceParameter> parameters = new List<ResourceParameter>()
            {
                new ResourceParameter()
                {
                    Key="Domain",
                    Value=domain,
                    Type=ParameterType.QueryString
                }
            };
            request.Parameters = parameters;
            string resource = "Api/Organization/{Domain}";
            string jsonResult = request.Get(resource);
            RequestResult result = JsonHelper.DeserializeObject<RequestResult>(jsonResult);

            if (result != null && result.Status == true && result.Data != null)
            {
                return result.Data.OrganizationID;
            }

            return "0";
        }
    }

    public class RequestResult
    {
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public RequestResultData Data { get; set; }
    }

    public class RequestResultData
    {
        public string OrganizationID
        {
            get; set;
        }
    }
}