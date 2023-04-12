using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility;

public partial class Resource_CourseManage_Controls_CourseInfoView : System.Web.UI.UserControl
{
    private static readonly Res_CourseLogic res_CourseLogic = new Res_CourseLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetViewData();
        }
    }

    protected void GetViewData()
    {
        if (null != Request.QueryString["CourseID"])
        {
            Res_Course course = res_CourseLogic.GetById(Request.QueryString["CourseID"].ToGuid());
            this.ltlCourseCode.Text = course.CourseCode;
            this.ltlCourseName.Text = course.CourseName;
            this.ltlCourseHours.Text = course.CourseHours.ToString();
            this.ltlCourseIntroduction.Text = course.CourseIntroduction;
            this.ltlCourseOutline.Text = course.CourseOutline;
            this.ltlForObject.Text = course.ForObject;
            this.lblCourseStatus.FieldIDValue = course.CourseStatus.ToString();
            this.lblIsPublic.FieldIDValue = course.IsPublic? "true" : "false";
            this.lblCourseType.FieldIDValue = course.CourseTypeID.ToString();
            this.lblCourseLevel.FieldIDValue = course.CourseLevelID.ToString();
            ltlCreateUser.Text = course.CreateUser;
            ltlCreateTime.Text = course.CreateTime.ToDate();
            imgLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(course.ThumbnailURL) ? "default.jpg" : course.ThumbnailURL);
            this.ltlTeacherNum.Text = new Res_TeacherCourseLogic().GetCourseTeacherNum(Request.ToparamValue<Guid>("CourseID")).ToString();
        }
    }
}