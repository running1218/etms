using System;
using System.Data;
using System.Text;
using System.Web.UI;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Controls;
using ETMS.AppContext;

public partial class Grade_GradeManage_OnLineTestList : System.Web.UI.Page
{
    public static Res_ItemCourse_OnLineTestLogic OnLineTestLogic = new Res_ItemCourse_OnLineTestLogic();
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
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        whereQuery.Append(string.Format(" And i.OrgID = '{0}' ", UserContext.Current.OrganizationID));
        DataTable dt = OnLineTestLogic.GetExceptionOnlineTestInfo(pageIndex, pageSize, string.Empty, whereQuery.ToString(), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        this.HideCustomGridView.DataSource = PageDataSourceExport(1, 99999999, out totalRecordCount);
        this.HideCustomGridView.DataBind();

        ETMS.Utility.FileDownLoadUtility.ExportToExcel("在线测试异常监控统计表.xls", this.HideCustomGridView);
    }

    private System.Collections.IList PageDataSourceExport(int pageIndex, int pageSize, out int totalRecordCount)
    {
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

        DataTable dt = OnLineTestLogic.GetExceptionOnlineTestInfo(pageIndex, pageSize, string.Empty, whereQuery.ToString(), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }


    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridView1.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] queryIds = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (queryIds.Length < 1)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要删除的数据");
            }
            else
            {
                OnLineTestLogic.BatchRemoveStudentOnlineTest(queryIds);
                //刷新
                this.PageSet1.DataBind();
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("在线测试异常监控删除成功！");
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        try
        {
            
                StringBuilder whereQuery = new StringBuilder();
                whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
                OnLineTestLogic.RemoveStudentOnlineTestBySQLCondition(whereQuery.ToString());
                //刷新
                this.PageSet1.DataBind();
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("在线测试异常监控删除成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}