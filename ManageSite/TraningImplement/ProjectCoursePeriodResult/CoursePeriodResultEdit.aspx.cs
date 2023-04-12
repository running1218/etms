using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class TraningImplement_ProjectCoursePeriodResult_CoursePeriodResultEdit : System.Web.UI.Page
{
    #region
    /// <summary>
    /// 项目课程课时ID
    /// </summary>
    public Guid ItemCourseHoursID
    {
        get
        {
            return ViewState["ItemCourseHoursID"].ToGuid();
        }
        set
        {
            ViewState["ItemCourseHoursID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID")))
            {
                ItemCourseHoursID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID"));
            }
            bind();
        }
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind() {
        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        Tr_ItemCourseHours courseHours = hoursLogic.GetById(ItemCourseHoursID);
        if (courseHours != null)
        {
            lblTraningDate.Text = string.Format("{0}（{1}-{2}）"
                , courseHours.TrainingDate.ToDate()
                , courseHours.TrainingBeginTime.ToString("HH:mm")
                , courseHours.TrainingEndTime.ToString("HH:mm"));

            //讲师名称
            lbl_TeacherName.Text = new PublicFacade().GetTeacherInfo(courseHours.TeacherID).UserInfo.RealName;
            dlblTrainingTimeDesc.FieldIDValue = courseHours.TrainingTimeDescID.ToString();
            ddl_CourseHoursStatus.SelectedValue = courseHours.CourseHoursStatusID.ToString();
            txtCourseHoursStatusDesc.Text = courseHours.CourseHoursStatusDesc;

            Tr_ItemCourse itemCourse = new Tr_ItemCourseLogic().GetById(courseHours.TrainingItemCourseID);
            if (itemCourse != null)
            {
                Lbl_CourseName.Text = new Res_CourseLogic().GetById(itemCourse.CourseID).CourseName;
                Lbl_ItemName.Text = new Tr_ItemLogic().GetById(itemCourse.TrainingItemID).ItemName;
            }
        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {

        try
        {
            Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
            hoursLogic.ItemCourseHours_HoursAudit(ItemCourseHoursID
                , ddl_CourseHoursStatus.SelectedValue.ToInt()
                , ETMS.AppContext.UserContext.Current.RealName
                , txtCourseHoursStatusDesc.Text);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}