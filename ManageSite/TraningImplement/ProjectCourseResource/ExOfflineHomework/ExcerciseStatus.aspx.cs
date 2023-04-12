using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class QuestionDB_ExOfflineHomework_ExcerciseStatus : ETMS.Controls.BasePage
{
    private static PublicFacade publicfacade = new PublicFacade();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    private static Sty_StudentCourseLogic studentCourseLogic = new Sty_StudentCourseLogic();
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
    private Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    private Guid ItemCourseOffLineJobID
    {
        get { return Request.QueryString["ItemCourseOffLineJobID"].ToGuid(); }
    }

    private Guid JobID
    {
        get { return Request.QueryString["JobID"].ToGuid(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControlers();
            aBack.HRef = this.ActionHref(string.Format("ExerciseList.aspx?CourseID={0}&TrainingItemCourseID={1}&TrainingItemID={2}", CourseID, TrainingItemCourseID, TrainingItemID));
        }
        Evaluated.ReLoad();
    }

    private void InitialControlers()
    {
        this.lblCourseID.Text = publicfacade.GetCourseNameByID(CourseID);
        Tr_Item item= itemLogic.GetById(TrainingItemID);
        this.lblItemID.Text = item.ItemName;
        Res_e_OffLineJob offlineJob = Logic.GetById(JobID);
        this.lblJobID.Text = offlineJob.JobName;
        this.lblTime.Text = offlineJob.BeginTime.ToString("yyyy-MM-dd") + "至" + offlineJob.EndTime.ToString("yyyy-MM-dd");
        string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", offlineJob.JobFileURL);
        this.lblJobFileName.Text = string.Format("<a href='{0}' target='_blank' class='link_colorRed'>{1}</a>", fullUrl, offlineJob.JobFileName);
        this.lblJobDescription.Text = offlineJob.JobDescription;
        this.lblStudentNum.Text = studentCourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();

        //自定义控件
        this.ExerciseUnEvaluation.CourseID = CourseID;
        this.ExerciseUnEvaluation.JobID = JobID;
        this.ExerciseUnEvaluation.TrainingItemCourseID = TrainingItemCourseID;
        this.ExerciseUnEvaluation.TrainingItemID = TrainingItemID;
        this.ExerciseUnEvaluation.ItemCourseOffLineJobID = ItemCourseOffLineJobID;

        this.Evaluated.CourseID = CourseID;
        this.Evaluated.JobID = JobID;
        this.Evaluated.TrainingItemCourseID = TrainingItemCourseID;
        this.Evaluated.TrainingItemID = TrainingItemID;
        this.Evaluated.ItemCourseOffLineJobID = ItemCourseOffLineJobID;

        this.ExcerciseNoSubmit.JobID = JobID;
        this.ExcerciseNoSubmit.ItemCourseOffLineJobID = ItemCourseOffLineJobID;

    }

}