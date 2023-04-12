using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.AppContext;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.BLL;

public partial class TraningImplement_ProjectCoursePeriod_Controls_OnlinePlayingInfo : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 操作动作
    /// </summary>
    public OperationAction Action
    {
        get;
        set;
    }

    /// <summary>
    /// 项目课程ID 
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return Request.ToparamValue<Guid>("TrainingItemCourseID");            
        }
    }

    /// <summary>
    /// 直播ID 
    /// </summary>
    public string OnlinePlayingID
    {
        get
        {
            return Request.ToparamValue<string>("OnlinePlayingID");
        }
    }

    /// <summary>
    /// 临时存储课时对象
    /// </summary>
    public Tr_CourseOnlinePlaying Source {
        get {
            if (ViewState["Source"] == null)
                ViewState["Source"] = new Tr_CourseOnlinePlaying();
            return (Tr_CourseOnlinePlaying)ViewState["Source"];
        }
        set {
            ViewState["Source"] = value;
        }
    }
    #endregion

    private static readonly OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            InitTeachers();

            if (Action == OperationAction.Add)
            {
                dtxtTrainingDate.Text = DateTime.Now.AddDays(7).ToDate();
                dtxtTrainingBeginTime.Text = "09:00";
                dtxtTrainingEndTime.Text = "12:00";
            }
            else if (Action == OperationAction.Edit)
            {
                InitOnlinePlayingInfo();
            }
        }
    }

    private void InitTeachers()
    {
        Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
        int totalRecordCount = 0;
        ddlTeacher.DataSource = ItemCourseTeacherLogic.GetTeacherListByItemCourseID(TrainingItemCourseID, out totalRecordCount);
        ddlTeacher.DataTextField = "RealName";
        ddlTeacher.DataValueField = "TeacherID";
        ddlTeacher.DataBind();
        ddlTeacher.Items.Insert(0, new ListItem("请选择", ""));
    }

    /// <summary>
    /// 邦定课时信息
    /// </summary>
    private void InitOnlinePlayingInfo()
    {
        OnlinePlayingLogic onlinePlayingLogic = new OnlinePlayingLogic();
        Source = onlinePlayingLogic.GetOnlinePlaying(OnlinePlayingID);
        if (Source != null)
        {
            txtSubject.Text = Source.PlayingSubject;
            dtxtTrainingDate.Text = Source.StartTime.ToDate();
            dtxtTrainingBeginTime.Text =Source.StartTime.ToString("HH:mm");
            dtxtTrainingEndTime.Text = Source.EndTime.ToString("HH:mm");
            ddlTeacher.SelectedValue = Source.TeacherID.ToString();
            txtOnlinePlayingDesc.Text = Source.Description;
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        //验证培训时段是否合法
        if (dtxtTrainingBeginTime.Text.ToDateTime() >= dtxtTrainingEndTime.Text.ToDateTime())
        {
            ETMS.Utility.JsUtility.AlertMessageBox("直播开始时间必须小于结束时间！");
            return;
        }
  
        if (Action == OperationAction.Add)
        {
            Source.PlayingSubject = txtSubject.Text.Trim();
            Source.StartTime = string.Format("{0} {1}:00.000", dtxtTrainingDate.Text, dtxtTrainingBeginTime.Text).ToDateTime();
            Source.EndTime = string.Format("{0} {1}:00.000", dtxtTrainingDate.Text, dtxtTrainingEndTime.Text).ToDateTime();
            Source.TeacherID = ddlTeacher.SelectedValue.ToInt();
            Source.TeacherName = ddlTeacher.SelectedItem.Text;
            Source.Description = txtOnlinePlayingDesc.Text;
            Source.WindowSize = 20;

            Source.TrainingItemCourseID = TrainingItemCourseID;
            Source.OnlineStatus = true;
            Source.CreateTime = DateTime.Now;
            Source.CreateUser = UserContext.Current.UserName;
            Source.CreateUserID = UserContext.Current.UserID;
        }
        else if (Action == OperationAction.Edit)
        {
            Source.PlayingSubject = txtSubject.Text.Trim();
            Source.StartTime = string.Format("{0} {1}:00.000", dtxtTrainingDate.Text, dtxtTrainingBeginTime.Text).ToDateTime();
            Source.EndTime = string.Format("{0} {1}:00.000", dtxtTrainingDate.Text, dtxtTrainingEndTime.Text).ToDateTime();
            Source.TeacherID = ddlTeacher.SelectedValue.ToInt();
            Source.TeacherName = ddlTeacher.SelectedItem.Text;
            Source.Description = txtOnlinePlayingDesc.Text;
            Source.WindowSize = 20;
        }

        try
        {
            onlinePlayingLogic.Save(Source, Action);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("直播信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}