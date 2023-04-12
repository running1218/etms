using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FCKEditor : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        Response.Write("FCKeditor1.Text=" + FCKeditor1.Text + "<br/>");
        Response.Write("FCKeditor2.Text=" + FCKeditor2.Text + "<br/>"); 
    }
}