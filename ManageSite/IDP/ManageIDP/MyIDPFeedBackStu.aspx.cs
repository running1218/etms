using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

public partial class IDP_ManageIDP_MyIDPFeedBackStu : System.Web.UI.Page
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
            PlanID = Request.QueryString["PlanID"].ToGuid();
            
            StudentFeedBackInfoShow1.PlanObjectID = Request.QueryString["PlanObjectID"].ToGuid();
            MyIDPFeedBackStu1.PlanObjectID = Request.QueryString["PlanObjectID"].ToGuid();
        }
    }

    protected string GetURL()
    {
        return this.ActionHref(string.Format("~/IDP/ManageIDP/IDPView.aspx?PlanID={0}", PlanID));
    }
}