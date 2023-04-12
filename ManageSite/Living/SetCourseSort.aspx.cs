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
using ETMS.AppContext;

public partial class Living_CourseManage_SetCourseSort : BasePage
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
        string Crieria = string.Format("{0} AND OrgID={1} And CourseModel = 2 ", "", UserContext.Current.OrganizationID);
        DataTable dt = new Res_CourseLogic().GetPagedList(1, int.MaxValue -1, " Sorting Desc ", Crieria, out totalRecords);

        foreach (DataRow row in dt.Rows)
        {
            lbCourseSort.Items.Add(new ListItem() { Value = row["CourseID"].ToString(), Text = string.Format("{0}", row["CourseName"]) });
        }
    }
}