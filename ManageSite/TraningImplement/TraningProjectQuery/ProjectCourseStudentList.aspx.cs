using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using System.Data;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_TraningProjectQuery_ProjectCourseStudentList : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = Guid.Empty;
            return ViewState["TrainingItemID"].ToGuid();
        }
        set { ViewState["TrainingItemID"] = value; }
    }
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

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
            this.PageSet1.QueryChange();
        }
        lbtnBack.PostBackUrl = this.ActionHref(string.Format("ProjectCourseList.aspx?TrainingItemID={0}", TrainingItemID));
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind() {
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            Tr_Item item = new Tr_ItemLogic().GetById(ItemCourse.TrainingItemID);
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;
            TrainingItemID = item.TrainingItemID;
            lblItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());

            Res_Course course = new Res_CourseLogic().GetById(ItemCourse.CourseID);
            lblCourseCode.Text = course.CourseCode;
            lblCourseName.Text = course.CourseName;

            lblCourseDate.Text = string.Format("{0} 至 {1}", ItemCourse.CourseBeginTime.ToDate(), ItemCourse.CourseEndTime.ToDate());
        }
    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string sortExpression = " u.OrganizationID,u.DepartmentID,u.RealName";
        DataTable dt =new Sty_StudentSignupLogic().GetStudentCourseListByTrainingItemCourseID(TrainingItemCourseID,pageIndex,pageSize,sortExpression,"",out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[2].Visible = false;
        }
    }
}