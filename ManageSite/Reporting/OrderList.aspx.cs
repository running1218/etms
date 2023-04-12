using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Reporting.Implement.BLL;
using ETMS.Utility;
using System.Data;
using Open.Excel.Provider;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.AppContext;

public partial class Reporting_OrderList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(CustomGridView1, PageDataSource);
        PageSet1.PageSize = 50;
        if (!Page.IsPostBack)
        {
            bind();
            txtBeginTime.Text = DateTime.Now.AddYears(-1).AddDays(1).ToDate();
            txtEndTime.Text = DateTime.Now.AddYears(1).ToDate();
            this.PageSet1.QueryChange();
        }
        btnSearch.Attributes.Add("onclick", string.Format("return CheckSelectData('{0}')", drpOrg.ClientID));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 邦定组织机构
    /// </summary>
    private void bind()
    {
        OrganizationLogic organizationLogic = new OrganizationLogic();
        drpOrg.DataSource = organizationLogic.GetPageListByParentID(UserContext.Current.OrganizationID);
        drpOrg.DataTextField = "OrganizationName";
        drpOrg.DataValueField = "OrganizationID";
        drpOrg.SelectedValue = UserContext.Current.OrganizationID.ToString();
        drpOrg.DataBind();
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        var beginDate = txtBeginTime.Text.Trim() == string.Empty ? DateTime.MinValue.AddYears(1900) : txtBeginTime.Text.ToDateTime();
        var endDate = txtEndTime.Text.Trim() == string.Empty ? DateTime.MaxValue : txtEndTime.Text.ToDateTime(); 
        var source = new StudentTrainingSummaryLogic().GetAllOrderList((cbkOrg.Checked) ? 1 : 0, txtOrderNo.Text.Trim(), int.Parse(drpOrderStatus.SelectedValue), txtLoginName.Text.Trim(), txtRealName.Text.Trim(), int.Parse(drpOrg.SelectedValue), beginDate, endDate, pageIndex, pageSize, "OrderNo desc", out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindReportExport();
    }

    int pageSheetSize = 60000;//excelSheet大小

    void BindReportExport()
    {
        var beginDate = txtBeginTime.Text.Trim() == string.Empty ? DateTime.MinValue.AddYears(1900) : txtBeginTime.Text.ToDateTime();
        var endDate = txtEndTime.Text.Trim() == string.Empty ? DateTime.MaxValue : txtEndTime.Text.ToDateTime();
        int totalRecordCount = 0;
        DataTable source = new StudentTrainingSummaryLogic().GetAllOrderList((cbkOrg.Checked) ? 1 : 0, txtOrderNo.Text.Trim(), int.Parse(drpOrderStatus.SelectedValue), txtLoginName.Text.Trim(), txtRealName.Text.Trim(), int.Parse(drpOrg.SelectedValue), beginDate, endDate, 1, int.MaxValue - 1, "", out totalRecordCount);
        string xmlTemplatePath = Server.MapPath(@"ExportXML\OrderList.xml");
        string xlsFileName = "订单列表";
        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, xlsFileName, source);
        provider.ExportExcel();
    }


}