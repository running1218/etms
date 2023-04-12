using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using System.Collections;
using ETMS.Components.QS.Implement.BLL;
using System.Text;
using ETMS.Utility;
using ETMS.Components.QS.API.Entity;

public partial class QS_StatDefault : BasePage
{

    #region 所属资源

    public int PollType
    {
        get
        {
            return int.Parse(Request.QueryString["PollTypeID"]);
        }
    }
    QS_QueryLogic QueryBiz = new QS_QueryLogic();
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
        if (this.PollType == 2)
        {
            this.GridViewList.Columns[this.GridViewList.Columns.Count - 2].Visible = false;
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("AND PollTypeID={0} AND OrganizationID={1} and IsPublish=1", PollType, ETMS.AppContext.UserContext.Current.OrganizationID);
        if (!string.IsNullOrEmpty(txtQueryName.Text.Trim()))
        {
            sb.AppendFormat("and QueryName like '%{0}%'", txtQueryName.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtCreateBeginTime.Text))
        {
            sb.AppendFormat(" and CreateTime>='{0}'", txtCreateBeginTime.Text);
        }
        if (!string.IsNullOrEmpty(txtCreateEndTime.Text))
        {
            sb.AppendFormat(" and CreateTime <'{0}'", txtCreateEndTime.Text);
        }
        if (!string.IsNullOrEmpty(txtQueryBeginTime.Text))
        {
            sb.AppendFormat(" and BeginTime>='{0}'", txtQueryBeginTime.Text);
        }
        if (!string.IsNullOrEmpty(txtQueryEndTime.Text))
        {
            sb.AppendFormat(" and EndTime<'{0}'", txtQueryEndTime.Text);
        }
        //载入资源下对应的问卷列表
        var p = QueryBiz.GetEntityList(pageIndex, 10, "", sb.ToString(), out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(p.ToList(), pageIndex, pageSize);
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
            ////获取资源下问卷提交份数
            string queryID = this.GridViewList.DataKeys[e.Row.RowIndex].Value.ToString();
            ////int count = UserResourceQuery.GetAnswerQueryUserCount(queryID, this.ResourceType, this.ResourceCode);
            //HyperLink lblCount = e.Row.FindControl("lblCount") as HyperLink;
            ////lblCount.Text = count.ToString();
            ////lblCount.NavigateUrl = this.ActionHref(string.Format("InvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 0, ResourceType));
            //Label labTotal = e.Row.FindControl("lblTotalCount") as Label;

            //HyperLink lblUnCount = e.Row.FindControl("lblUnCount") as HyperLink;
            //// lblUnCount.NavigateUrl = this.ActionHref(string.Format("UnInvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 1, ResourceType));
            //var a = Convert.ToInt32(labTotal.Text);
            //var b = Convert.ToInt32(lblCount.Text);


            //Label lblComplate = e.Row.FindControl("lblComplate") as Label;

            //if (a >= b && a != 0)
            //{
            //    lblUnCount.Text = (a - b).ToString();
            //    if (b == 0)
            //    {
            //        lblComplate.Text = "0%";
            //    }
            //    else
            //    {
            //        var p = Convert.ToInt32(Math.Round((decimal)b / a, 2) * 100);
            //        lblComplate.Text = p + "%";
            //    }
            //}
            ////获取资源问卷发布对象
            //// Poll_QueryPublishObject resourceQuery = Logic.GetQueryPublishObjectForResource(queryID, this.ResourceType, this.ResourceCode);
            QS_Query queryEntity = new QS_Query();
            queryEntity = QueryBiz.GetById(new Guid(queryID));
            HyperLink hl = e.Row.FindControl("hlFileInfo") as HyperLink;
            hl.Text = queryEntity.FileName;
            if (!string.IsNullOrEmpty(queryEntity.FilePath))
            {
                hl.NavigateUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("PollReport", queryEntity.FilePath);
            }
            else
            {
                hl.Visible = false;
            }

            Label lbl = e.Row.FindControl("lblScore") as Label;
            //lbl.Text = resourceQuery.Score.ToString();

            hl = e.Row.FindControl("hlUpload") as HyperLink;
            hl.NavigateUrl = "javascript:showWindow('上传分析报告','" + this.ActionHref(string.Format("UploadReport.aspx?id={0}", queryID)) + "',650,350)";

            hl = e.Row.FindControl("hlSetScore") as HyperLink;
            if (PollType == 1)//仅满意度调查时显示
            {
                hl.NavigateUrl = "javascript:showWindow('设置综合评分','" + this.ActionHref(string.Format("SetScore.aspx?id={0}", queryID)) + "',650,350)";
            }
            else
            {
                hl.Visible = false;
            }

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
                string queryID = e.CommandArgument.ToString();
                CustomGridView2.PageSize = int.MaxValue - 1;
                int totalCount = 0;
                var queryResultList = new QS_QueryResultLogic().GetPagedList(1, int.MaxValue - 1, "", " and QueryID=" + new Guid(queryID), out totalCount);
                PageDataSourceProvider psp = new PageDataSourceProvider(queryResultList, 1, 10000);

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
            //int queryID = this.CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToInt();
            ////int count = UserResourceQuery.GetAnswerQueryUserCount(queryID, this.ResourceType, this.ResourceCode);
            //HyperLink lblCount = e.Row.FindControl("lblCount") as HyperLink;
            ////lblCount.Text = count.ToString();
            ////lblCount.NavigateUrl = this.ActionHref(string.Format("InvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 0, ResourceType));
            //Label labTotal = e.Row.FindControl("lblTotalCount") as Label;

            //HyperLink lblUnCount = e.Row.FindControl("lblUnCount") as HyperLink;
            ////lblUnCount.NavigateUrl = this.ActionHref(string.Format("UnInvestDetail.aspx?queryid={0}&state={1}&ResourceType={2}", queryID, 1, ResourceType));
            //var a = Convert.ToInt32(labTotal.Text);
            //var b = Convert.ToInt32(lblCount.Text);


            Label lblComplate = e.Row.FindControl("lblComplate") as Label;

            //if (a >= b && a != 0)
            //{
            //    lblUnCount.Text = (a - b).ToString();
            //    if (b == 0)
            //    {
            //        lblComplate.Text = "0%";
            //    }
            //    else
            //    {
            //        var p = Convert.ToInt32(Math.Round((decimal)b / a, 2) * 100);
            //        lblComplate.Text = p + "%";
            //    }
            //}
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

