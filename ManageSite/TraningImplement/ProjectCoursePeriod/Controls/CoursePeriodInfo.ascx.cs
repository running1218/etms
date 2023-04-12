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

public partial class TraningImplement_ProjectCoursePeriod_Controls_CoursePeriodInfo : System.Web.UI.UserControl
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
            if (ViewState["TrainingItemCourseID"] == null)
                ViewState["TrainingItemCourseID"] = Guid.Empty;
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    /// <summary>
    /// 项目课程课时ID
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get
        {
            if (ViewState["ItemCourseHoursID"] == null)
                ViewState["ItemCourseHoursID"] = Guid.Empty;
            return ViewState["ItemCourseHoursID"].ToGuid();
        }
        set
        {
            ViewState["ItemCourseHoursID"] = value;
        }
    }

    /// <summary>
    /// 临时存储课时对象
    /// </summary>
    public Tr_ItemCourseHours CourseHours {
        get {
            if (ViewState["CourseHours"] == null)
                ViewState["CourseHours"] = new Tr_ItemCourseHours();
            return (Tr_ItemCourseHours)ViewState["CourseHours"];
        }
        set {
            ViewState["CourseHours"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
            if (Action == OperationAction.Add)
            {
                dtxtTrainingDate.Text = DateTime.Now.ToDate();
                dtxtTrainingBeginTime.Text = "09:00";
                dtxtTrainingEndTime.Text = "11:00";
            }
            else if (Action == OperationAction.Edit)
            {
                bindHours();
            }
        }
    }

    /// <summary>
    /// 邦定
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            #region 项目代码与名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
            {
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.ItemName;
                lblOrganization.FieldIDValue=item.OrgID.ToString();
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseCode.Text = Course.CourseCode;
                lblCourseName.Text = Course.CourseName;
                dlblCourseType.FieldIDValue = Course.CourseTypeID.ToString();
                dlblCourseLevel.FieldIDValue = Course.CourseLevelID.ToString();
            }
            #endregion

            dlblTeachModel.FieldIDValue = ItemCourse.TeachModelID.ToString();
            dlblTrainingModel.FieldIDValue = ItemCourse.TrainingModelID.ToString();
            dlblCourseAttr.FieldIDValue = ItemCourse.CourseAttrID.ToString();
        }

        //讲师
        Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
        int totalRecordCount = 0;
        ddlTeacher.DataSource = ItemCourseTeacherLogic.GetTeacherListByItemCourseID(TrainingItemCourseID, out totalRecordCount);
        ddlTeacher.DataTextField = "RealName";
        ddlTeacher.DataValueField = "TeacherID";
        ddlTeacher.DataBind();
        ddlTeacher.Items.Insert(0, new ListItem("请选择", ""));
        //教室
        Res_ClassRoomLogic classRoomLogic = new Res_ClassRoomLogic();
        string criteria = string.Format(" AND ClassRoomStatus=1 AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable tabClassRoom = classRoomLogic.GetPagedList(1, int.MaxValue - 1, string.Empty,criteria, out totalRecordCount);
        foreach (DataRow row in tabClassRoom.Rows)
        {
            string address = string.IsNullOrEmpty(row["Address"].ToString()) ? "" : "（" + row["Address"].ToString() + "）";
            ddlClassRoomAddress.Items.Add(new ListItem(row["ClassRoomName"].ToString() + address, row["ClassRoomID"].ToString()));
        }
        ddlClassRoomAddress.Items.Insert(0, new ListItem("请选择", ""));
    }

    /// <summary>
    /// 邦定课时信息
    /// </summary>
    private void bindHours()
    {
        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        Tr_ItemCourseHours courseHours = hoursLogic.GetById(ItemCourseHoursID);
        if (courseHours != null) {
            CourseHours = courseHours;
            dtxtTrainingDate.Text = courseHours.TrainingDate.ToDate();
            dtxtTrainingBeginTime.Text = courseHours.TrainingBeginTime.ToString("HH:mm");
            dtxtTrainingEndTime.Text = courseHours.TrainingEndTime.ToString("HH:mm");
            ddlTeacher.SelectedValue = courseHours.TeacherID.ToString();
            dddlTrainingTimeDesc.SelectedValue = courseHours.TrainingTimeDescID.ToString();
            txtCourseHours.Text = decimal.Round(courseHours.CourseHours,1).ToString();//保留一位小数
            txtCourseHours.Attributes.Add("t_value", txtCourseHours.Text);
            ddlClassRoomAddress.SelectedValue = courseHours.ClassRoomID.ToString();
            txtCourseHoursDesc.Text = courseHours.CourseHoursDesc;
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        //验证培训时段是否合法
        if (CheckMsg()) {
            ETMS.Utility.JsUtility.AlertMessageBox("培训时段开始时间必须小于结束时间！");
            return;
        }

        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        Tr_ItemCourseHours courseHours = new Tr_ItemCourseHours();

        if (Action == OperationAction.Add)
        {
            courseHours.ItemCourseHoursID = Guid.NewGuid();
            courseHours.TrainingItemCourseID = TrainingItemCourseID;
            SetCourseHourse(courseHours);
            courseHours.CourseHoursStatusID = 0;
            courseHours.CreateTime = DateTime.Now;
            courseHours.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            courseHours.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            courseHours.ModifyTime = DateTime.Now;
            courseHours.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            courseHours.DelFlag = false;
        }
        else if (Action == OperationAction.Edit)
        {
            courseHours.ItemCourseHoursID = ItemCourseHoursID;
            courseHours.TrainingItemCourseID = TrainingItemCourseID;
            courseHours = CourseHours;
            SetCourseHourse(courseHours);
            courseHours.ModifyTime = DateTime.Now;
            courseHours.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
        }

        try
        {
            hoursLogic.Save(courseHours, Action);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("课时信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    /// <summary>
    /// 验证培训时段是否合法
    /// </summary>
    private bool CheckMsg() {
        bool res = false;
        if (dtxtTrainingBeginTime.Text.ToDateTime() >= dtxtTrainingEndTime.Text.ToDateTime())
            res = true;
        return res;
    }

    /// <summary>
    /// 设置课时对象
    /// </summary>
    private void SetCourseHourse(Tr_ItemCourseHours courseHours)
    {
        courseHours.ClassRoomID = ddlClassRoomAddress.SelectedValue.ToGuid();
        courseHours.TrainingTimeDescID = dddlTrainingTimeDesc.SelectedValue.ToInt();
        courseHours.TeacherID = ddlTeacher.SelectedValue.ToInt();
        courseHours.TrainingDate = dtxtTrainingDate.Text.ToDateTime();
        courseHours.TrainingBeginTime = GetDateTime(dtxtTrainingDate.Text.ToDateTime(), dtxtTrainingBeginTime.Text.ToDateTime());
        courseHours.TrainingEndTime = GetDateTime(dtxtTrainingDate.Text.ToDateTime(), dtxtTrainingEndTime.Text.ToDateTime());
        courseHours.CourseHours = txtCourseHours.Text.ToDecimal();
        courseHours.CourseHoursDesc = txtCourseHoursDesc.Text;
    }

    private DateTime GetDateTime(DateTime TrainingDate, DateTime TrainingTime)
    {
        string str = string.Format("{0} {1}", TrainingDate.ToString("yyyy-MM-dd"), TrainingTime.ToString("HH:mm"));
        return str.ToDateTime();
    }
}