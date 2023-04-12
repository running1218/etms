using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Utility;

public partial class Information_AfficheManager_AfficheView : ETMS.Controls.BasePage
{
    private static Inf_BulletinLogic bulletinLogic = new Inf_BulletinLogic();
    private static PublicFacade publicFacade = new PublicFacade();
    /// <summary>
    /// 获取公告编码
    /// </summary>
    public int ArticleID
    {
        get
        {
            if (ViewState["ArticleID"] == null)
            {
                ViewState["ArticleID"] = Request.QueryString["ArticleID"].ToInt();
            }
            return (int)ViewState["ArticleID"];
        }
        set
        {
            ViewState["ArticleID"] = value;
        }
    }
    /// <summary>
    /// 获取公告实例
    /// </summary>
    public Inf_Bulletin Entity
    {
        get
        {
            if (ViewState["Bulletin"] == null)
            {
                ViewState["Bulletin"] = new Inf_Bulletin();
            }
            return (Inf_Bulletin)ViewState["Bulletin"];
        }
        set
        {
            ViewState["Bulletin"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }
    private void Initial()
    {
        Entity = bulletinLogic.GetById(ArticleID);
        this.ltlArticleContent.Text = Entity.ArticleContent;
        this.ltlBrief.Text = Entity.Brief;
        if (string.IsNullOrEmpty(this.ltlBrief.Text))
            cltlKeywords.Visible = false;
        this.ltlMainHead.Text = Entity.MainHead;
        this.ltlOrg.Text = "发布机构：" + publicFacade.GetOrgNameByID(Entity.OrgID);
        this.ltlCreateTime.Text = "发布时间：" + Entity.CreateTime;
    }

}