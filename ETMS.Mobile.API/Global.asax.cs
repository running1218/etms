using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.Implement;
using ETMS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace ETMS.Mobile.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //加载应用上下文
            ApplicationContextLoader.Load(new LoadComponentsHandler[] {
                /*核心组件装配器*/
               BasicComponentsAssemble.DoAssemble
            });
        }
    }
}
