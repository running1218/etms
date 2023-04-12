using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using System.Data;
using ETMS.Utility.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;
using System.Text;

public partial class Grade_GradeManage_DownLoadFiled : ETMS.Controls.BasePage
{
    #region 页面参数
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();

    public Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }   
    #endregion

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
        Import_StudentCourseGradeLogic logic = new Import_StudentCourseGradeLogic();
        int totalRecords = 0;
        string filter = " and taskid=" + Request.QueryString["taskid"];
        System.Data.DataTable dt = logic.GetPagedList(1, int.MaxValue-1, " taskid asc", filter, out totalRecords);
        //导出的excel文件名
        string xlsFileName = string.Format("成绩导入错误{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/Grade/GradeManage/成绩导入错误模板.xls"), fullXlsFilePath, true);
        //按照excelsheet结构构造数据表
        DataTable exportDT = new DataTable();
        exportDT.Columns.Add("学员账号", typeof(string));
        exportDT.Columns.Add("学员姓名", typeof(string));
        exportDT.Columns.Add("总成绩", typeof(string));
        exportDT.Columns.Add("培训项目名称", typeof(string));
        exportDT.Columns.Add("培训项目编码", typeof(string));
        exportDT.Columns.Add("课程名称", typeof(string));
        exportDT.Columns.Add("课程编码", typeof(string));
        exportDT.Columns.Add("学员选课ID", typeof(string));
        exportDT.Columns.Add("状态");
        exportDT.Columns.Add("描述");
        int total = 0;
        //int pageIndex = 1;
        //int pageSize = int.MaxValue - 1;

        //项目课程信息
        total = 0;
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(string.Format(" and Tr_Item.OrgID={0} and Tr_Item.IsIssue='1' and Tr_Item.IsIssue='1' and Tr_Item.ItemStatus='20'", ETMS.AppContext.UserContext.Current.OrganizationID));
        whereQuery.Append(string.Format(" and Tr_ItemCourse.TrainingItemCourseID='{0}'", TrainingItemCourseID));

        DataTable dtItemCourse = itemCourseLogic.GetGradeIssueList(1, 1, string.Empty, whereQuery.ToString(), out total);

        total = 0;
        //DataTable dtGrade = itemCourseLogic.GetItemCourseStudentScoreList(TrainingItemCourseID, pageIndex, pageSize, string.Empty, string.Empty, out total);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);
            newRow["学员账号"] = dt.Rows[i]["LoginName"].ToString();
            newRow["学员姓名"] = dt.Rows[i]["RealName"].ToString();
            newRow["总成绩"] = dt.Rows[i]["SumGrade"].ToString();
            newRow["培训项目名称"] = dtItemCourse.Rows[0]["CourseName"].ToString();
            newRow["培训项目编码"] = dtItemCourse.Rows[0]["CourseCode"].ToString();
            newRow["课程名称"] = dtItemCourse.Rows[0]["ItemName"].ToString();
            newRow["课程编码"] = dtItemCourse.Rows[0]["ItemCode"].ToString();
            newRow["学员选课ID"] = dt.Rows[i]["StudentCourseID"].ToString();
            newRow["状态"] = (0 == (Int16)dt.Rows[i]["Status"]) ? "失败" : "";
            newRow["描述"] = (string)dt.Rows[i]["Remark"]; 
        }
        
        //2、输出Excel文件
        ExcelDataAccess EDA = new ExcelDataAccess();
        EDA.SaveExcel(exportDT, "", fullXlsFilePath, "Sheet1");
        FileDownLoadUtility.DownFile(fullXlsFilePath);
    }
}