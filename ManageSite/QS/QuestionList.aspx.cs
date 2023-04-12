using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using System.Data;

public partial class QS_QuestionList : System.Web.UI.Page
{
    public QS_QueryTitle queryTitleEntity = new QS_QueryTitle();
    protected QS_QueryTitleLogic queryTitleBiz = new QS_QueryTitleLogic();

    private static QS_QueryLogic QueryBiz = new QS_QueryLogic();
    private static QS_Query queryEntity = new QS_Query();
    protected string QueryID
    {
        get
        {
            return Request.QueryString["QueryID"];
        }
    }

    protected int PollType
    {
        get
        {
            return int.Parse(Request.QueryString["PollTypeID"]);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            queryEntity = QueryBiz.GetById(new Guid(QueryID));
            this.lblQueryName.Text = queryEntity.QueryName;
            this.lblBeginTime.DateTimeValue = queryEntity.BeginTime;
            this.lblEndTime.DateTimeValue = queryEntity.EndTime;

            this.PageSet1.QueryChange();

            //返回按钮
            this.hylReturn.NavigateUrl = this.ActionHref("../qs/Default.aspx?PollTypeID=" + PollType);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //获取选中题列表
            Guid[] questionIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            //删除
            queryTitleBiz.Remove(questionIDs);
            //TitleLogic.Remove(questionIDs);
            //刷新当页数据
            this.PageSet1.DataBind();
            ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(bizEx.Message);
        }

    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        totalRecordCount = 0;

        DataTable dt = queryTitleBiz.GetQueryTitleAllInfoByQueryID(new Guid(QueryID), pageIndex, pageSize, " qt.TitleNo", "", out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

}