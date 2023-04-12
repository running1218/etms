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
using ETMS.Components.Fee.Implement.BLL;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class FeeDetailsList : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {

        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

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

        Crieria += string.Format("{0} And a.DelFlag=0", Crieria);

        Fee_FeeCostDetailsLogic feeCostDetailsLogic = new Fee_FeeCostDetailsLogic();

        DataTable dt = feeCostDetailsLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                Fee_FeeCostDetailsLogic feeCostDetailsLogic = new Fee_FeeCostDetailsLogic();
                feeCostDetailsLogic.Remove(e.CommandArgument.ToGuid());
                
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected string getItemName(Guid ItemID)
    {
        try
        {
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            return itemLogic.GetById(ItemID).ItemName;
        }
        catch
        {
            return "";
        }
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

        ETMS.Utility.FileDownLoadUtility.ExportToExcel("费用流水统计表.xls", this.CustomGridView1);
    }

}