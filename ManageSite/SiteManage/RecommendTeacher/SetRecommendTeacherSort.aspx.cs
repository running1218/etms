using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteManage_RecommendTeacher_SetRecommendTeacherSort : System.Web.UI.Page
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
        Rec_TeacherLogic teacherLogic = new Rec_TeacherLogic();
        string Crieria = string.Format(" AND OrganizationID={0}", UserContext.Current.OrganizationID);
        DataTable dt = teacherLogic.GetPagedList(1, int.MaxValue - 1, "Sort ASC", Crieria, out totalRecords);
        foreach (DataRow row in dt.Rows)
        {
            lbTeacherSort.Items.Add(new ListItem() { Value = row["TeacherID"].ToString(), Text = string.Format("[{0}] {1}", row["TeacherCode"], row["RealName"]) });
        }
    }
}