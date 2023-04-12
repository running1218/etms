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
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Security_StudentManager_StudentImport : ETMS.Controls.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
            AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
            this.txtDefaultPassword.Text = strategy.Security_PassWord_Default;//系统默认密码,基于数据库
            //this.txtDefaultPassword.Text = ApplicationContext.Current.AppSettings["Security.DefaultPassword"];//系统默认密码，基于配置文件
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        if (uploaders.Count == 0)
        {
            JsUtility.AlertMessageBox("请上传文件！");
            return;
        }

        ////1、提取上传文件信息
        FileUploadInfo fileDefine = this.uploader.FileUrl[0];
        //文件物理路径

        string xlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("ImportStudentInfo", fileDefine.BizUrl);

        //2、新增数据导入任务 
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = new Import_Task()
          {
              OrganizationID = UserContext.Current.OrganizationID,//任务所属机构
              ImportTypeID = (int)ImportType.StudentInfo,//导入类型
              TaskName = string.Format("导入学员信息({0})", DateTime.Now.ToString("yyyyMMddHHmmss")),
              FilePath = fileDefine.BizUrl,
              FilleName = fileDefine.FileName,
              CreateTime = DateTime.Now,
              CreatorID = UserContext.Current.UserID,
          };
        taskLogic.Save(task);

        //3、由学员导入逻辑处理结果并返回任务执行结果
        Import_Detail_StudentLogic importStudentLogic = new Import_Detail_StudentLogic();
        bool flag = importStudentLogic.ImportStudentInfo(task, xlsFilePath, this.txtDefaultPassword.Text);
        if (!flag)//导入失败
        {
            this.lblRemark.Text = task.Remark;//显示导入失败信息
            if (task.Status == 2)//到数据校验步骤后才提供下载功能，供用户对比修改。
            {
                this.lblRemark.Text += string.Format("<a href='{0}'>查看详情</a>", this.ActionHref("DownLoadFailed.aspx?taskid=" + task.TaskID.ToString()));
            }
            JsUtility.FailedMessageBox("导入学员失败！");
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