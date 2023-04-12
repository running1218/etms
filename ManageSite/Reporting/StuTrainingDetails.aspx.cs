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
using System.IO;
using System.Text;
using Open.Excel.HSSF.UserModel;

public partial class Reporting_StuTrainingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(rptTrainingDetails, PageDataSource);
        PageSet1.PageSize = 20;
        if (!Page.IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.AddYears(-1).AddDays(1).ToDate();
            txtEndTime.Text = DateTime.Now.AddYears(1).ToDate();
            this.PageSet1.QueryChange();
        }
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
        var beginDate = txtBeginTime.Text.Trim() == string.Empty? DateTime.MinValue.AddYears(1900):txtBeginTime.Text.ToDateTime();
        var endDate = txtEndTime.Text.Trim() == string.Empty? DateTime.MaxValue:txtEndTime.Text.ToDateTime();
        var source = new StudentTrainingDetailsLogic().GetStudentTrainingDetails(beginDate, endDate, txtName.Text.Trim(), txtWorkerNo.Text.Trim(), txtDepartment.Text.Trim(), txtPost.Text.Trim(), pageIndex, pageSize, out totalRecordCount);
                                                                                
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
        System.Data.DataTable dt = new StudentTrainingDetailsLogic().GetStudentTrainingDetailsExport(beginDate, endDate, txtName.Text.Trim(), txtWorkerNo.Text.Trim(), txtDepartment.Text.Trim(), txtPost.Text.Trim());
        if (dt == null||dt.Rows.Count==0)
        {
            JsUtility.AlertMessageBox("提示", "抱歉，没有数据可以导出！");
            return;
        }
        string xmlTemplatePath = Server.MapPath(@"ExportXML/StuTrainingDetails.xml");
        string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        string fileName = "学员培训明细列表";
        string pathFileName = Server.MapPath("~/Temp/" + fileName + strDateTime + ".xls");
       //导出
        ExcelProvider provider = new ExcelProvider(xmlTemplatePath, fileName, dt);
        provider.DataFormate = "yyyy-MM-dd HH:MM:ss";
        provider.SheetSize = pageSheetSize;
        provider.XlsSavePath = pathFileName;
        provider.XlsWriteModel = XlsWriteModel.FileOutput;
        provider.ExportExcel();

        //合并
        GetExcelDatable(pathFileName, dt);

        //压缩文件
        string sourcePath = Server.MapPath("../Temp/");
        string downFileName = string.Format(fileName + "{0}.xls", strDateTime);
        string downPath = Server.MapPath("../Temp/");
        string downFileNameCompress = string.Format(fileName + "{0}.zip", strDateTime);
        FileUtility.Compress(sourcePath, downFileName, downPath, downFileNameCompress, CompressType.Zip, CompressModel.File);
        System.IO.File.Delete(pathFileName);

        //下载
        FileDownLoadUtility.ExportFile(System.IO.Path.Combine(downPath, downFileNameCompress));
    }


    /// <summary>
    /// 获取excel中的数据
    /// </summary>
    /// <param name="strFileName"></param>
    private void GetExcelDatable(string strPathFileName,DataTable dt)
    {
        FileStream fs = new FileStream(strPathFileName, FileMode.Open, FileAccess.ReadWrite);
        HSSFWorkbook wb = new HSSFWorkbook(fs);
        HSSFSheet sheet = null;

        for (int s = 0; s < wb.NumberOfSheets; s++) //多个sheet
        {
            sheet = wb.GetSheetAt(s);
            int numberStart = s * pageSheetSize + 1;
            int numberEnd = (s + 1) * pageSheetSize; ;
            DataRow[] rowList = dt.Select(" number >=" + numberStart + " and number<" + numberEnd);
            
            DataTable tmp = dt.Clone() ;
            if (rowList != null || rowList.Length != 0)
            {
                 foreach (DataRow item in rowList)
                 {
                     tmp.ImportRow(item); 
                 }
            }

            //RangeSheet(tmp, sheet);
        }

        MemoryStream ms = null;
        try
        {
        ms = new MemoryStream();
        fs = new FileStream(strPathFileName, FileMode.Open, FileAccess.ReadWrite);

        wb.Write(ms);
        ms.Flush();
        ms.Position = 0;
        byte[] data = ms.ToArray();
        fs.Write(data, 0, data.Length);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        { 
        ms.Close();
        ms.Dispose();
        fs.Close();
        fs.Dispose();
        }

    }

    /// <summary>
    /// 合并单元格
    /// </summary>
    /// <param name="dtSource"></param>
    /// <param name="sheet"></param>
    private void RangeSheet(DataTable dtSource, HSSFSheet sheet)
    { 
        #region 用户相关信息

        for (int i = 0; i < dtSource.Rows.Count ; )
        {
            string realName = dtSource.Rows[i]["LoginName"].ToString();

            DataRow[] dr = dtSource.Select("LoginName='" + realName + "'");
            int regionCount = dr.Count();

            if (dr.Count() > 1)
            {
                for (int col = 0; col < 7; col++)
                {
                    sheet.AddMergedRegion(new Open.Excel.HSSF.Util.Region(i + 1, col, i + regionCount, col));
                }
            }
            i = i + regionCount;
        }
        #endregion

        #region 项目信息

        for (int intItem = 0; intItem < dtSource.Rows.Count; )
        {
            string itemName = dtSource.Rows[intItem]["ItemName"].ToString();
            string RealNameItem = dtSource.Rows[intItem]["LoginName"].ToString();
            DataRow[] drItem = dtSource.Select("LoginName='" + RealNameItem + "' and ItemName='" + itemName + "'");
            if (drItem.Count() > 1)
            {
                sheet.AddMergedRegion(new Open.Excel.HSSF.Util.Region(intItem + 1, 7, intItem + drItem.Count(), 7));
            }
            intItem = intItem + drItem.Count();
        }

        #endregion

        #region 课程相关信息
        for (int iCourse = 0; iCourse < dtSource.Rows.Count; )
        {
            string cRealName = dtSource.Rows[iCourse]["LoginName"].ToString();
            string cItemName = dtSource.Rows[iCourse]["ItemName"].ToString();
            string courseName = dtSource.Rows[iCourse]["CourseName"].ToString();
            DataRow[] drCourse = dtSource.Select("LoginName='" + cRealName + "' and ItemName='" + cItemName + "' and " + " CourseName='" + courseName + "'");
            int regionCount = drCourse.Count();
            if (regionCount > 1)
            {
                for (int col = 8; col < 14; col++)
                {
                    sheet.AddMergedRegion(new Open.Excel.HSSF.Util.Region(iCourse + 1, col, iCourse + regionCount, col));
                }
            }
            iCourse = iCourse + regionCount;
        }
        #endregion

    }

}