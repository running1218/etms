using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;

public partial class TraningOrgManager_TraningOrgManager_CourseEdit : System.Web.UI.Page
{
    /// <summary>
    ///  外部培训机构ID
    /// </summary>
    public Guid OuterOrgID
    {
        get { return Request.QueryString["OuterOrgID"].ToGuid(); }

    }
    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid OuterOrgCourseID
    {
        get { return Request.QueryString["OuterOrgCourseID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CourseInfo.Action = OperationAction.Edit;
        this.CourseInfo.OuterOrgID = OuterOrgID;
        this.CourseInfo.OuterOrgCourseID = OuterOrgCourseID;
    }
}