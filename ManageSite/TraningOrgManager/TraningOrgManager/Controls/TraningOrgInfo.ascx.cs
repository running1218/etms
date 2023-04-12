using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Utility.Service.FileUpload;


public partial class TraningOrgManager_TraningOrgManager_Controls_TraningOrgInfo : System.Web.UI.UserControl
{
    public static Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();
    #region 页面条件参数存放

    /// <summary>
    /// 保存是添加还是修改
    /// </summary>
    protected string Op
    {
        get
        {
            if (ViewState["Op"] == null)
            {
                ViewState["Op"] = BasePage.UrlParamDecode(Request.QueryString["op"].ToLower());
            }
            return (string)ViewState["Op"];
        }
        set
        {
            ViewState["Op"] = value;
        }
    }
    /// <summary>
    /// 机构实例
    /// </summary>
    public Tr_OuterOrg Entity
    {
        get
        {
            if (ViewState["Entity"] == null)
            {
                ViewState["Entity"] = new Tr_OuterOrg();
            }
            return (Tr_OuterOrg)ViewState["Entity"];
        }
        set { ViewState["Entity"] = value; }
    }

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


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }

    private void Initial()
    {
        this.Dic_Status.SelectedIndex = 0;
        switch (Op)
        {
            case "edit":
                Entity = outerOrgLogic.GetById(OuterOrgID);
                this.txtBestClass.Text = Entity.BestCourse;
                this.txtEMAIL.Text = Entity.EMAIL;
                this.txtHistoryCooperation.Text = Entity.HistoryCooperation;
                this.txtLinkMan.Text = Entity.LinkMan;
                this.txtLinkMode.Text = Entity.LinkMode;
                this.txtOrgAssess.Text = Entity.OrgAssess;
                this.txtOuterOrgCode.Text = Entity.OuterOrgCode;
                //this.txtOuterOrgCode.Enabled = false;
                this.txtOuterOrgName.Text = Entity.OuterOrgName;
                this.txtServiceContent.Text = Entity.ServiceContent;
                this.Dic_Status.SelectedValue = Entity.OuterOrgStatus.ToString();
                string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", this.Entity.ContractURL);
                this.lblFilePath.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ", fullUrl, this.Entity.ContractModal);
                this.txtAddress.Text = Entity.OuterOrgAddr;
                this.txtHttp.Text = Entity.OuterOrgURL;
                this.txtRemark.Text = Entity.Remark;
                break;
        }
    }
    private void InitialControlers()
    {       
        Entity.BestCourse = this.txtBestClass.Text;
        Entity.EMAIL = this.txtEMAIL.Text;
        Entity.HistoryCooperation = this.txtHistoryCooperation.Text;
        Entity.LinkMan = this.txtLinkMan.Text;
        Entity.LinkMode = this.txtLinkMode.Text;
        Entity.OrgAssess = this.txtOrgAssess.Text;
        Entity.OuterOrgCode=this.txtOuterOrgCode.Text.Trim();        
        Entity.OuterOrgName = this.txtOuterOrgName.Text;
        Entity.ServiceContent = this.txtServiceContent.Text;
        Entity.OuterOrgStatus = this.Dic_Status.SelectedValue.ToInt();

        BasePage basePage = this.Page as BasePage;
        FileUploadCard entitypath = basePage.SaveUploadFiles("OfflineJob");
        if (entitypath.Files.Count > 0)
        {
            string relUrl = entitypath.FileDetails[0].BizUrl;
            string fileName = entitypath.FileDetails[0].FileName;
            //string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", relUrl);
            Entity.ContractModal = fileName;//离线作业附件路径
            Entity.ContractURL = relUrl;
        }

        Entity.OuterOrgURL = this.txtHttp.Text;
        Entity.OuterOrgAddr = this.txtAddress.Text;
        Entity.Remark = this.txtRemark.Text;
    }
    protected void lbnSave_Click(object sender, EventArgs e)
    {
        try
        {
            switch (Op)
            {
                case "add":
                    Entity = new Tr_OuterOrg();
                    InitialControlers();
                    Entity.CreateTime = DateTime.Now;
                    Entity.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                    Entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                    Entity.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                    //Entity.OuterOrgCode = this.txtOuterOrgCode.Text.Trim();
                    Entity.OuterOrgStatus = 1;
                    outerOrgLogic.Save(Entity);
                    break;
                case "edit":
                    InitialControlers();
                    Entity.ModifyTime = DateTime.Now;
                    Entity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    outerOrgLogic.Save(Entity);
                    break;

            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ex.Message);
        }
    }
}