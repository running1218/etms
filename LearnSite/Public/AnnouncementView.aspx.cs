using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using System;

namespace ETMS.Studying.Public
{
    public partial class AnnouncementView : System.Web.UI.Page
    {
        private int ArticleID
        {
            get
            {
                int ArticleID = 0;
                if (Request.QueryString["ArticleID"] != null)
                {
                    ArticleID = Request.QueryString["ArticleID"].ToInt();
                }
                return ArticleID;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControlers();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitialControlers()
        {
            Entity = new Inf_BulletinLogic().GetById(ArticleID);
            this.lblArticleContent.Text = Entity.ArticleContent;
        }
   }
}