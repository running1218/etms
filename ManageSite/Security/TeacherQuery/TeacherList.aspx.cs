using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;

public partial class Security_TeacherQuery_TeacherList :BasePage
{
    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = "";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Site_TeacherLogic TeacherLogic = new Site_TeacherLogic();
        List<TeacherCourseMuiltyInfo> list = TeacherLogic.GetTeacherMuiltyInfoList(ddl_IsUse.SelectedValue.Trim() == "" ? -1 : ddl_IsUse.SelectedValue.ToInt()
             , txt_TeacherName.Text.Trim()
             , txt_CourseName.Text.Trim()
             , begin_CourseBeginTime.Text.Trim().ToDateTime()
             , end_CourseBeginTime.Text.Trim().ToDateTime()
             , pageIndex
             , pageSize
             , out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string TeacherID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            //讲师
            LinkButton lbtnRealName = (LinkButton)e.Row.FindControl("lbtnRealName");
            lbtnRealName = lbtnRealName == null ? new LinkButton() : lbtnRealName;

            //负责课程数
            LinkButton lbtnTeachNum = (LinkButton)e.Row.FindControl("lbtnTeachNum");
            lbtnTeachNum = lbtnTeachNum == null ? new LinkButton() : lbtnTeachNum;

            //参与项目数
            LinkButton lbtnItemNum = (LinkButton)e.Row.FindControl("lbtnItemNum");
            lbtnItemNum = lbtnItemNum == null ? new LinkButton() : lbtnItemNum;

            //负责项目总课次
            LinkButton lbtnTeachCourseNum = (LinkButton)e.Row.FindControl("lbtnTeachCourseNum");
            lbtnTeachCourseNum = lbtnTeachCourseNum == null ? new LinkButton() : lbtnTeachCourseNum;
            
            //负责项目总课时安排数
            LinkButton lbtnCourseHoursNum = (LinkButton)e.Row.FindControl("lbtnCourseHoursNum");
            lbtnCourseHoursNum = lbtnCourseHoursNum == null ? new LinkButton() : lbtnCourseHoursNum;

            lbtnRealName.Attributes["onclick"] = string.Format("javascript:showWindow('查看讲师信息','{0}');javascript:return false;", this.ActionHref(string.Format("../ProfessorManage/ProfessorViewOutside.aspx?TeacherID={0}", TeacherID)));
            lbtnTeachNum.PostBackUrl = this.ActionHref(string.Format("TeacherTeachCourseList.aspx?TeacherID={0}",TeacherID));
            lbtnItemNum.PostBackUrl = this.ActionHref(string.Format("TeacherItemList.aspx?TeacherID={0}&CourseBeginTimeBegin={1}&CourseBeginTimeEnd={2}"
                , TeacherID
                , begin_CourseBeginTime.Text
                , end_CourseBeginTime.Text));

            lbtnTeachCourseNum.PostBackUrl = this.ActionHref(string.Format("TeacherTraningItemCourseList.aspx?TeacherID={0}&CourseBeginTimeBegin={1}&CourseBeginTimeEnd={2}"
                , TeacherID
                , begin_CourseBeginTime.Text
                , end_CourseBeginTime.Text));

           lbtnCourseHoursNum.PostBackUrl=this.ActionHref(string.Format("TeacherTraningItemCourseHoursList.aspx?TeacherID={0}&CourseBeginTimeBegin={1}&CourseBeginTimeEnd={2}"
                , TeacherID
                , begin_CourseBeginTime.Text
                , end_CourseBeginTime.Text));
        }
    }
}