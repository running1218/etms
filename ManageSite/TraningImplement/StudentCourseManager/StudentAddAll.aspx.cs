using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_StudentCourseManager_StudentAddAll : System.Web.UI.Page
{
    #region 页面参数
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            bind();
        }
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Tr_ItemCourseLogic itemCourseLogic=new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = itemCourseLogic.GetById(TrainingItemCourseID);
        //项目信息
        Tr_Item item = new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID);
        lblItemCode.Text = item.ItemCode;
        lblItemName.Text = item.ItemName;
        //课程信息
        Res_Course course = new Res_CourseLogic().GetById(ItemCourse.CourseID);
        lblCourseCode.Text = course.CourseCode;
        lblCourseName.Text = course.CourseName;
        
        int trainingItemSignupNumber=0;
        int trainingItemCourseSignupNumber=0;
        int courseSignupNumber=0;
        int courseReSignupNumber=0;
        itemCourseLogic.Tr_ItemCourse_GetStudentSignupOrNotNumber(TrainingItemCourseID
            , out trainingItemSignupNumber
            , out trainingItemCourseSignupNumber
            , out courseSignupNumber
            , out courseReSignupNumber);
        //项目学员数
        lblProjectStudentTotal.Text = trainingItemSignupNumber.ToString();
        //已报名数
        lbl_SignUpStudentTotal.Text = trainingItemCourseSignupNumber.ToString();
        //已学学员数
        lblAlreadylearnTotal.Text = courseSignupNumber.ToString();
        //已学学员报名数
        lblAlreadylearnSignUpTotal.Text = courseReSignupNumber.ToString();
        //未报名学员数
        lblNotSignUpTotal.Text = (trainingItemSignupNumber - trainingItemCourseSignupNumber).ToString();
        //未学未报名学员数
        lblNotStudyingNotSignUpTotal.Text = (trainingItemSignupNumber - courseSignupNumber - (trainingItemCourseSignupNumber - courseReSignupNumber)).ToString();
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e) {
        try
        {
            new Sty_StudentCourseLogic().AddAllTrainingItemStudentSignupToStudentCourse(TrainingItemCourseID
                , rblAddStudentWay.SelectedValue.ToInt()
                , ETMS.AppContext.UserContext.Current.UserID
                , ETMS.AppContext.UserContext.Current.RealName);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("学员信息添加成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}