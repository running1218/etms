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
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
public partial class TraningImplement_TraningProjectResult_TraningProjectList : System.Web.UI.Page
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
        this.PageSet1.QueryChange(); upList.Update();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND OrgID={1} AND ItemStatus in (20,40,90)", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemLogic item = new Tr_ItemLogic();
        DataTable dt = item.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
            string TrainingItemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            LinkButton lbtn_File = (LinkButton)e.Row.FindControl("lbtn_File");
            lbtn_File = lbtn_File == null ? new LinkButton() : lbtn_File;

            //归档是否可用
            HiddenField hfItemEndMode = (HiddenField)e.Row.FindControl("hfItemEndMode");
            if (hfItemEndMode != null && hfItemEndMode.Value.Trim() == "0")
            {
                lbtn_File.Enabled = true;
                lbtn_File.Attributes["onclick"] = string.Format("javascript:showWindow('项目归档','{0}',650,500);javascript:return false;", this.ActionHref(string.Format("TraningProjectResultEdit.aspx?TrainingItemID={0}", TrainingItemID)));
            }
            else
            {
                lbtn_File.Enabled = false;
                lbtn_File.CssClass = "link_colorGray";
            }
        }
    }
}