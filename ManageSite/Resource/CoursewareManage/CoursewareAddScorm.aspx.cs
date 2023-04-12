using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class Resource_CoursewareManage_CoursewareAddScorm : ETMS.Controls.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CoursewareInfoScorm1.CourseID = Request.ToparamValue<Guid>("CourseID");
        }
    }
}