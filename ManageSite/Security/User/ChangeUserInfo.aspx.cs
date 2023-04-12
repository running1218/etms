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

using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Admin_Site_User_View : BasePage
{
    private static UserLogic Logic = new UserLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Button2.OnClientClick = "window.location='" + this.ActionHref("~/home.aspx") + "'; return false;";
            ETMS.Components.Basic.API.Entity.Security.User userInfo = Logic.GetUserByID(ETMS.AppContext.UserContext.Current.UserID);

            this.UserControl2.BindFromData(userInfo, ViewMode.Edit);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            User entity = (User)this.UserControl2.DomainModel;
            Logic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "个人信息修改成功！", "function(){window.location='" + this.ActionHref("~/home.aspx") + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)//
        {
            //显示错误信息
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}

