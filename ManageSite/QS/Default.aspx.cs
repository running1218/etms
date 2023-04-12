using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using System.Collections;
using ETMS.Utility;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;
using System.Text;

public partial class QS_Default : BasePage
{
    QS_QueryLogic QueryBiz = new QS_QueryLogic();

    #region 所属资源

    public int PollType
    {
        get
        {
            return int.Parse(Request.QueryString["PollTypeID"]);
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

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            QS_Query bindItem = e.Row.DataItem as QS_Query;

            HyperLink hy = e.Row.FindControl("hySetArea") as HyperLink;

            if (hy != null)
            {
                switch (PollType)
                {
                    case 1://满意度调查
                        hy.NavigateUrl = this.ActionHref(String.Format("ResourceQueryArea.aspx?QueryID={0}", bindItem.QueryID));
                        break;
                    case 2://培训需求调查
                        hy.NavigateUrl = this.ActionHref(String.Format("ResourceQueryArea.aspx?QueryID={0}", bindItem.QueryID));
                        //hy1.NavigateUrl = this.ActionHref(String.Format("ResourceQueryArea.aspx?QueryID={0}", bindItem.QueryID));
                        //hy.Target = "_blank";
                        break;
                    default:
                        hy.Visible = false;//其他类型支持设置范围
                        break;
                }
            }
        }
    }

    protected void UserOpeator_Command(object sender, CommandEventArgs e)
    {
        try
        {
            Guid queryID = e.CommandArgument.ToGuid();
            CustomLinkButton linkBtn = (CustomLinkButton)sender;

            switch (e.CommandName)
            {
                case "SwitchTemplate":
                    if (linkBtn.Text == "设为模板")
                    {
                        QueryBiz.SetTemplate(queryID, ETMS.AppContext.UserContext.Current.RealName);

                    }
                    else
                    {
                        QueryBiz.CancelTemplate(queryID, ETMS.AppContext.UserContext.Current.RealName);
                    }

                    ETMS.Utility.JsUtility.SuccessMessageBox("操作成功！");

                    break;
            }
            //刷新
            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        totalRecords = 0;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("AND PollTypeID={0} AND OrganizationID={1}", PollType, ETMS.AppContext.UserContext.Current.OrganizationID);
        if (!string.IsNullOrEmpty(txtQueryName.Text.Trim()))
        {
            sb.AppendFormat("and QueryName like '%{0}%'", txtQueryName.Text.Trim());
        }
        if (ddlIsDiplayResult.SelectedValue != "-1")
        {
            sb.AppendFormat("and IsDisplayResult={0}", ddlIsDiplayResult.SelectedValue);
        }
        if (ddlIsTemplate.SelectedValue != "-1")
        {
            sb.AppendFormat("and IsTemplate={0}", ddlIsTemplate.SelectedValue);
        }
        if (ddlIsStatus.SelectedValue != "-1")
        {
            sb.AppendFormat(" and QueryStatus={0}", ddlIsStatus.SelectedValue);
        }
        if (ddlIsPublish.SelectedValue != "-1")
        {
            sb.AppendFormat(" and IsPublish={0}", ddlIsPublish.SelectedValue);
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
        IList<QS_Query> dataList = QueryBiz.GetEntityList(pageIndex, 10, "", sb.ToString(), out totalRecords);

        PageDataSourceProvider psp = new PageDataSourceProvider((IList)dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {

        this.PageSet1.QueryChange();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] queryIds = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
            if (queryIds.Length < 1)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择调查问卷");
            }
            else
            {

                QueryBiz.BatchDelete(queryIds);
                ETMS.Utility.JsUtility.SuccessMessageBox("问卷删除成功！");
                //刷新
                this.PageSet1.DataBind();
            }

        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ex.Message);
        }
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] queryIds = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
            if (queryIds.Length == 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请选择问卷后再发布！");
            }
            else
            {
                QueryBiz.BatchIssue(queryIds, 1, ETMS.AppContext.UserContext.Current.RealName);
                ETMS.Utility.JsUtility.SuccessMessageBox("问卷发布成功！");
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(bizEx.Message);
        }
        finally
        {
            this.PageSet1.DataBind();
        }
    }
}

