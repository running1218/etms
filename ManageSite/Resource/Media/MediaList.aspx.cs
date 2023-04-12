using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Controls;
using System.Data.SqlClient;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.WebApp.Manage;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;
using ETMS.Components.Basic.API.Entity;
using ETMS.Utility.Service.FileUpload;

public partial class Resource_Media_MediaList : BasePage
{
    private static readonly Res_MediaLogic res_MediaLogic = new Res_MediaLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    #region 页面参数
    /// <summary>
    /// 查询条件 
    /// </summary>
    private string criteria;
    protected string Crieria
    {
        get
        {
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    private string sortExpression;
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " CreateTime DESC ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
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
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);

        DataTable dt = res_MediaLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["ImagePath"] = string.IsNullOrEmpty(dt.Rows[i]["ImagePath"].ToString()) ? StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, "Default.jpg") : StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, dt.Rows[i]["ImagePath"].ToString());
        }
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
       
    }
       

    public string SecondsStr(int Seconds)
    {
        return Seconds.formatSeconds();
    }

  
    /// <summary>
    /// 行命令
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void gvList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Recommend":
                Res_Media res_Media=res_MediaLogic.GetById(e.CommandArgument.ToGuid());
                res_Media.IsRecommend = !res_Media.IsRecommend;
                res_Media.RecommendTime = DateTime.Now;
                res_MediaLogic.Save(res_Media);
                if (res_Media.IsRecommend)
                {
                    PageSet1.QueryChange();
                    JsUtility.SuccessMessageBox("推荐成功");
                }
                else
                {
                    PageSet1.QueryChange();
                    JsUtility.SuccessMessageBox("取消推荐成功");
                }
                break;          
            case "Del":
                try
                {
                    res_MediaLogic.Remove(e.CommandArgument.ToGuid());
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                }
                break;
           
        }
    }
}