using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Controls;
using ETMS.AppContext;
using ETMS.Utility;

public partial class QS_TemplateAdd : System.Web.UI.Page
{
    protected QS_QueryLogic QueryBiz = new QS_QueryLogic();

    protected string PollTypeID
    {
        get { return Request.QueryString["PollTypeID"]; }
    }

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
        IList<QS_Query> templateQuerys = QueryBiz.GetEntityList(pageIndex, 10, "", " and PollTypeID=" + PollTypeID + " and IsTemplate=1 and OrganizationID=" + ETMS.AppContext.UserContext.Current.OrganizationID, out totalRecords);
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

                    Guid queryid = new Guid(this.GridViewList.DataKeys[row.RowIndex].Value.ToString());
                    string newQueryID = QueryBiz.CopyTemplate(queryid, UserContext.Current.RealName, UserContext.Current.UserID).ToString();
                    //通过复制调查问卷模板来创建新问卷
                    //int templateId = this.GridViewList.DataKeys[row.RowIndex].Value.ToInt();
                    //int newQueryID = ResourceQueryLogic.QueryTemplateCopy(templateId, this.ResourceType, this.ResourceCode);
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