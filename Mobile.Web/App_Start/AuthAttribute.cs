using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Mobile.Web.App_Start
{
    public class AuthAttribute: ActionFilterAttribute
    {
        /// <summary> 
        /// 用户是否登录验证 
        /// </summary> 
        /// <param name="filterContext"></param> 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var uid=HttpContext.Current.Request.Cookies["cookie_userid"]==null?string.Empty: HttpContext.Current.Request.Cookies["cookie_userid"].Value;
            if (string.IsNullOrEmpty(uid))
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}