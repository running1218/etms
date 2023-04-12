using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;

public partial class TraningOrgManager_TraningOrgManager_Controls_TraningOrgView : System.Web.UI.UserControl
{
    public static Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();
    public Guid OuterOrgID
    {
        get
        {
            if (ViewState["OuterOrgID"] == null)
            {
                ViewState["OuterOrgID"] = Request.QueryString["id"];
            }
            return ViewState["OuterOrgID"].ToGuid();
        }
        set { ViewState["OuterOrgID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Inital();
        }
    }
   

    private void Inital()
    {
        Tr_OuterOrg outerOrg=new Tr_OuterOrg();
        outerOrg = outerOrgLogic.GetById(OuterOrgID);
        this.lblBestClass.Text = outerOrg.BestCourse;
        this.lblEMAIL.Text = outerOrg.EMAIL;
        this.lblHistoryCooperation.Text = outerOrg.HistoryCooperation;
        this.lblLinkMan.Text = outerOrg.LinkMan;
        this.lblLinkMode.Text = outerOrg.LinkMode;
        this.lblOrgAssess.Text = outerOrg.OrgAssess;
        this.lblOuterOrgCode.Text = outerOrg.OuterOrgCode;
        this.lblOuterOrgName.Text = outerOrg.OuterOrgName;
        this.lblServiceContent.Text = outerOrg.ServiceContent;
        this.lblAddress.Text = outerOrg.OuterOrgAddr;
        this.lblHttp.Text = outerOrg.OuterOrgURL;
        this.lblRemark.Text = outerOrg.Remark;
        this.lblStatus.Text = outerOrg.OuterOrgStatus == 1 ? "启用" : "停用";
        string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", outerOrg.ContractURL);
        hlContractModal.NavigateUrl = fullUrl;
        hlContractModal.Text = outerOrg.ContractModal;
    }
}