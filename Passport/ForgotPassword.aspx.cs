using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Notify;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 重置密码
    /// </summary>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ETMS.Components.Basic.API.Entity.Security.User user = new UserLogic().GetUserByLoginName(txtUserName.Text.Trim());
        if (user != null)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                JsUtility.FailedMessageBox("提示", "此用户没有邮箱信息，请联系管理员!"); return;
            }

            try
            {
                //接收者变量定义
                NotifyReceiver receiver = new NotifyReceiver()
                {
                    UserID = Convert.ToString(user.UserID),//学员ID
                    LoginName=user.LoginName,
                    Email = user.Email,
                    MobilePhone = user.MobilePhone,
                    UserName = user.RealName,//真实姓名
                };

                string strUrl = string.Format("http://{0}{1}?uid={2}&pwd={3}&random={4}"
                    , Request.Url.Authority
                    , this.ResolveUrl("~/ActivationPassword.aspx")
                    , CrypProvider.EncryptString(user.UserID.ToString())
                    , CrypProvider.EncryptString(txtNewPassword.Text.Trim())
                    , Guid.NewGuid());

                //业务变量定义
                object bizContext = new { ResetPasswordUrl = strUrl};

                //设置当前机构，用户环境信息
                ETMS.AppContext.UserContext.Current.OrganizationID = user.OrganizationID;
                ETMS.AppContext.UserContext.Current.UserID = user.UserID;//默认机构管理员
                NotifyUtility.Notify(ETMS.Components.Basic.API.Entity.Notify.Notify_MessageClass.ResetPasswordNotify.MessageClassName, receiver, bizContext);

                JsUtility.SuccessMessageBox("提示", "操作成功 请登录[" + user.Email + "]激活新密码！", "function(){javascript:closeWindow(null)}");
            }
            catch(Exception ex)
            {
                ETMS.Utility.Logging.ErrorLogHelper.Error(ex);
                JsUtility.FailedMessageBox("提示", "密码重置失败，请联系管理员!");
            }
        }
        else
        {
            JsUtility.AlertMessageBox("提示", "当前用户不存在，请重新输入！");
        }
    }
}