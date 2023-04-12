
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
    /// Ĭ�ϵ�ͳһ��֤����Ȩ����ģ��
    /// </summary>
    public sealed class DefaultSecurityModule : IHttpModule
    {
        /// <summary>
        /// ��Ҫִ�е�ҳ������
        /// ȥ����, ".ashx" 
        /// </summary>
        public static string[] RunTypes = { ".aspx", ".asx", ".ashx"};
#if DEBUG
        /// <summary>
        /// ���ڵ��Ը���
        /// </summary>
        public static void DebugMethod()
        {
            string messsage = "";
            messsage += "";
        }
#endif

        /// <summary>
        /// MVCӦ���¾�̬��Դ·��
        /// </summary>
        internal static string MVCApplicationContentPath = "/Content/";

        #region IHttpModule ��Ա
        /// <summary>
        /// ��������
        /// </summary>
        public void Dispose()
        {

        }
        /// <summary>
        /// ��ʼ������
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
            if (!IsStaticResourceRequest(request))//�ų���̬��Դ�������ƶ�̬��Դ
            {
                ITicket ticket = null;
                if (AuthenticateDirSettings.GetConfig().PageNeedAuthenticate())//�ܡ�������Դ����
                {
                    //1����������֤
                    ticket = DoAuthentication();
                    //2�������û�������
                    SetUserContext(ticket);
                    //3�������Ȩ��֤
                    if (PassportClientSettings.GetConfig().IsIntegrationAuthorization)//����Ȩ����֤����
                    {
                        DoAuthorization(ticket);
                    }
                }
                else //��������Դ��������Ҫ���ѵ�¼���û������Ĺ������
                {
                    bool fromCookie;
                    ticket = PassportManager.GetTicket(out fromCookie);
                    //�û���¼��Ч
                    if (ticket != null && ticket.IsValid())
                    {
                        SetUserContext(ticket);
                        //����cookie
                        ticket.SaveToCookie();
                    }
                }
            }
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            //�����ע����������ǰ����
            if (request.Path.IndexOf(Common.C_LOGOFF_CALLBACK_VIRTUAL_PATH, StringComparison.OrdinalIgnoreCase) != -1)
            {
                HttpResponse response = HttpContext.Current.Response;
                //ע��Ӧ�õ�¼����
                //1�����Ӧ�õ�¼��֤Cookie
                PassportManager.ClearAppSignInCookie();
                //2�����SessionID,��ֹ�û�״̬���ݽ���
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
        /// �жϵ�ǰ������Դ�Ƿ�ʱ�Ǿ�̬��Դ����������ʱ�����ã�IIS�����̬��Դ������IISģ��ӹܣ�
        /// </summary>
        /// <param name="request">����</param>
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
        /// �����û�������
        /// </summary>
        /// <param name="ticket"></param>
        private void SetUserContext(ITicket ticket)
        {
            if (ticket != null)
            {
                //�����û�������
                UserContext.Current = new UserContext()
                {
                    UserID = int.Parse(ticket.SignInInfo.UserID),
                    UserName = ticket.SignInInfo.UserName,
                    RealName = ticket.SignInInfo.RealName,
                    CultureName = "zh-CN",//���Դ�cookie�ж�ȡ����  
                    OrganizationID = int.Parse(ticket.AppEnvironment),//�û���ǰ���ڵĻ���              
                    AppCode = PassportClientSettings.GetConfig().AppID,//�û���ǰ���ڵ�Ӧ�ñ���
                    IP = ticket.AppSignInIP,//�û���¼ʱ��IP
                    RequestUrl = HttpContext.Current.Request.RawUrl,//����url��ַ
                    AppAsignID = ticket.AppSignInSessionID
                };

                //�����û�����״̬ 
                //ServiceRepository.OnlineUserStateService.SetUserOnlineState(UserContext.Current.UserID, UserContext.Current.AppCode);
            }
        }

        /// <summary>
        /// ִ�а�ȫ��֤���
        /// </summary>
        private ITicket DoAuthentication()
        {
            AuthenticateDirElement aDir = AuthenticateDirSettings.GetConfig().AuthenticateDirs.GetMatchedElement<AuthenticateDirElement>();
            bool autoRedirect = (aDir == null || aDir.SelfAuthenticated == false);
            //1������û������֤�����δ��¼�����ݲ��Խ�����ת�����򣬸���cookie
            ITicket ticket = PassportManager.CheckAuthenticated(autoRedirect);
            /*
             * ���ߣ�Ѧ����
             * ԭ�������ǰ�����а���Ʊ��t������Ϊ��url�к��ԣ���Ҫȥ��Ʊ�ݲ���t
             * ʱ�䣺2011-5-19
             */
            //2���Ż�url����
            this.RemoveTicketParmFromUrl();
            return ticket;
        }

        /// <summary>
        /// ��Ȩ��֤���̣����δ����Ȩ�����׳��쳣��
        /// </summary>
        private void DoAuthorization(ITicket ticket)
        {
            /*
             * Ȩ����֤���̣�
             * 1��ȷ��Ӧ�����ͣ���Ӧ��ģʽ�����л�������һ��Ӧ�ã�����Ӧ��ģʽ��ÿ�������ж�����Ӧ�ã�
             * 1.1 ��Ӧ��ģʽ����ȡӦ�����ã�AppID���жϵ�ǰ�û��Ƿ���Ȩ
             * 1.2 ��Ӧ��ģʽ��
             *     1.2.1 cookie�ж�ȡ��ǰ�û��Ƿ��Ѿ�ѡ������
             *        1.2.1.1 �û���δ��ָ�����������ض���url���û�����ѡ��ҳ��
             *        1.2.1.2 �û����ѡ�ָ�������������ɻ���Ӧ�����еı�ţ�AppID_������ţ����жϵ�ǰ�û��Ƿ���Ȩ
             * 
             * 
             */
            //��ȡͳһȨ�޷���
            //IPassportPermissionLogic handler = ServiceRepository.PassportPermissionService;
            IPassportFacade handler = ApplicationContext.Current.ComponentRepository.GetBizComponentByID<IPassportFacade>();
            //�û�����
            HttpRequest request = HttpContext.Current.Request;

            //����url��Ϊ���ܱ��룬ע��url��������ǰӦ�ø�·����֧������
            string funCode = request.RawUrl.Length > 1 ? request.RawUrl.Substring(request.ApplicationPath.Length + 1) : "";
            string appID = ETMS.Security.PassportClientSettings.GetConfig().AppID;

            //Ӧ��Ȩ���ж�ִ���߼���Ԫ
            if (!PassportClientSettings.GetConfig().isCacheUserRole)//δ�����û���ɫ�б���
            {
                //�û���ɫͨ��ʵʱ��ѯ���ݿ�����ȡ����������Ӱ��
                if (!handler.DoesUserHasPermission(appID, UserContext.Current.UserID, funCode))
                {
                    throw new AuthorizationException(request.Url.PathAndQuery);
                }
            }
            else//�����û���ɫ�б���
            {
                #region �û���ɫͨ��cookie���棬����ÿ�β�ѯ���ݿ��ȡ��ɫ�б����������
                if (string.IsNullOrEmpty(ticket.AppRoles))//�����ǰӦ��Ʊ����δ�����û���ɫ�б����������
                {
                    ticket.AppRoles = handler.GetUserRoles(appID, UserContext.Current.UserID);
                    //����cookie
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
        /// �����ͳһ��֤֮�󣬵�ǰ����url�м���TicketParm��
        /// �ڽ�ticket����cookie�� url�е�ticketParmʵ���ǾͿ����Ƴ��ˡ�
        /// Ϊ���û������кã�ʵ��url��໯�����Ƴ�URL��TicketParm
        /// �Ƴ���ʵ�ַ�ʽ�������µ�url���ض��򵽿ͻ��ˡ�
        /// </summary>
        private void RemoveTicketParmFromUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            if (!string.IsNullOrEmpty(request.QueryString[PassportManager.TicketParamName]))
            {
                string redirectUrl = "";
                if (request.QueryString.Count == 1)//������Ʊ��t����,�����¶�������·��
                {
                    redirectUrl = request.Path;
                }
                else//����ҳ��������ų�Ʊ��t�����¶���
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

                //������ɺ��ض��򵽼���url
                response.Redirect(redirectUrl);
            }
        }

        #endregion

    }
}
