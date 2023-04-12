using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.IDP.API.Entity;
public partial class IDP_ManageIDP_IDPList : System.Web.UI.Page
{

    #region 页面参数
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

    private static readonly IDP_PlanLogic PlanLogic = new IDP_PlanLogic();
    protected void Page_Load(object sender, EventArgs e)
    {

        PageSet1.pageInit(this.gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
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
        Crieria += string.Format(" And a.OrgID ={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
       
        //按学员的部门、岗位、职级、姓名、创建日期降序排序
        SortExpression = "a.CreateTime desc, c.RealName ASC,d.RealName ASC ";
        DataTable dt = PlanLogic.GetPagedListForManage(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 单个删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                PlanLogic.Remove(new Guid(e.CommandArgument.ToString()));
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
        if (e.CommandName == "Open")
        {
            IDP_Plan plan = PlanLogic.GetById(new Guid(e.CommandArgument.ToString()));

            IDP_PlanLogic planLogic = new IDP_PlanLogic();

            string criteria = string.Format(" And MentorID={0} And StudentID={1} And IsClose=0 ", plan.MentorID, plan.StudentID);
            int totalRecords = 0;
            DataTable dt = planLogic.GetPagedList(1, 1, "", criteria, out totalRecords);

            if (totalRecords == 0)
            {
                try
                {
                    plan.IsClose = false;
                    PlanLogic.Save(plan);
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("一个学员、一个辅导导师之间只能存在一个开启的IDP。");
                return;
            }

        }
    }

    /// <summary>
    /// 关闭IDP
    /// </summary>
    /// <returns></returns>
    protected string getUrl2(Guid planID, string isClose)
    {
        string returnValue = "";
        if (isClose.ToLower() == "false")
        {
            string url = this.ActionHref(string.Format("~/IDP/ManageIDP/CloseIDP.aspx?PlanID={0}", planID));
            returnValue = string.Format("<a href=\"javascript:showWindow('关闭IDP','{0}');\">关闭</a>", url);
        }

        return returnValue;
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(gvList.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
    /// <summary>
    /// 数据导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        this.gvList.Columns.RemoveAt(this.gvList.Columns.Count - 1);
        this.gvList.DataSource = PageDataSource(1, 99999999, out totalRecordCount);
        this.gvList.DataBind();

        ETMS.Utility.FileDownLoadUtility.ExportToExcel("学员导师关系统计表.xls", this.gvList);
    }
}