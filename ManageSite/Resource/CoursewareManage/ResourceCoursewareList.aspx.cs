using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

namespace ETMS.WebApp.Manage
{
    public partial class ResourceCoursewareList : ETMS.Controls.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            resourceWareList.CourseID = getSafeRequest(this, "CourseID").ToGuid();
        }
    }
}