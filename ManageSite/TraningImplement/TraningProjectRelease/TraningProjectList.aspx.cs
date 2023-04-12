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
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.WebApp.Manage;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectRelease_TraningProjectList : BasePage
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
        Crieria = string.Format(" {0} AND OrgID={1} AND ItemStatus=20 ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

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
            #region

            HiddenField hfIsIssue = (HiddenField)e.Row.FindControl("hfIsIssue");
            hfIsIssue = hfIsIssue == null ? new HiddenField() : hfIsIssue;

            LinkButton lbtnRelease = (LinkButton)e.Row.FindControl("lbtnRelease");
            lbtnRelease = lbtnRelease == null ? new LinkButton() : lbtnRelease;

            Label lblStudentTotal = (Label)e.Row.FindControl("lblStudentTotal");
            lblStudentTotal = lblStudentTotal == null ? new Label() : lblStudentTotal;
            
            #endregion

            lblStudentTotal.Text = new ETMS.Components.Basic.Implement.BLL.TrainingItem.Student.Sty_StudentSignupLogic().GetTrainingItemStudentTotal(itemID.ToGuid()).ToString();
            //如果已发布 或者 报名学员为0时 发布按钮不可用
            if (hfIsIssue.Value.Trim().ToLower() == "true")
            {
                lbtnRelease.Enabled = false;
                lbtnRelease.ToolTip = "已发布";
                lbtnRelease.CssClass = "link_colorGray";
            }
            lbtnRelease.PostBackUrl = this.ActionHref(string.Format("TraningProjectRelease.aspx?TrainingItemID={0}", itemID));
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Release")
        {
    
        }
    }
}