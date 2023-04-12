using ETMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginToStudying : System.Web.UI.Page
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
            //1、验证url参数是否有效
            DefaultAuthenticator.ValidSignInParams(Request);
            //2、验证用户输出参数：用户名、密码未空时，提示
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("用户名或密码为空！");
            }
            //剔除前后空白字符
            username = username.Trim();
            password = password.Trim();
            //登录验证
            string appsignInUrl = DefaultAuthenticator.SignIn(username, password, isSaveUserName, isAutoSignIn);
            //保存用户默认设置
            SignInPageData pd = new SignInPageData();
            if (isSaveUserName)
            {
                pd.UserName = username;
            }
            pd.IsSaveUserName = isSaveUserName;
            pd.IsAutoSignIn = isAutoSignIn;
            pd.SaveToCookie();
            //跳转到相应的应用Url
            Response.Redirect(appsignInUrl);
        }
        //登录出错时处理
        catch (ETMS.AppContext.BusinessException ex)
        {
            SignInPageData pd = new SignInPageData();
            pd.LoadFromCookie();
            pd.UserName = username;
            //载入登录页(将用户的默认设置传入） 
            string message = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex);
            lblMessage.Text = message;
        }
    }
}