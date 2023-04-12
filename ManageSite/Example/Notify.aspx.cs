using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Components.Basic.API.Entity.Notify;
public partial class Example_Notify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string notifyBizCode = Notify_MessageClass.NotifyMessageClass_Demo.MessageClassName;
            NotifyReceiver receiver = new NotifyReceiver()
            {
                UserID = "1",
                LoginName="",
                UserName = "SysAdmin",
                Email = this.txtReceiver.Text,
                MobilePhone = "12343212341",
            };
           
            object context = new { ActiveCode = DateTime.Now.ToString("ssfff") };//设置业务参数，通过匿名类解决
            NotifyUtility.Notify(notifyBizCode, receiver, context);
            JsUtility.SuccessMessageBox("消息成功推送到邮件队列，等待服务发送！");
        }
        catch (Exception ex)
        {
            JsUtility.FailedMessageBox("邮件发送失败！原因：" + ex.Message);
        }
    }
}