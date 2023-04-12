using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_FunctionGroup_alert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.CloseWindow("function(){window.parent.location.href=window.parent.location.href;}");//刷新页面 
    }
}