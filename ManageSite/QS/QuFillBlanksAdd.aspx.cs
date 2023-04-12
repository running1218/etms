using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QS_QuFillBlanksAdd : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        string operType = Request.QueryString["Action"];
        if (operType == "add")
        {
            QuFillBlanksEdit2.Action = ETMS.AppContext.OperationAction.Add;
        }
        else if (operType == "edit")
        {
            QuFillBlanksEdit2.Action = ETMS.AppContext.OperationAction.Edit;
        }
    }
}