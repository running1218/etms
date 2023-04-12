using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Poll.Implement.BLL;

public partial class Poll_ResourceQuery_QueryAnswerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ltContent.Text = PollManager.GetResponseViewPreView(int.Parse(Request.Params["queryID"]), int.Parse(Request.Params["batchID"])).ToString();
        ltContent.Text = PollManager.CreateResltListXMLByQueryID(int.Parse(Request.Params["queryID"]),"").ToString();
    }
}