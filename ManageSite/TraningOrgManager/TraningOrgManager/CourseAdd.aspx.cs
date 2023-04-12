using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Controls;

public partial class TraningOrgManager_TraningOrgManager_CourseAdd : System.Web.UI.Page
{
    /// <summary>
    ///  外部培训机构ID
    /// </summary>
    public Guid OuterOrgID
    {
        get { return Request.QueryString["OuterOrgID"].ToGuid(); }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CourseInfo.Action = OperationAction.Add;
        this.CourseInfo.OuterOrgID = OuterOrgID;
    }
}