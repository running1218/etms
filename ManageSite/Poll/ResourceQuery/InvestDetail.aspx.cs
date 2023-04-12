using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using System.Collections;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Poll.API.Entity;
using ETMS.Utility;

public partial class Poll_ResourceQuery_InvestDetail : BasePage
{
    Poll_UserResourceQueryResultLogic ursrl = new Poll_UserResourceQueryResultLogic();
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

    protected int QueryID
    {
        get
        {
            return int.Parse(Request.Params["queryid"]);
        }
    }
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new ETMS.Controls.IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            Poll_Query pq = Logic.GetById(QueryID);
            lblQueryName.Text = pq.QueryName;
            lblBeginTime.Text = pq.BeginTime.ToString("yyyy-MM-dd");
            lblEndTime.Text = pq.EndTime.ToString("yyyy-MM-dd");
            this.PageSet1.QueryChange();



            this.hylReturn.NavigateUrl = this.ActionHref("StatDefault.aspx?ResourceType=" + this.ResourceType);
        }
    }

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        //1、载入所选(全部）学员列表
        System.Data.DataTable dt = ursrl.GetEntityListByQueryID(pageIndex, pageSize, "OrganizationID,DepartmentID,PostID", string.Format("and QueryID={0} and ResourceCode='{1}'", QueryID, ResourceCode), out totalRecords);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(GridViewList.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecords = 0;
        CustomGridView1.PageSize = int.MaxValue - 1;
        this.CustomGridView1.DataSource = this.getDataSource1(1, int.MaxValue - 1, out totalRecords);
        this.CustomGridView1.DataBind();
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("调查已提交的学生.xls", this.CustomGridView1);
    }
}