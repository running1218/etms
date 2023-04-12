using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class TraningImplement_ProjectCoursePeriod_SetsStudentClassAdd : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
                bind();
            }

            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID")))
            {
                ItemCourseHoursID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID"));
            }
        }
    }
    
    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            #region 项目名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
                lblItemName.Text = item.ItemName;
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
                lblCourseName.Text = Course.CourseName;
            #endregion
        }
        //项目课程学员
        lblCourseStudent.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(TrainingItemCourseID).ToString();
    }
        
    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e) { 
    
    }
}