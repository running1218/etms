using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Utility;

public partial class CourseWare_OpenCourseware : System.Web.UI.Page
{
    protected string serverUrl;
    protected string strLogUrl;
    protected string getCourseNameByID(int CourseID)
    {   
        return "";
    }

    private static readonly Res_CoursewareLogic logic = new Res_CoursewareLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindActionUrl();
        } 
    }

    public void BindActionUrl()
    {

        if (Request.QueryString["CourseWareID"] != null && Request.QueryString["CourseID"] != null)
        {
            Response.Redirect(this.ActionHref(string.Format("{0}/{1}?CourseWareID={2}&CourseID={3}&ScormType=brower&ItemCourseResID={4}", System.Configuration.ConfigurationManager.AppSettings["CourseWareHost"], "Defualt.aspx", Request.QueryString["CourseWareID"], Request.QueryString["CourseID"], Guid.Empty)));

        }
    }
}