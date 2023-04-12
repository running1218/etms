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
public partial class Poll_ResourceQuery_Default : BasePage
{
    Poll_QueryPublishObjectLogic Logic = new Poll_QueryPublishObjectLogic();
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();

    #region ������Դ

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

    public Poll_QueryArea CurrentQueryArea
    {
        get
        {
            return (Poll_QueryArea)ViewState["CurrentQueryArea"];
        }
        set
        {
            ViewState["CurrentQueryArea"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, this.getDataSource1);
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Poll_Query bindItem = e.Row.DataItem as Poll_Query;
            HyperLink hy1 = e.Row.FindControl("HyperLink1") as HyperLink;
            hy1.NavigateUrl = this.ActionHref(String.Format("../QuestionManage/QuestionList.aspx?ResourceType={1}&QueryID={0}", new Object[] { bindItem.QueryID, Request.QueryString["ResourceType"] }));

            if (bindItem.IsPublish)
            {
                //modify 2013-3-26 �޸���360����������ɵ���ǵ�תҳ���bug
                Poll_QueryLogic queryLogic = new Poll_QueryLogic();
                if (queryLogic.GetQueryUserResultCount(bindItem.QueryID) > 0)
                {
                    //hy1.Attributes["disabled"] = "disabled";
                    hy1.CssClass = "link_colorGray";
                    hy1.Enabled = false;
                }
            }

            HyperLink hy = e.Row.FindControl("hySetArea") as HyperLink;
            if (hy != null)
            {
                switch (this.Request.QueryString["ResourceType"])
                {
                    case "R1"://����ȵ���
                        hy.NavigateUrl = "javascript:showWindow('���õ��鷶Χ','" + this.ActionHref(String.Format("SetArea_R1.aspx?QueryID={0}", bindItem.QueryID)) + "',650,350)";
                        break;
                    case "R2"://��ѵ�������
                        hy.NavigateUrl = this.ActionHref(String.Format("SetArea_R2.aspx?QueryID={0}", bindItem.QueryID));
                        //hy1.NavigateUrl = this.ActionHref(String.Format("ResourceQueryArea.aspx?QueryID={0}", bindItem.QueryID));
                        //hy.Target = "_blank";
                        break;
                    default:
                        hy.Visible = false;//��������֧�����÷�Χ
                        break;
                }
            }
        }
    }

    protected void UserOpeator_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int queryID = e.CommandArgument.ToInt();
            switch (e.CommandName)
            {
                case "SwitchTemplate":
                    Poll_QueryLogic queryLogic = new Poll_QueryLogic();
                    Poll_Query query = queryLogic.GetById(queryID);
                    query.IsTemplate = !query.IsTemplate;
                    //������������
                    query.OrganizationID = ETMS.AppContext.UserContext.Current.OrganizationID;
                    query.CreateTime = DateTime.Now;
                    query.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                    query.ModifyTime = DateTime.Now;
                    query.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    queryLogic.Save(query);

                    ETMS.Utility.JsUtility.SuccessMessageBox("�����ɹ���");
                    break;
            }
            //ˢ��
            this.PageSet1.DataBind();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        //������Դ�¶�Ӧ���ʾ��б�
        IList<Poll_Query> dataList = Logic.GetQueryListByResource(
            this.ResourceType,
            this.ResourceCode,
            this.txtQueryName.Text.Trim(),
            int.Parse(this.ddlIsStatus.SelectedValue),
            int.Parse(this.ddlIsDiplayResult.SelectedValue),
            int.Parse(this.ddlIsTemplate.SelectedValue),
            int.Parse(this.ddlIsPublish.SelectedValue),
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int[] queryIds = CustomGridView.GetSelectedValues<int>(this.GridViewList);
            if (queryIds.Length < 1)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("��ѡ������ʾ�");
            }
            else
            {
                Poll_QueryLogic queryLogic = new Poll_QueryLogic();
                queryLogic.Remove(queryIds);
                ETMS.Utility.JsUtility.SuccessMessageBox("�ʾ�ɾ���ɹ���");
                //ˢ��
                this.PageSet1.DataBind();
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            int[] queryIds = CustomGridView.GetSelectedValues<int>(this.GridViewList);
            if (queryIds.Length == 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("��ѡ���ʾ���ٷ�����");
            }
            else
            {
                Poll_QueryLogic queryLogic = new Poll_QueryLogic();
                foreach (int queryId in queryIds)
                {
                    Poll_Query query = queryLogic.GetById(queryId);
                    query.IsPublish = true;
                    query.ModifyTime = DateTime.Now;
                    query.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                    queryLogic.Save(query);

                    //1������ҳǩ1��Ĭ�����á�//���»�����Χ
                    this.CurrentQueryArea = QueryAreaLogic.GetResourceQueryArea(queryId, ResourceType, ResourceCode);

                }
                ETMS.Utility.JsUtility.SuccessMessageBox("�ʾ����ɹ���");
                //ˢ��
                this.PageSet1.DataBind();
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}

