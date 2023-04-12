using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Reporting.Implement.BLL;
using ETMS.Utility;
using Open.Excel.Provider;
using System.Data;

public partial class Reporting_StuRegisterStatistical : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 100;
        if (!IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        var dt = new StudentTrainingDetailsLogic().GetStudentRegisterNumber(ddl_OrganizationID.SelectedValue.ToInt(), ddlUserStatus.SelectedValue.ToInt());
        totalRecordCount = dt.Rows.Count;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        var dt = ((DataView)PageDataSource(1, int.MaxValue - 1, out totalRecordCount)).Table;

        string xmlTemplatePath = Server.MapPath(@"ExportXML\StuRegisterStatistical.xml");
        string xlsFileName = "公司注册人数汇总";
        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, xlsFileName, dt);
        provider.ExportExcel();
    }
}