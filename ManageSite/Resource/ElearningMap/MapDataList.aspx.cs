using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;

using System.Data;
using System.Data.SqlClient;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Utility;
using ETMS.AppContext;

public partial class Resource_ElearningMap_MapDataList : System.Web.UI.Page
{ 
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

    private static Guid defaultGuidValue = new Guid();

    /// <summary>
    /// 学习地图ID
    /// </summary>
    public Guid StudyMapID
    {
        get
        {
            if (ViewState["StudyMapID"] == null)
            {
                ViewState["StudyMapID"] = defaultGuidValue;
            }
            return (Guid)ViewState["StudyMapID"];
        }
        set
        {
            ViewState["StudyMapID"] = value;
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
                ViewState["SortExpression"] = " CourseCode ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected static readonly Res_StudyMapReferIDPLogic studyMapIDPLogic = new Res_StudyMapReferIDPLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.gvList, PageDataSource);
        
        if (!Page.IsPostBack)
        {            
            ElearningMapInfoView1.StudyMapID = StudyMapID = Request.ToparamValue<Guid>("StudyMapID");
            ElearningMapInfoView1.StudyMapDescVisible = false;
            lbtAdd.PostBackUrl = this.ActionHref(string.Format("~/Resource/ElearningMap/ChooseData.aspx?StudyMapID={0}", StudyMapID));                       
            PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 获取学习地图下的课程列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {        
        var source = studyMapIDPLogic.GetMapDataList(StudyMapID, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 批量删除学习地图与课程的关系
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeletes_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.gvList);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("请选择要删除的资料！");
            return;
        }
        try
        {
            studyMapIDPLogic.Remove(selectedValues);
            JsUtility.SuccessMessageBox(string.Format("成功删除[{0}]条资料信息！", selectedValues.Length));
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
                
        //刷新数据
        this.PageSet1.DataBind();
    }
}