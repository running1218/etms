using University.Mooc.Security;
using System;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;

namespace ETMS.Studying
{
    public partial class Login2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool isSaveUserName = false;
            bool isAutoSignIn = false;

            //调用登录
            try
            {
                //登录验证
                string redirectUrl = string.Format("{0}/Index.aspx", WebUtility.AppPath); ;
                User userInfo = new UserLogic().GetUserByLoginName(username);

                if (userInfo != null)
                {
                    new SignInPageData()
                    {
                        IsAutoSignIn = false,
                        IsSaveUserName = false,
                        UserName = username
                    }.SaveToCookie();

                    DefaultAuthenticator.SignIn(username, ETMS.Utility.Cryptography.MD5Utility.MD516(password), isSaveUserName, isAutoSignIn);
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    lblMessage.Text = "用户不存在";
                }
            }
            catch (ETMS.AppContext.BusinessException ex)
            {
                lblMessage.Text = University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex);
            }
            catch (University.Mooc.AppContext.BusinessException ex)
            {
                lblMessage.Text = University.Mooc.AppContext.BusinessException.GetBusinessErrorCode(ex);
            }
        }
    }
}