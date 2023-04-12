//#warning 本页代码中包含#define DEBUG 定义，正式使用时必须取消
//#define DEBUG

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

using System.Xml;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_QueryResultSave : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.ContentType = "text/xml";
        Response.Charset = "UTF-8";
        string msg = "";
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Request.InputStream);
            PollManager.ViewSubmitResultSave(doc, ETMS.AppContext.UserContext.Current.UserName, "");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            msg = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx);
        }
        finally
        {
            doc = null;
        }
        if (msg.Length == 0)
        {
            Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><Result><IsSuccess>1</IsSuccess><Msg>操作成功！</Msg></Result>");
        }
        else
        {
            Response.Write(string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><Result><IsSuccess>0</IsSuccess><Msg>操作失败！原因：{1}</Msg></Result>", 0, msg));
        }
        Response.End();
    }
}
