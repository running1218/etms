using System;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Scrom.Implement.BLL;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;

public partial class Resource_CoursewareManage_ImportScormFiles : ETMS.Controls.BasePage
{
    private static readonly Res_CoursewareLogic res_CoursewareLogic = new Res_CoursewareLogic();

    #region 页面条件参数存放

    //课件ID
    public Guid CoursewareID
    {
        get
        {
            return (Guid)ViewState["CoursewareID"];
        }
        set
        {
            ViewState["CoursewareID"] = value;
        }
    }
    public Res_Courseware CoursewareEntity
    {
        get
        {
            return (Res_Courseware)ViewState["ResCourseware"];
        }
        set
        {
            ViewState["ResCourseware"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        CoursewareID = Request.ToparamValue<Guid>("CoursewareID");

        if (!IsPostBack)
        {
            InitControl();
            btnUpFile2.Attributes["onclick"] = string.Format("javascript:showWindow('上传文件','{0}',450,350);javascript:return false;"
                , this.ActionHref(string.Format("UpFile3.aspx?CoursewareID={0}&AllowType=FtpScromAllowType&FunctionType=ScormUp", CoursewareID)));
        }
    }

    //初始化控件值
    private void InitControl()
    {
        CoursewareEntity = res_CoursewareLogic.GetById(CoursewareID);
        ltlCoursewareName.Text = CoursewareEntity.CoursewareName;
        txtFileName.Text = CoursewareEntity.ResourcePath;
        lblFileName.Text = CoursewareEntity.ResourcePath;
        string message = string.Empty;

        switch (CoursewareEntity.ResourceStatus)
        { 
            case 0:
                message = "未上传";
                break;
            case 1:
                message = "已上传，未导入";
                lbtnSave.Enabled = true;
                break;
            case 2:
                message = "已上传，导入失败";
                lbtnSave.Enabled = true;
                break;
            case 3:
                message = "已上传, 已导入";
                btnUpFile2.Enabled = false;
                lbtnSave.Enabled = false;
                btnUpFile2.CssClass = "btn_upload_enabled";
                break;
        }

        lblState.Text ="<span class='colorGreen'>" + message + "</span>";        
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {

        try
        {
            string root = (ETMS.Utility.Service.ServiceRepository.FileUploadStrategyService as DefaultFileUploadStrategyService).Root;

            string filePath = string.Format("{0}\\UploadFiles\\{1}\\{2}", root, DateTime.Now.Year, DateTime.Now.ToString("MM"));
            string fileName = txtFileName.Text;
            ImportScormPackgeLogic importScorm = new ImportScormPackgeLogic();
            string uncompressPath = string.Format("\\{0}\\", CoursewareID);
            importScorm.ImportScormPackge(filePath, fileName, CoursewareID, uncompressPath);

            // 更新上传文件路径
            CoursewareEntity.CoursewarePath = uncompressPath;
            CoursewareEntity.ResourceStatus = 3;
            res_CoursewareLogic.Save(CoursewareEntity, Guid.Empty, CoursewareID, ETMS.AppContext.OperationAction.Edit); 

            ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("课件信息保存成功，点击“确定”后，重新刷新当前页数据！");
        }
        catch (ETMS.AppContext.BusinessException ex)
        {            
            CoursewareEntity.ResourceStatus = 2;
            res_CoursewareLogic.Save(CoursewareEntity, Guid.Empty, CoursewareID, ETMS.AppContext.OperationAction.Edit); 
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ex.Message);
        }
        catch
        {            
            CoursewareEntity.ResourceStatus = 2;
            res_CoursewareLogic.Save(CoursewareEntity, Guid.Empty, CoursewareID, ETMS.AppContext.OperationAction.Edit);
            ETMS.WebApp.Manage.Extention.FailedMessageBox("Scorm课件导入失败，请检查课件或与管理员联系！"); 
        }
    }
}