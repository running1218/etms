using System;
using System.Collections.Generic;
using ETMS.Utility;
using University.Mooc.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

namespace ETMS.Studying
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect(WebUtility.AppPath + "/index.aspx");
            //}
        }
        protected void lkLogin_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"].Trim();
            string oripassword = Request.Form["password"].Trim();
            string password = this.Decode(oripassword);           
            if (username == "")
            {
                lblMsg.Text = "请输入用户名";
                return;
            }
            if (password == "")
            {
                lblMsg.Text = "请输入密码";
                return;
            }
            try
            {
                string redirectUrl = string.Format("{0}/Index.aspx", WebUtility.AppPath);
                User userInfo = new UserLogic().GetUserByLoginName(username.Trim());

                if (userInfo != null)
                {
                    new SignInPageData()
                    {
                        IsAutoSignIn = false,
                        IsSaveUserName = false,
                        UserName = username
                    }.SaveToCookie();

                    DefaultAuthenticator.SignIn(username, ETMS.Utility.Cryptography.MD5Utility.MD516(password), false, false);
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    lblMsg.Text = "用户不存在";
                }
            }
            catch (ETMS.AppContext.BusinessException ex)
            {
                lblMsg.Text = University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex);
            }
            catch (University.Mooc.AppContext.BusinessException ex)
            {
                lblMsg.Text = University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex);
            }
        }
        private string Decode(string rawStr)
        {
            int len = rawStr.Length;
            if (len % 4 != 0)
            {
                return "error!";
            }
            int curCharCode = 0;
            List<string> resultStr = new List<string>();
            for (int i = 0; i < len; i += 4)
            {
                curCharCode = Convert.ToInt32(rawStr.Substring(i, 4), 16);
                resultStr.Add(Convert.ToString((char)curCharCode));
            }
            return string.Concat(resultStr).Replace("\n", "\r\r").Replace("\r\r", "\r\n");
        }
    }
}