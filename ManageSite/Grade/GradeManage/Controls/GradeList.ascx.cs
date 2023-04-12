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
public partial class Grade_GradeManage_Controls_GradeList : System.Web.UI.UserControl
{
    public static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    public static Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();
    private static Ex_OnLineTestLogic Logic = new Ex_OnLineTestLogic();
    public static PublicFacade publicFacade = new PublicFacade();

    #region 页面条件参数存放

    public enum GradeStyle
    {
        Manage,
        Public,
        GradeSearch
    }

    /// <summary>
    /// 操作类型 1 Add 2 Edit 3 View
    /// </summary>
    public GradeStyle Operation
    {
        get
        {           
            return (GradeStyle)ViewState["Operation"];
        }
        set
        {
            ViewState["Operation"] = value;
        }
    }

    #endregion

    //public string GetStudentNum()
    //{
    //    return studentcourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }              
    }

    private void Initial()
    {
        switch (Operation)
        {
            case GradeStyle.Public:
                Literal7.Text = "成绩发布";
                break;
            case GradeStyle.Manage:
                Literal7.Text = "成绩管理";
                dv_select.Visible = false;
                break;
            case GradeStyle.GradeSearch:
                Literal7.Text = "成绩查询";
                dv_select.Visible = false;
                break;
            default:
                break;
        }
    }
    
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        StringBuilder whereQuery= new StringBuilder();       
        whereQuery.Append( BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        whereQuery.Append(string.Format(" and Tr_Item.OrgID={0} and Tr_Item.IsIssue='1' and Tr_Item.IsIssue='1' and Tr_Item.ItemStatus='20'", ETMS.AppContext.UserContext.Current.OrganizationID));

        DataTable dt = itemCourseLogic.GetGradeIssueList(pageIndex,pageSize,string.Empty,whereQuery.ToString(),out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        { 
            DataRowView drv=e.Row.DataItem as DataRowView;
           // ((DictionaryLabel)e.Row.FindControl("ddlDutyDeptID")).FieldIDValue = drv["DutyDeptID"].ToString();
            //Label lblStudentNum = e.Row.FindControl("lblStudentNum") as Label;
            //lblStudentNum.Text = studentcourseLogic.GetItemCourseStudentNum(drv["TrainingItemCourseID"].ToGuid()).ToString();
            LinkButton lbnInput = e.Row.FindControl("lbnInput") as LinkButton;
            LinkButton lbnView = e.Row.FindControl("lbnView") as LinkButton;
            LinkButton lbnPublic = e.Row.FindControl("lbnPublic") as LinkButton;
            LinkButton lbnImport = e.Row.FindControl("lbnImport") as LinkButton;
            LinkButton lbnPublishView = e.Row.FindControl("lbnPublishView") as LinkButton;
            switch (Operation)
            {
                case GradeStyle.Public:
                    lbnInput.Visible = false;
                    lbnView.Visible = false;
                    lbnImport.Visible = false;
                    if (drv["IsIssueGrade"] != null && drv["IsIssueGrade"].ToBoolean() == false)
                    {
                        lbnPublishView.Visible = false;
                    }
                    else
                    {
                        lbnPublic.Visible = false;
                    }
                    break;
                case GradeStyle.Manage:
                    //e.Row.Cells[0].Width = 0;
                    lbnPublic.Visible = false;
                    lbnView.Visible = false;
                    lbnPublishView.Visible = false;
                    break;
                case GradeStyle.GradeSearch:
                    //e.Row.Cells[0].Width = 0;
                    lbnPublic.Visible = false;
                    lbnInput.Visible = false;
                    lbnImport.Visible = false;
                    lbnPublishView.Visible = false;
                    break;
                default:
                    break;
            }
            if (studentcourseLogic.GetItemCourseStudentNum(drv["TrainingItemCourseID"].ToGuid()) != 0)
            {
                lbnPublic.Attributes.Add("href", this.ActionHref(string.Format("{0}/Grade/GradePublish/GradePublish.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}", WebUtility.AppPath, drv["TrainingItemCourseID"].ToGuid(), drv["CourseID"].ToGuid(), drv["TrainingItemID"].ToGuid())));
                lbnView.Attributes.Add("href", this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeViewList.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}", WebUtility.AppPath, drv["TrainingItemCourseID"].ToGuid(), drv["CourseID"].ToGuid(), drv["TrainingItemID"].ToGuid())));
                lbnPublishView.Attributes.Add("href", this.ActionHref(string.Format("{0}/Grade/GradePublish/GradePublish.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}&view=1", WebUtility.AppPath, drv["TrainingItemCourseID"].ToGuid(), drv["CourseID"].ToGuid(), drv["TrainingItemID"].ToGuid())));

            }
            else
            {
                lbnView.CssClass = "link_colorGray";
                lbnPublic.CssClass = "link_colorGray";
                //lbnView.ForeColor =System.Drawing.Color.Gray;
                //lbnPublic.ForeColor = System.Drawing.Color.Gray;

            }

        }
    }

    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName.ToLower())
        //{
        //    case "input":
        //        break;
        //    case "import":
        //        break;
        //    case "view":
        //        break;
        //    case "public":
        //        break;
        //}
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
}