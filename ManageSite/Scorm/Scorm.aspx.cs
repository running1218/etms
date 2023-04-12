using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Controls;

public partial class Scorm_Scorm : System.Web.UI.Page
{
    #region 页面参数
    public Guid CourseID
    {
        get
        {
            return (Guid)(ViewState["CourseID"]);
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    public Guid CourseWareID
    {
        get
        {
            return (Guid)(ViewState["CourseWareID"]);
        }
        set
        {
            ViewState["CourseWareID"] = value;
        }
    }
    public Guid ItemCourseResID
    {
        get
        {
            return Guid.Empty;
        }
        set
        {
            ViewState["ItemCourseResID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request.QueryString["CourseWareID"] != null)
            {
                CourseWareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseWareID")));
            }
            if (Request.QueryString["CourseID"] != null)
            {
                CourseID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseID")));
            }
        }
    }
}
