using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class TraningImplement_SignInManager_ImportSignInInfo : ETMS.Controls.BasePage
{
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

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

        }
    }   
}