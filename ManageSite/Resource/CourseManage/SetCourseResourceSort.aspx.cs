using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.Course;

public partial class Resource_CourseManage_SetCourseResourceSort : BasePage
{

    private Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
            {
                ViewState["CourseID"] = Guid.Empty;
            }
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "CourseID")))
        {
            CourseID = new Guid(BasePage.getSafeRequest(this.Page, "CourseID"));
            bind();
        }
    }

    private void bind()
    {
        int totalRecords = 0;
        Res_ContentLogic contentLogic = new Res_ContentLogic();
        DataTable dt = contentLogic.GetPagedList(1, int.MaxValue - 1, " RCCR.[Sort] ASC ", "", CourseID, out totalRecords);
        foreach (DataRow row in dt.Rows)
        {
            lbCourseSort.Items.Add(new ListItem() { Value = row["ContentID"].ToString(), Text = string.Format("{0}", row["Name"]) });
        }
    }
}