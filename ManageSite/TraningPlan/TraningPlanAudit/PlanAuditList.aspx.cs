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
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;
using ETMS.Utility;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlanAudit
{
    public partial class PlanAuditList : BasePage
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
        }

        /// <summary>
        /// 查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange(); 
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Crieria = string.Format(" {0} AND PlanStatus IN (10,20,40) AND IsUse=1 ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

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

                LinkButton lbtn_Audit = (LinkButton)e.Row.FindControl("lbtn_Audit");
                lbtn_Audit = lbtn_Audit == null ? new LinkButton() : lbtn_Audit;

                CustomLinkButton lbtnUnapproved = (CustomLinkButton)e.Row.FindControl("lbtnUnapproved");
                lbtnUnapproved = lbtnUnapproved == null ? new CustomLinkButton() : lbtnUnapproved;

                LinkButton lbtn_View = (LinkButton)e.Row.FindControl("lbtn_View");
                lbtn_View = lbtn_View == null ? new LinkButton() : lbtn_View;
                #endregion

                lbtn_View.PostBackUrl = this.ActionHref("PlanAuditView.aspx?PlanID=" + PlanID.ToString());

                switch (Hf_PlanStatus.Value) { 
                    case "10"://待审核
                        lbtn_Audit.Visible = true;
                        lbtn_Audit.PostBackUrl = this.ActionHref("PlanAudit.aspx?PlanID=" + PlanID.ToString());
                        lbtnUnapproved.Visible = false;
                        lbtnUnapproved.EnableConfirm = false;
                        break;
                    default://审核通过与审核不通过
                        lbtn_Audit.Visible = false;
                        lbtnUnapproved.Visible = true;
                        lbtnUnapproved.EnableConfirm = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 行操作
        /// </summary>
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unapproved")
            {
                try
                {
                    Tr_PlanLogic logic = new Tr_PlanLogic();
                    logic.Tr_Plan_CancelAudit(e.CommandArgument.ToGuid(), ETMS.AppContext.UserContext.Current.RealName, "");
                    ETMS.Utility.JsUtility.SuccessMessageBox("提示", "计划取消审核成功！");
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