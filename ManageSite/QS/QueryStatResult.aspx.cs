using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QS_QueryStatResult : System.Web.UI.Page
{




    #region 资源属性

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    //获取时间html片段
        //    ltContent.Text = PollManager.GetResponseStatResultView(int.Parse(Request.QueryString["QueryID"])
        //        , this.ResourceType
        //        , this.ResourceCode
        //        ).ToString();
        //}
        //else//导出
        //{
        //    //获取时间html片段
        //    ltContent.Text = PollManager.GetStaticResultExportView(int.Parse(Request.QueryString["QueryID"])
        //        , this.ResourceType
        //        , this.ResourceCode
        //        ).ToString();

        //    //excel 导出
        //    ETMS.Utility.FileDownLoadUtility.ExportToExcel(string.Format("{0}.xls", DateTime.Now.ToString("yyyy-MM-dd")), this.ltContent);
        //}
    }
}