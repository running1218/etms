using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility;
using System.Collections;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_ResourceQuery_TemplateAdd : System.Web.UI.Page
{
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    #region 页面条件参数存放

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
        this.PageSet1.pageInit(this.GridViewList, getDataSource1);
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        IList<Poll_Query> templateQuerys = ResourceQueryLogic.GetQueryListByResource(
            this.ResourceType, this.ResourceCode, null,
            1, -1, 1, -1, DateTime.MinValue, DateTime.MinValue,
            DateTime.MinValue, DateTime.MinValue, 1, 999, out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider((IList)templateQuerys, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridViewList.Rows)
            {
                RadioButton rbt = (RadioButton)row.FindControl("rbIsTemplateSelected");
                if (rbt != null && rbt.Checked)
                {
                    //通过复制调查问卷模板来创建新问卷
                    int templateId = this.GridViewList.DataKeys[row.RowIndex].Value.ToInt();
                    int newQueryID = ResourceQueryLogic.QueryTemplateCopy(templateId, this.ResourceType, this.ResourceCode);
                    string url = this.ActionHref(string.Format("view.aspx?op=edit&id={0}", newQueryID));
                    ETMS.Utility.JsUtility.SuccessMessageBox("提示", "问卷复制成功，点击“确定”完成相关信息修改！", "function(){window.location.href='" + url + "'}");
                    return;
                }
            }
            ETMS.Utility.JsUtility.AlertMessageBox("请选择模板！");
            return;


        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }


    }
}