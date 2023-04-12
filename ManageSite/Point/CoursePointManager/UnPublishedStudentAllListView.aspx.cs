using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;

public partial class Point_CoursePointManager_UnPublishedStudentAllListView : System.Web.UI.Page
{
    #region 页面参数
    protected Guid TrainingItemCourseID
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
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
            {
                TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            }
        }
        lbtn_Return.PostBackUrl = this.ActionHref(string.Format("ProjectCourseList.aspx"));
    }
}