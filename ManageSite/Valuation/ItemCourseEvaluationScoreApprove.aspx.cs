using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility.Service.FileUpload;

public partial class Valuation_ItemCourseEvaluationScoreApprove : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return (Guid)ViewState["TrainingItemCourseID"];
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TrainingItemCourseID = Request.QueryString["ObjectID"].ToGuid();
            initCourseInfo();

            EvaluationMark1.ObjectType = ETMS.Components.Basic.Implement.BLL.BizEvaluationObjectType.ItemCourse;
            EvaluationMark1.ObjectID = TrainingItemCourseID.ToString();
            EvaluationMark1.IsApprove = true;

            EvaluationScore1.ObjectID = TrainingItemCourseID.ToString();
            EvaluationScore1.IsApprove = true;
        }
    }

    /// <summary>
    /// 初始化课程信息
    /// </summary>
    private void initCourseInfo()
    {
        Tr_ItemCourse itemCourse = new Tr_ItemCourse();
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        itemCourse = itemCourseLogic.GetById(TrainingItemCourseID);

        //获得项目名称
        Tr_ItemLogic tr_ItemLogic = new Tr_ItemLogic();

        //获得课程名称和简介
        Res_CourseLogic res_CourseLogic = new Res_CourseLogic();
        Res_Course res_Course = res_CourseLogic.GetById(itemCourse.CourseID);
        ltlCourseName.Text = string.Format("项目：{0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;课程：{1}", tr_ItemLogic.GetById(itemCourse.TrainingItemID).ItemName, res_Course.CourseName);
        //ltlCourseIntroduction.Text = res_Course.CourseIntroduction == "" ? "暂无" : res_Course.CourseIntroduction;

        imgCourse.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(res_Course.ThumbnailURL) ? "default.jpg" : res_Course.ThumbnailURL);

    }    
}