using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Activity.Implement.BLL;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Controls;

public partial class Activity_ProductionList : System.Web.UI.Page
{
    protected static readonly ProductionLogic logic = new ProductionLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(rptList, PageDataSource);

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
        var data = logic.GetMarkingActivity(UserContext.Current.OrganizationID, txt_AppraisalName.Text.Trim(), txtBeginTime.Text.ToStartDateTime(), txtEndTime.Text.ToEndDateTime(), pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(data, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
}