using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.AppContext.Component;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
public partial class Admin_Dic_Post_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.UserControl1.BindFromData(new object(), ViewMode.Manage);
    }
}

