using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.WebApp.Manage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteManage_RecommendTeacher_RecommendTeacherList : System.Web.UI.Page
{
    private static readonly Rec_TeacherLogic teacherLogic = new Rec_TeacherLogic();
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
        Crieria += string.Format("{0} AND OrganizationID={1}", Crieria, UserContext.Current.OrganizationID);
        DataTable dt = teacherLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
                teacherLogic.Remove(e.CommandArgument.ToInt());

            }
            else if (e.CommandName == "Top")//置顶
            {
                Rec_Teacher recTeacher = teacherLogic.GetById(e.CommandArgument.ToInt());
                recTeacher.IsTop = true;
                recTeacher.ModifyTime = DateTime.Now;
                recTeacher.ModifyUser = UserContext.Current.UserName;
                teacherLogic.Update(recTeacher);
            }
            else if (e.CommandName == "UnTop")//取消置顶
            {
                Rec_Teacher recTeacher = teacherLogic.GetById(e.CommandArgument.ToInt());
                recTeacher.IsTop = false;
                recTeacher.ModifyTime = DateTime.Now;
                recTeacher.ModifyUser = UserContext.Current.UserName;
                teacherLogic.Update(recTeacher);
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