using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Point.Implement.BLL;
using System.Data;

public partial class Point_CoursePointManager_Controls_CourseRoleListView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binding();
        }
    }
    private void binding()
    {
        int total = 0;
        Point_Student_CourseRoleLogic pointStudentCourseRoleLogic = new Point_Student_CourseRoleLogic();
        string criteria = string.Format(" AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = pointStudentCourseRoleLogic.GetPagedList(1, int.MaxValue - 1, "CourseAttrID,MinNum,MaxNum", criteria, out total);
        this.CustomGridView1.DataSource = dt.DefaultView;
        this.CustomGridView1.DataBind();
    }
}