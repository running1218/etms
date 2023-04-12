using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using System.Data;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Utility;

public partial class Resource_CourseManage_Controls_CoursewareList : System.Web.UI.UserControl
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
            {
                ViewState["Crieria"] = "";
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件 " CoursewareName "
    /// </summary>

    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " CoursewareName ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    protected Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
            {
                ViewState["CourseID"] = Guid.Empty;
            }
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CourseID"] != null)
            {
                CourseID = Request.QueryString["CourseID"].ToGuid();
            }
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" and CourseID='{0}'", CourseID);

        DataTable dt = new Res_CoursewareLogic().Res_CoursewareGetPagedList(pageIndex, pageSize, " CreateTime desc", Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}