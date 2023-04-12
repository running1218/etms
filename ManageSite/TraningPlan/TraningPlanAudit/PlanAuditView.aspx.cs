using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

public partial class TraningPlan_TraningPlanAudit_PlanAuditView : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 计划ID
    /// </summary>
    private Guid PlanID
    {
        get
        {
            if (ViewState["PlanID"] == null)
                ViewState["PlanID"] = Guid.Empty;
            return ViewState["PlanID"].ToGuid();
        }
        set
        {
            ViewState["PlanID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanID")))
        {
            PlanID = BasePage.getSafeRequest(this.Page, "PlanID").ToGuid();
        }
        aBack.HRef = "PlanAuditList.aspx";
        PlanInfoView1.PlanID = PlanID;
        CourseListView1.PlanID = PlanID;
    }
}