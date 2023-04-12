using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class LearningManagement_GroupManager_GroupAdd : ETMS.Controls.BasePage
{
    // <summary>
    /// 班级ID
    /// </summary>
    public Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GroupInfo1.ClassID = ClassID;
    }
}