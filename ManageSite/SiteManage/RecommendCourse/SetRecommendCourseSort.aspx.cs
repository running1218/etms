using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteManage_RecommendCourse_SetRecommendCourseSort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bind();
        }
    }

    private void bind()
    {
        int totalRecords = 0;
        Rec_CourseLogic courseLogic = new Rec_CourseLogic();
        string Crieria = string.Format(" AND rc.OrgID={0}", UserContext.Current.OrganizationID);
        DataTable dt = courseLogic.GetPagedList(1, int.MaxValue - 1, "Sort ASC", Crieria, out totalRecords);
        foreach (DataRow row in dt.Rows)
        {
            lbCourseSort.Items.Add(new ListItem() { Value = row["RecommendID"].ToString(), Text = string.Format("[{0}] {1}", row["CourseCode"], row["CourseName"]) });
        }
    }
}