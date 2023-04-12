using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Skip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = this.Request.Url.ToString();

        if (Request.QueryString["ru"].ToString().ToLower().Contains("login.aspx"))
        {            
            this.Response.Redirect(string.Format("LoginToStudying.aspx{0}", this.Request.Url.ToString().Substring(url.LastIndexOf("?"))));
        }
        else
        {
            this.Response.Redirect(string.Format("Login.aspx{0}", this.Request.Url.ToString().Substring(url.LastIndexOf("?"))));
        }
    }
}