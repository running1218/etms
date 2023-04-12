using System;
using System.Text;
using System.Web;
using ETMS.AppContext;
namespace ETMS.Security
{
    /// <summary>
    /// Url安全保护模块（做为防止url篡改验证的一个校验模块） 
    /// 依赖：appSettings中增加url匹配规则
    /// </summary>
    public class UrlSecurityModule : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }
        /// <summary>
        /// 挂载事件
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //启用url安全处理时才需要进行配套的验证
            if (ETMS.Utility.HrefUtility.IsEnableSafeUrl)
            {
                context.PostAuthenticateRequest += new EventHandler(context_PostAuthenticateRequest);
            }
        }
        void context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpRequest request = application.Request;
            HttpResponse response = application.Response;
            string url = request.Path;
            //没有url参数或静态资源或首页“/”，不参与url验证，直接跳过。
            if (url == "/" || request.QueryString.Count == 0 || DefaultSecurityModule.IsStaticResourceRequest(request))//直接跳过
            {
                return;//直接跳过
            }
            //如果有且仅有一个url参数，而且没有key，则当做url随机参数处理，如default.aspx?=12313
            if (request.QueryString.Count == 1 && string.IsNullOrEmpty(request.QueryString.Keys[0]))
            {
                return;//直接跳过
            }
            //是否在排除url内
            if (Array.Exists<string>(ETMS.Utility.HrefUtility.ExcludeSafeUrlRules, (item) =>
            {
                return (url.ToLower().Contains(item.ToLower()));
            }))
            {
                return;//直接跳过
            }

            //获取令牌
            string iToken = request.QueryString["token"];
            if (string.IsNullOrWhiteSpace(iToken))
            {
                throw new ETMS.Security.InValidRequestException(url);
            }
            //检查参数
            StringBuilder parms = new StringBuilder();
            foreach (string key in request.QueryString.Keys)
            {
                //key 为null时通常是随即参数
                if (string.IsNullOrEmpty(key) || key.Equals("token", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                parms.AppendFormat("{0},", request.QueryString[key]);
            }
            string token = ETMS.Utility.HrefUtility.CreateToken(parms.ToString());
            if (!iToken.Equals(token, StringComparison.InvariantCultureIgnoreCase))
            {
                ETMS.Utility.Logging.ErrorLogHelper.Error(new Exception(string.Format("url={0}||iToken={1}||token={2}||UserID={3}", url, iToken, token, UserContext.Current.UserID)));
                throw new InValidRequestException("用户信息失效，请重新登录!", url);
            }
            else
            {
                return;
            }
        }
    }
}
