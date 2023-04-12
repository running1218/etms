using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Course;

public partial class TraningImplement_ProjectCourseResource_CourseWareAdd : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }
    
    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            return ViewState["CourseID"].ToGuid();
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = int.MaxValue - 1;
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
            this.PageSet1.QueryChange();
        }
        this.aBack.HRef = this.ActionHref(string.Format("CourseWareList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            lblItemName.Text = new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID).ItemName;
            Res_Course course = new Res_CourseLogic().GetById(ItemCourse.CourseID);
            lblCourseName.Text = course.CourseName;
            CourseID = course.CourseID;
            ttbResBeginTime.Text = ItemCourse.CourseBeginTime.ToDate();
            ttbResEndTime.Text = ItemCourse.CourseEndTime.ToDate();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();

        DataTable dt = CoursewareLogic.GetTrainingItemNoSelectResourcesList(TrainingItemCourseID, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的课件！");
            return;
        }
        else
        {
            try
            {
                Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();
                CoursewareLogic.BatchAdd(TrainingItemCourseID, selectedValues, ttbResBeginTime.Text.ToDateTime(), (ttbResEndTime.Text+" 23:59:59").ToDateTime());
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "课件添加成功！", "function (){window.location='"+this.ActionHref("CourseWareList.aspx?TrainingItemCourseID=" + TrainingItemCourseID)+"'}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
    
    /// <summary>
    /// 课件预览路径
    /// </summary>
    protected string GetViewUrl(object CoursewareID)
    {
        return this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}", CoursewareID, CourseID));
    }
}