using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;
using ETMS.Utility;
using ETMS.Components.IDP.Implement.BLL.NotCourseData;
using ETMS.Controls;

public partial class IDP_IDPNotCourseDataList : ETMS.Controls.BasePage
{
    private static IDP_NotCourseDataLogic idpNotCourseDataLogic = new IDP_NotCourseDataLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(this.getDataSource1));
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        StringBuilder whereQuery = new StringBuilder();
        whereQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        whereQuery.Append(string.Format(" and OrgID={0}",ETMS.AppContext.UserContext.Current.OrganizationID));
        DataTable dataList = idpNotCourseDataLogic.GetPagedList(pageIndex, pageSize, " CreateTime desc", whereQuery.ToString(), out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    //单个删除课程信息
    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            try
            {
                idpNotCourseDataLogic.doRemove(e.CommandArgument.ToGuid());
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
}