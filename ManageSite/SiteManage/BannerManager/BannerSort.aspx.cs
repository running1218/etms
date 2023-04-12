using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Operation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteManage_BannerManager_BannerSort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bind();
        }
    }

    private void bind()
    {      
        BannerSpreadLogic bannerSpreadLogic = new BannerSpreadLogic();
        DataTable dt = bannerSpreadLogic.GetPageList("","", UserContext.Current.OrganizationID);
        foreach (DataRow row in dt.Rows)
        {
            lbBannerSort.Items.Add(new ListItem() { Value = row["BannerSpreadID"].ToString(), Text = string.Format("{0}",row["SpreadName"]) });
        }
    }
}