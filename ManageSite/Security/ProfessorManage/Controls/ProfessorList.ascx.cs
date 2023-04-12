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
public partial class Resource_ProfessorManage_Controls_ProfessorList : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// 操作类型 1 Inner 2 Outside
    /// </summary>
    public Int32 ProfessorType
    {
        get
        {
            if (ViewState["ProfessorType"] == null)
            {
                ViewState["ProfessorType"] = 1;
            }
            return (Int32)ViewState["ProfessorType"];
        }
        set
        {
            ViewState["ProfessorType"] = value;
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
        if (ProfessorType == 1)
        {
            btn_Add1.Visible = true;
            btn_Add2.Visible = false;
        }
        else
        {
            btn_Add2.Visible = true;
            btn_Add1.Visible = false;
        }

        ;
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            if (ProfessorType==1)
            {
                return BizDataSourceEnum.ProfessorListInner;
            }
            else
            {
                return BizDataSourceEnum.ProfessorListOutside;
            }
            
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
        
        
    }

    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}