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

using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Controls;

public partial class Scorm_top : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 课程ID
    /// </summary>
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
    /// <summary>
    /// 课件ID
    /// </summary>
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


    /// <summary>
    /// 项目课程资源ID
    /// </summary>
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


    /// <summary>
    /// 当前用户
    /// </summary>
    protected string UserName
    {
        get
        {
            return ETMS.AppContext.UserContext.Current.RealName;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 传参

        if (Request.QueryString["CourseWareID"] != null)
        {
            CourseWareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseWareID")));

        }
        if (Request.QueryString["CourseID"] != null)
        {
            CourseID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseID")));
        }

        #endregion

        InitPageData();
    }

    /// <summary>
    /// 页面信息
    /// </summary>
    private void InitPageData()
    {
        Res_Course course = new Res_Course();
        Res_CourseLogic courseLogic = new Res_CourseLogic();

        course = courseLogic.GetById(CourseID);
        ltlCourseCode.Text = course.CourseCode;
        ltlCourseName.Text = course.CourseName;
    }
    
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        ETMS.Utility.FileDownLoadUtility.ExportFile(Server.MapPath("IECheckJre/jre-6u7-windows-i586-p.exe"));
    }
}
