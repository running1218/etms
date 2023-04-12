using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Controls;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility;
using ETMS.Components.ExOfflineHomework.Implement.BLL;

public partial class TraningImplement_ProjectCourseResource_ExOfflineHomework_EvaluationInfo : ETMS.Controls.BasePage
{
    private static Sty_StudentOffLineJobLogic Logic = new Sty_StudentOffLineJobLogic();
    private static Res_e_OffLineJobLogic offlineJobLogic = new Res_e_OffLineJobLogic();
 
    protected string Oper
    {
        get { return UrlParamEncode(Request.QueryString["op"].ToLower()); }
    }

    /// <summary>
    /// 培训项目编号
    /// </summary>
    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }

    /// <summary>
    /// 课程编号
    /// </summary>
    private Guid CourseID
    {
        get { return Request.QueryString["CourseID"].ToGuid(); }
    }

    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    private Guid StudentJobID
    {
        get { return Request.QueryString["StudentJobID"].ToGuid(); }
    }
    /// <summary>
    /// 离线作业编码
    /// </summary>
    private Guid JobID
    {
        get { return Request.QueryString["JobID"].ToGuid(); }
    }

    /// <summary>
    /// 学生编码
    /// </summary>
    private int StudentID
    {
        get { return Request.QueryString["UserID"].ToInt(); }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Evaluation.StudentJobID = StudentJobID;
        this.Evaluation.TrainingItemID = TrainingItemID;
        this.Evaluation.CourseID = CourseID;
        this.Evaluation.JobID = JobID;
        this.Evaluation.StudentID = StudentID;
        if (!IsPostBack)
        {
            InitialControlers();
            Initial();
        }
        
    }
    private void Initial()
    {
        //根据操作类型显示不同操作界面
        switch (Oper)
        {
            case "edit":
                this.Evaluation.BindFromData(Logic.GetById(StudentJobID), ViewMode.Edit);
                break;
            case "view":
                this.Evaluation.BindFromData(Logic.GetById(StudentJobID), ViewMode.Browse);
                break;

        }
    }

    private void InitialControlers()
    {
        this.Evaluation.StudentJobID = StudentJobID;
        this.Evaluation.TrainingItemID = TrainingItemID;
        this.Evaluation.CourseID = CourseID;
        this.Evaluation.JobID = JobID;
        this.Evaluation.StudentID = StudentID;
        
    }
    protected override void OnPreRender(EventArgs e)
    {
        //btn显示控制
        this.lbnSave.Visible = false;
        switch (Oper)
        {            
            case "edit":
                this.lbnSave.Visible = true;
                break;
            case "view":
                this.lbnSave.Visible = false;
                break;            
        }
        base.OnPreRender(e);
    }
    protected void btn_ClickHandle(object sender, EventArgs e)
    {
        try
        {
            switch (Oper)
            {

                case "edit":
                    Sty_StudentOffLineJob entity = (Sty_StudentOffLineJob)this.Evaluation.DomainModel;
                    //Logic.Save(entity);
                    offlineJobLogic.SetEvaluationOffLineJob(StudentJobID, entity.MarkFilePath, entity.MarkFileName, entity.EvaluationUser, entity.Evaluation);
                    break;
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("离线作业信息保存成功");

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
}