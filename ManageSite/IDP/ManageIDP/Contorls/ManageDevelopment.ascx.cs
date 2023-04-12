using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL;
using ETMS.Utility;

public partial class IDP_ManageIDP_Contorls_ManageDevelopment : System.Web.UI.UserControl
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

    #endregion


    private static readonly IDP_PlanLogic planlogic = new IDP_PlanLogic();
    private static readonly IDP_PlanObjectLogic planObjectlogic = new IDP_PlanObjectLogic();
    private static readonly IDP_PlanObjectFeedbackLogic planObjectFeedbackLogic = new IDP_PlanObjectFeedbackLogic();

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
        rptList.DataSource = planObjectlogic.GetPagedList(1, 9999999, sortExpression, criteria, out totalRecords);
        rptList.DataBind();
    }

    protected string GetUrl3(Guid planObjectID)
    {
        return this.ActionHref(string.Format("~/IDP/ManageIDP/MyIDPFeedBackStu.aspx?PlanObjectID={0}&PlanID={1}", planObjectID, PlanID));
    }
}