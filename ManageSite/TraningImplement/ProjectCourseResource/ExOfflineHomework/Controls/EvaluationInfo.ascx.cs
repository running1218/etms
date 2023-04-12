using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;

public partial class TraningImplement_ProjectCourseResource_ExOfflineHomework_Controls_EvaluationInfo : System.Web.UI.UserControl, IMuliViewControl
{
    private static PublicFacade publicFacade = new PublicFacade();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Res_e_OffLineJobLogic offlineJobLogic = new Res_e_OffLineJobLogic();
    private static Sty_StudentCourseLogic studentCourseLogic = new Sty_StudentCourseLogic();
    private static Site_StudentLogic studentLogic = new Site_StudentLogic();
    private static Tr_Item item = new Tr_Item();
    private static Res_e_OffLineJob offlineJob = new Res_e_OffLineJob();
    protected static Site_Student student = new Site_Student();
    protected static UserLogic userLogic = new UserLogic();
    protected static User userInfo = new User();
    /// <summary>
    /// 培训项目编号
    /// </summary>
    private Guid trainingItemID;
    public Guid TrainingItemID
    {
        get { return trainingItemID; }
        set { trainingItemID = value; }
    }

    /// <summary>
    /// 课程编号
    /// </summary>
    private Guid courseID;
    public Guid CourseID
    {
        get { return courseID; }
        set { courseID = value; }
    }
    
    /// <summary>
    /// 离线作业编号
    /// </summary>
    private Guid studentJobID;
    public Guid StudentJobID
    {
        get { return studentJobID; }
        set { studentJobID = value; }
    }
    /// <summary>
    /// 离线作业编码
    /// </summary>
    private Guid jobID;
    public Guid JobID
    {
        get { return jobID; }
        set { jobID = value; }
    }
    /// <summary>
    /// 离线作业编码
    /// </summary>
    private int studentID;
    public int StudentID
    {
        get { return studentID; }
        set { studentID = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region IMuliViewControl 成员
    private object m_DomainModel;
    public Object DomainModel
    {
        get
        {
            return this.Views.DomainModel;
        }
        set
        {
            m_DomainModel = value;
        }
    }

    public ViewMode ControlMode
    {
        get
        {
            return this.Views.ControlMode;
        }
    }

    #region Manage View
    public void BindFromData_Manage(object domainModel)
    {
        
    }    
    #endregion

    #region Edit_View
    public void BindFromData_Edit(object domainModel)
    {
        if (!this.Page.IsPostBack)
        {
            //单机构版本隐藏“组织机构”列
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrgnization.Visible = false;
            }
            Sty_StudentOffLineJob entity = (Sty_StudentOffLineJob)domainModel;
            this.lblCourseIDEdit.Text = publicFacade.GetCourseNameByID(courseID);
            item = itemLogic.GetById(TrainingItemID);
            this.lblItemIDEdit.Text = item.ItemName;
            offlineJob = offlineJobLogic.GetById(JobID);
            this.lblJobIDEdit.Text = offlineJob.JobName;//.JobFileName;
            this.lblTimeEdit.Text = offlineJob.BeginTime.ToString("yyyy-MM-dd") + "-" + offlineJob.EndTime.ToString("yyyy-MM-dd");
            student = studentLogic.GetById(studentID);
            this.lblWorkerNoEdit.Text = student.LoginName;
            userInfo = userLogic.GetUserByID(studentID);
            this.lblRealNameEdit.Text = userInfo.RealName;
            this.lblTelEdit.Text = userInfo.MobilePhone;
            this.lblMailEdit.Text = userInfo.Email;
            this.lblJobContentEdit.Text = offlineJob.JobDescription;
            //学生提交的作业附件
            string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", entity.UploadFilePath);
            this.lblUploadEdit.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ", fullUrl, entity.UploadFileName);

            this.lblUpLoadTimeEdit.Text = entity.UploadTime.ToString("yyyy-MM-dd");

            this.lblDepartmentIDEdit.FieldIDValue = userInfo.DepartmentID.ToString();
            this.lblPostIDEdit.FieldIDValue = student.PostID.ToString();
            this.lblOrgnization.FieldIDValue = userInfo.OrganizationID.ToString();
        }
    }
    #endregion

    #region Browse_View
    public void BindFromData_Browse(object domainModel)
    {
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.trOrg.Visible = false;
        }
        Sty_StudentOffLineJob entity = (Sty_StudentOffLineJob)domainModel;

        this.lblCourseID.Text = publicFacade.GetCourseNameByID(courseID);
        item = itemLogic.GetById(TrainingItemID);
        this.lblItemID.Text = item.ItemName;
        offlineJob = offlineJobLogic.GetById(JobID);
        this.lblJobID.Text = offlineJob.JobName;//.JobFileName;
        this.lblTime.Text = offlineJob.BeginTime.ToString("yyyy-MM-dd") + "-" + offlineJob.EndTime.ToString("yyyy-MM-dd");
        student = studentLogic.GetById(studentID);
        this.lblWorkerNo.Text = student.LoginName;
        userInfo = userLogic.GetUserByID(studentID);
        this.lblRealName.Text = userInfo.RealName;
        this.lblTel.Text = userInfo.MobilePhone;
        this.lblMail.Text = userInfo.Email;
        this.lblJobContent.Text = offlineJob.JobDescription;
        
        //学生提交的作业附件
        string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", entity.UploadFilePath);
        this.lblUpload.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ", fullUrl, entity.UploadFileName);

        this.lblUpLoadTime.Text = entity.UploadTime.ToString("yyyy-MM-dd");
        fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", entity.MarkFilePath);
        this.lblMarkFileName.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ", fullUrl, entity.MarkFileName);
        this.lblEvaluation.Text = entity.Evaluation;

        this.lblDept.FieldIDValue = userInfo.DepartmentID.ToString();
        this.lblPost.FieldIDValue = student.PostID.ToString();
        this.lblOrg.FieldIDValue = userInfo.OrganizationID.ToString();
    }
    #endregion

    #region List_View
    public void BindFromData_List(object domainModel)
    {

    }
    #endregion

    #region Handler

    public object UnBindFromData(object domainModel)
    {
        if (this.ControlMode == ViewMode.Edit)
        {
            Sty_StudentOffLineJob entity = (Sty_StudentOffLineJob)domainModel;

            List<FileUploadInfo> uploaders = this.uploader.FileUrl;
            FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
            if (fileDefine != null)
            {
                entity.MarkFileName = fileDefine.FileName;//离线作业附件路径
                entity.MarkFilePath = fileDefine.BizUrl;
            }
            
            entity.Evaluation = this.lblEvaluationEdit.Text;
            
            entity.EvaluationTime = DateTime.Now;
            entity.EvaluationUser = ETMS.AppContext.UserContext.Current.RealName;
            entity.StudentJobStatus = 1;
           
        }
        return domainModel;
    }

    public void BindFromData(object domainModel, ViewMode controlMode)
    {
        this.Views.BindFromData(domainModel, controlMode);
    }

    #endregion
    #endregion

}