using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

public partial class TraningImplement_TraningProjectManager_TraningProjectView : System.Web.UI.Page
{   
    #region 页面参数
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
        }
        TraningProjectView1.TrainingItemID = TrainingItemID;
        TraningStudentListView1.TrainingItemID = TrainingItemID;
        TraningCourseListView3.TrainingItemID = TrainingItemID;
        TraningCourseListView3.IsTeacherTotal = true;

        aBack.HRef = this.ActionHref("TraningProjectList.aspx");
    }
}