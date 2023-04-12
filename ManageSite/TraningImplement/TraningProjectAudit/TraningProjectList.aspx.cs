using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;

public partial class TraningImplement_TraningProjectAudit_TraningProjectList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
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

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND OrgID={1} AND ItemStatus in (10,20,40) AND IsUse=1 ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemLogic item = new Tr_ItemLogic();
        DataTable dt = item.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); upList.Update();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string itemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            ShortTextLabel lblPlan = (ShortTextLabel)e.Row.FindControl("lblPlan");
            lblPlan = lblPlan == null ? new ShortTextLabel() : lblPlan;

            HiddenField hfPlanID = (HiddenField)e.Row.FindControl("hfPlanID");
            hfPlanID = hfPlanID == null ? new HiddenField() : hfPlanID;
            
            HiddenField Hf_ItemStatus = (HiddenField)e.Row.FindControl("Hf_ItemStatus");
            Hf_ItemStatus = Hf_ItemStatus == null ? new HiddenField() : Hf_ItemStatus;

            LinkButton lbtnAudit = (LinkButton)e.Row.FindControl("lbtnAudit");
            lbtnAudit = lbtnAudit == null ? new LinkButton() : lbtnAudit;

            CustomLinkButton lbtnUnapproved = (CustomLinkButton)e.Row.FindControl("lbtnUnapproved");
            lbtnUnapproved = lbtnUnapproved == null ? new CustomLinkButton() : lbtnUnapproved;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            switch (Hf_ItemStatus.Value)
            {
                case "10":
                    lbtnAudit.PostBackUrl = this.ActionHref("TraningProjectAudit.aspx?TrainingItemID=" + itemID);
                    lbtnAudit.Visible = true;
                    lbtnUnapproved.Visible = false;
                    break;
                case "20":
                case "40":
                    lbtnAudit.Visible = false;
                    lbtnUnapproved.Visible = true;
                    break;
                default:
                    lbtnAudit.Visible = true;
                    lbtnAudit.Enabled = false;
                    lbtnAudit.CssClass = "link_colorGray";
                    lbtnUnapproved.Visible = false;
                    break;
            }
            if (hfPlanID.Value.Trim() != "" && hfPlanID.Value.ToGuid() != Guid.Empty)
                lblPlan.Text = new Tr_PlanLogic().GetById(hfPlanID.Value.ToGuid()).PlanName;

            //查看
            lbtnView.PostBackUrl = this.ActionHref("TraningProjectAuditView.aspx?TrainingItemID=" + itemID);
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
                Tr_ItemLogic ItemLogic = new Tr_ItemLogic();
                ItemLogic.Tr_Item_CancelAudit(new Guid(e.CommandArgument.ToString()), ETMS.AppContext.UserContext.Current.RealName, "");

                ETMS.WebApp.Manage.Extention.SuccessMessageBox("项目取消审核成功！");
                this.PageSet1.DataBind(); upList.Update();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }    
}