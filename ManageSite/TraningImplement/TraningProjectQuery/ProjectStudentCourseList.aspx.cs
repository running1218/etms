using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;

public partial class TraningImplement_TraningProjectQuery_ProjectStudentCourseList : System.Web.UI.Page
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
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public int UserID
    {
        get
        {
            if (ViewState["UserID"] == null)
                ViewState["UserID"] = 0;

            return ViewState["UserID"].ToInt();
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "UserID")))
                UserID = BasePage.getSafeRequest(this.Page, "UserID").ToInt();

            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
            }

            bind();
            this.PageSet1.QueryChange();
        }
        lbtnReturn.PostBackUrl = this.ActionHref(string.Format("ProjectStudentList.aspx?TrainingItemID={0}", TrainingItemID));
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Tr_Item item = new Tr_ItemLogic().GetById(TrainingItemID);
        if (item != null)
        {
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;
            lblItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());

            ETMS.Components.Basic.API.Entity.Security.User user = new UserLogic().GetUserByID(UserID);

            lblDepartmentID.FieldIDValue = user.DepartmentID.ToString();
            lblOrganization.FieldIDValue=user.OrganizationID.ToString();
            lblRealName.Text = user.RealName;
            lblPost.FieldIDValue = new Site_StudentLogic().GetById(UserID).PostID.ToString();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable dt = StudentSignupLogic.GetStudentCourseListByTrainingItemID(UserID, TrainingItemID, pageIndex, pageSize, "", "", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    
    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();

            #region 获取控件
            LinkButton lbtnCourseName = (LinkButton)e.Row.FindControl("lbtnCourseName");
            lbtnCourseName = lbtnCourseName == null ? new LinkButton() : lbtnCourseName;

            LinkButton lbtnTeacherTotal = (LinkButton)e.Row.FindControl("lbtnTeacherTotal");
            lbtnTeacherTotal = lbtnTeacherTotal == null ? new LinkButton() : lbtnTeacherTotal;

            #endregion


            //课程名
            if (lbtnCourseName.Text.Length > 10)
                lbtnCourseName.Text = lbtnCourseName.Text.Substring(0, 10) + "...";
            lbtnCourseName.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程信息','{0}');javascript:return false;"
                , this.ActionHref(string.Format("../TraningProjectManager/SetsCourseView.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));

            //讲师名
            lbtnTeacherTotal.Text = new Tr_ItemCourseTeacherLogic().GetTeacherTotal(TrainingItemCourseID).ToString();
            lbtnTeacherTotal.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程讲师信息','{0}');javascript:return false;"
                , this.ActionHref(string.Format("ProjectCourseTeacherList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
        }
    }
}