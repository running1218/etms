using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.ExOnlineTest.Implement.BLL.ExOnlineTest;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
public partial class Grade_GradeManage_GradeView : System.Web.UI.Page
{
    private static Site_Student student = new Site_Student();
    private static Site_StudentLogic studentLogic = new Site_StudentLogic();
    private static PublicFacade publicFacade = new PublicFacade();
    private static readonly Sty_StudentSignupLogic logic = new Sty_StudentSignupLogic();
    
    /// <summary>
    /// 用户Id
    /// </summary>
    public int StudentID
    {
        get{ return Request.ToparamValue<int>("UserID"); }
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
    private Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    private Guid StudentCourse
    {
        get { return Request.QueryString["StudentCourse"].ToGuid(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 5;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
       
    }
    private void Initial()
    {
        student=studentLogic.GetById(StudentID);
        this.lblPost.FieldIDValue =student.PostID.ToString();
        this.lblDept.FieldIDValue = student.DepartmentID.ToString();
        this.lblStudentName.Text = student.RealName;
        this.lblCourseName.Text = publicFacade.GetCourseNameByID(CourseID);

        var scoreStandard = new ETMS.Components.Basic.Implement.BLL.TrainingItem.Tr_AppraiseLogic().Get(TrainingItemID);

        if (null != scoreStandard)
        {
            lblCourseRate.Text = scoreStandard.CourseRate.ToString();
            lblJobRate.Text = scoreStandard.ActualRate.ToString();
            lblTestingRate.Text = scoreStandard.StudyingRate.ToString();
        }

        var standard = new ETMS.Components.Basic.Implement.BLL.TrainingItem.Tr_AppraiseLogic().GetStandardCalulate(StudentCourse);
        if (null != standard)
        {
            lblCourse.Text = standard.CourseScore.ToString();
            lblJob.Text = standard.ActualScore.ToString();
            lblTesting.Text = standard.TestingScore.ToString();
        }

        if (!string.IsNullOrEmpty(lblCourse.Text) || !string.IsNullOrEmpty(lblJob.Text) || !string.IsNullOrEmpty(lblTesting.Text))
        {
            lblScore.Text = (standard.CourseScore.ToInt() + standard.ActualScore.ToInt() + standard.TestingScore.ToInt()).ToString();// (lblCourse.Text.ToInt() + lblTesting.Text.ToInt() + lblJob.Text.ToInt()).ToString();
        }
    }
   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = logic.GetStudentCourseOnLineTestLisByStudentID(StudentID, StudentCourse);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }

}