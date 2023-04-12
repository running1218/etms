using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Text;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.StudentStudy;

public partial class Grade_GradeManage_GradeEntryList : System.Web.UI.Page
{
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    public static Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();
    private static Sty_StudentCourse studentCourse = new Sty_StudentCourse();
    private static Ex_OnLineTestLogic Logic = new Ex_OnLineTestLogic();
    public static PublicFacade publicFacade = new PublicFacade();
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
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        whereQuery.Append(string.Format(" and Tr_Item.OrgID={0} and Tr_Item.IsIssue=1 and Tr_Item.IsUse=1 and Tr_Item.ItemStatus=20 and Tr_Item.ItemStatus<>90 and Tr_ItemCourse.IsIssueGrade=0 ", ETMS.AppContext.UserContext.Current.OrganizationID));

        DataTable dt = itemCourseLogic.GetGradeIssueList(pageIndex, pageSize, " Tr_Item.OrgID", whereQuery.ToString(), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;           
            LinkButton lbnInput = e.Row.FindControl("lbnInput") as LinkButton;            
            LinkButton lbnImport = e.Row.FindControl("lbnImport") as LinkButton;
            lbnInput.Attributes.Add("href", this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeEntry.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}", WebUtility.AppPath, drv["TrainingItemCourseID"].ToGuid(), drv["CourseID"].ToGuid(), drv["TrainingItemID"].ToGuid())));
            lbnImport.Attributes.Add("onclick", string.Format("javascript:showWindow('导入成绩','{0}',500,400);javascript: return false;", this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeEntryImport.aspx?TrainingItemCourseID={1}", WebUtility.AppPath, drv["TrainingItemCourseID"].ToGuid()))));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    protected int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
    {
        return Logic.GetItemCourseOnlineTestTotal(trainingItemCourseID);
    }
    protected int GetUninputNumber(Guid trainingItemCourseID)
    {
        return studentCourse.GetNoInputGradeTotalByItemCourseID(trainingItemCourseID);
    }
}