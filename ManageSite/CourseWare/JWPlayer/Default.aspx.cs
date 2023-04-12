using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Utility.Service;

public partial class JWPlayer_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string murl = Request.QueryString["url"].ToString();
        Guid CourseID = Request.QueryString["CourseID"].ToGuid();
        InitPageData(CourseID);
        string rootUrl = (ServiceRepository.FileUploadStrategyService as ETMS.Utility.Service.FileUpload.DefaultFileUploadStrategyService).UrlRoot;

        murl = string.Format("{0}/{1}", rootUrl, murl);
        litJWPlayer.Text = string.Format("<script language=javascript>player('{0}');</script>", murl);
    }
    /// <summary>
    /// 页面信息
    /// </summary>
    private void InitPageData(Guid courseID)
    {
        Res_Course course = new Res_Course();
        Res_CourseLogic courseLogic = new Res_CourseLogic();

        course = courseLogic.GetById(courseID);
        ltlCourseCode.Text = course.CourseCode;
        ltlCourseName.Text = course.CourseName;
    }
}