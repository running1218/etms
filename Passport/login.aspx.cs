using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Security;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //调用登录
            try
            {
                //1、验证参数是否有效
                DefaultAuthenticator.ValidSignInParams(Request);
                string appsignInUrl = "";
                SignInPageData pd = new SignInPageData();

                //2、提取用户设置（cookie中）                   
                pd.LoadFromCookie();
                if (pd.LoginErrorNum > 0)
                {
                    liValidCode.Style.Add("display","block");
                }
                //3、尝试自动登录应用（不同应用之前切换时，如用户在A应用上已经登录，访问B应用是应该自动登录到B）
                appsignInUrl = DefaultAuthenticator.AutoSignIn();

                if (!string.IsNullOrEmpty(appsignInUrl))//完成自动登录
                {
                    if (appsignInUrl.IndexOf("login.aspx", StringComparison.InvariantCultureIgnoreCase) == -1)//防止login页面循环调用
                    {
                        Response.Redirect(appsignInUrl);
                        return;
                    }
                }
                //载入登录页(将用户的默认设置传入）                
            }
            //登录出错时处理
            catch (ETMS.AppContext.BusinessException ex)
            {
                SignInPageData pd = new SignInPageData();
                pd.LoadFromCookie();
                //载入登录页(将用户的默认设置传入）
                string message = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex);
                ETMS.Utility.JsUtility.AlertMessageBox(message);
                return;
            }
        }
    }
    protected void lkLogin_Click(object sender, EventArgs e)
    {
        SignInPageData pd = new SignInPageData();
        pd.LoadFromCookie();
        if(pd.LoginErrorNum>0)
        {
            if (Request.Form["validcode"] != null && !Request.Form["validcode"].Equals(ETMS.Utility.ValidCodeUtility.ValidateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                ETMS.Utility.JsUtility.AlertMessageBox("验证码错误！");
                return;
            }
        }

        string username = Request.Form["username"];
        string password = Request.Form["password"];
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
          
            if (isSaveUserName)
            {
                pd.UserName = username;
            }
            pd.IsSaveUserName = isSaveUserName;
            pd.IsAutoSignIn = isAutoSignIn;
            pd.SaveToCookie();
            //跳转到相应的应用Url
            Response.Redirect(appsignInUrl);
            return;
        }
        //登录出错时处理
        catch (ETMS.AppContext.BusinessException ex)
        {
            //pd.LoadFromCookie();
            pd.UserName = username;

            //添加登录的错误次数
            pd.LoginErrorNum += 1;
            pd.SaveToCookie();
            if (pd.LoginErrorNum > 0)
            {
                liValidCode.Style.Add("display", "block");
            }
            //载入登录页(将用户的默认设置传入） 
            string message = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex);
            ETMS.Utility.JsUtility.AlertMessageBox(message);
            return;
        }
    }
}