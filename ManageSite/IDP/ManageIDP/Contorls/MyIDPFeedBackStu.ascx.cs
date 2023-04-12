using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL;
using System.Data;
using ETMS.Controls;

public partial class IDP_ManageIDP_Contorls_MyIDPFeedBackStu : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// ID
    /// </summary>
    public Guid PlanObjectID
    {
        get
        {
            if (ViewState["PlanObjectID"] == null)
            {
                ViewState["PlanObjectID"] = Guid.Empty;
            }
            return (Guid)ViewState["PlanObjectID"];
        }
        set
        {
            ViewState["PlanObjectID"] = value;
        }
    }
    #endregion

    private static readonly IDP_PlanObjectFeedbackLogic planObjectFeedbackLogic = new IDP_PlanObjectFeedbackLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        psPager.pageInit(rptList, PageDataSource);

        if (!IsPostBack)
        {
            this.psPager.QueryChange();
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
        string sortExpression = " CreateTime DESC ";
        string criteria = string.Format(" And IDPPlanObjectID ='{0}'", PlanObjectID);
        DataTable dt = planObjectFeedbackLogic.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecordCount);
        if (totalRecordCount == 0)
        {
            divNoResult.Visible = true;
        }
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

}