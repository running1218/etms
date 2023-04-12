using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;

public partial class TraningPlan_TraningPlanResult_TraningPlanResultView : System.Web.UI.Page
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
        aBack.HRef = "TraningPlanResultList.aspx";
        PlanInfoView1.PlanID = PlanID;
        CourseListView1.PlanID = PlanID;
    }
}