using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.WebApp.Manage;
using ETMS.Controls;
using System.Data;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Operation;

public partial class SiteManage_BannerManager_BannerList : BasePage
{
    private static BannerSpreadLogic bannerSpreadLogic = new BannerSpreadLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.gvList, new IPageDataSource(this.PageDataSource));
        PageSet1.PageSize = 5;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('Banner管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("BannerAdd.aspx?op={0}", "add"))));
    }
    #region 页面参数
    /// <summary>
    /// 学习资源预览地址
    /// </summary>
    public string StudyingUrl
    {
        get
        {
            //return ConfigurationManager.AppSettings["StudyingUrl"];
            return HttpContext.Current.Request.ApplicationPath;
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }
    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecords)
    {
        var spreadName = this.txt_SpreadName.Text.Trim();
        var publishStatus = Convert.ToInt32(this.ddl_ReleaseStatus.SelectedValue)==-1?"": this.ddl_ReleaseStatus.SelectedValue;
        DataTable dataList = bannerSpreadLogic.GetPageList(spreadName, publishStatus, UserContext.Current.OrganizationID);
        totalRecords = dataList.Rows.Count;
        PageDataSourceProvider psp = new PageDataSourceProvider(dataList, pageIndex, pageSize);
        return psp.PageDataSource;

    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.gvList.IsEmpty)
        {                  
            DataRowView view = (DataRowView)e.Row.DataItem;
            LinkButton lbnModify = (LinkButton)e.Row.FindControl("btnModify");
            //CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");      
            lbnModify.Attributes.Add("onclick", string.Format("javascript:showWindow('Banner管理','{0}',650,500);javascript: return false;", this.ActionHref(string.Format("BannerAdd.aspx?op={0}&BannerSpreadID={1}", "edit", view["BannerSpreadID"]))));
        }
    }
    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Del")
            {
                bannerSpreadLogic.Remove(e.CommandArgument.ToGuid());

            }
            this.PageSet1.DataBind();
            this.upList.Update();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}