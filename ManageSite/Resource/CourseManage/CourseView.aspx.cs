using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;

namespace ETMS.WebApp.Manage
{
    public partial class CourseView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (default(string) != Request.ToparamValue<string>("Flag"))
                {
                    lbnBack.PostBackUrl = "~/Resource/CourseManage/CourseQuery.aspx";
                }
            }
        }
    }
}