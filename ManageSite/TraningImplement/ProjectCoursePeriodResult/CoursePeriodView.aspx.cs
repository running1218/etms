using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

public partial class TraningImplement_ProjectCoursePeriodResult_CoursePeriodView : System.Web.UI.Page
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
            }

            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID")))
            {
                ItemCourseHoursID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseHoursID"));
            }
            CoursePeriodView1.TrainingItemCourseID = TrainingItemCourseID;
            CoursePeriodView1.ItemCourseHoursID = ItemCourseHoursID;
            SetsStudentListView1.TrainingItemCourseID = TrainingItemCourseID;
            SetsStudentListView1.ItemCourseHoursID = ItemCourseHoursID;
        }
    }
}