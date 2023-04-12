using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
public partial class Fee_CourseFeeSetting_FeeSettingEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FeeSetting1.Action = ETMS.AppContext.OperationAction.Edit;
            FeeSetting1.CourseFeeSettingID = Request.QueryString["CourseFeeSettingID"].ToGuid();
        }
    }
}