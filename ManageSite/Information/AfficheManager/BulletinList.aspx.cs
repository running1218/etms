using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using System.Text;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Bulletin;

public partial class Information_AfficheManager_BulletinList : ETMS.Controls.BasePage
{
    Inf_BulletinLogic Logic = new Inf_BulletinLogic();
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
        StringBuilder strQuery=new StringBuilder();
        strQuery.Append(BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        strQuery.Append(string.Format(" and OrgID={0} and ArticleTypeID={1}", ETMS.AppContext.UserContext.Current.OrganizationID.ToString(), BulletinTypeEnum.Builletin.ToEnumValue()));

        DataTable dataList = Logic.GetPagedList(pageIndex, pageSize, " CreateTime desc", strQuery.ToString(), out totalRecords);
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }

    protected void GridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                try
                {
                    int articleID = e.CommandArgument.ToInt();
                    Logic.Remove(articleID);
                    this.PageSet1.DataBind();
                    
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
                break;
            case "IsUse":
                try
                {
                    string[] parm = e.CommandArgument.ToString().Split(',');
                    int articleID = int.Parse(parm[1]);
                    int isUse = int.Parse(parm[0]) == 1 ? 0 : 1;
                    Logic.SetBulletinIsUse(articleID, isUse);
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
                break;
        }
    }
}