using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.WebApp;
using ETMS.Controls;
public partial class QuestionDB_ExOfflineHomework_Controls_ExerciseList : System.Web.UI.UserControl
{
    public Boolean isReadOnly
    {
        get
        {
            if (ViewState["isReadOnly"] == null)
            {
                ViewState["isReadOnly"] = false;
            }
            return (Boolean)ViewState["isReadOnly"];
        }
        set
        {
            ViewState["isReadOnly"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();

        }
        if (isReadOnly)
        {
            dv_selectall.Visible = false;
        }
    }



    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            return BizDataSourceEnum.qu_ExOfflineHomeworkList;
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

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (isReadOnly)
            {
                  e.Row.Cells[0].Text = "";
            }
     
        }
    }
}