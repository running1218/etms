using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.IDP.Implement.BLL;
using ETMS.Components.IDP.API.Entity;

public partial class IDP_ManageIDP_CloseIDP : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// ID
    /// </summary>
    public Guid PlanID
    {
        get
        {
            if (ViewState["PlanID"] == null)
            {
                ViewState["PlanID"] = Guid.Empty;
            }
            return (Guid)ViewState["PlanID"];
        }
        set
        {
            ViewState["PlanID"] = value;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PlanID = Request.QueryString["PlanID"].ToGuid();
            IDPInfoShow1.PlanID = PlanID;
        }
    }

    /// <summary>
    /// 关闭IDP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            IDP_PlanLogic logic = new IDP_PlanLogic();
            IDP_Plan plan = logic.GetById(PlanID);

            plan.CompletionRate = txtCompletionRate.Text.ToInt();
            plan.Evaluation = txtRemark.Text;
            plan.IsClose = true;
            plan.CloseTime = System.DateTime.Now;

            logic.Save(plan);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("IDP成功关闭！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}