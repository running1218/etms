using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Utility;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;
using ETMS.AppContext;

public partial class QS_ImportStudent : System.Web.UI.Page
{


    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>

    private static int succssCount = 0;
    private string QueryID;
    public static QS_Query QueryEntity = new QS_Query();
    QS_QueryLogic QueryBiz = new QS_QueryLogic();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        QueryID = Request.QueryString["QueryID"];
        if (!Page.IsPostBack)
        {
            bind();
        }

    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        QueryEntity = QueryBiz.GetById(new Guid(QueryID));
        if (QueryEntity != null)
        {
            //lblItemCode.Text = item.ItemCode;
            lblItemName.Text = QueryEntity.QueryName;
            lblItemTime.Text = QueryEntity.BeginTime.ToDate() + " 至 " + QueryEntity.EndTime.ToDate();
            lblRemark.Text = QueryEntity.Remark.Trim();
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

        //3、由学员导入逻辑处理结果并返回任务执行结果
        int errorNum = 0;
        bool flag = new Import_SurveyAreaLogic().CheckOutImportData(int.Parse(QueryID),1, fileDefine.FileName, task, xlsFilePath, QueryEntity.OrganizationID, ETMS.AppContext.UserContext.Current.RealName, ETMS.AppContext.UserContext.Current.UserID,"", out errorNum);


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
        if (succssCount > 0)
        {
            ClientScriptManager CSM = Page.ClientScript;
            string ScriptName = "clientScript";
            if (!CSM.IsClientScriptBlockRegistered(ScriptName))
            {
                //string strScript = strjs;
                CSM.RegisterStartupScript(this.GetType(), ScriptName, "<script>javascript:CloseRefesh();</script>");
            }
            //this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "function(){alert('ccc')}");
        }
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
        System.IO.File.Copy(Server.MapPath("~/QS/Template/PollStudentImport.xls"), fullXlsFilePath, true);

        //Excel导出
        FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }
}