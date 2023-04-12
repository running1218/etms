using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Reporting.Implement.BLL;
using ETMS.Utility;
using ETMS.Product;
using System.Data;
using Open.Excel.Provider;
public partial class Reporting_OrgTrainingSummary : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(CustomGridView1, PageDataSource);
        PageSet1.PageSize = 50;
        if (!Page.IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.AddYears(-1).AddDays(1).ToDate();
            txtEndTime.Text = DateTime.Now.AddYears(1).ToDate();
            this.PageSet1.QueryChange();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
        var source = new OrgnizationTrainingSummaryLogic().GetOrgnizationTrainingSummary(ProductDefine.IsSingleOrganizationVersion, beginDate, endDate, txtOrgnizationName.Text.Trim(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnExportNew_Click(object sender, EventArgs e)
    {
        Export();
    }
    private void Export()
    {
        string OrgnizationName = txtOrgnizationName.Text;
        DateTime BeginTime = string.IsNullOrEmpty(txtBeginTime.Text) ? "1900-01-01".ToDateTime() : txtBeginTime.Text.ToDateTime();
        DateTime EndTime = string.IsNullOrEmpty(txtEndTime.Text) ? DateTime.MaxValue : txtEndTime.Text.ToDateTime();
        DataTable dt = new OrgnizationTrainingSummaryLogic().GetOrgnizationTrainingSummaryTab(ETMS.Product.ProductDefine.IsSingleOrganizationVersion, BeginTime, EndTime, OrgnizationName);
        dt.Columns.Add("OnLineCourseHoursTotal");
        dt.Columns.Add("AvgExam");

        foreach (DataRow row in dt.Rows)
        {
            row["OnLineCourseHoursTotal"] = decimal.Round((row["OnLineCourseHours"].ToHours() + row["OffLineCourseHours"].ToHours()), 2);
            row["AvgExam"] = row["JoinExamNum"].ToString() == "0" ? string.Empty : (decimal.Round(row["Score"].ToString().ToDecimal() / row["JoinExamNum"].ToString().ToDecimal(), 2)).ToString();
        }
        //导出
        string xmlTemplatePath = Server.MapPath(@"ExportXML/OrgTrainingSummary.xml");

        string fileName = "机构培训汇总表";
        string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string pathFileName = Server.MapPath("~/Temp/" + fileName + strDateTime + ".xls");

        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, fileName, dt);
        provider.DataFormate = "yyyy-MM-dd HH:MM:ss";
        provider.SheetSize = 60000;
        provider.XlsSavePath = pathFileName;
        provider.XlsWriteModel = XlsWriteModel.FileOutput;
        provider.ExportExcel();

        //压缩
        string sourcePath = Server.MapPath("../Temp/");
        string downFileName = string.Format(fileName + "{0}.xls", strDateTime);
        string downPath = Server.MapPath("../Temp/");
        string downFileNameCompress = string.Format(fileName + "{0}.zip", strDateTime);

        FileUtility.Compress(sourcePath, downFileName, downPath, downFileNameCompress, CompressType.Zip, CompressModel.File);
        System.IO.File.Delete(pathFileName);
        //下载
        FileDownLoadUtility.ExportFile(System.IO.Path.Combine(downPath, downFileNameCompress));
    }

  
}