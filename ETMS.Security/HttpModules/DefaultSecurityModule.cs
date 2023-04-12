
using System;
using System.Web;
using System.Text;
using System.Drawing.Imaging;
using ETMS.Security.Properties;
using ETMS.AppContext;
using ETMS.Components.Basic.API;
namespace ETMS.Security
{
    /// <summary>
    /// 默认的统一认证、授权拦截模块
    /// </summary>
    public sealed class DefaultSecurityModule : IHttpModule
    {
        /// <summary>
        /// 需要执行的页面类型
        /// 去掉了, ".ashx" 
        /// </summary>
        public static string[] RunTypes = { ".aspx", ".asx", ".ashx"};
#if DEBUG
        /// <summary>
        /// 用于调试跟踪
        /// </summary>
        public static void DebugMethod()
        {
            string messsage = "";
            messsage += "";
        }
#endif

        /// <summary>
        /// MVC应用下静态资源路径
        /// </summary>
        internal static string MVCApplicationContentPath = "/Content/";

        #region IHttpModule 成员
        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {

        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="context">HttpApplication</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
        }
        #endregion

        #region EventHandler

        private void context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!IsStaticResourceRequest(request))//排除静态资源，仅限制动态资源
            {
                ITicket ticket = null;
                if (AuthenticateDirSettings.GetConfig().PageNeedAuthenticate())//受【保护资源区】
                {
                    //1、完成身份认证
                    ticket = DoAuthentication();
                    //2、构造用户上下文
                    SetUserContext(ticket);
                    //3、完成授权验证
                    if (PassportClientSettings.GetConfig().IsIntegrationAuthorization)//【需权限验证区】
                    {
                        DoAuthorization(ticket);
                    }
                }
                else //【公共资源区】，需要将已登录的用户上下文构造出来
                {
                    bool fromCookie;
                    ticket = PassportManager.GetTicket(out fromCookie);
                    //用户登录有效
                    if (ticket != null && ticket.IsValid())
                    {
                        SetUserContext(ticket);
                        //更新cookie
                        ticket.SaveToCookie();
                    }
                }
            }
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            //如果是注销请求，则提前处理
            if (request.Path.IndexOf(Common.C_LOGOFF_CALLBACK_VIRTUAL_PATH, StringComparison.OrdinalIgnoreCase) != -1)
            {
                HttpResponse response = HttpContext.Current.Response;
                //注销应用登录过程
                //1、清除应用登录认证Cookie
                PassportManager.ClearAppSignInCookie();
                //2、清除SessionID,防止用户状态数据交叉
                HttpCookie sessionCookie = request.Cookies["ASP.NET_SessionId"];
                if (sessionCookie != null)
                {
                    sessionCookie.Expires = DateTime.Now.AddDays(-1);
                    response.Cookies.Set(sessionCookie);
                }

                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                response.ContentType = "image/gif";
                try
                {
                    Resource.success.Save(response.OutputStream, ImageFormat.Gif);
                }
                catch (System.Exception)
                {
                    Resource.fail.Save(response.OutputStream, ImageFormat.Gif);
                }
                finally
                {
                    response.AddHeader("P3P", "CP-TST");
                    response.Flush();
                    response.End();
                }
            }
        }
        #endregion

        #region Helper
        /// <summary>
        /// 判断当前请求资源是否时是静态资源（开发环境时起作用，IIS部署后静态资源请求由IIS模块接管）
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>true</returns>
        public static bool IsStaticResourceRequest(HttpRequest request)
        {
            string fileType = System.IO.Path.GetExtension(request.Path);

            return !(Array.Exists<string>(RunTypes, new Predicate<string>(delegate(string item)
                 {
                     return item.Equals(fileType, StringComparison.InvariantCultureIgnoreCase);
                 })));
        }
        /// <summary>
        /// 设置用户上下文
        /// </summary>
        /// <param name="ticket"></param>
        private void SetUserContext(ITicket ticket)
        {
            if (ticket != null)
            {
                //设置用户上下文
                UserContext.Current = new UserContext()
                {
                    UserID = int.Parse(ticket.SignInInfo.UserID),
                    UserName = ticket.SignInInfo.UserName,
                    RealName = ticket.SignInInfo.RealName,
                    CultureName = "zh-CN",//可以从cookie中读取设置  
                    OrganizationID = int.Parse(ticket.AppEnvironment),//用户当前所在的机构              
                    AppCode = PassportClientSettings.GetConfig().AppID,//用户当前所在的应用编码
                    IP = ticket.AppSignInIP,//用户登录时的IP
                    RequestUrl = HttpContext.Current.Request.RawUrl,//请求url地址
                    AppAsignID = ticket.AppSignInSessionID
                };

                //设置用户在线状态 
                //ServiceRepository.OnlineUserStateService.SetUserOnlineState(UserContext.Current.UserID, UserContext.Current.AppCode);
            }
        }

        /// <summary>
        /// 执行安全认证检查
        /// </summary>
        private ITicket DoAuthentication()
        {
            AuthenticateDirElement aDir = AuthenticateDirSettings.GetConfig().AuthenticateDirs.GetMatchedElement<AuthenticateDirElement>();
            bool autoRedirect = (aDir == null || aDir.SelfAuthenticated == false);
            //1、检查用户身份认证，如果未登录，根据策略进行跳转，否则，更新cookie
            ITicket ticket = PassportManager.CheckAuthenticated(autoRedirect);
            /*
             * 作者：薛永波
             * 原因：如果当前请求中包含票据t参数，为了url有好性，需要去除票据参数t
             * 时间：2011-5-19
             */
            //2、优化url处理
            this.RemoveTicketParmFromUrl();
            return ticket;
        }

        /// <summary>
        /// 授权验证过程，如果未被授权，则抛出异常！
        /// </summary>
        private void DoAuthorization(ITicket ticket)
        {
            /*
             * 权限验证过程：
             * 1、确定应用类型：单应用模式（所有机构共用一个应用），多应用模式（每个机构有独立的应用）
             * 1.1 单应用模式，读取应用配置：AppID，判断当前用户是否被授权
             * 1.2 多应用模式：
             *     1.2.1 cookie中读取当前用户是否已经选定机构
             *        1.2.1.1 用户【未】指定机构，则重定向url至用户机构选择页面
             *        1.2.1.2 用户【已】指定机构，则生成机构应用特有的编号（AppID_机构编号），判断当前用户是否被授权
             * 
             * 
             */
            //获取统一权限服务
            //IPassportPermissionLogic handler = ServiceRepository.PassportPermissionService;
            IPassportFacade handler = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            //用户请求
            HttpRequest request = HttpContext.Current.Request;

            //请求url做为功能编码，注意url不包含当前应用根路径，支持灵活部署。
            string funCode = request.RawUrl.Length > 1 ? request.RawUrl.Substring(request.ApplicationPath.Length + 1) : "";
            string appID = ETMS.Security.PassportClientSettings.GetConfig().AppID;

            //应用权限判定执行逻辑单元
            if (!PassportClientSettings.GetConfig().isCacheUserRole)//未启用用户角色列表缓存
            {
                //用户角色通过实时查询数据库来获取，性能上有影响
                if (!handler.DoesUserHasPermission(appID, UserContext.Current.UserID, funCode))
                {
                    throw new AuthorizationException(request.Url.PathAndQuery);
                }
            }
            else//启用用户角色列表缓存
            {
                #region 用户角色通过cookie缓存，避免每次查询数据库获取角色列表，以提高性能
                if (string.IsNullOrEmpty(ticket.AppRoles))//如果当前应用票据中未设置用户角色列表，则进行设置
                {
                    ticket.AppRoles = handler.GetUserRoles(appID, UserContext.Current.UserID);
                    //更新cookie
                    ticket.SaveToCookie();
                }

                if (!handler.DoesUserHasPermissionWithAppRoles(appID, UserContext.Current.UserID, ticket.AppRoles, funCode))
                {
                    throw new AuthorizationException(request.Url.PathAndQuery);
                }
                #endregion
            }
        }

        /// <summary>
        /// 在完成统一认证之后，当前请求url中加入TicketParm。
        /// 在将ticket存入cookie后， url中的ticketParm实际是就可以移除了。
        /// 为了用户体验有好，实现url简洁化，需移除URL中TicketParm
        /// 移除的实现方式：构造新的url，重定向到客户端。
        /// </summary>
        private void RemoveTicketParmFromUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            if (!string.IsNullOrEmpty(request.QueryString[PassportManager.TicketParamName]))
            {
                string redirectUrl = "";
                if (request.QueryString.Count == 1)//仅包含票据t参数,则重新定向到请求路径
                {
                    redirectUrl = request.Path;
                }
                else//提起页面参数，排除票据t，重新定向
                {
                    System.Text.StringBuilder writer = new StringBuilder(request.Path);
                    bool isFirst = true;
                    foreach (string parm in request.QueryString)
                    {
                        if (!parm.Equals(PassportManager.TicketParamName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (isFirst)
                            {
                                writer.AppendFormat("?{0}={1}", parm, request.QueryString[parm]);
                                isFirst = false;
                            }
                            else
                            {
                                writer.AppendFormat("&{0}={1}", parm, request.QueryString[parm]);
                            }
                        }
                    }
                    redirectUrl = writer.ToString();
                }

                //处理完成后重定向到简洁的url
                response.Redirect(redirectUrl);
            }
        }

        #endregion

    }
}
