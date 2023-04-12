using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_TraningProjectQuery_TraningProjectList : System.Web.UI.Page
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
                ViewState["SortExpression"] = " CreateTime DESC";
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
            begin_ItemBeginTime.Text = string.Format("{0}-01-01", DateTime.Now.ToString("yyyy"));
            end_ItemBeginTime.Text = DateTime.Now.ToDate();
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemLogic item = new Tr_ItemLogic();
        DataTable dt = item.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
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
            Guid itemID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获取控件
            LinkButton lbtnCourseTotal = (LinkButton)e.Row.FindControl("lbtnCourseTotal");
            lbtnCourseTotal = lbtnCourseTotal == null ? new LinkButton() : lbtnCourseTotal;

            LinkButton lbtnStudetnTotal = (LinkButton)e.Row.FindControl("lbtnStudetnTotal");
            lbtnStudetnTotal = lbtnStudetnTotal == null ? new LinkButton() : lbtnStudetnTotal;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;
            #endregion

            //课程数
            lbtnCourseTotal.Text = new Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(itemID).ToString();
            lbtnCourseTotal.PostBackUrl = this.ActionHref(string.Format("ProjectCourseList.aspx?TrainingItemID={0}", itemID));  
            //学员数
            lbtnStudetnTotal.Text = new Sty_StudentSignupLogic().GetTrainingItemStudentTotal(itemID).ToString();
            lbtnStudetnTotal.PostBackUrl = this.ActionHref(string.Format("ProjectStudentList.aspx?TrainingItemID={0}", itemID));

            //查看
            lbtnView.PostBackUrl = this.ActionHref(string.Format("TraningProjectView.aspx?TrainingItemID={0}", itemID));
            lbtnView.Enabled = true;
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}