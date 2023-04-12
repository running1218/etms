﻿using System;
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

public partial class TraningImplement_TraningProjectManager_ImportStudent : BasePage
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
            bind();
        }
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;
            lblItemTime.Text = item.ItemBeginTime.ToDate()+" 至 "+item.ItemEndTime.ToDate();

            hfOrganizationID.Value = item.OrgID.ToString();
        }
    }

    /// <summary>
    /// 导入信息
    /// </summary>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

        if (fileDefine == null)
        {
            JsUtility.AlertMessageBox("请上传文件！");
            return;
        }

        //1、提取上传文件信息
        //文件物理路径
        string xlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("ImportStudentInfo", fileDefine.BizUrl);

        //2、新增数据导入任务 
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = new Import_Task()
        {
            OrganizationID = UserContext.Current.OrganizationID,//任务所属机构
            ImportTypeID = (int)ImportType .TrainingItemStudent,//导入类型
            TaskName = string.Format("项目学员信息导入({0})", DateTime.Now.ToString("yyyyMMddHHmmss")),
            FilePath = fileDefine.BizUrl,
            FilleName = fileDefine.FileName,
            CreateTime = DateTime.Now,
            CreatorID = UserContext.Current.UserID,
        };
        taskLogic.Save(task);

        //3、由学员导入逻辑处理结果并返回任务执行结果
        int errorNum = 0;
        bool flag = new Import_StudentSignupLogic().ImportStudentInfo(task, xlsFilePath, TrainingItemID, ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName, out errorNum);
        if (!flag || errorNum > 0)//导入失败
        {
            this.lblRemark.Text = string.Format("{0}{1}", task.Remark, errorNum > 0 ? ",<span style='color:Red'> " + errorNum + " </span>条数据错误。" : "");//显示导入失败信息
            if (task.Status == 2)//到数据校验步骤后才提供下载功能，供用户对比修改。
            {
                this.lblRemark.Text += string.Format("<a href='{0}'>查看详情</a>", this.ActionHref(string.Format("DownLoadFailed.aspx?taskid={0}&TrainingItemID={1}", task.TaskID.ToString(), TrainingItemID)));
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


    /// <summary>
    /// 导出学员信息
    /// </summary>
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        //copy template.xls到temp文件夹
        string xlsFileName = string.Format("培训项目“{0}”学员导入.xls", lblItemName.Text);
        string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        //复制excel
        System.IO.File.Copy(Server.MapPath("~/TraningImplement/TraningProjectManager/Template/导入培训项目学员模板.xls"), fullXlsFilePath, true);

        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }
}