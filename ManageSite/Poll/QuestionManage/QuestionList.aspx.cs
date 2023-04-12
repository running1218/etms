using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Questionnaire_QuestionManage_QuestionList : System.Web.UI.Page
{
    private static Poll_TitleLogic TitleLogic = new Poll_TitleLogic();
    private static Poll_ColumnLogic ColumnLogic = new Poll_ColumnLogic();
    protected string QueryID
    {
        get
        {
            return Request.QueryString["QueryID"];
        }
    }
    public string ResourceType
    {
        get
        {
            return Request.QueryString["ResourceType"];
        }
    }
    protected string ColumnID
    {
        get
        {
            return (string)ViewState["ColumnID"];
        }
        set
        {
            ViewState["ColumnID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            Poll_Query query = new Poll_QueryLogic().GetById(int.Parse(this.QueryID));
            this.lblQueryName.Text = query.QueryName;
            this.lblBeginTime.DateTimeValue = query.BeginTime;
            this.lblEndTime.DateTimeValue = query.EndTime;

            //取问卷下的默认栏目
            int totalRecords;
            ColumnID = ColumnLogic.GetEntityList(1, 1, "", string.Format(" AND QueryID={0}", QueryID), out totalRecords)[0].ColumnID.ToString();
            this.PageSet1.QueryChange();

            //返回按钮
            this.hylReturn.NavigateUrl = this.ActionHref("../ResourceQuery/Default.aspx?ResourceType=" + this.ResourceType);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //获取选中题列表
            int[] questionIDs = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
            //删除
            TitleLogic.Remove(questionIDs);
            //刷新当页数据
            this.PageSet1.DataBind();
            ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        totalRecordCount = 0;

        DataTable dt = TitleLogic.GetPagedList(pageIndex, pageSize, " TitleNo asc", string.Format(" AND ColumnID={0}", this.ColumnID), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected Poll_UserResourceQueryResultLogic userResourceQueryResultLogic = new Poll_UserResourceQueryResultLogic();
    protected bool isJoinQuery()
    {
        int total=0;

        //DataTable dt=  userResourceQueryResultLogic.GetPagedList(1, int.MaxValue - 1, "", string.Format(" and QueryID={0}",QueryID), out total);
        //if (dt.Rows.Count > 0)

        //huangzf 修改2013-01-16
        DataTable dt=  userResourceQueryResultLogic.GetPagedList(1, 1, "", string.Format(" and QueryID={0}",QueryID), out total);
        if (total > 0)
            return true;
        else
            return false;
    }
}