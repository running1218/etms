using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;

public partial class TraningPlan_TraningPlanAudit_PlanAudit :BasePage
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
        PlanInfoView1.IsAuditVisible = false;
        CourseListView1.PlanID = PlanID;
    }

    /// <summary>
    /// 审核通过
    /// </summary>
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_PlanLogic logic = new Tr_PlanLogic();
            logic.Tr_Plan_Audit(PlanID, 20, ETMS.AppContext.UserContext.Current.RealName, labOpinion.Text.Trim());
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "计划审核成功！", "function(){window.location = '" + this.ActionHref("PlanAuditList.aspx") + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx)); 
            return;
        }
    }

    /// <summary>
    /// 审核不通过
    /// </summary>
    protected void btnDeny_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_PlanLogic logic = new Tr_PlanLogic();
            logic.Tr_Plan_Audit(PlanID, 40, ETMS.AppContext.UserContext.Current.RealName, labOpinion.Text.Trim());
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "操作成功！", "function(){window.location = '" + this.ActionHref("PlanAuditList.aspx") + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}