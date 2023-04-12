using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Operation;
using ETMS.Components.Basic.Implement.BLL.Operation;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteManage_Controls_BannerInfo : System.Web.UI.UserControl
{
    private BannerSpreadLogic Logic = new BannerSpreadLogic();
    private BannerSpread entity = new BannerSpread();
    /// <summary>
    /// 保存是添加还是修改
    /// </summary>
    protected string Op
    {
        get
        {
            if (ViewState["Op"] == null)
            {
                string temp = Request.QueryString["op"].ToLower();
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
    /// 作业id
    /// </summary>
    protected Guid BannerSpreadID
    {
        get
        {
            if (ViewState["BannerSpreadID"] == null)
            {
                string obj = Request.QueryString["BannerSpreadID"].ToLower();
                ViewState["BannerSpreadID"] = new Guid(BasePage.UrlParamDecode(Request.QueryString["BannerSpreadID"].ToLower()));
            }
            return (Guid)ViewState["BannerSpreadID"];
        }
        set
        {
            ViewState["BannerSpreadID"] = value;
        }
    }
    /// <summary>
    /// 图片ID
    /// </summary>
    public string ImageID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {      
            Intital();
        }
    }

    private void Intital()
    {
       
        switch (Op)
        {
            case "edit":
                entity = Logic.GetByID(BannerSpreadID);
                this.txtSpreadName.Text = this.entity.SpreadName;
                this.txtKeyWords.Text = this.entity.KeyWords;
                this.imgPC.ImageUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("BannerImage", entity.PCImagePath == null ? "" : entity.PCImagePath);
                this.imgMobile.ImageUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("BannerImage", entity.MobileImagePath == null ? "" : entity.MobileImagePath);
                this.txtPCSpreadLink.Text = entity.SpreadPCLink;
                this.txtMobileSpreadLink.Text = entity.SpreadMobileLink;              
                this.rbPubStatus.SelectedValue = entity.ReleaseStatus.ToString();
                break;
            default:
                break;
        }

    }

    protected void lbnSave_Click(object sender, EventArgs e)
    {
        switch (Op)
        {
            case "add":
                Add();
                break;
            case "edit":
                Edit();
                break;

        }
    }

    protected void Add()
    {
        entity.BannerSpreadID = Guid.NewGuid();
        if (!String.IsNullOrEmpty(this.txtSpreadName.Text))
            entity.SpreadName = this.txtSpreadName.Text;
        entity.KeyWords = this.txtKeyWords.Text;
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
        List<FileUploadInfo> uploaders2 = this.uploader2.FileUrl;      
        FileUploadInfo fileDefine2 = uploaders2.Count > 0 ? this.uploader2.FileUrl[0] : null;
        entity.PCImageName = fileDefine == null ? "" : fileDefine.FileName;
        entity.PCImagePath = fileDefine == null ? entity.PCImagePath : fileDefine.BizUrl;
        entity.MobileImageName = fileDefine2 == null ? "" : fileDefine2.FileName;
        entity.MobileImagePath = fileDefine2 == null ? entity.MobileImagePath : fileDefine2.BizUrl;
        entity.SpreadPCLink  =this.txtPCSpreadLink.Text;
          entity.SpreadMobileLink=this.txtMobileSpreadLink.Text;
        entity.Order = Logic.GetMaxOrderValue()+1;       
          entity.ReleaseStatus= Convert.ToInt16(this.rbPubStatus.SelectedValue);
        entity.CreateTime = System.DateTime.Now;
        entity.Creator = ETMS.AppContext.UserContext.Current.UserID;
        entity.OrgID = UserContext.Current.OrganizationID;
      
        try
        {
            Logic.Insert(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }

    }
    protected void Edit()
    {
        entity = Logic.GetByID(BannerSpreadID);
        if (!String.IsNullOrEmpty(this.txtSpreadName.Text))
            entity.SpreadName = this.txtSpreadName.Text;
        entity.KeyWords = this.txtKeyWords.Text;
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
        List<FileUploadInfo> uploaders2 = this.uploader2.FileUrl;   
        FileUploadInfo fileDefine2 = uploaders2.Count > 0 ? this.uploader2.FileUrl[0] : null;
        entity.PCImageName = fileDefine == null ? entity.PCImageName : fileDefine.FileName;
        entity.PCImagePath = fileDefine == null ? entity.PCImagePath : fileDefine.BizUrl;
        entity.MobileImageName = fileDefine2 == null ? entity.MobileImageName : fileDefine2.FileName;
        entity.MobileImagePath = fileDefine2 == null ? entity.MobileImagePath : fileDefine2.BizUrl;
        entity.SpreadPCLink = this.txtPCSpreadLink.Text;
        entity.SpreadMobileLink = this.txtMobileSpreadLink.Text;   
        entity.ReleaseStatus = Convert.ToInt16(this.rbPubStatus.SelectedValue);
        entity.ModifyTime = System.DateTime.Now;
        entity.Modifier = ETMS.AppContext.UserContext.Current.UserID;
        try
        {
            Logic.Update(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

   
}