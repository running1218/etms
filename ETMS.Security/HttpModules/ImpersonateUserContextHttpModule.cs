using System;

using System.Web;
using ETMS.AppContext;
namespace ETMS.Security
{
    /// <summary>
    /// HttpModule，在安全认证完成后，设置：模拟用户上下文
    /// </summary>
    public class ImpersonateUserContextHttpModule : IHttpModule
    { 
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //挂载应用生命周期内的事件
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            UserContext.Current = new UserContext()
            {
                UserID = 2,
                UserName = "orgadmin",
                CultureName = "zh-CN",//可以从cookie中读取设置  
                OrganizationID = 1,//机构ID 
                AppCode = "",//用户当前所在的应用编码
                IP = (sender as HttpApplication).Request.UserHostAddress
            };
        }
    }
}
