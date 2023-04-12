using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Activity.Implement.BLL;

public partial class ActivityMarking : System.Web.UI.Page
{
    ProductionLogic logic = new ProductionLogic();
    public Guid AppraisalID { get { return Request.QueryString["AppraisalID"].ToGuid(); } }
    public int GrouplID { get { return Request.QueryString["GroupID"].ToInt(); } }
    public int TypeID { get { return Request.QueryString["ProductType"].ToInt(); } }
    public string RuleUrl { get { return this.ActionHref(string.Format("{0}/activity/rule.aspx?AppraisalID={1}", WebUtility.AppPath, AppraisalID)); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProductions();
        }
    }

    private void GetProductions()
    {
        var data = logic.GetProductions(AppraisalID, GrouplID, TypeID);
        rptProduction.DataSource = data;
        rptProduction.DataBind();
    }
}