using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class IDP_ManageIDP_IDPView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ManageDevelopment1.PlanID = Request.QueryString["PlanID"].ToGuid();
            ManageContent1.PlanID = Request.QueryString["PlanID"].ToGuid();
            IDPInfo1.PlanID = Request.QueryString["PlanID"].ToGuid();
        }
    }
}