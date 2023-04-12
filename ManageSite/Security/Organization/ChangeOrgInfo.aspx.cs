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
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_Organization_ChangeOrgInfo : BasePage
{
    private static OrganizationLogic Logic = new OrganizationLogic();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UserControl1.BindFromData(Logic.GetNodeByID(ETMS.AppContext.UserContext.Current.OrganizationID), ViewMode.Edit);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        //btn显示控制  
        this.btnUpdate.Visible = false;
        this.btnUpdate.Visible = true;
        base.OnPreRender(e);
    }
    protected void btn_ClickHandle(object sender, EventArgs e)
    {
        string sOperType = ((Button)sender).CommandName;
        Organization entity = null;
        switch (sOperType)
        {
            case "edit":
                entity = (Organization)this.UserControl1.DomainModel;
                entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                entity.ModifyTime = DateTime.Now;                
                Logic.Save(entity);
                break;
        }
        ETMS.Utility.JsUtility.SuccessMessageBox("提示：", "机构信息修改成功！", "function(){window.location.href=window.location.href;}");//刷新页面
    }
}

