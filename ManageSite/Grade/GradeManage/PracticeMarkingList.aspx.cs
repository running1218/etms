using System;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.ExOfflineHomework.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Controls;

public partial class Grade_GradeManage_PracticeMarkingList : System.Web.UI.Page
{
    //private static PublicFacade publicfacade = new PublicFacade();
    //private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    //private static Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    //private static Sty_StudentCourseLogic studentCourseLogic = new Sty_StudentCourseLogic();
    ///// <summary>
    ///// 培训项目编号
    ///// </summary>
    //private Guid TrainingItemID
    //{
    //    get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    //}

    //private Guid ItemCourseOffLineJobID
    //{
    //    get { return Request.QueryString["ItemCourseOffLineJobID"].ToGuid(); }
    //}

    //private Guid JobID
    //{
    //    get { return Request.QueryString["JobID"].ToGuid(); }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControlers();
            //aBack.HRef = this.ActionHref(string.Format("TrainingProjectPracticeList.aspx"));
        }
        MarkingEvaluated.ReLoad();
    }

    private void InitialControlers()
    {
         BindControls();
        //this.lblCourseID.Text = publicfacade.GetCourseNameByID(CourseID);
        //Tr_Item item = itemLogic.GetById(TrainingItemID);
        //this.lblItemID.Text = item.ItemName;
        //Res_e_OffLineJob offlineJob = Logic.GetById(JobID);
        //this.lblJobID.Text = offlineJob.JobName;
        //this.lblTime.Text = string.Format("{0} 至 {1}", offlineJob.BeginTime.ToDate(), offlineJob.EndTime.ToDate());

        //this.lblJobDescription.Text = offlineJob.JobDescription;
        //this.lblStudentNum.Text = new Sty_StudentSignupLogic().GetTrainingItemStudentTotal(TrainingItemID).ToString();

        //自定义控件             
        //this.MarkingNoSubmit.JobID = JobID;
        //this.MarkingNoSubmit.ItemCourseOffLineJobID = ItemCourseOffLineJobID;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindControls();
        MarkingEvaluated.ReLoad();
        MarkingUnEvaluation.ReLoad();
        MarkingNoSubmit.ReLoad();
    }

    protected void BindControls()
    {
        var jobName = this.txt_JobName.Text.Trim();
        var itemName = this.txt_ItemName.Text.Trim();
        this.MarkingUnEvaluation.JobName = jobName;
        this.MarkingUnEvaluation.ItemName = itemName;
        this.MarkingEvaluated.JobName = jobName;
        this.MarkingEvaluated.ItemName = itemName;

    }
}