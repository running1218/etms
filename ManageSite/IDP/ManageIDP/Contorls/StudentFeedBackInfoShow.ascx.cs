using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL;
using ETMS.Components.IDP.API.Entity;
using ETMS.Components.Mentor.Implement.BLL.Mentor;
using ETMS.Components.Mentor.API.Entity.Mentor;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;

public partial class IDP_ManageIDP_Contorls_StudentFeedBackInfoShow : System.Web.UI.UserControl
{

    #region 页面参数

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();
        }
    }

    private void InitData()
    {
        try
        {
            IDP_PlanObjectLogic logic = new IDP_PlanObjectLogic();

            IDP_PlanObject planObject = logic.GetById(PlanObjectID);

            ltlPlanDevelopment.Text = planObject.PlanDevelopment;
            ltlAbility.Text = planObject.Ability;
            ltlHopeLevel.Text = planObject.HopeLevel;
            ltlSuperiorEvaluation.Text = planObject.SuperiorOpinion;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}