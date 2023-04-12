using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseWare_playcsf : System.Web.UI.Page
{
    protected string Url
    {
        get
        {
            return Request.QueryString["url"].ToString();
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}