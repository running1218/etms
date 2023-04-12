using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class TraningImplement_TraningProjectQuery_ProjectCourseList : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
            this.PageSet1.QueryChange();
        }
        lbtnBack.PostBackUrl = this.ActionHref("TraningProjectList.aspx");
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            lbl_ItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize,"OrderNum", out totalRecordCount);

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

            LinkButton lbtnStudentTotal = (LinkButton)e.Row.FindControl("lbtnStudentTotal");
            lbtnStudentTotal = lbtnStudentTotal == null ? new LinkButton() : lbtnStudentTotal;
            #endregion

            //课程名
            if (lbtnCourseName.Text.Length > 10)
                lbtnCourseName.Text = lbtnCourseName.Text.Substring(0, 10) + "...";
            lbtnCourseName.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程信息','{0}');javascript:return false;"
                , this.ActionHref(string.Format("../TraningProjectManager/SetsCourseView.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));

            //讲师数
            lbtnTeacherTotal.Text = new Tr_ItemCourseTeacherLogic().GetTeacherTotal(TrainingItemCourseID).ToString();
            lbtnTeacherTotal.Attributes["onclick"] = string.Format("javascript:showWindow('项目课程讲师信息','{0}');javascript:return false;"
                , this.ActionHref(string.Format("ProjectCourseTeacherList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));

            //学员数
            lbtnStudentTotal.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(TrainingItemCourseID).ToString();
            lbtnStudentTotal.PostBackUrl = this.ActionHref(string.Format("ProjectCourseStudentList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
        }
    }
}