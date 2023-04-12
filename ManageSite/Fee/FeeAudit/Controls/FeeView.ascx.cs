using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.WebApp.Manage;
using ETMS.AppContext;

public partial class Fee_FeeAudit_Controls_FeeView : System.Web.UI.UserControl
{
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        PageSet1.PageSize = 10;

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            if (Action != OperationAction.Audit)
                divAudit.Style.Add("display", "none");
        }
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            return BizDataSourceEnum.tb_CourseFeeConfirmAudit11;
        }
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        totalRecordCount = 0;
        DataTable dt = ReadExcel.GetData(DataSourceEnum);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }
}