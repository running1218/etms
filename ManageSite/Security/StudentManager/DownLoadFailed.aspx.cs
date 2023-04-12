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
        Import_Detail_StudentLogic logic = new Import_Detail_StudentLogic();
        int totalRecords = 0;
        string filter = " and taskid=" + Request.QueryString["taskid"];
        System.Data.DataTable dt = logic.GetPagedList(1, 9999999, " taskid asc", filter, out totalRecords);
        //导出的excel文件名
        string xlsFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("导出模板.xls"), fullXlsFilePath, true);
        //按照excelsheet结构构造数据表
        System.Data.DataTable exportDT = new System.Data.DataTable();
        exportDT.Columns.Add("学员账号", typeof(string));
        exportDT.Columns.Add("学员姓名", typeof(string));
        exportDT.Columns.Add("学员身份", typeof(string));
        exportDT.Columns.Add("邮箱", typeof(string));
        exportDT.Columns.Add("手机", typeof(string));
        exportDT.Columns.Add("工号", typeof(string));
        exportDT.Columns.Add("性别", typeof(string));
        exportDT.Columns.Add("身份证号", typeof(string));
        exportDT.Columns.Add("工作职务", typeof(string));
        exportDT.Columns.Add("直接上级", typeof(string));
        exportDT.Columns.Add("出生日期", typeof(string));
        exportDT.Columns.Add("电话", typeof(string));
        exportDT.Columns.Add("最高学历", typeof(string));
        exportDT.Columns.Add("专业", typeof(string));
        exportDT.Columns.Add("入职日期", typeof(string));

        exportDT.Columns.Add("状态", typeof(string));
        exportDT.Columns.Add("描述", typeof(string));
        foreach (System.Data.DataRow row in dt.Rows)
        {
            System.Data.DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);

            newRow["学员账号"] = (string)row["LoginName"];
            newRow["学员姓名"] = (string)row["RealName"];
            newRow["学员身份"] = (string)row["ResettlementWayName"];
            newRow["邮箱"] = (string)row["Email"];
            newRow["手机"] = (string)row["Mobile"];
            newRow["工号"] = (string)row["WorkerNo"];
            newRow["性别"] = 1 == (int)row["SexTypeID"] ? "男" : "女";
            newRow["身份证号"] = (string)row["Identity"];
            newRow["工作职务"] = (string)row["TitleName"];
            newRow["直接上级"] = (string)row["Superior"];
            newRow["出生日期"] = DBNull.Value.Equals(row["Birthday"]) ? "" : row["Birthday"];
            newRow["电话"] = (string)row["OfficeTelphone"];
            newRow["最高学历"] = (string)row["LastEducation"];
            newRow["专业"] = (string)row["Specialty"];
            newRow["入职日期"] = DBNull.Value.Equals(row["JoinTime"]) ? "" : row["JoinTime"];

            newRow["状态"] = (0 == (Int16)row["Status"]) ? "失败" : "";
            newRow["描述"] = (string)row["Remark"];
        }
        //2、输出Excel文件
        ExcelDataAccess EDA = new ExcelDataAccess();
        EDA.SaveExcel(exportDT, "", fullXlsFilePath, "Sheet1");
        FileDownLoadUtility.DownFile(fullXlsFilePath);
    }

}