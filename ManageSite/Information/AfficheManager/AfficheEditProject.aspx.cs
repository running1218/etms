using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Information_AfficheManager_AfficheEditProject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AfficheInfo1.Operation = 2;
        AfficheInfo1.InfoType = 2;
    }
}