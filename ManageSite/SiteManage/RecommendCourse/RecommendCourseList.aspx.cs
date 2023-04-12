using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.WebApp.Manage;
using ETMS.Controls;
using System.Data;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course;

public partial class SiteManage_RecommendCourse_RecommendCourseList : BasePage
{
    private static readonly Rec_CourseLogic courseLogic = new Rec_CourseLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    #region 页面参数
    /// <summary>
    /// 查询条件 
    /// </summary>
    private string criteria;
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

    /// <summary>
    /// 排序条件
    /// </summary>
    private string sortExpression;
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " Sort ASC ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        Crieria += string.Format("{0} AND rc.OrgID={1}", Crieria, UserContext.Current.OrganizationID);
        DataTable dt = courseLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Del")
            {
                courseLogic.Remove(e.CommandArgument.ToGuid());

            }
            else if (e.CommandName == "Top")//置顶
            {
                Rec_Course recCourse = courseLogic.GetById(e.CommandArgument.ToGuid());
                recCourse.IsTop = true;
                recCourse.ModifyTime = DateTime.Now;
                recCourse.ModifyUser = UserContext.Current.UserName;
                courseLogic.Update(recCourse);
            }
            else if (e.CommandName == "UnTop")//取消置顶
            {
                Rec_Course recCourse = courseLogic.GetById(e.CommandArgument.ToGuid());
                recCourse.IsTop = false;
                recCourse.ModifyTime = DateTime.Now;
                recCourse.ModifyUser = UserContext.Current.UserName;
                courseLogic.Update(recCourse);
            }
            this.PageSet1.DataBind();
            this.upList.Update();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
       
    }
}