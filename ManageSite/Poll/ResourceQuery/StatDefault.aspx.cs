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

using System.Collections.Generic;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_ResourceQuery_StatDefault : BasePage
{
    Poll_QueryPublishObjectLogic Logic = new Poll_QueryPublishObjectLogic();
    Poll_UserResourceQueryResultLogic UserResourceQuery = new Poll_UserResourceQueryResultLogic();
    #region 所属资源

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
        this.PageSet1.pageInit(this.GridViewList, new ETMS.Controls.IPageDataSource(this.getDataSource1));

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();

        }
    }


    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        //非满意度调查时，屏蔽显示列“综合分数”
        if (this.ResourceType != "R1")
        {
            this.GridViewList.Columns[this.GridViewList.Columns.Count - 2].Visible = false;
        }

        //载入资源下对应的问卷列表
        IList<Poll_Query> dataList = Logic.GetQueryListByResource(
            this.ResourceType,
            this.ResourceCode,
            this.txtQueryName.Text,
            1,
            -1,
            -1,
            1,
            this.txtCreateBeginTime.DateTimeValue,
            this.txtCreateEndTime.DateTimeValue,
            this.txtQueryBeginTime.DateTimeValue,
            this.txtQueryEndTime.DateTimeValue,
            pageIndex,
            pageSize,
            out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider((IList)dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.GridViewList.IsEmpty)
            {
                return;
            }
            //获取资源下问卷提交份数
            int queryID = this.GridViewList.DataKeys[e.Row.RowIndex].Value.ToInt();

            //调查总人数
            HyperLink lblTotalCount = e.Row.FindControl("lblTotalCount") as HyperLink;
            if (lblTotalCount != null)
            {
                lblTotalCount.NavigateUrl = this.ActionHref(string.Format("QueryUserAllList.aspx?ResourceType={1}&QueryID={0}", queryID, ResourceType));
            }

            //int count = UserResourceQuery.GetAnswerQueryUserCount(queryID, this.ResourceType, this.ResourceCode);
            HyperLink lblCount = e.Row.FindControl("lblCount") as HyperLink;
            //lblCount.Text = count.ToString();
            lblCount.NavigateUrl = this.ActionHref(string.Format("InvestDetail.aspx?ResourceType={2}&queryid={0}&state={1}", queryID, 0, ResourceType));

            HyperLink lblUnCount = e.Row.FindControl("lblUnCount") as HyperLink;
            lblUnCount.NavigateUrl = this.ActionHref(string.Format("UnInvestDetail.aspx?ResourceType={2}&queryid={0}&state={1}", queryID, 1, ResourceType));
            var a = Convert.ToInt32(lblTotalCount.Text);
            var b = Convert.ToInt32(lblCount.Text);

            Label lblComplate = e.Row.FindControl("lblComplate") as Label;

            if (a >= b && a != 0)
            {
                lblUnCount.Text = (a - b).ToString();
                if (b == 0)
                {
                    lblComplate.Text = "0%";
                }
                else
                {
                    var p = Convert.ToInt32(Math.Round((decimal)b / a, 2) * 100);
                    lblComplate.Text = p + "%";
                }
            }
            //获取资源问卷发布对象
            Poll_QueryPublishObject resourceQuery = Logic.GetQueryPublishObjectForResource(queryID, this.ResourceType, this.ResourceCode);
            HyperLink hl = e.Row.FindControl("hlFileInfo") as HyperLink;
            hl.Text = resourceQuery.FileName;
            if (!string.IsNullOrEmpty(resourceQuery.FilePath))
            {
                hl.NavigateUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("PollReport", resourceQuery.FilePath);
            }
            else
            {
                hl.Visible = false;
            }

            //Label lbl = e.Row.FindControl("lblScore") as Label;
            //lbl.Text = resourceQuery.Score.ToString();

            hl = e.Row.FindControl("hlUpload") as HyperLink;
            hl.NavigateUrl = "javascript:showWindow('上传分析报告','" + this.ActionHref(string.Format("UploadReport.aspx?id={0}", resourceQuery.QueryPublishID)) + "',650,350)";

            //hl = e.Row.FindControl("hlSetScore") as HyperLink;
            //if (this.ResourceType == "R1")//仅满意度调查时显示
            //{
            //    hl.NavigateUrl = "javascript:showWindow('设置综合评分','" + this.ActionHref(string.Format("SetScore.aspx?id={0}", resourceQuery.QueryPublishID)) + "',650,350)";
            //}
            //else
            //{
            //    hl.Visible = false;
            //}

        }
    }


    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //启用与停用
        if (e.CommandName == "Import")
        {
            try
            {
                int querid = int.Parse(e.CommandArgument.ToString());
                CustomGridView2.PageSize = int.MaxValue - 1;
                PageDataSourceProvider psp = new PageDataSourceProvider(new Poll_UserResourceQueryResultLogic().GetSubmitTotalUserByQuerID(querid, ResourceType), 1, int.MaxValue - 1);

                this.CustomGridView2.DataSource = psp.PageDataSource;
                this.CustomGridView2.DataBind();
                ETMS.Utility.FileDownLoadUtility.ExportToExcel("调查试卷的学生信息.xls", this.CustomGridView2);
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }

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
    protected void GridViewList_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.GridViewList.IsEmpty)
            {
                return;
            }
            //获取资源下问卷提交份数
            int queryID = this.CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToInt();

            //调查总人数
            HyperLink lblTotalCount = e.Row.FindControl("lblTotalCount") as HyperLink;
            if (lblTotalCount != null)
            {
                lblTotalCount.NavigateUrl = this.ActionHref(string.Format("QueryUserAllList.aspx?QueryID={0}", queryID));
            }
            //int count = UserResourceQuery.GetAnswerQueryUserCount(queryID, this.ResourceType, this.ResourceCode);
            HyperLink lblCount = e.Row.FindControl("lblCount") as HyperLink;
            //lblCount.Text = count.ToString();
            lblCount.NavigateUrl = this.ActionHref(string.Format("InvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 0, ResourceType));

            HyperLink lblUnCount = e.Row.FindControl("lblUnCount") as HyperLink;
            lblUnCount.NavigateUrl = this.ActionHref(string.Format("UnInvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 1, ResourceType));
            var a = Convert.ToInt32(lblTotalCount.Text);
            var b = Convert.ToInt32(lblCount.Text);


            Label lblComplate = e.Row.FindControl("lblComplate") as Label;

            if (a >= b && a != 0)
            {
                lblUnCount.Text = (a - b).ToString();
                if (b == 0)
                {
                    lblComplate.Text = "0%";
                }
                else
                {
                    var p = Convert.ToInt32(Math.Round((decimal)b / a, 2) * 100);
                    lblComplate.Text = p + "%";
                }
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecords = 0;
        CustomGridView1.PageSize = int.MaxValue - 1;
        this.CustomGridView1.DataSource = this.getDataSource1(1, int.MaxValue - 1, out totalRecords);
        this.CustomGridView1.DataBind();
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("培训需求调查信息.xls", this.CustomGridView1);
    }


    protected void btnExportDetail_Click(object sender, EventArgs e)
    {
        //lb_Click(sender, e);
    }

    public string GetSubmitState(object createTime)
    {
        if (string.IsNullOrEmpty(createTime.ToString()))
        {
            return "未提交";
        }
        else return "已提交";
    }
}

