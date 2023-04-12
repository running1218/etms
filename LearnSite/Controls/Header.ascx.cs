using System;
using System.Web;
using University.Mooc.AppContext;
using ETMS.Utility;
using University.Mooc.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility.Cryptography;
using System.Web.Security;

namespace ETMS.Studying.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        public bool IsLogin
        {
            get
            {
                if (UserContext.Current == null || UserContext.Current.UserID == 0)
                    return false;
                else
                {
                    if (UserContext.Current.UserOrgs == BaseUtility.SiteOrganizationID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 二维码跳转地址
        /// </summary>
        public string QRcodeText
        {
            get {
                if (IsLogin)
                {
                    return MobileAddress + "?callway=qrcode&uid="+ TripleDesCryptographyUtility.EncryptTextToString(UserContext.Current.UserID.ToString());
                }
                else
                {
                    return MobileAddress + "?callway=qrcode";
                }
            }
        }
        /// <summary>
        /// 移动端访问地址
        /// </summary>
        public string MobileAddress
        {
            get {
                return string.Format("http://{0}/mobile/home/index", BaseUtility.Domain);
            }
        }
        public string ImgUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserContext.Current == null || UserContext.Current.UserID == 0)
            {
                liLogin.Style.Add("display", "block");
                liLoginInfo.Style.Add("display", "none");
            }
            else
            {
                liLogin.Style.Add("display", "none");
                liLoginInfo.Style.Add("display", "block");
            }

            lblVistorNum.Text = (Application["total"].ToInt() + WebUtility.DefaultVistorNum).ToString();

            User user = new UserLogic().GetUserBaseData(UserContext.Current.UserID);
            if (user != null)
            {
                ImgUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(user.PhotoUrl) ? "default.gif" : user.PhotoUrl);
            }

            imgLogo.ImageUrl = BaseUtility.LogoImage;
        }

        protected void btnQuit_Click(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;

            HttpResponse response = HttpContext.Current.Response;
            //注销应用登录过程
            //1、清除应用登录认证Cookie
            FormsAuthentication.SignOut();

            ClearAppSignInCookie();

            //2、清除用户登录信息
            new SignInPageData().ClearSignInPageDataCookie();
            //Session.Clear();
            //3、清除SessionID,防止用户状态数据交叉
            HttpCookie sessionCookie = request.Cookies["ASP.NET_SessionId"];
            if (sessionCookie != null)
            {
                sessionCookie.Expires = DateTime.Now.AddDays(-1);
                response.Cookies.Set(sessionCookie);
            }

            HttpCookie userSessionCookie = request.Cookies["User_SessionID"];
            if (userSessionCookie != null)
            {
                sessionCookie.Expires = DateTime.Now.AddDays(-1);
                response.Cookies.Set(sessionCookie);
            }
            var url = WebUtility.AppPath + "/Index.aspx";
            response.Redirect(url);
        }

        /// <summary>
        /// 清除应用的Cookie
        /// </summary>
        public static void ClearAppSignInCookie()
        {
            University.Mooc.Security.Common.CheckHttpContext();

            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpCookie cookie = request.Cookies["HTicket_"];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = null;
                response.SetCookie(cookie);
            }
        }
    }
}