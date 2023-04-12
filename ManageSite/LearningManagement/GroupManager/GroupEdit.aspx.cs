using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class LearningManagement_GroupManager_GroupEdit : ETMS.Controls.BasePage
{
    /// <summary>
    /// 班级ID
    /// </summary>
    private Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    /// <summary>
    /// 学习群组Id
    /// </summary>
    private Guid ClassSubgroupID
    {
        get { return Request.QueryString["ClassSubgroupID"].ToGuid(); }
    }   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GroupInfo1.ClassID = ClassID;
        this.GroupInfo1.ClassSubgroupID = ClassSubgroupID;
    }    
}