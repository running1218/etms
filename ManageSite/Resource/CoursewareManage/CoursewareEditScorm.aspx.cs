using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
public partial class Resource_CoursewareManage_CoursewareEditScorm : ETMS.Controls.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CoursewareInfoScorm1.CoursewareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CoursewareID"))); ;
    }
}