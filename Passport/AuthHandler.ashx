<%@ WebHandler Language="C#" Class="AuthHandler" %>

using System;
using System.Web;
using ETMS.Security;
using ETMS.Utility;
using ETMS.AppContext;

public class AuthHandler : IHttpHandler {
    HttpContext currentContext = null;

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        currentContext = context;

        context.Response.Write(AuthUser());
        context.Response.End();
    }

    public string AuthUser()
    {
        string username = currentContext.Request.QueryString["username"];
        string password = currentContext.Request.QueryString["password"];

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return JsonHelper.GetParametersInValidJson();
        }
        //剔除前后空白字符
        username = username.Trim();
        password = password.Trim();
        try
        {
            //登录验证
            string appsignInUrl = DefaultAuthenticator.SignIn(username, password, false, false);
            //保存用户默认设置
            SignInPageData pd = new SignInPageData();
            pd.IsSaveUserName = false;
            pd.IsAutoSignIn = false;
            pd.SaveToCookie();
        }
        catch (BusinessException bx)
        {
            return JsonHelper.GetInvokeFailedJson(-1, bx.Message);
        }

        return JsonHelper.GetInvokeSuccessJson();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}