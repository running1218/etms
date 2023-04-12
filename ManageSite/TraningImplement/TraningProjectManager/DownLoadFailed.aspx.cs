using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Utility;
using ETMS.Utility.Data;

public partial class TraningImplement_TraningProjectManager_DownLoadFailed : BasePage
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
        }

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
        Import_StudentSignupLogic logic = new Import_StudentSignupLogic();
        int totalRecords = 0;
        string filter = " and taskid=" + Request.QueryString["taskid"];
        System.Data.DataTable dt = logic.GetPagedList(1, int.MaxValue - 1, " taskid asc", filter, out totalRecords);
        //导出的excel文件名
        string xlsFileName = string.Format("导入项目“{0}”学员错误.xls", new ETMS.Components.Basic.Implement.BLL.TrainingItem.Tr_ItemLogic().GetById(TrainingItemID).ItemName);
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/TraningImplement/TraningProjectManager/Template/导入培训项目学员错误模板.xls"), fullXlsFilePath, true);
        //按照excelsheet结构构造数据表
        System.Data.DataTable exportDT = new System.Data.DataTable();
        exportDT.Columns.Add("学员账号");
        exportDT.Columns.Add("学员姓名");
        //exportDT.Columns.Add("部门");
        //exportDT.Columns.Add("岗位");
        //exportDT.Columns.Add("职级");

        exportDT.Columns.Add("状态");
        exportDT.Columns.Add("描述");
        foreach (System.Data.DataRow row in dt.Rows)
        {
            System.Data.DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);

            newRow["学员账号"] = (string)row["LoginName"];
            newRow["学员姓名"] = (string)row["RealName"];
            //newRow["部门"] = (string)row["DepartmentName"];
            //newRow["岗位"] = (string)row["PostName"];
            //newRow["职级"] = (string)row["RankName"];

            newRow["状态"] = (0 == (Int16)row["Status"]) ? "失败" : "";
            newRow["描述"] = (string)row["Remark"];
        }
        //2、输出Excel文件
        ExcelDataAccess EDA = new ExcelDataAccess();
        EDA.SaveExcel(exportDT, "", fullXlsFilePath, "Sheet1");
        FileDownLoadUtility.DownFile(fullXlsFilePath);
    }
}