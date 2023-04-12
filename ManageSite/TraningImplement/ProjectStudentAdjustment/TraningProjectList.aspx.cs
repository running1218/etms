using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_ProjectStudentAdjustment_TraningProjectList :BasePage
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
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //对于已发布未归档的启用的培训项目
        Crieria = string.Format(" {0} AND OrgID={1} AND IsIssue=1 AND ItemEndModeID=0 AND IsUse=1 AND ItemStatus=20", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

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
            Label lblCourseTotal = (Label)e.Row.FindControl("lblCourseTotal");
            lblCourseTotal = lblCourseTotal == null ? new Label() : lblCourseTotal;

            Label lblStudetnTotal = (Label)e.Row.FindControl("lblStudetnTotal");
            lblStudetnTotal = lblStudetnTotal == null ? new Label() : lblStudetnTotal;

            LinkButton lbtnSetsStudent = (LinkButton)e.Row.FindControl("lbtnSetsStudent");
            lbtnSetsStudent = lbtnSetsStudent == null ? new LinkButton() : lbtnSetsStudent;
            #endregion

            //课程数
            lblCourseTotal.Text = new Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(itemID).ToString();
            //学员数
            lblStudetnTotal.Text = new Sty_StudentSignupLogic().GetTrainingItemStudentTotal(itemID).ToString();

            //设置学员
            lbtnSetsStudent.PostBackUrl = this.ActionHref(string.Format("ProjectStudentAdjustment.aspx?TrainingItemID={0}", itemID));
            lbtnSetsStudent.Enabled = true;
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}