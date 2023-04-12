using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.AppContext;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Utility.Data;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.QS.Implement.BLL;

public partial class TraningImplement_TraningProjectManager_ImportStudent : BasePage
{



    public Poll_QueryArea CurrentQueryArea
    {
        get
        {
            return (Poll_QueryArea)ViewState["CurrentQueryArea"];
        }
        set
        {
            ViewState["CurrentQueryArea"] = value;
        }
    }
    public string ResourceType
    {
        get
        {
            return "R2";
        }
    }
    public string ResourceCode
    {
        get
        {
            //case "R2":
            return "00000000-0000-0000-0000-000000000002";
        }
    }

    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
    private static Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
    public string orgType;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        QueryID = int.Parse(Request.QueryString["QueryID"]);
        orgType = Request.Params["orgtype"];
        if (!Page.IsPostBack)
        {
            this.CurrentQueryArea = QueryAreaLogic.GetResourceQueryArea(QueryID, this.ResourceType, this.ResourceCode);
            bind();
        }

        //this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "function(){alert('ccc')}");
    }
    public static int QueryID;
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Poll_QueryLogic Logic = new Poll_QueryLogic();
        //Tr_Item item = itemLogic.GetById(TrainingItemID);

        Poll_Query pl = Logic.GetById(QueryID);
        if (pl != null)
        {
            //lblItemCode.Text = item.ItemCode;
            lblItemName.Text = pl.QueryName;
            lblItemTime.Text = pl.BeginTime.ToDate() + " 至 " + pl.EndTime.ToDate();
            lblRemark.Text = pl.Remark.Trim();
            //hfOrganizationID.Value = item.OrgID.ToString();
        }
    }

    /// <summary>
    /// 导入信息
    /// </summary>
    protected void btnImport_Click(object sender, EventArgs e)
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
            OrganizationID = UserContext.Current.OrganizationID,//任务所属机构
            ImportTypeID = (int)ImportType.TrainingItemStudent,//导入类型
            TaskName = string.Format("需求调查范围学员信息导入({0})", DateTime.Now.ToString("yyyyMMddHHmmss")),
            FilePath = fileDefine.BizUrl,
            FilleName = fileDefine.FileName,
            CreateTime = DateTime.Now,
            CreatorID = UserContext.Current.UserID,
        };
        taskLogic.Save(task);

        ////3、由学员导入逻辑处理结果并返回任务执行结果
        ////int errorNum = 0;
        //ETMS.Components.Basic.Implement.BLL.Import.Import_PollStudentLogic import_PollStudentLogic = new ETMS.Components.Basic.Implement.BLL.Import.Import_PollStudentLogic();
        //import_PollStudentLogic.ExcelPath = xlsFilePath;
        //import_PollStudentLogic.ImportTask = task;
        //import_PollStudentLogic.QueryID = QueryID;
        //import_PollStudentLogic.Creater = ETMS.AppContext.UserContext.Current.RealName;
        //import_PollStudentLogic.QueryPublishID = this.CurrentQueryArea.QueryPublishID;
        ////import_PollStudentLogic.
        //int errorNum = 0;
        //bool flag = import_PollStudentLogic.CheckOutImportData(this.CurrentQueryArea.QueryAreaID, out errorNum, out succssCount);
        ////int errorNum = 1;
        Poll_QueryLogic Logic = new Poll_QueryLogic();
        Poll_Query pl = Logic.GetById(QueryID);
        //3、由学员导入逻辑处理结果并返回任务执行结果
        int errorNum = 0;
        bool flag = new Import_SurveyAreaLogic().CheckOutImportData(QueryID, this.CurrentQueryArea.QueryAreaID, fileDefine.FileName, task, xlsFilePath, pl.OrganizationID, ETMS.AppContext.UserContext.Current.RealName, ETMS.AppContext.UserContext.Current.UserID, orgType, out errorNum);


        if (!flag || errorNum > 0)//导入失败
        {
            this.lblRemark.Text = string.Format("{0}{1}", task.Remark, errorNum > 0 ? "<span style='color:Red'> " + errorNum + " </span>条数据错误。" : "");//显示导入失败信息
            if (task.Status == 2)//到数据校验步骤后才提供下载功能，供用户对比修改。
            {
                this.lblRemark.Text += string.Format("<a href='{0}'>查看详情</a>", this.ActionHref(string.Format("DownLoadFailed.aspx?taskid={0}&TrainingItemID={1}", task.TaskID.ToString(), QueryID)));
            }

            JsUtility.FailedMessageBox("提示", "导入学员失败！");


            return;
        }

        if (!flag || errorNum > 0)//导入失败
        {
            this.lblRemark.Text = string.Format("{0}{1}", task.Remark, errorNum > 0 ? "<span style='color:Red'> " + errorNum + " </span>条数据错误。" : "");//显示导入失败信息
            if (task.Status == 2)//到数据校验步骤后才提供下载功能，供用户对比修改。
            {
                this.lblRemark.Text += string.Format("<a href='{0}'>查看详情</a>", this.ActionHref(string.Format("DownLoadFailed.aspx?taskid={0}&TrainingItemID={1}", task.TaskID.ToString(), QueryID)));
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

    protected void CloseWindow(object sender, EventArgs e)
    {
        JsUtility.CloseWindow("function(){window.parent.location.href=window.parent.location.href;}");
        return;
    }

    /// <summary>
    /// 导出学员信息
    /// </summary>
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        //copy template.xls到temp文件夹
        string xlsFileName = string.Format("调查范围“{0}”学员导入.xls", lblItemName.Text);
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/poll/Template/PollStudentImport.xls"), fullXlsFilePath, true);

        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }
}