using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;

public partial class TraningImplement_AskApplyAudit_AskApplyAuditView :ETMS.Controls.BasePage
{
    private static UserLogic userLogic = new UserLogic();
    private Site_StudentLogic studentLogic = new Site_StudentLogic();
    protected Site_Student student = new Site_Student();
    private static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
    /// <summary>
    /// 培训项目课程课时学员编码
    /// </summary>
    public Guid ItemCourseHoursStudentID
    {
        get { return Request.QueryString["ItemCourseHoursStudentID"].ToGuid(); }
    }
    /// <summary>
    /// 获取项目名称
    /// </summary>
    protected string ItemName
    {
        get { return Request.QueryString["ItemName"].ToString(); }
    }
    /// <summary>
    /// 获取课程名称
    /// </summary>
    protected string CourseName
    {
        get { return Request.QueryString["CourseName"].ToString(); }
    }
    /// <summary>
    /// 面授时间
    /// </summary>
    protected string TrainingDate
    {
        get { return Request.QueryString["TrainingDate"].ToString(); }
    }
    /// <summary>
    /// 培训项目课程课时学员表
    /// </summary>
    protected Tr_ItemCourseHoursStudent ItemCourseHoursStudent
    {
        set { ViewState["ItemCourseHoursStudent"] = value; }
        get
        {
            if (ViewState["ItemCourseHoursStudent"] == null)
            {
                ViewState["ItemCourseHoursStudent"] = new Tr_ItemCourseHoursStudent();
            }
            return (Tr_ItemCourseHoursStudent)ViewState["ItemCourseHoursStudent"];
        }
    }

    /// <summary>
    /// 获取学生编号
    /// </summary>
    protected int StudentID
    {
        get { return Request.QueryString["UserID"].ToInt(); }
    }

    /// <summary>
    /// 学生实例
    /// </summary>
    protected User UserInfo
    {
        set { ViewState["UserInfo"] = value; }
        get
        {
            if (ViewState["UserInfo"] == null)
            {
                ViewState["UserInfo"] = new User();
            }
            return (User)ViewState["UserInfo"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControler();
        }
    }

    private void InitialControler()
    {
        try
        {
            this.lblItemName.Text = ItemName;
            this.lblCourseName.Text = CourseName;
            //用户信息
            UserInfo = userLogic.GetUserByID(StudentID);
            this.lblRealName.Text = UserInfo.RealName;
            //学生信息
            student = studentLogic.GetStudentById(StudentID);
            this.lblWorkerNo.Text = student.WorkerNo;
            lblDepartment.FieldIDValue = UserInfo.DepartmentID.ToString();
            lblPost.FieldIDValue = student.PostID.ToString();
            lblRankID.FieldIDValue = student.RankID.ToString();
            lblWorkerNo.Text = student.WorkerNo;
            //面授时间
            this.lblTrainingDate.Text = TrainingDate;
            //请假时间与原因
            Tr_ItemCourseHoursStudent itemCourseHoursStudent = itemCourseHoursStudentLogic.GetById(ItemCourseHoursStudentID);
            this.lblLeaveTime.Text = itemCourseHoursStudent.LeaveTime.ToString("yyyy-MM-dd HH:mm");
            this.lblLeaveReason.Text = itemCourseHoursStudent.LeaveReason;
            this.lblAuditOpinion.Text = itemCourseHoursStudent.AuditOpinion;
            this.lblAuditResult.FieldIDValue = itemCourseHoursStudent.AuditStatus.ToString();
            
        }
        catch { }
    }

}