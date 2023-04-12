using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Courseware.Implement.BLL;
using System.Data;

public partial class TraningImplement_ProjectCourseResource_CourseWareSort : System.Web.UI.Page
{
    private Guid TrainingItemCourseID
    {
        get
        {
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            bind();
        }
    }

    private void bind()
    {
        int totalRecords = 0;
        Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();

        DataTable dt = CoursewareLogic.GetTrainingItemSelectResourcesList(TrainingItemCourseID, out totalRecords);
        foreach (DataRow row in dt.Rows)
        {
            lbCourseSort.Items.Add(new ListItem() { Value = row["ItemCourseResID"].ToString(), Text = row["CoursewareName"].ToString() });
        }
    }
}