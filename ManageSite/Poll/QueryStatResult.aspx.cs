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
using System.IO;

using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;

public partial class Poll_ResourceQuery_QueryStatResult : BasePage
{
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();

    #region 资源属性
    public string ResourceType
    {
        get
        {
            return Request.QueryString["ResourceType"];
        }
    }
    public string ResourceCode
    {
        get
        {
            switch (ResourceType)
            {
                case "R1":
                    return "00000000-0000-0000-0000-000000000001";
                case "R2":
                    return "00000000-0000-0000-0000-000000000002";
                default:
                    return Request.QueryString["ResourceCode"];
            }
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //获取时间html片段
            ltContent.Text = PollManager.GetResponseStatResultView(int.Parse(Request.QueryString["QueryID"])
                , this.ResourceType
                , this.ResourceCode
                ).ToString();
        }
        else//导出
        {
            //获取时间html片段
            ltContent.Text = PollManager.GetStaticResultExportView(int.Parse(Request.QueryString["QueryID"])
                , this.ResourceType
                , this.ResourceCode
                ).ToString();              

            //excel 导出
            ETMS.Utility.FileDownLoadUtility.ExportToExcel(string.Format("{0}.xls", DateTime.Now.ToString("yyyy-MM-dd")), this.ltContent);
        }
    }
}

