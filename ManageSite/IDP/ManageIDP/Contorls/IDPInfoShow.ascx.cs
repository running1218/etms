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

public partial class IDP_ManageIDP_Contorls_IDPInfoShow : System.Web.UI.UserControl
{
    #region 页面参数

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
            IDP_PlanLogic planLogic = new IDP_PlanLogic();
            IDP_Plan plan = planLogic.GetById(PlanID);

            Site_MentorLogic site_MentorLogic = new Site_MentorLogic();
            Site_Mentor mentor = new Site_Mentor();
            mentor = site_MentorLogic.GetMentorByMentorID(plan.MentorID);

            Site_Student student = new Site_Student();
            Site_StudentLogic studentLogic = new Site_StudentLogic();

            student = studentLogic.GetById(plan.StudentID);

            lblDate.Text = string.Format("{0} 至 {1}", plan.IDPPlanBeginTime.ToDate(), plan.IDPPlanEndTime.ToDate());
            lblLeaders.Text = mentor.RealName;
            lblLeadersRank.Text = mentor.RankName;
            lblStudentName.Text = student.RealName;
            ltlCreateTime.Text = plan.CreateTime.ToDate();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}