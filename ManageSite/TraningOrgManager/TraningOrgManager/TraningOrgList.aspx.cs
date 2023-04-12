using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using System.Text;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.API.Entity.TraningOrgnization;
using ETMS.Utility;


public partial class TraningOrgManager_TraningOrgManager_TraningOrgList : ETMS.Controls.BasePage
{
    public static Tr_OuterOrgLogic outerOrgLogic=new Tr_OuterOrgLogic();
    public static Tr_OuterOrg outerOrg = new Tr_OuterOrg();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);        

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        StringBuilder strQuery = new StringBuilder();
        if (!string.IsNullOrEmpty(this.txtOuterOrgCode.Text.Trim()))
            strQuery.Append(string.Format(" and OuterOrgCode like '%{0}%'", this.txtOuterOrgCode.Text.Trim().ToSafeSQLValue()));
        if (!string.IsNullOrEmpty(this.Dic_Status.SelectedValue) && this.Dic_Status.SelectedValue.ToInt() >= 0)
            strQuery.Append(string.Format(" and OuterOrgStatus={0}", this.Dic_Status.SelectedValue));
        if (!string.IsNullOrEmpty(this.txtServiceContent.Text.Trim()))
            strQuery.Append(string.Format(" and ServiceContent like '%{0}%'", this.txtServiceContent.Text.Trim().ToSafeSQLValue()));
        if (!string.IsNullOrEmpty(this.txtOuterOrgName.Text.Trim()))
            strQuery.Append(string.Format(" and OuterOrgName like '%{0}%'", this.txtOuterOrgName.Text.Trim().ToSafeSQLValue()));
        strQuery.Append(string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID));

        var source = outerOrgLogic.GetOuterOrgList(pageIndex, pageSize, " CreateTime desc", strQuery.ToString(), out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            //e.Row.Cells[7].Text = "<a href='#'>" + e.Row.Cells[7].Text + "</a>";
            //e.Row.Cells[8].Text = "<a href='#'>" + e.Row.Cells[8].Text + "</a>";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Del":
                    
                    Guid outerOrgID = (e.CommandArgument.ToString()).ToGuid();
                    outerOrgLogic.doRemove(outerOrgID);
                    this.PageSet1.DataBind();
                    upList.Update();
                    break;
                case "IsUse": 
                    string[] parm = e.CommandArgument.ToString().Split(',');                    
                    Guid outerID = parm[1].ToGuid();
                    outerOrg= outerOrgLogic.GetById(outerID);
                    outerOrg.OuterOrgStatus = int.Parse(parm[0]) == 1 ? 0 : 1;
                    outerOrgLogic.Update(outerOrg);
                    this.PageSet1.DataBind();
                    upList.Update();
                    break;
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            //JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}