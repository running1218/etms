using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL;

public partial class IDP_ManageIDP_Contorls_ManageContent : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// ID
    /// </summary>
    public Guid PlanID
    {
        get
        {
            if (ViewState["PlanID"] == null)
            {
                ViewState["PlanID"] = Guid.Empty;
            }
            return (Guid)ViewState["PlanID"];
        }
        set
        {
            ViewState["PlanID"] = value;
        }
    }
    /// <summary>
    /// ID
    /// </summary>
    public Guid IDPPlanContentID
    {
        get
        {
            if (ViewState["IDPPlanContentID"] == null)
            {
                ViewState["IDPPlanContentID"] = Guid.Empty;
            }
            return (Guid)ViewState["IDPPlanContentID"];
        }
        set
        {
            ViewState["IDPPlanContentID"] = value;
        }
    }

    #endregion

    private static readonly IDP_PlanContentLogic PlanContentLogic = new IDP_PlanContentLogic();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Init();
        }
    }

    private void Init()
    {
  
        InitList();
    }

    private void InitList()
    {
        int totalRecords = 0;
        string sortExpression = " CreateTime Desc ";
        string criteria = string.Format(" And IDP_PlanID='{0}' ", PlanID);
        rptList.DataSource = PlanContentLogic.GetPagedList(1, 9999999, sortExpression, criteria, out totalRecords);
        rptList.DataBind();
    }
}