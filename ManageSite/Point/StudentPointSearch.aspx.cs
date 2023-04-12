using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Point.Implement.BLL;

public partial class Point_StudentPointSearch : ETMS.Controls.BasePage
{
    public static Point_Student_IssueDetailLogic isssueDetailLogic = new Point_Student_IssueDetailLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InitialControl();
        }
    }
    private void InitialControl()
    {
        this.begin_a999IssueTime.Text = DateTime.Now.ToString("yyyy-01-01");
        this.end_a999IssueTime.Text = DateTime.Now.ToDate();
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        if (string.IsNullOrEmpty(this.end_a999IssueTime.Text.Trim()))
        {
            this.end_a999IssueTime.Text = DateTime.Now.ToDate();
        }
        if (string.IsNullOrEmpty(this.begin_a999IssueTime.Text.Trim()))
        {
            this.begin_a999IssueTime.Text = DateTime.Now.ToString("yyyy-01-01");
        }
        string crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        DataTable dt = isssueDetailLogic.StatStudentPointAllInfoListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, pageSize, "", crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.begin_a999IssueTime.Text.Trim()))
        {
            JsUtility.AlertMessageBox("日期范围起始日期不能为空!");
            return;
        }
        if (string.IsNullOrEmpty(this.end_a999IssueTime.Text.Trim()))
        {
            JsUtility.AlertMessageBox("日期范围截止日期不能为空!");
            return;
        }
        this.PageSet1.QueryChange();
    }
}