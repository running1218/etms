using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan;

public partial class TraningPlan_TraningPlan_Controls_PlanInfoView : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 计划ID
    /// </summary>
    public Guid PlanID
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

    /// <summary>
    /// 是否显示审核信息
    /// </summary>
    public bool IsAuditVisible
    {
        get
        {
            if (ViewState["IsAuditVisible"] == null)
            {
                ViewState["IsAuditVisible"] = true;
            }
            return (bool)ViewState["IsAuditVisible"];
        }
        set { ViewState["IsAuditVisible"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            bind();
        }
    }


    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Tr_PlanLogic planLogic = new Tr_PlanLogic();
        Tr_Plan plan = planLogic.GetById(PlanID);
        lblPlanCode.Text = plan.PlanCode;
        lblPlanName.Text = plan.PlanName;
        lblPlanTime.Text = plan.PlanBeginTime.ToDate()+" 至 "+ plan.PlanEndTime.ToDate();
        dlblPlanStatus.FieldIDValue = plan.PlanStatus.ToString();
        dlblTrainingLevel.FieldIDValue= plan.TrainingLevelID.ToString();
        Tr_Department.Visible = plan.TrainingLevelID == 1 ? false : true;
        dlblIsUse.FieldIDValue = plan.IsUse.ToString();
        lblDept.FieldIDValue = plan.DutyDeptID.ToString();
        dlblPlanType.FieldIDValue = plan.PlanTypeID.ToString();
        lblDutyUser.Text = plan.DutyUser;
        lblMobile.Text = plan.Mobile;
        lblEMAIL.Text = plan.EMAIL;
        lblBudgetFee.Text = plan.BudgetFee.ToString();
        lblStudentNum.Text = plan.StudentNum.ToString();
        lblCreateUser.Text = plan.CreateUser;
        lblCreateTime.Text = plan.CreateTime.ToDate();
        lblPlanTarget.Text = plan.PlanTarget;
        lblPlanObjectStudent.Text = plan.PlanObjectStudent;
        lblRemark.Text = plan.Remark;

        //是否显示审核信息
        tabAudit.Visible = IsAuditVisible;
        if (plan.PlanStatus != 10)
        {
            labAuditUser.Text = plan.AuditUser;
            labAuditTime.Text = plan.AuditTime.ToDate();
            labAuditOpinion.Text = plan.AuditOpinion;
        }
        if (plan.PlanStatus == 90)
        {
            lblModifyUser.Text = plan.ModifyUser;
            lblModifyTime.Text = plan.ModifyTime.ToDate();
            dlblPlanEndModeID.FieldIDValue = plan.PlanEndModeID.ToString();
            lblPlanEndRemark.Text = plan.PlanEndRemark;
        }
    }
}