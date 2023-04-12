using ETMS.Components.Basic.Implement.BLL.Operation;
using System;

namespace ETMS.Studying
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            BannerSpreadLogic bannerSpreadLogic = new BannerSpreadLogic();
            this.rpList.DataSource=bannerSpreadLogic.GetBannerList(BaseUtility.SiteOrganizationID);
            this.rpList.DataBind();
        }
    }
}