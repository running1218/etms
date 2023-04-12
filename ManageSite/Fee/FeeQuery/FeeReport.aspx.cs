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
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Utility.Data;

public partial class FeeReport : System.Web.UI.Page
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
                ViewState["SortExpression"] = " a.CreateTime DESC ";
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
            begin_a999ModifyTime.Text = string.Format("{0}-1-1", DateTime.Now.Year).ToDate();
            end_a999ModifyTime.Text = DateTime.Now.ToDate();
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

        //PayStatus 支付状态 课时安排状态必须是“已执行” TeacherSourceID = 1 必须是内部讲师
        Crieria += string.Format("{0} And a.PayStatus={1} And a.CourseHoursStatusID=1 And c.TeacherSourceID = 1 and d.OrganizationID={2}", Crieria, 1,ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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

    protected string getUrl(string ItemCourseHoursID)
    {
        return string.Format("javascript:showWindow(\"讲师课时课酬明细\",\"{0}\")", this.ActionHref(string.Format("~/Fee/FeeQuery/FeeReportView.aspx?ItemCourseHoursID={0}", ItemCourseHoursID)));
    }


    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridView1.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecordCount = 0;
        this.CustomGridView1.Columns.RemoveAt(this.CustomGridView1.Columns.Count - 1);
        this.CustomGridView1.DataSource = PageDataSource(1, 99999999, out totalRecordCount);
        this.CustomGridView1.DataBind();

        ETMS.Utility.FileDownLoadUtility.ExportToExcel("课时课酬统计表.xls", this.CustomGridView1);
    }


    
}