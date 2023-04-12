using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Controls;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectAdjust_TraningProjectList : BasePage
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

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //培训项目发布，未规档的培训项目
        Crieria = string.Format(" {0} AND OrgID={1} and IsIssue=1 and ItemStatus != 90", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

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
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string itemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            #region 获取控件
            LinkButton Lbtn_Edit = (LinkButton)e.Row.FindControl("Lbtn_Edit");
            Lbtn_Edit = Lbtn_Edit == null ? new LinkButton() : Lbtn_Edit;

            LinkButton Lbtn_CourseList = (LinkButton)e.Row.FindControl("Lbtn_CourseList");
            Lbtn_CourseList = Lbtn_CourseList == null ? new LinkButton() : Lbtn_CourseList;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;
            #endregion
            Lbtn_Edit.Attributes["onclick"] = string.Format("javascript:showWindow('编辑培训项目','{0}');javascript:return false;", this.ActionHref(string.Format("TraningProjectEdit.aspx?TrainingItemID={0}", itemID)));

            //查看
            lbtnView.PostBackUrl = this.ActionHref("TraningProjectView.aspx?TrainingItemID=" + itemID);
            lbtnView.Enabled = true;
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
        }
    }
}