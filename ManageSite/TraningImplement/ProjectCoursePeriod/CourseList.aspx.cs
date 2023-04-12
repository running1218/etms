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
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class TraningImplement_ProjectCoursePeriod_CourseList : System.Web.UI.Page
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
            ddl_Tr_ItemCourse999TeachModelID.Items.Add(new ListItem("在线", "1"));
            ddl_Tr_ItemCourse999TeachModelID.Items.Add(new ListItem("面授", "2"));
            ddl_Tr_ItemCourse999TeachModelID.SelectedValue = "2";

            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //对本组织机构创建的已发布未归档的启用的培训项目进行课程课时安排的管理
        Crieria = string.Format(" AND Tr_Item.ItemStatus in (10,20,40) AND IsIssue=1 AND IsUse=1 {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Tr_ItemCourseLogic courseLogic = new Tr_ItemCourseLogic();
        DataTable dt = courseLogic.GetItemCourseListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID
            , pageIndex
            , pageSize
            , Crieria
            , out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid itemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获取控件
            Label lblTeacher = (Label)e.Row.FindControl("lblTeacher");
            lblTeacher = lblTeacher == null ? new Label() : lblTeacher;

            Label lblPeriod = (Label)e.Row.FindControl("lblPeriod");
            lblPeriod = lblPeriod == null ? new Label() : lblPeriod;            

            HiddenField Hf_ItemStatus = (HiddenField)e.Row.FindControl("Hf_ItemStatus");
            Hf_ItemStatus = Hf_ItemStatus == null ? new HiddenField() : Hf_ItemStatus;

            LinkButton lbtnPeriod = (LinkButton)e.Row.FindControl("lbtnPeriod");
            lbtnPeriod = lbtnPeriod == null ? new LinkButton() : lbtnPeriod;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            Label lblSelectCourse = (Label)e.Row.FindControl("lblSelectCourse");
            lblSelectCourse = lblSelectCourse == null ? new Label() : lblSelectCourse;

            LinkButton lbnOnlinePlaying = (LinkButton)e.Row.FindControl("lbnOnlinePlaying") ?? new LinkButton();

            #endregion

            //已设课时安排数
            lblPeriod.Text = new Tr_ItemCourseHoursLogic().GetItemCourseHourseTotal(itemCourseID).ToString();
            //讲师数
            lblTeacher.Text = new Tr_ItemCourseTeacherLogic().GetTeacherTotal(itemCourseID).ToString();
            //选课人数
            lblSelectCourse.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(itemCourseID).ToString();

            //switch (Hf_ItemStatus.Value.Trim())
            //{
            //    case "90": //已归档的只显示查看
            //        lbtnView.Visible = true;
            //        lbtnView.PostBackUrl = this.ActionHref(string.Format("CoursePeriodListView.aspx?TrainingItemCourseID={0}", itemCourseID));
            //        lbtnPeriod.Visible = false;
            //        break;
            //    default:
                    lbtnView.Visible = false;
                    lbtnPeriod.Visible = true;
                    lbtnPeriod.PostBackUrl = this.ActionHref(string.Format("CoursePeriodList.aspx?TrainingItemCourseID={0}", itemCourseID));
                    lbnOnlinePlaying.PostBackUrl = this.ActionHref(string.Format("CourseOnlinePlayingList.aspx?TrainingItemCourseID={0}", itemCourseID));
            //        break;
            //}
        }
    }
}