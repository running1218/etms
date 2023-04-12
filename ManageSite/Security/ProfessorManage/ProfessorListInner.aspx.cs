using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.WebApp.Manage;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;

public partial class Resource_ProfessorManage_ProfessorListInner : ETMS.Controls.BasePage
{
    public static Res_TeacherCourseLogic teacherCourseLogic = new Res_TeacherCourseLogic();
    public static Site_TeacherLogic site_TeacherLogic = new Site_TeacherLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        this.btnAdd.PostBackUrl= this.ActionHref(string.Format("{0}/Security/ProfessorManage/ProfessorAddInner.aspx", WebUtility.AppPath));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string teacherName = txtTeacherName.Text.Trim();  
       
        int isUse = Dic_Status.SelectedValue.ToInt();
        int teacherLevelID = Dic_ProfessorGrade.SelectedValue.ToInt();
        string workNo = txtWorkNo.Text.Trim();
        int teacherTypeID =this.ddlTeacherType.SelectedValue.ToInt();
        int isCourseTeacher = this.ddlCourseDesiner.SelectedValue.ToInt();
        DataTable dt = site_TeacherLogic.GetInnerTeacherList(ETMS.AppContext.UserContext.Current.OrganizationID, workNo, teacherName, teacherTypeID, teacherLevelID, isUse, isCourseTeacher);
        totalRecordCount = dt.Rows.Count;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }

    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Del":
                    int TeacherID = int.Parse(e.CommandArgument.ToString());
                    if (teacherCourseLogic.GetTeacherTeachCourse(TeacherID).Count > 0)
                    {
                        ETMS.Utility.JsUtility.FailedMessageBox("讲师已有授课，无法删除！");
                    }
                    else
                    {
                        site_TeacherLogic.Remove(TeacherID);
                        this.PageSet1.DataBind();
                        this.upList.Update();
                    }
                    break;
                case "IsUse":
                    break;
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}