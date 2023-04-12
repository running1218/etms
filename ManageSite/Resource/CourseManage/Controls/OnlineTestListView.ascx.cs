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
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Utility;

public partial class Resource_CourseManage_Controls_OnlineTestListView : System.Web.UI.UserControl
{
    private Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
                ViewState["CourseID"] = Guid.Empty;
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
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
        string criteria = string.Format(" and CourseID='{0}'", CourseID);

        Ex_OnLineTestLogic Logic = new Ex_OnLineTestLogic();
        DataTable dt = Logic.GetPagedList(pageIndex, pageSize, "", criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
}