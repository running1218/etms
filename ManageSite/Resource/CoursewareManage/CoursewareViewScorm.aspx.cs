using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Controls;
using ETMS.Utility;

public partial class Resource_CoursewareManage_CoursewareViewScorm : System.Web.UI.Page
{
    #region 页面条件参数存放

    //课件ID
    public Guid CoursewareID
    {
        get
        {
            if (ViewState["CoursewareID"] == null)
            {
                ViewState["CoursewareID"] = defaultGuidValue;
            }
            return (Guid)ViewState["CoursewareID"];
        }
        set
        {
            ViewState["CoursewareID"] = value;
        }
    }

    #endregion

    private static readonly Res_CoursewareLogic res_CoursewareLogic = new Res_CoursewareLogic();
    private static Guid defaultGuidValue = new Guid();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CoursewareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CoursewareID")));
            InitControl();            
        }

    }

    //初始化控件值
    private void InitControl()
    {
        Res_Courseware courseware = new Res_Courseware();
        courseware = res_CoursewareLogic.GetById(CoursewareID);

        ltlCoursewareName.Text = courseware.CoursewareName;
        ltlCoursewarePath.Text = courseware.CoursewarePath;

        ltlCoursewareStatus.Text = courseware.CoursewareStatus.ToString();
        ltlShowHoures.Text = courseware.ShowHoures.ToString();
        ltlCoursewareSource.Text = courseware.CoursewareSource;
        ltlRemark.Text = courseware.Remark;
        SetCourseInfo(courseware.CoursewareID);
    }

    void SetCourseInfo(Guid courseWareID)
    {
        Res_Course course = new Res_CourseLogic().GetById(new Res_CourseResLogic().getCourseIDByResID(courseWareID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.Courseware)) ?? new Res_Course();
        ltlCourseCode.Text = course.CourseCode;
        lblCourseName.Text = course.CourseName;
    }
}