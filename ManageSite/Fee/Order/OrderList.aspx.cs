using System;
using System.Web.UI;
using ETMS.Controls;
using ETMS.Components.Order.Implement.BLL;
using ETMS.Utility;
using System.Data;
using Open.Excel.Provider;

public partial class Fee_Order_OrderList : System.Web.UI.Page
{
    private readonly static OrderLogic orderLogic = new OrderLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.txtStartTime.Text = DateTime.Now.AddMonths(-1).ToDate();
            this.txtEndTime.Text = DateTime.Now.ToDate();
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DateTime startTime = DateTime.Now.AddMonths(-1);
        if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            startTime = txtStartTime.Text.Trim().ToStartDateTime();
        DateTime endTime = DateTime.Now;
        if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            endTime = txtEndTime.Text.Trim().ToEndDateTime();
        DataTable dt = orderLogic.GetOrders(startTime, endTime, txtUserName.Text.Trim(), ddlOrderStatus.SelectedValue.ToInt(), ETMS.AppContext.UserContext.Current.OrganizationID, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        var dt = ((DataView)PageDataSource(1, int.MaxValue - 1, out totalRecordCount)).Table;

        string xmlTemplatePath = Server.MapPath(@"~/Reporting/ExportXML\OrderList.xml");
        string xlsFileName = "订单导出";
        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, xlsFileName, dt);
        provider.RemainDigits = 2;
        provider.DataFormate = "yyyy-MM-dd HH:mm:ss";
        provider.ExportExcel();
    }
}