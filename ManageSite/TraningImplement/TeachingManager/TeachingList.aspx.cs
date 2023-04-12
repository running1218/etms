using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class TraningImplement_TeachingManager_TeachingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            //this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt =new DataTable();

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }


    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            //e.Row.Cells[3].Text = "<a href='TeachingViewList.aspx?type=0'>" + e.Row.Cells[3].Text + "</a>";
            //e.Row.Cells[4].Text = "<a href='TeachingViewList.aspx?type=1'>" + e.Row.Cells[4].Text + "</a>";
            //e.Row.Cells[5].Text = "<a href='TeachingViewList.aspx?type=2'>" + e.Row.Cells[5].Text + "</a>";
            //e.Row.Cells[6].Text = "<a href='TeachingViewList.aspx?type=3'>" + e.Row.Cells[6].Text + "</a>";
        }
    }
    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}