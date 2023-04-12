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
public partial class Information_AfficheManager_Controls_AfficheList : System.Web.UI.UserControl
{
    /// <summary>
    /// 类型 1 公司级 2 项目级
    /// </summary>
    public Int32 InfoType
    {
        get
        {
            if (ViewState["InfoType"] == null)
            {
                ViewState["InfoType"] = 1;
            }
            return (Int32)ViewState["InfoType"];
        }
        set
        {
            ViewState["InfoType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (InfoType == 1)
        {
            Literal12.Text = "公司公告管理";
        }
        else
        {
            Literal12.Text = "项目公告管理";
        }
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            return BizDataSourceEnum.tb_Affiche;
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