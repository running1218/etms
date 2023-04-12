using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resource_ElearningMap_ChooseCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ChooseCourse1.ObjectRefType = ETMS.Components.Basic.Implement.BLL.ObjectCourseRelation.StudyMapReferCourse;
        ChooseCourse1.ObjectRefID = Request.QueryString["StudyMapID"].ToString();
    }
    

}