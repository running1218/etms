using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlan.Controls
{
    public partial class PlanInfo : System.Web.UI.UserControl
    {
        #region 页面参数

        /// <summary>
        /// 操作动作
        /// </summary>
        public OperationAction Action
        {
            get;
            set;
        }
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
        /// 计划对象
        /// </summary>
        public Tr_Plan Plan
        {
            get
            {
                if (ViewState["Plan"] == null)
                    return null;
                else
                    return (Tr_Plan)ViewState["Plan"];
            }
            set { ViewState["Plan"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Action == OperationAction.Add)
                {
                    rblTrainingLevelID.SelectedValue = "1";
                    rblIsUse.SelectedValue = "1";
                }
                else if (Action == OperationAction.Edit && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanID")))
                {
                    PlanID = BasePage.getSafeRequest(this.Page, "PlanID").ToGuid();
                    bind();
                }
            }
        }

        /// <summary>
        /// 邦定信息
        /// </summary>
        private void bind()
        {
            Tr_PlanLogic planLogic = new Tr_PlanLogic();
            Tr_Plan newPlan = planLogic.GetById(PlanID);
            Plan = newPlan;
            txtPlanCode.Text = newPlan.PlanCode;
            txtPlanName.Text = newPlan.PlanName;
            dttPlanBeginTime.Text = newPlan.PlanBeginTime.ToDate();
            dttPlanEndTime.Text = newPlan.PlanEndTime.ToDate();
            rblTrainingLevelID.SelectedValue = newPlan.TrainingLevelID.ToString();
            rblIsUse.SelectedValue = newPlan.IsUse.ToString();
            ddlDutyDeptID.SelectedValue = newPlan.DutyDeptID.ToString();
            ddlPlanType.SelectedValue = newPlan.PlanTypeID.ToString();
            txtDutyUser.Text = newPlan.DutyUser;
            txtMobile.Text = newPlan.Mobile;
            txtEMAIL.Text = newPlan.EMAIL;
            txtBudgetFee.Text = newPlan.BudgetFee.ToString();
            txtStudentNum.Text = newPlan.StudentNum.ToString();
            txtPlanTarget.Text = newPlan.PlanTarget;
            txtPlanObjectStudent.Text = newPlan.PlanObjectStudent;
            txtRemark.Text = newPlan.Remark;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            Tr_PlanLogic planLogic = new Tr_PlanLogic();
            Tr_Plan newPlan = new Tr_Plan();
            
            if (Action == OperationAction.Add)
            {
                SetPlan(newPlan);
                newPlan.PlanID = Guid.NewGuid();
                newPlan.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                newPlan.PlanStatus = 10;
                newPlan.CreateTime = DateTime.Now;
                newPlan.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                newPlan.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                newPlan.DelFlag = false;
            }
            else if (Action == OperationAction.Edit)
            {
                newPlan = Plan;
                SetPlan(newPlan);
                newPlan.ModifyTime = DateTime.Now;
                newPlan.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            }


            try
            {
                planLogic.Save(newPlan, Action);
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("计划信息保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }

        /// <summary>
        /// 为对象赋值
        /// </summary>
        private void SetPlan(Tr_Plan newPlan)
        {
            newPlan.PlanCode = txtPlanCode.Text;
            newPlan.PlanName = txtPlanName.Text;
            newPlan.PlanBeginTime = dttPlanBeginTime.Text.ToDateTime();
            newPlan.PlanEndTime = dttPlanEndTime.Text.ToDateTime();
            newPlan.TrainingLevelID = rblTrainingLevelID.SelectedValue.ToInt();
            newPlan.IsUse = rblIsUse.Text.ToInt();
            newPlan.DutyDeptID = ddlDutyDeptID.Text.ToInt();
            newPlan.PlanTypeID = ddlPlanType.SelectedValue.ToInt();
            newPlan.DutyUser = txtDutyUser.Text;
            newPlan.Mobile = txtMobile.Text;
            newPlan.EMAIL = txtEMAIL.Text;
            newPlan.BudgetFee = txtBudgetFee.Text.Trim() == "" ? 0 : txtBudgetFee.Text.ToDecimal();
            newPlan.StudentNum = txtStudentNum.Text.ToInt();
            newPlan.PlanTarget = txtPlanTarget.Text;
            newPlan.PlanObjectStudent = txtPlanObjectStudent.Text;
            newPlan.Remark = txtRemark.Text;           
        }
    }
}