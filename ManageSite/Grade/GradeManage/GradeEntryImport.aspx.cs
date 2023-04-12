using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility.Data;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;


public partial class Grade_GradeManage_GradeEntryImport : ETMS.Controls.BasePage
{
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    public static Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();


    public Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControl();
        }
    }

    private void InitialControl()
    {
        try
        {
            int total = 0;
            StringBuilder whereQuery = new StringBuilder();
            whereQuery.Append(string.Format(" and Tr_Item.OrgID={0} and Tr_Item.IsIssue='1' and Tr_Item.IsIssue='1' and Tr_Item.ItemStatus='20'", ETMS.AppContext.UserContext.Current.OrganizationID));
            whereQuery.Append(string.Format(" and Tr_ItemCourse.TrainingItemCourseID='{0}'", TrainingItemCourseID));

            DataTable dt = itemCourseLogic.GetGradeIssueList(1, 1, string.Empty, whereQuery.ToString(), out total);
            this.lblItemCode.Text = dt.Rows[0]["ItemCode"].ToString();
            this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
            this.lblCourseName.Text = dt.Rows[0]["CourseName"].ToString();
            this.lblStudentNum.Text = studentcourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();
            //导出用到
            this.lblCourseCodeHide.Text = dt.Rows[0]["CourseCode"].ToString();
            this.lblCourseNameHide.Text = dt.Rows[0]["CourseName"].ToString();
            this.lblItemCodeHide.Text = dt.Rows[0]["ItemCode"].ToString();
            this.lblItemNameHide.Text = dt.Rows[0]["ItemName"].ToString();
        }
        catch{ }
    }

    private void ExcelExport()
    {
        ExcelDataAccess EDA = new ExcelDataAccess();
        //copy template.xls到temp文件夹
        string xlsFileName = string.Format("成绩导入{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/Grade/GradeManage/成绩导入模板.xls"), fullXlsFilePath, true);
        //保存物理文件
        EDA.SaveExcel(GetDataTable(), "", fullXlsFilePath, "sheet1");
        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);        
    }

    private DataTable GetDataTable()
    {
        DataTable exportDT = new DataTable();
        exportDT.Columns.Add("学员账号", typeof(string));
        exportDT.Columns.Add("学员姓名", typeof(string));
        exportDT.Columns.Add("总成绩", typeof(string));
        exportDT.Columns.Add("培训项目名称", typeof(string));
        exportDT.Columns.Add("培训项目编码", typeof(string));
        exportDT.Columns.Add("课程名称", typeof(string));
        exportDT.Columns.Add("课程编码", typeof(string));
        exportDT.Columns.Add("学员选课ID", typeof(string));

        int total = 0;
        int pageIndex = 1;
        int pageSize = int.MaxValue - 1;

        DataTable dt = itemCourseLogic.GetItemCourseStudentScoreList(TrainingItemCourseID, pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", string.Empty, out total);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow newRow = exportDT.NewRow();
            exportDT.Rows.Add(newRow);
            newRow["学员账号"] = dt.Rows[i]["LoginName"].ToString();
            newRow["学员姓名"] = dt.Rows[i]["RealName"].ToString();
            newRow["总成绩"] = string.IsNullOrEmpty(dt.Rows[i]["Remark"].ToString()) ? "" : dt.Rows[i]["SumGrade"].ToString().Split('.')[0].ToInt().ToString();
            newRow["培训项目名称"] = lblItemNameHide.Text;
            newRow["培训项目编码"] = lblItemCodeHide.Text;           
            newRow["课程名称"] =lblCourseNameHide.Text;
            newRow["课程编码"] = lblCourseCodeHide.Text;
            newRow["学员选课ID"] = dt.Rows[i]["StudentCourse"].ToString();
        }
        return exportDT;
    }

    protected void lbnExportStudentList_Click(object sender, EventArgs e)
    {
        try
        {
            ExcelExport();
        }
        catch(Exception ex){}
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {      

        FileUploadCard entity = this.fileUpload1.SaveUploadFiles();
        if (entity.FileDetails.Count == 0)
        {
            JsUtility.AlertMessageBox("请上传文件！");
            return;
        }

        //1、提取上传文件信息
        UploadFileDefine fileDefine = entity.FileDetails[0];
        //文件物理路径
        string xlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("ImportStudentInfo", fileDefine.BizUrl);

        //2、新增数据导入任务 
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = new Import_Task()
        {
            OrganizationID =ETMS.AppContext.UserContext.Current.OrganizationID,//任务所属机构
            ImportTypeID = (int)ImportType.StudentCourseGrade,//导入类型
            TaskName = string.Format("成绩导入({0})", DateTime.Now.ToString("yyyyMMddHHmmss")),
            FilePath = fileDefine.BizUrl,
            FilleName = fileDefine.FileName,
            CreateTime = DateTime.Now,
            CreatorID = ETMS.AppContext.UserContext.Current.UserID,
        };
        taskLogic.Save(task);

        //3、由学员导入逻辑处理结果并返回任务执行结果
        bool flag = new Import_StudentCourseGradeLogic().ImportStudentCourseGrade(task, xlsFilePath,ETMS.AppContext.UserContext.Current.RealName);
        if (!flag )//导入失败
        {
            this.lblDescription.Text = string.Format("{0}", task.Remark);//显示导入失败信息
            if (task.Status == 2)//到数据校验步骤后才提供下载功能，供用户对比修改。
            {
                this.lblDescription.Text += string.Format("<a href='{0}'>查看详情</a>", this.ActionHref(string.Format("DownLoadFiled.aspx?taskid={0}&TrainingItemCourseID={1}", task.TaskID.ToString(), TrainingItemCourseID)));
            }
            JsUtility.FailedMessageBox("提示", "导入学员失败！");
            return;
        }
        else
        {
            //刷新主窗口
            JsUtility.SuccessMessageBoxAndCloseWindow("导入学员成功！", "function(){window.parent.location.href=window.parent.location.href;}");
            return;
        }

    }
}