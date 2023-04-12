using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;
using University.Mooc.AppContext;

namespace ETMS.Studying
{
    public class BaseUtility
    {
        public static int SiteOrganizationID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["SiteDomain"] == null || HttpContext.Current.Request.Cookies["SiteDomain"].Value.ToString().ToLower() != Domain)
                {
                    var organization = new OrganizationLogic().GetNodeByDomain(Domain);
                    var organizationID = organization == null ? System.Configuration.ConfigurationManager.AppSettings["BulletinOrganizationID"] : organization.OrganizationID.ToString();
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
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
            {
                cookie = new HttpCookie(name);
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

        public static string LogoImage
        {
            get {

                HttpCookie cookie = HttpContext.Current.Request.Cookies["LogoImage_" + SiteOrganizationID.ToString()];
                if (cookie == null)
                {
                    string logoimage = string.Empty;
                    var organization = new OrganizationLogic().GetNodeByID(SiteOrganizationID) as Organization;
                    if (!string.IsNullOrEmpty(organization.Logo))
                    {
                        logoimage = ETMS.Utility.StaticResourceUtility.GetOrgLogoFullPath(organization.Logo);
                    }
                    cookie = new HttpCookie("LogoImage_" + SiteOrganizationID.ToString());
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 12, 0, 0, 0);
                    cookie.Expires = dt.Add(ts);
                    cookie.Value = logoimage;
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                return HttpContext.Current.Request.Cookies["LogoImage_" + SiteOrganizationID.ToString()].Value;
            }
        }

        public static string FooterInfo
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["FooterInfo_" + SiteOrganizationID.ToString()];
                if (cookie == null)
                {
                    var organization = new OrganizationLogic().QueryByID(SiteOrganizationID);
                    cookie = new HttpCookie("FooterInfo_" + SiteOrganizationID.ToString());
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 12, 0, 0, 0);
                    cookie.Expires = dt.Add(ts);
                    cookie.Value = organization == null ? string.Empty : HttpUtility.UrlEncode(organization.FooterInfo, System.Text.Encoding.UTF8);
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["FooterInfo_" + SiteOrganizationID.ToString()].Value, System.Text.Encoding.UTF8);
            }
        }

        public static string Title
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Title_" + SiteOrganizationID.ToString()];
                if (cookie == null)
                {
                    var organization = new OrganizationLogic().QueryByID(SiteOrganizationID);
                    cookie = new HttpCookie("Title_" + SiteOrganizationID.ToString());
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 12, 0, 0, 0);
                    cookie.Expires = dt.Add(ts);
                    cookie.Value = organization == null ? string.Empty : HttpUtility.UrlEncode(organization.Title, System.Text.Encoding.UTF8);
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["Title_" + SiteOrganizationID.ToString()].Value, System.Text.Encoding.UTF8);
            }
        }

        public static OrganizationLimits Limits
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Limits_" + SiteOrganizationID.ToString()];
                if (cookie == null)
                {
                    var organization = new OrganizationLogic().QueryByID(SiteOrganizationID);
                    cookie = new HttpCookie("Limits_" + SiteOrganizationID.ToString());
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 12, 0, 0, 0);
                    cookie.Expires = dt.Add(ts);
                    cookie.Value = organization == null ? string.Empty : organization.MenuLimit;
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                string limits = HttpContext.Current.Request.Cookies["Limits_" + SiteOrganizationID.ToString()].Value;
                return limits != string.Empty ? JsonHelper.DeserializeObject<OrganizationLimits>(limits) : new OrganizationLimits();
            }
        }

        public static bool IsLogin
        {
            get
            {
                if (UserContext.Current == null || UserContext.Current.UserID == 0)
                    return false;
                else
                    return true;
            }
        }

        public static string CourseNoticeMark
        {
            get
            {
                return MarkSettings.Count < 1 ? "课程公告" : MarkSettings["CourseNoticeMark"] ?? "课程公告";
            }
        }

        public static string EvaluationMark
        {
            get
            {
                return MarkSettings.Count < 1 ? "测评" : MarkSettings["EvaluationMark"] ?? "测评";
            }
        }

        public static Dictionary<string, string> MarkSettings
        {
            get
            {
                string configs = System.Configuration.ConfigurationManager.AppSettings[Domain] ?? string.Empty;
                var items = configs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    var option = item.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    dic.Add(option[0], option[1]);
                }

                return dic;
            }
        }
    }
}