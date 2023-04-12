using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Security;
using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;

namespace University.Mooc.Security
{
    /// <summary>
    /// 尹震海新做的统一认证、授权拦截模块
    /// </summary>
    public sealed class SSOSecurityModule : IHttpModule
    {
        /// <summary>
        /// 需要执行的页面类型
        /// </summary>
        public static string[] RunTypes = { ".aspx", ".asx", ".ashx" };
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
            context.AuthorizeRequest += new EventHandler(context_AuthenticateRequest);
        }
        #endregion

        #region EventHandler

        private void context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!IsStaticResourceRequest(request))//排除静态资源，仅限制动态资源
            {
                if (AuthenticateDirSettings.GetConfig().PageNeedAuthenticate())//受【保护资源区】
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        SetUserContext();
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(string.Format("{0}?ru={1}"
                            ,FormsAuthentication.LoginUrl
                            ,System.Web.HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString())));
                    }
                    if (!AuthenticateDirSettings.GetConfig().PageRolesNeedAuthenticate(UserContext.Current.UserType.ToString()))//【需权限验证区】
                    {
                        throw new AuthorizationException(request.Url.PathAndQuery);
                    }
                }
                else //【公共资源区】，需要将已登录的用户上下文构造出来
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        SetUserContext();
                    }
                }
            }
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 清除应用的Cookie
        /// </summary>
        public static void ClearAppSignInCookie()
        {
            Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpCookie cookie = request.Cookies[Common.UserContext_COOKIE];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = null;
                response.SetCookie(cookie);
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


        private void SaveToCookie(UserContext userContext, TimeSpan Timeout)
        {
            Common.CheckHttpContext();
            BinaryFormatter bf = new BinaryFormatter(); //声明一个序列化类
            MemoryStream ms = new MemoryStream(); //声明一个内存流
            bf.Serialize(ms, userContext); //执行序列化操作
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = System.Convert.ToBase64String(result);

            /*此处为关键步骤，将得到的字节数组按照一定的编码格式转换为字符串，不然当对象包含中文时，进行反序列化操作时会产生编码错误*/

            ms.Flush();
            ms.Close();
            HttpCookie cookie = new HttpCookie(Common.UserContext_COOKIE); //声明一个Key为person的Cookie对象
            cookie.Expires = DateTime.Now.Add(Timeout); //设置Cookie的有效期
            cookie.Value = temp; //将cookie的Value值设置为temp
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public UserContext GetFromCookie()
        {
            Common.CheckHttpContext();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Common.UserContext_COOKIE];
            if (cookie != null && cookie.Value != null && cookie.Value != string.Empty)
            {
                string result = cookie.Value;
                byte[] b = System.Convert.FromBase64String(result);  //将得到的字符串根据相同的编码格式分成字节数组
                MemoryStream ms = new MemoryStream(b, 0, b.Length);  //从字节数组中得到内存流
                BinaryFormatter bf = new BinaryFormatter();
                UserContext userContext = bf.Deserialize(ms) as UserContext;
                ms.Flush();
                ms.Close();
                return userContext;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 设置用户上下文
        /// </summary>
        private void SetUserContext()
        {
            UserContext userContext = GetFromCookie();
            if (userContext != null && userContext.UserID ==int.Parse(HttpContext.Current.User.Identity.Name))
            {
                UserContext.Current = userContext;
            }
            else
            {
                UserLogic userLogic = new UserLogic();
                User userInfo = userLogic.GetUserInfoByID(Int32.Parse(HttpContext.Current.User.Identity.Name));

                //设置用户上下文
                UserContext.Current = new UserContext()
                {
                    UserID = userInfo.UserID,
                    LoginName = userInfo.LoginName,
                    //NickName = userInfo.NickName,
                    RealName = userInfo.RealName,
                    //UserType = userInfo.UserType,
                    UserOrgs = userInfo.OrganizationID,                    
                    CultureName = "zh-CN",//可以从cookie中读取设置                     
                    IP = HttpContext.Current.Request.UserHostAddress,//用户登录时的IP
                    RequestUrl = HttpContext.Current.Request.RawUrl,//请求url地址
                    Roles =""// new UserRoleRelationLogic().GetUserRoles(userInfo.UserID)
                };
            }
            SaveToCookie(UserContext.Current, FormsAuthentication.Timeout);
        }
        /// <summary>
        /// 授权验证过程，如果未被授权，则抛出异常！
        /// </summary>
        private void DoAuthorization()
        {
            //用户请求
            HttpRequest request = HttpContext.Current.Request;
            //请求url做为功能编码，注意url不包含当前应用根路径，支持灵活部署。

            string funCode = "";
            if (request.ApplicationPath.Length >= request.RawUrl.Length)
            {
                funCode = "Default.aspx";
            }
            else
            {
                funCode = request.RawUrl.Substring(request.ApplicationPath.Length);
            }

            //RoleLogic RoleService = new RoleLogic();
            //if (!RoleService.IsAllowManageUserAccessURL(UserContext.Current.Roles, UserContext.Current.UserID, funCode))
            //{
            //    throw new AuthorizationException(request.Url.PathAndQuery);
            //}
        }
        #endregion
    }
}
