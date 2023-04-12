using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class LearningManagement_ClassManager_ClassAdd : System.Web.UI.Page
{
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ClassInfo1.TrainingItemID = TrainingItemID;

    }
}