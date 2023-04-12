using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility;
using ETMS.Utility.Data;
using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Utility.Service.FileUpload;
using ETMS.Controls;
public partial class Security_StudentManager_DownLoadFailed : BasePage
{
    protected override RequestParameter[] PageRequestArgs
    {
        get
        {
            return new RequestParameter[] { RequestParameter.CreateRangeRequestParameter("taskid", RequestParameter.NaturalInt32RangeVerify) };
        }
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    if (!control.GetType().Equals(GridView1.GetType()))
    //    {
    //        base.VerifyRenderingInServerForm(control);
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * 说明：平台支持两种方式的Excel导出
         * 1、GridView + FileDownLoadUtility.ExportToExcel() + override void VerifyRenderingInServerForm
         *   优点：方便导出，导出数据样式完全有GridView来定义
         *   缺点：丧失了Excel特有支持（输出本质上是Html,仅能Excel打开，不能再次做为附件导入！）
         * 2、DataTable+ExcelDataAccess.SaveExcel
         *  优点：保持了Excel特有文件格式，能够再次做为附件导入！
         *  缺点：需要一份导出.xls文件及对应数据结构的DataTable
         */
        int taskid = int.Parse(Request.QueryString["taskid"]);
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = taskLogic.GetById(taskid);

        //1、提取任务对应的数据
        Import_SurveyAreaLogic logic = new Import_SurveyAreaLogic();

       
        string filter = " and taskid=" + Request.QueryString["taskid"];
        System.Data.DataTable dt = logic.GetPollImportStudentList(taskid).Tables[0];
        //导出的excel文件名
        string xlsFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("../Template/PollStudentImportError.xls"), fullXlsFilePath, true);
        //按照excelsheet结构构造数据表
        System.Data.DataTable exportDT = new System.Data.DataTable();
        exportDT.Columns.Add("用户名", typeof(string));
        exportDT.Columns.Add("姓名", typeof(string));
        exportDT.Columns.Add("工号", typeof(string));
        exportDT.Columns.Add("组织机构", typeof(string));
        exportDT.Columns.Add("部门", typeof(string));
        exportDT.Columns.Add("岗位", typeof(string));
        exportDT.Columns.Add("职级", typeof(string));
        exportDT.Columns.Add("邮箱", typeof(string));
        exportDT.Columns.Add("状态", typeof(string));
        exportDT.Columns.Add("描述", typeof(string));
        foreach (System.Data.DataRow row in dt.Rows)
        {
            System.Data.DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);

            newRow["用户名"] = (string)row["LoginName"];
            newRow["姓名"] = (string)row["RealName"];
            newRow["工号"] = (string)row["WorkNo"];
            newRow["组织机构"] = (string)row["DisplayPath"];
            newRow["部门"] = (string)row["DepartmentName"];
            newRow["岗位"] = (string)row["PostName"];
            newRow["职级"] = (string)row["RankName"];
            newRow["邮箱"] = (string)row["Email"];
           

            newRow["状态"] = (0 == (Int16)row["Status"]) ? "失败" : "";
            newRow["描述"] = (string)row["Remark"];
        }
        //2、输出Excel文件
        ExcelDataAccess EDA = new ExcelDataAccess();
        EDA.SaveExcel(exportDT, "", fullXlsFilePath, "Sheet1");
        FileDownLoadUtility.DownFile(fullXlsFilePath);
    }

}