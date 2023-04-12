using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.WebApp.Manage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resource_CourseLivingResourceOpen : BasePage
{
    private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
    private static readonly Res_ContentLogic contentLogic = new Res_ContentLogic();
    private static readonly Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
    private static readonly OnlinePlayingLogic livingLogic = new OnlinePlayingLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取资源ID
        CourseID = this.Request.ToparamValue<Guid>("CourseID");
        var entity = courseLogic.GetById(CourseID);
        this.lblCourseName.Text = entity.CourseName;
        this.lblLivingType.FieldIDValue = entity.LivingType.ToString();

        PageSet1.pageInit(gvList, PageDataSource);
        
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
      
    }
    #region 页面参数
    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    public int TotalRecordCount
    {
        get
        {
            return ViewState["TotalRecordCount"] == null ? 0 : (int)ViewState["TotalRecordCount"];
        }
        set
        {
            ViewState["TotalRecordCount"] = value;
        }
    }
    /// <summary>
    /// 排序URL
    /// </summary>
    protected string SortUrl
    {
        get
        {
            return this.ActionHref(string.Format("../CourseManage/SetCourseResourceSort.aspx?CourseID={0}", CourseID));
        }
    }

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

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        var data = livingLogic.GetLivingByCourseID(CourseID).PageList<Res_Living>(pageIndex, pageSize, out totalRecordCount);
        var openSource = contentLogic.GetCourseOpenResource(UserContext.Current.OrganizationID, CourseID);
        foreach (var item in data)
        {
            int count = openSource.Count(f => f.LivingID == item.LivingID);
            if (count > 0)
            {
                item.IsOpen = 1;
            }
            else {
                item.IsOpen = 0;
            }
        }

        TotalRecordCount = totalRecordCount;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(data, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    /// <summary>
    /// 删除课程信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                contentLogic.Remove(e.CommandArgument.ToGuid());
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }
    }

    public string GetViewLink(Guid contentID,int playtime,int type) {
        string linktxt = "<a href=\"javascript:void(0);\" style=\"color:#999999\">预览</a>";
        if (playtime > 0) {
            switch (type)
            {
                case 1:
                    linktxt = "<a href=\"javascript:showWindow('预览', '"+this.ActionHref(string.Format("VideoPreview.aspx?ContentID={0}&Type={1}", contentID.ToString(),1))+"', 800, 484)\">预览</a>";
                    break;
                case 2:
                    linktxt = "<a href=\"javascript:showWindow('预览', '" + this.ActionHref(string.Format("VideoPreview.aspx?ContentID={0}&Type={1}", contentID.ToString(),2)) + "', 780, 600)\">预览</a>";
                    break;
                default:
                    break;
            }
        }
        return linktxt;
    }


    protected void gvList_DataBound(object sender, EventArgs e)
    {
       
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ckb = e.Row.FindControl("CheckBox1");
            Res_Living row = e.Row.DataItem as Res_Living;
            if (row.IsOpen == 1)
            {
                ((CheckBox)ckb).Checked = true;
            }
        }
    }

    protected void btnSetting_Click(object sender, EventArgs e)
    {
        //先清除开放资源
        new Res_ContentLogic().RemoveCourseOpenResource(UserContext.Current.OrganizationID, CourseID);
        //重新设置开放资源
        var keys = gvList.AllCheckValues;
        foreach(string key in keys)
        {
            new Res_ContentLogic().SetCourseOpenLiving(UserContext.Current.OrganizationID, CourseID, key, UserContext.Current.UserID);
        }

        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("开放设置成功！");
    }
}