using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility.Service;
using ETMS.Components.Basic.API.Entity;
using ETMS.AppContext;
using ETMS.WebApp.Manage;

public partial class Resource_Media_Controls_MediaInfo : System.Web.UI.UserControl
{

    private static readonly Res_MediaLogic res_MediaLogic = new Res_MediaLogic();
    private static Guid defaultGuidValue = new Guid();
    /// <summary>
    /// 操作类型 add  edit
    /// </summary>
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
        }
    }

    //媒体ID
    public Guid MediaID
    {
        get
        {
            if (ViewState["MediaID"] == null)
            {
                ViewState["MediaID"] = BasePage.UrlParamDecode(Request.QueryString["MediaID"]).ToGuid();
            }
            return ViewState["MediaID"].ToGuid();
        }
        set
        {
            ViewState["MediaID"] = value;
        }
    }

    private Res_Media ResMedia
    {
        get
        {
            if (ViewState["ResMedia"] == null)
            {
                ViewState["ResMedia"] = new Res_Media();
            }
            return (Res_Media)ViewState["ResMedia"];
        }
        set
        {
            ViewState["ResMedia"] = value;
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // radlMediaType.SelectedValue = "1";
            fuCoverImage.FunctionType = FileUploadFunctionType.MediaLogo;
             //编辑
            if (Action == OperationAction.Edit)
            {
                InitControl();
            }
            else
            {
                imgCoverLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, "Default.jpg");
            }
        }
    }

    //初始化控件值
    private void InitControl()
    {
        ResMedia = res_MediaLogic.GetById(MediaID);
        txtMediaName.Text = ResMedia.MediaName;
       // radlMediaType.SelectedValue = ResMedia.MediaType.ToString();
        lblState.Text = string.IsNullOrEmpty(ResMedia.MediaPath) ? "<span class='colorRed'>无媒体！</span>" : "<span class='colorGreen'>已有媒体！</span>";
        imgCoverLogo.ImageUrl = string.IsNullOrEmpty(ResMedia.ImagePath) ? StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, "Default.jpg") : StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, ResMedia.ImagePath);
        txtRemark.Text = ResMedia.MediaInstroduce;
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {   
            //增加
        if (MediaID.ToString() == defaultGuidValue.ToString())
        {
            ResMedia.MediaPath = "";
            ResMedia.PlayTime = 0;
            if (MiniUpFile.FileUrl.Count > 0)
            {
                ResMedia.MediaPath = MiniUpFile.FileUrl[0].BizUrl;
                //ResMedia.PlayTime = (int)MediaHelper.GetDuration(string.Format("{0}\\{1}", WebUtility.FileRoot, ResMedia.MediaPath.Replace("/", "\\")));
            }
            ResMedia.MediaType = 1;
            ResMedia.PlayRate = 0;
            ResMedia.CreateTime = DateTime.Now;
            ResMedia.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            ResMedia.IsRecommend = false;
          
        }
        else
        {
            if (MiniUpFile.FileUrl.Count > 0)
            {
                ResMedia.MediaPath = MiniUpFile.FileUrl[0].BizUrl;
                ResMedia.PlayTime = (int)MediaHelper.GetDuration(string.Format("{0}\\{1}", WebUtility.FileRoot, ResMedia.MediaPath.Replace("/", "\\")));
            }
            ResMedia.ModifyTime = DateTime.Now;
            ResMedia.ModifyUserID = ETMS.AppContext.UserContext.Current.UserID;
            
        }
        ResMedia.ImagePath = fuCoverImage.UploadFileEntity().BizUrl ?? ResMedia.ImagePath;
        ResMedia.MediaInstroduce = txtRemark.Text.Trim();
        ResMedia.MediaName = txtMediaName.Text.Trim();        
        res_MediaLogic.Save(ResMedia);

        ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("媒体信息保存成功！");

    }

    protected void linkDownload_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }
    protected void lbtnClose_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Hide();
    }
}