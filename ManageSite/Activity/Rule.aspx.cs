using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Activity.Implement.BLL;

public partial class Activity_Rule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRule();
        }
    }

    private void GetRule()
    {
        var apprisialID = Request.QueryString["AppraisalID"].ToGuid();
        var entity = new AppraisalLogic().GetAppraisalByID(apprisialID);
        if (null != entity)
        {
            ltlRule.Text = entity.ReviewRule;
        }
    }
}