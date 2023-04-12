using System;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Poll.Implement.BLL;
using System.Data;
using ETMS.Components.Poll.API.Entity;
using System.Web.UI;

public partial class Poll_ResourceQuery_QueryUserAllList : BasePage
{
    #region 页面参数
    public string ResourceType
    {
        get
        {
            if (ViewState["ResourceType"] == null)
            {
                ViewState["ResourceType"] = "";
            }

            return ViewState["ResourceType"].ToString();
        }
        set
        {
            ViewState["ResourceType"] = value;
        }
    }
    private int QueryID
    {
        get
        {
            if (ViewState["QueryID"] == null)
            {
                ViewState["QueryID"] = 0;
            }
            return ViewState["QueryID"].ToInt();
        }
        set
        {
            ViewState["QueryID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "QueryID")))
            {
                QueryID = BasePage.getSafeRequest(this.Page, "QueryID").ToInt();
            }
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ResourceType")))
            {
                ResourceType = BasePage.getSafeRequest(this.Page, "ResourceType");
            }
            bind();
            this.PageSet1.QueryChange();
        }
        this.hylReturn.NavigateUrl = this.ActionHref("StatDefault.aspx?ResourceType=" + this.ResourceType);
    }

    /// <summary>
    /// 邦定基本信息
    /// </summary>
    private void bind()
    {
        Poll_QueryLogic queryLogic = new Poll_QueryLogic();
        Poll_Query query = queryLogic.GetById(QueryID);

        lbl_QueryName.Text = query.QueryName;
        lbl_QueryDate.Text = string.Format("{0}至{1}", query.BeginTime.ToDate(), query.EndTime.ToDate());
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
        DataTable dt = QueryAreaDetailLogic.GetQueryAreaOrQueryAreaDetailPagedList(QueryID, ResourceType, pageIndex, pageSize, "OrganizationID,DepartmentID,PostID", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 导出
    /// </summary>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecords = 0;
        CustomGridView2.PageSize = int.MaxValue-1;
        this.CustomGridView2.DataSource = this.PageDataSource(1, int.MaxValue-1, out totalRecords);
        this.CustomGridView2.DataBind();
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("调查试卷的学生信息.xls", this.CustomGridView2);
    }


    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridView2.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
}