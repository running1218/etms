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
using System.Web.UI.HtmlControls;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Utility;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlan
{
    public partial class TraningPlanList : BasePage
    {
        #region 页面参数

        /// <summary>
        /// 查询条件 
        /// </summary>
        protected string Crieria
        {
            get
            {
                if (ViewState["Crieria"] == null)
                    ViewState["Crieria"] = "";
                return (string)ViewState["Crieria"];
            }
            set
            {
                ViewState["Crieria"] = value;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        protected string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = " CreateTime DESC";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }
            btnAdd.Attributes["onclick"] = "javascript:showWindow('新增培训计划','TraningPlanAdd.aspx',650,500);javascript:return false;";
        }

        /// <summary>
        /// 查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange(); upList.Update();
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);

            Tr_PlanLogic planLogic = new Tr_PlanLogic();

            DataTable dt = planLogic.GetPlanListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        /// <summary>
        /// 行邦定
        /// </summary>
        protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string PlanID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                #region 获取控件
                HiddenField Hf_PlanStatus = (HiddenField)e.Row.FindControl("Hf_PlanStatus");
                Hf_PlanStatus = Hf_PlanStatus == null ? new HiddenField() : Hf_PlanStatus;

                Label lblCourseTotal = (Label)e.Row.FindControl("lblCourseTotal");
                lblCourseTotal = lblCourseTotal == null ? new Label() : lblCourseTotal;

                CustomLinkButton lbtn_IsUse = (CustomLinkButton)e.Row.FindControl("lbtn_IsUse");
                lbtn_IsUse = lbtn_IsUse == null ? new CustomLinkButton() : lbtn_IsUse;

                LinkButton lbtn_Edit = (LinkButton)e.Row.FindControl("lbtn_Edit");
                lbtn_Edit = lbtn_Edit == null ? new LinkButton() : lbtn_Edit;

                CustomLinkButton lbtn_Del = (CustomLinkButton)e.Row.FindControl("lbtn_Del");
                lbtn_Del = lbtn_Del == null ? new CustomLinkButton() : lbtn_Del;

                LinkButton lbtn_SetCourse = (LinkButton)e.Row.FindControl("lbtn_SetCourse");
                lbtn_SetCourse = lbtn_SetCourse == null ? new LinkButton() : lbtn_SetCourse;

                LinkButton lbtn_View = (LinkButton)e.Row.FindControl("lbtn_View");
                lbtn_View = lbtn_View == null ? new LinkButton() : lbtn_View;
                #endregion

                lblCourseTotal.Text = new Tr_PlanLogic().GetPlanCourseTotal(PlanID.ToGuid()).ToString();
                #region 启用与停用
                string[] str = lbtn_IsUse.CommandArgument.ToString().Split(',');
                if (str[1] == "1")
                {
                    lbtn_IsUse.Text = "停用";
                    lbtn_IsUse.ConfirmMessage = "确定停用吗？";
                }
                else
                {
                    lbtn_IsUse.Text = "启用";
                    lbtn_IsUse.ConfirmMessage = "确定启用吗？";
                }
                #endregion
                lbtn_View.PostBackUrl = this.ActionHref("TraningPlanView.aspx?PlanID=" + PlanID.ToString());

                switch (Hf_PlanStatus.Value)
                {
                    case "10"://待审核
                        lbtn_IsUse.Enabled = true;
                        lbtn_IsUse.EnableConfirm = true;
                        
                        lbtn_Edit.Enabled = true;
                        lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('编辑培训计划','{0}',650,500);javascript:return false;", this.ActionHref("TraningPlanEdit.aspx?PlanID=" + PlanID.ToString()));

                        lbtn_Del.Enabled = true;
                        lbtn_Del.EnableConfirm = true;

                        lbtn_SetCourse.PostBackUrl =this.ActionHref("SetsCourse.aspx?PlanID=" + PlanID.ToString());
                        lbtn_SetCourse.Enabled = true;
                        break;
                    case "20"://审核通过
                    case "40"://审核不通过
                        lbtn_IsUse.Enabled = true;
                        lbtn_IsUse.EnableConfirm = true;
                        lbtn_SetCourse.CssClass = "link_colorGray";
                        lbtn_Edit.CssClass = "link_colorGray";
                        lbtn_Del.CssClass = "link_colorGray";
                        break;
                    case "90"://结束
                        lbtn_IsUse.CssClass = "link_colorGray";
                        lbtn_SetCourse.CssClass = "link_colorGray";
                        lbtn_Edit.CssClass = "link_colorGray";
                        lbtn_Del.CssClass = "link_colorGray";
                        break;
                }
            }
        }

        /// <summary>
        /// 行操作
        /// </summary>
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //启用与停用
            if (e.CommandName == "IsUse")
            {
                try
                {
                    Tr_PlanLogic planLogic = new Tr_PlanLogic();
                    string[] str = e.CommandArgument.ToString().Split(',');
                    planLogic.SetTrainingPlanStatus(str[0].ToGuid(), str[1].ToInt() == 1 ? 0 : 1);

                    ETMS.WebApp.Manage.Extention.SuccessMessageBox(string.Format("计划{0}成功！", str[1] == "1" ? "停用" : "启用"));
                    this.PageSet1.DataBind(); upList.Update();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
            //删除
            if (e.CommandName == "Dels")
            {
                try
                {
                    Tr_PlanLogic planLogic = new Tr_PlanLogic();

                    planLogic.Remove(e.CommandArgument.ToGuid());

                    ETMS.WebApp.Manage.Extention.SuccessMessageBox("计划删除成功！");
                    this.PageSet1.DataBind(); upList.Update();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }

        /// <summary>
        /// 拼接字符串 用于点击启用与停用
        /// </summary>
        /// <param name="planID">planID</param>
        /// <param name="IsUse">IsUse</param>
        /// <returns></returns>
        protected string GetPlanIDIsUseValue(object planID, object IsUse)
        {
            return planID + "," + IsUse;
        }
    }
}