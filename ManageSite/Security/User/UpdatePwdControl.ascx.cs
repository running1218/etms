using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Security_User_UpdatePwdControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnReturn.OnClientClick = "window.location='" + this.ActionHref("~/home.aspx") + "'; return false;";

        }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            //获得用户ID 与新密码
            int uid = ETMS.AppContext.UserContext.Current.UserID;
            ETMS.Components.Basic.API.Entity.Security.User user = new UserLogic().GetUserByID(uid);
            if (user.PassWord.Equals(ETMS.Utility.Cryptography.MD5Utility.MD516(txtOldPassWord.Text)))
            {
                //更新密码
                new UserLogic().ResetPassword(uid, txtPassWord.Text);

                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "密码修改成功！", "function(){window.location='" + this.ActionHref("~/home.aspx") + "'}");
            }
            else {
                ETMS.Utility.JsUtility.AlertMessageBox("提示", "原密码错误，请重新输入！");
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)//
        {
            //显示错误信息
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}