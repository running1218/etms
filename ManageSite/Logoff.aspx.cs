using ETMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logoff : System.Web.UI.Page
{
    public string RedirctUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //强制清除存放在相同Cookie.Domain下各应用Ticket，防止多个用户登录之间互相影响！
        ETMS.Security.DefaultAuthenticator.ForceClearAllAppTicket();
        SignInPageData pd = new SignInPageData();
        pd.ClearSignInPageDataCookie();
        RedirctUrl = HttpUtility.UrlDecode(string.Format("http://{0}{1}", Request.Url.Authority, this.ResolveUrl("~/Default.aspx")));
    }
}