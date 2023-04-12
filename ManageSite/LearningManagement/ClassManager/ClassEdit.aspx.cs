using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class LearningManagement_ClassManager_ClassEdit : ETMS.Controls.BasePage
{
    /// <summary>
    /// 班级ID
    /// </summary>
    private Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ClassInfo1.ClassID = ClassID;
        this.ClassInfo1.TrainingItemID = TrainingItemID;
    }    
}