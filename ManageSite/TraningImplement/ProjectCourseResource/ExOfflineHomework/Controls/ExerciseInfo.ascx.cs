using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.ExOfflineHomework.Implement;
using ETMS.Controls;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;


public partial class QuestionDB_ExOfflineHomework_Controls_ExerciseInfo : System.Web.UI.UserControl
{
    public Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    public OffLineJob OfflineJob = new OffLineJob();
    //public Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    //public Res_CourseLogic courseLogic = new Res_CourseLogic();
    //public Tr_Item trItem = new Tr_Item();
    //public Res_Course resCourse = new Res_Course();
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    #region 页面条件参数存放

    /// <summary>
    /// 获取项目id
    /// </summary>
    protected Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
            {
                //ViewState["TrainingItemCourseID"] = new Guid("a7f97727-b0c2-4611-8060-ca05c3437227");
                ViewState["TrainingItemCourseID"] = BasePage.UrlParamDecode(Request.QueryString["TrainingItemCourseID"]).ToGuid();
            }
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    ///// <summary>
    ///// 获取课程id
    ///// </summary>
    //protected Guid CourseID
    //{
    //    get
    //    {
    //        if (ViewState["CourseID"] == null)
    //        {
    //            ViewState["CourseID"] = BasePage.UrlParamDecode(Request.QueryString["CourseID"]).ToGuid();
    //        }
    //        return ViewState["CourseID"].ToGuid();
    //    }
    //    set
    //    {
    //        ViewState["CourseID"] = value;
    //    }
    //}
    /// <summary>
    /// 保存是添加还是修改
    /// </summary>
    protected string Op
    {
        get
        {
            if (ViewState["Op"] == null)
            {
                string temp = Request.QueryString["op"].ToLower();
                ViewState["Op"] = BasePage.UrlParamDecode(Request.QueryString["op"].ToLower());
            }
            return (string)ViewState["Op"];
        }
        set
        {
            ViewState["Op"] = value;
        }
    }
    /// <summary>
    /// 项目id
    /// </summary>
    protected Guid JobID
    {
        get
        {
            if (ViewState["JobID"] == null)
            {
                string obj = Request.QueryString["id"].ToLower();
                ViewState["JobID"] = new Guid(BasePage.UrlParamDecode(Request.QueryString["id"].ToLower()));
            }
            return (Guid)ViewState["JobID"];
        }
        set
        {
            ViewState["JobID"] = value;
        }
    }

    #endregion
    /// <summary>
    /// 保存实例
    /// </summary>
    protected Res_e_OffLineJob entity
    {
        get
        {
            if (ViewState["Entity"] == null)
            {
                ViewState["Entity"] = new Res_e_OffLineJob();
            }
            return (Res_e_OffLineJob)ViewState["Entity"];
        }
        set
        {
            ViewState["Entity"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Intital();
        }

    }

    private void Intital()
    {
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemCourseID(TrainingItemCourseID);       
        this.lblItemID.Text = dt.Rows[0]["ItemName"].ToString();
        this.lblCourseID.Text = dt.Rows[0]["CourseName"].ToString();
        this.dttbBeginDate.Text = dt.Rows[0]["CourseBeginTime"].ToDate();
        this.dttbEndDate.Text = dt.Rows[0]["CourseEndTime"].ToDate();
        switch (Op)
        {
            case "edit":
                entity = Logic.GetById(JobID);
                this.txtJobName.Text = this.entity.JobName;
                this.txtJobDescription.Text = this.entity.JobDescription;
                string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", this.entity.JobFileURL);
                this.lblFilePath.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ", fullUrl, this.entity.JobFileName);
                this.rblStatus.SelectedValue = this.entity.IsUse.ToString();
                this.dttbBeginDate.Text = entity.BeginTime.ToDate();
                this.dttbEndDate.Text = entity.EndTime.ToDate();
                break;
            default:
                break;
        }
        
    }

    protected void lbnSave_Click(object sender, EventArgs e)
    {
        switch (Op)
        {
            case "add":
                Add();
                break;
            case "edit":
                Edit();
                break;

        }
    }

    protected void Add()
    {
        entity.JobID = Guid.NewGuid();

        if (!String.IsNullOrEmpty(this.txtJobName.Text))
            entity.JobName = this.txtJobName.Text;

        
        entity.JobDescription = this.txtJobDescription.Text;

        entity.IsUse = this.rblStatus.SelectedValue.ToInt();

        if (!String.IsNullOrEmpty(this.dttbBeginDate.Text))
            entity.BeginTime = this.dttbBeginDate.Text.ToDateTime();
        if (!String.IsNullOrEmpty(this.dttbEndDate.Text))
            entity.EndTime = this.dttbEndDate.Text.ToDateTime();
        entity.TeacherID = ETMS.AppContext.UserContext.Current.UserName;

        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

        if (fileDefine != null)
        {           
            entity.JobFileName = fileDefine.FileOldName;//离线作业附件路径
            entity.JobFileURL = fileDefine.BizUrl;
            entity.JobFileSize = fileDefine.FileSize;
        }

        entity.CreateTime = System.DateTime.Now;
        entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
        entity.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;

        try
        {
            //Logic.Add(entity);            
            //OfflineJob.SetOfflineJobToItemCourse(entity.JobID.ToString(), TrainingItemCourseID);
            Logic.AddItemCourseOffLineJob(entity, TrainingItemCourseID);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }

    }
    protected void Edit()
    {

        if (!String.IsNullOrEmpty(this.txtJobName.Text))
            entity.JobName = this.txtJobName.Text;

        entity.JobDescription = this.txtJobDescription.Text;

        entity.IsUse = Convert.ToInt16(this.rblStatus.SelectedValue);
        if (!String.IsNullOrEmpty(this.dttbBeginDate.Text))
            entity.BeginTime = this.dttbBeginDate.Text.ToDateTime();
        if (!String.IsNullOrEmpty(this.dttbEndDate.Text))
            entity.EndTime = this.dttbEndDate.Text.ToDateTime();

        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

        if (fileDefine != null)
        {
            entity.JobFileName = fileDefine.FileName;//离线作业附件路径
            entity.JobFileURL = fileDefine.BizUrl;
            entity.JobFileSize = fileDefine.FileSize;          
        }

        entity.ModifyTime = DateTime.Now;
        entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        try
        {
            Logic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

}