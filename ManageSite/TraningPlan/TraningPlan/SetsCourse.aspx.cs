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
using ETMS.Components.Basic.Implement.BLL.TrainingPlan.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan;
using ETMS.Components.Basic.API.Entity.TrainingPlan.Course;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlan
{
    public partial class SetsCourse : System.Web.UI.Page
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
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);
            if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanID")))
            {
                PlanID = BasePage.getSafeRequest(this.Page, "PlanID").ToGuid();
                bind();
                this.PageSet1.QueryChange();
            }
            btnAdd.PostBackUrl = this.ActionHref("SetsCourseAdd.aspx?PlanID=" + PlanID);
            aBack.HRef = "TraningPlanList.aspx";
        }

        /// <summary>
        /// 邦定信息
        /// </summary>
        private void bind() {
            Tr_PlanLogic planLogic = new Tr_PlanLogic();
            Tr_Plan plan = planLogic.GetById(PlanID);
            if (plan != null)
            {
                lblPlanCode.Text = plan.PlanCode;
                lblPlanName.Text = plan.PlanName;
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Tr_PlanCourseLogic courseLogic = new Tr_PlanCourseLogic();
            DataTable dt = courseLogic.GetPlanCourseListByPlanID(PlanID);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            totalRecordCount = dt.Rows.Count;
            return pageDataSource.PageDataSource;
        }

        /// <summary>
        /// 行邦定
        /// </summary>
        protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string PlanCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                #region 获取控件
                LinkButton lbtnEdit=(LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit = lbtnEdit==null? new LinkButton():lbtnEdit;

                LinkButton lbtnSave = (LinkButton)e.Row.FindControl("lbtnSave");
                lbtnSave = lbtnSave == null ? new LinkButton() : lbtnSave;

                LinkButton lbtnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");
                lbtnCancel = lbtnCancel == null ? new LinkButton() : lbtnCancel;

                LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
                lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

                lbtnEdit.CommandArgument = e.Row.RowIndex.ToString();
                lbtnSave.CommandArgument = e.Row.RowIndex.ToString();
                lbtnView.Attributes["onclick"] = string.Format("javascript:showWindow('查看课程信息','{0}',650,500); javascript:return false;"
                    , this.ActionHref(string.Format("SetsCourseView.aspx?PlanCourseID={0}", PlanCourseID)));                
                #endregion

                #region 授课方式
                DropDownList ddlTeachModel = (DropDownList)e.Row.FindControl("ddlTeachModel");
                ddlTeachModel = ddlTeachModel == null ? new DropDownList() : ddlTeachModel;

                HiddenField Hf_TeachModelID = (HiddenField)e.Row.FindControl("Hf_TeachModelID");
                Hf_TeachModelID = Hf_TeachModelID == null ? new HiddenField() : Hf_TeachModelID;

                foreach(ListItem item in  dddlTeachModely.Items){
                    ddlTeachModel.Items.Add(item);
                }
                ddlTeachModel.SelectedValue = Hf_TeachModelID.Value;
                #endregion

                #region 培训方式
                DropDownList ddlTrainingModel = (DropDownList)e.Row.FindControl("ddlTrainingModel");
                ddlTrainingModel = ddlTrainingModel == null ? new DropDownList() : ddlTrainingModel;

                HiddenField Hf_TrainingModelID = (HiddenField)e.Row.FindControl("Hf_TrainingModelID");
                Hf_TrainingModelID = Hf_TrainingModelID == null ? new HiddenField() : Hf_TrainingModelID;

                foreach (ListItem item in dddlTrainingModel.Items)
                {
                    ddlTrainingModel.Items.Add(item);
                }
                ddlTrainingModel.SelectedValue = Hf_TrainingModelID.Value;
                #endregion

                #region 外训机构
                DropDownList ddlOuterOrg = (DropDownList)e.Row.FindControl("ddlOuterOrg");
                ddlOuterOrg = ddlOuterOrg == null ? new DropDownList() : ddlOuterOrg;

                HiddenField Hf_OuterOrgID = (HiddenField)e.Row.FindControl("Hf_OuterOrgID");
                Hf_OuterOrgID = Hf_OuterOrgID == null ? new HiddenField() : Hf_OuterOrgID;
                
                foreach (ListItem item in dddlOuterOrg.Items)
                {
                    ddlOuterOrg.Items.Add(new ListItem(item.Text,item.Value)); 
                }
                ddlOuterOrg.SelectedValue = Hf_OuterOrgID.Value;
                #endregion
            }
        }

        /// <summary>
        /// 行操作
        /// </summary>
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //指定编辑的列
            if (e.CommandName == "Edit") {
                CustomGridView1.EditIndex = e.CommandArgument.ToInt();
                this.PageSet1.DataBind();
                CustomGridView1.Rows[e.CommandArgument.ToInt()].Cells[4].CssClass = "visibleS";
                CustomGridView1.Rows[e.CommandArgument.ToInt()].Cells[5].CssClass = "visibleS";
                CustomGridView1.Rows[e.CommandArgument.ToInt()].Cells[6].CssClass = "visibleS";
                CustomGridView1.Rows[e.CommandArgument.ToInt()].Cells[7].CssClass = "visibleS";
            }
            //保存编辑列
            if (e.CommandName == "Save")
            {
                #region
                //获取控件
                Guid planCourseID = CustomGridView1.DataKeys[e.CommandArgument.ToInt()].Value.ToGuid();
                DropDownList ddlTeachModel = (DropDownList)CustomGridView1.Rows[e.CommandArgument.ToInt()].FindControl("ddlTeachModel");
                TextBox txtBudgetFee = (TextBox)CustomGridView1.Rows[e.CommandArgument.ToInt()].FindControl("txtBudgetFee");
                DropDownList ddlTrainingModel = (DropDownList)CustomGridView1.Rows[e.CommandArgument.ToInt()].FindControl("ddlTrainingModel");
                DropDownList ddlOuterOrg = (DropDownList)CustomGridView1.Rows[e.CommandArgument.ToInt()].FindControl("ddlOuterOrg");

                //验证
                if (txtBudgetFee != null) {
                    if (txtBudgetFee.Text.Trim() == "")
                    {
                        ETMS.Utility.JsUtility.AlertMessageBox("预算不能为空！");
                        return;
                    }
                    else
                    {
                        string strRegex = @"^\d+(\.\d{1,2})?$";
                        System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(strRegex);
                        if (!rg.IsMatch(txtBudgetFee.Text.Trim()))
                        {
                            ETMS.Utility.JsUtility.AlertMessageBox("预算格式错误必须为数值类型 如：888.68！");
                            return;
                        }
                    }
                }

                //保存
                try
                {
                    Tr_PlanCourseLogic courseLogic = new Tr_PlanCourseLogic();
                    Tr_PlanCourse planCourse = courseLogic.GetById(planCourseID);
                    if (ddlTeachModel != null)
                        planCourse.TeachModelID = ddlTeachModel.SelectedValue.ToInt();
                    if (txtBudgetFee != null && txtBudgetFee.Text.Trim() != "")
                        planCourse.BudgetFee = txtBudgetFee.Text.ToDecimal();
                    if (ddlTrainingModel != null)
                        planCourse.TrainingModelID = ddlTrainingModel.SelectedValue.ToInt();
                    if (ddlOuterOrg != null)
                        planCourse.OuterOrgID = ddlOuterOrg.SelectedValue == "" ? Guid.Empty : ddlOuterOrg.SelectedValue.ToGuid();

                    //修改时间与修改人
                    planCourse.ModifyTime = DateTime.Now;
                    planCourse.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;

                    courseLogic.Save(planCourse, AppContext.OperationAction.Edit);
                    ETMS.Utility.JsUtility.SuccessMessageBox("课程信息保存成功！");
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
                //保存完成
                CustomGridView1.EditIndex = -1;
                this.PageSet1.DataBind();
                #endregion
            }
            //取消编辑列
            if (e.CommandName == "Cancel")
            {
                CustomGridView1.EditIndex = -1;
                this.PageSet1.DataBind();
            }
        }
        
        /// <summary>
        /// 删除课程关系信息
        /// </summary>
        protected void cbtnDel_Click(object sender, EventArgs e)
        {
            Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (selectedValues.Length == 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请选择要删除的课程！");
                return;
            }
            else
            {
                try
                {
                    Tr_PlanCourseLogic courseLogic = new Tr_PlanCourseLogic();
                    courseLogic.Remove(selectedValues);
                    ETMS.Utility.JsUtility.SuccessMessageBox("提示","信息删除成功！");
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }
}
}