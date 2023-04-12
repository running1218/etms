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

public partial class Information_AfficheManager_Controls_IssuanceObjectGroup : System.Web.UI.UserControl
{
    private string op = string.Empty;
    public string Op
    {
        get { return op; }
        set { op = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Op == "list")
        {
            btn_Add.Visible = true;
            btn_Save.Visible = false;
            btn_Del.Visible = true;
        }
        else
        {
            btn_Add.Visible = false;
            btn_Save.Visible = true;
            btn_Del.Visible = false;
        }

        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 10;

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            return BizDataSourceEnum.tb_AfficheGroup;
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