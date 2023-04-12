using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class FeeDetailsEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FeeDetails.Action = ETMS.AppContext.OperationAction.Edit;
        FeeDetails.FeeCostDetailID = Request.QueryString["FeeCostDetailID"].ToGuid();
    }
}