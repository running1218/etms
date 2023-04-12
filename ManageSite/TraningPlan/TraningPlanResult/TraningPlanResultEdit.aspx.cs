using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;

using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan;

public partial class TraningPlan_TraningPlanResult_TraningPlanResultEdit : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 计划ID
    /// </summary>
    public Guid PlanID {
        get {
            if (ViewState["PlanID"] == null)
                ViewState["PlanID"] = Guid.Empty;
            return ViewState["PlanID"].ToGuid();
        }
        set {
            ViewState["PlanID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanID")))
        {
            PlanID = BasePage.getSafeRequest(this.Page, "PlanID").ToGuid();
            bind();
        }
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind() {
        Tr_PlanLogic planLogic = new Tr_PlanLogic();
        Tr_Plan plan = planLogic.GetById(PlanID);
        if (plan != null) {
            lblPlanCode.Text = plan.PlanCode;
            lblPlanName.Text = plan.PlanName;
            dlblPlanTypeID.FieldIDValue = plan.PlanTypeID.ToString();
            lblPlanTime.Text = plan.PlanBeginTime.ToDate() + " 至 " + plan.PlanEndTime.ToDate();

            switch (plan.PlanStatus)
            {
                case 20://状态为审核通过时
                        ddl_PlanEndMode.Items.Add(new ListItem("正常结束", "1"));
                        ddl_PlanEndMode.Items.Add(new ListItem("异常结束", "2"));
                        ddl_PlanEndMode.Items.Add(new ListItem("审核通过结束", "3"));
                    break;
                case 40://状态为审核不通过时  只显示审核不通过结束
                    ddl_PlanEndMode.Items.Add(new ListItem("审核不通过结束", "4"));
                    break;
            }
            //ddl_PlanEndMode.Items.Insert(0, new ListItem("请选择", ""));
        }
    }

    /// <summary>
    /// 归档
    /// </summary>
    protected void btnFile_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_PlanLogic planLogic = new Tr_PlanLogic();
            planLogic.Tr_Plan_Achive(PlanID, ddl_PlanEndMode.Text.ToInt(), txtPlanEndRemark.Text, ETMS.AppContext.UserContext.Current.RealName);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("计划归档成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}