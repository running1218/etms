using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Data;
using ETMS.Controls;

public partial class TraningImplement_TraningProjectManager_Selected_SelectUser : System.Web.UI.Page
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
        PageSet1.PageSize = 5;
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
        Crieria = string.Format(" {0} AND u.OrganizationID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Site_StudentLogic studentLogic = new Site_StudentLogic();
        DataTable dt = studentLogic.GetUserStudentPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    { 
       
    }
}