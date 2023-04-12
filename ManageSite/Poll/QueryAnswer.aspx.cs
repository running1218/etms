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
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;

public partial class Poll_ResourceQuery_QueryAnswer : BasePage
{
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        //��ȡʱ��htmlƬ��
        ltContent.Text = PollManager.GetResponseView(
              int.Parse(Request.QueryString["QueryID"])
            , ETMS.AppContext.UserContext.Current.UserName
            , ""
            , Request.QueryString["ResourceType"]
            , Request.QueryString["ResourceCode"]
            ).ToString();
    }
}

