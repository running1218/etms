using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Controls;
using ETMS.Utility;
using System.Data;
using ETMS.Utility.Service.FileUpload;

public partial class Information_AfficheManager_Controls_BulletinInfo : System.Web.UI.UserControl
{
    Inf_BulletinLogic Logic = new Inf_BulletinLogic();
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
    public Inf_Bulletin Entity
    {
        get 
        {
            if (ViewState["Entity"] == null)
            {
                ViewState["Entity"] = new Inf_Bulletin();
            }
            return (Inf_Bulletin)ViewState["Entity"];
        }
        set { ViewState["Entity"] = value; }
    }
    /// <summary>
    /// 获取id
    /// </summary>
    protected int ArticleID
    {
        get
        {
            if (ViewState["ArticleID"] == null)
            {
                string obj = Request.QueryString["id"].ToLower();
                ViewState["ArticleID"] = int.Parse(BasePage.UrlParamDecode(Request.QueryString["id"]));
            }
            return (int)ViewState["ArticleID"];
        }
        set
        {
            ViewState["ArticleID"] = value;
        }
    }
    private void Intital()
    {
        this.ddlInfoLevelID.SelectedIndex=0;
        this.dttbBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.dttbEndTime.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");
       
        switch (Op)
        {
            case "add":
                this.imgBulletin.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BulletinImage, "default.png");
                break;
            case "edit":
                Entity = Logic.GetById(ArticleID);
                

                this.txtMainHead.Text = Entity.MainHead;
                //this.txtBrief.Text = Entity.Brief;

                int total=0;
                string critre=string.Format(" and ArticleID={0}",ArticleID);
                DataTable dt = new Inf_BulletinObjectLogic().GetPagedList(1, 10, string.Empty, critre, out total);
                if (dt != null)
                {
                    this.rbnBulletinObjectTypeName.SelectedValue = dt.Rows[0]["BulletinObjectTypeID"].ToString();
                }

                this.ddlInfoLevelID.SelectedValue = Entity.InfoLevelID.ToString();
                this.fckEditor.Text = Entity.ArticleContent;
                this.rblStatus.SelectedValue = Entity.IsUse.ToString();
                this.dttbBeginTime.Text = Entity.BeginDate.ToDate();
                this.dttbEndTime.Text = Entity.EndDate.ToDate();
                this.imgBulletin.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BulletinImage, string.IsNullOrEmpty(Entity.ImageUrl) ? "default.png" : Entity.ImageUrl);
                break;

        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Intital();
            
        }
    }
    protected void InitialControlers()
    {
        Entity.MainHead = this.txtMainHead.Text.Trim();
        //Entity.Brief = this.txtBrief.Text.Trim();
        Entity.BulletinObjectTypeID = this.rbnBulletinObjectTypeName.SelectedValue.ToInt();
        Entity.InfoLevelID = this.ddlInfoLevelID.SelectedValue.ToInt();
        Entity.ArticleContent = this.fckEditor.Text;
        Entity.IsUse = this.rblStatus.SelectedValue.ToInt();
        Entity.BeginDate =this.dttbBeginTime.Text.ToDateTime();
        Entity.EndDate = this.dttbEndTime.Text.ToDateTime();
        Entity.BulletinObjectTypeID = rbnBulletinObjectTypeName.SelectedValue.ToInt();
        //图片
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
        Entity.ImageUrl = fileDefine == null ? Entity.ImageUrl : fileDefine.BizUrl;

    }
    protected void lbnSave_Click(object sender, EventArgs e)
    {
        InitialControlers();
        switch (Op)
        {
            case "add":
                Entity.CreateTime = DateTime.Now;
                Entity.CreateMan = ETMS.AppContext.UserContext.Current.RealName;
                Entity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                Entity.ArticleTypeID = BulletinTypeEnum.Builletin.ToEnumValue();
                Entity.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                try
                {
                    Logic.AddBulletin(Entity);
                    ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("保存成功");
                } 
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                }
                break;
            case "edit":
                Entity.UpdateMan = ETMS.AppContext.UserContext.Current.RealName;
                Entity.UpdateTime = DateTime.Now;
                Logic.SaveBulletin(Entity);
                try
                {
                    ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("保存成功");
                } 
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                }
                break;
        }
    }
}