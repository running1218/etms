using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Security;


public partial class ActivationPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["uid"]) && !string.IsNullOrEmpty(Request.QueryString["pwd"]))
            {
                try
                {
                    //获得用户ID 与新密码
                    int uid = CrypProvider.DecryptString(Request.QueryString["uid"]).ToInt();
                    string pwd = CrypProvider.DecryptString(Request.QueryString["pwd"]);
                    new UserLogic().ResetPassword(uid, pwd);

                    labMsg.Text = "恭喜你，新密码激活成功！";
                }
                catch
                {
                    labMsg.Text = "密码激活失败，请联系管理员!";
                }
            }
            else {
                labMsg.Text = "密码激活失败，参数有误。";
            }
        }
    }
}