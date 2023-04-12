using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.AppContext;
using ETMS.Utility;

namespace ETMS.WebApp.Manage.Living
{
    public partial class CourseEdit : ETMS.Controls.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            courseInfo.CourseID = BasePage.getSafeRequest(this, "CourseID").ToGuid();           
        }
    }
}