using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Data;


public partial class TraningImplement_SignInManager_ManagerSignInEdit : ETMS.Controls.BasePage
{
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();
    private static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
    /// <summary>
    /// 培训项目编码
    /// </summary>
    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    /// <summary>
    /// 项目课时ID 
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get { return Request.QueryString["ItemCourseHoursID"].ToGuid(); }
    }
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    /// <summary>
    /// 老师名称
    /// </summary>
    public int TeacherID
    {
        get { return Request.QueryString["TeacherID"].ToInt(); }
    }
    /// <summary>
    /// 签到人数
    /// </summary>
    public int SignNumber
    {
        get
        {
            if (Request.QueryString["SignNumber"] == null)
                return 0;
            return Request.QueryString["SignNumber"].ToInt();
        }
    }
    /// <summary>
    /// 培训项目课程课时学员编码批量
    /// </summary>
    public string ItemCourseHoursStudentIDs
    {
        get { return Request.QueryString["ItemCourseHoursStudentIDs"].ToString(); }
    }
    /// <summary>
    /// 培训项目课程课时学员编码批量
    /// </summary>
    public Guid ItemCourseHoursStudentID
    {
        get { return Request.QueryString["ItemCourseHoursStudentID"].ToGuid(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
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
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseCode.Text = Course.CourseCode;
                lblCourseName.Text = Course.CourseName;
            }
            #endregion
            Tr_ItemCourseHours hours = itemCourseHoursLogic.GetById(ItemCourseHoursID);
            this.lblTrainingDate.Text = hours.TrainingDate.ToDate();
            this.lblTrainingTime.Text = hours.TrainingBeginTime.ToDateTime().ToString("HH:mm") + "-" + hours.TrainingEndTime.ToDateTime().ToString("HH:mm");
            UserLogic userlogic = new UserLogic();
            this.lblTeacherName.Text = userlogic.GetUserByID(TeacherID).RealName;
            this.lblSelectStudentNum.Text = SignNumber.ToString();
            //签到信息
            if (ItemCourseHoursStudentID != null && ItemCourseHoursStudentID != Guid.Empty || SignNumber == 1)
            {
                Tr_ItemCourseHoursStudent entity = new Tr_ItemCourseHoursStudent();
                if (SignNumber == 1)
                {
                    string[] ItemCourseHoursStudentIDGroup = ItemCourseHoursStudentIDs.Split(',');
                    entity = itemCourseHoursStudentLogic.GetById(ItemCourseHoursStudentIDGroup[0].ToGuid());
                }
                else
                {
                    entity = itemCourseHoursStudentLogic.GetById(ItemCourseHoursStudentID);
                }
                this.txtLeaveMinutes.Text = entity.LeaveMinutes.ToString();
                this.ddlLawlessness.SelectedValue = entity.LawlessnessID.ToString();
                this.ddlSigninTypeID.SelectedValue = entity.SigninTypeID.ToString();
                if (SignNumber == 0)
                {
                    int total = 0;
                    string whereStr = string.Format(" and ItemCourseHoursStudentID='{0}'", ItemCourseHoursStudentID);
                    DataTable dt = itemCourseHoursStudentLogic.GetItemCourseHoursStudentByItemCourseHoursID(ItemCourseHoursID, 1, 1, string.Empty, whereStr, out total);
                    this.lblSelectStudentName.Text = dt.Rows[0]["RealName"].ToString();
                }

                #region 请假审核通过，签到信息为课前请假 不可编辑 签到信息
                if (entity.AuditStatus == 20 && entity.SigninTypeID == 2)
                {
                    ddlSigninTypeID.Visible = false;
                    labSigninTypeName.Visible = true;
                    signMsg.Visible = false;
                    labSigninTypeName.Text = ddlSigninTypeID.SelectedItem.Text;
                    RequiredFieldValidatorJobName.ValidationGroup = "";
                }
                #endregion
            }
        }
    }
    protected void lbnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_ItemCourseHoursStudent entity = new Tr_ItemCourseHoursStudent();
            if (SignNumber > 0)//批量签到
            {
                string[] ItemCourseHoursStudentIDGroup = ItemCourseHoursStudentIDs.Split(',');
                for (int i = 0; i < ItemCourseHoursStudentIDGroup.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ItemCourseHoursStudentIDGroup[i]))
                    {
                        entity = itemCourseHoursStudentLogic.GetById(ItemCourseHoursStudentIDGroup[i].ToGuid());
                        entity.SigninTypeID = this.ddlSigninTypeID.SelectedValue.ToInt();
                        entity.LeaveMinutes = string.IsNullOrEmpty(this.txtLeaveMinutes.Text.Trim()) ? 0 : this.txtLeaveMinutes.Text.ToInt();
                        entity.LawlessnessID = this.ddlLawlessness.SelectedValue.ToInt();
                        entity.SigninTime = DateTime.Now;
                        entity.ModifyTime = DateTime.Now;
                        entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                        itemCourseHoursStudentLogic.Update(entity);
                    }
                }
            }
            else if (SignNumber == 0)//修改
            {
                entity = itemCourseHoursStudentLogic.GetById(ItemCourseHoursStudentID);
                entity.SigninTypeID = this.ddlSigninTypeID.SelectedValue.ToInt();
                entity.LeaveMinutes = string.IsNullOrEmpty(this.txtLeaveMinutes.Text.Trim()) ? 0 : this.txtLeaveMinutes.Text.ToInt();
                entity.LawlessnessID = this.ddlLawlessness.SelectedValue.ToInt();
                entity.SigninTime = DateTime.Now;
                entity.ModifyTime = DateTime.Now;
                entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                itemCourseHoursStudentLogic.Update(entity);
            }
            //string pathStr = this.ActionHref(string.Format("ManagerSignIn.aspx?ItemCourseHoursID={0}&TrainingItemID={1}&TrainingItemCourseID={2}&TeacherID={3}",ItemCourseHoursID,TrainingItemID, TrainingItemCourseID,TeacherID));
            //ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "保存成功！", "function(){window.location = window.location");

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
            // ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindow("保存成功");
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