using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;

public partial class Grade_GradeManage_GradeEntry : ETMS.Controls.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        aBack.HRef = this.ActionHref(string.Format("GradeEntryList.aspx"));

    }
}