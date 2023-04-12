using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Controls;
using System.Data;
using System.Data.SqlClient;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Utility;
using ETMS.AppContext;

public partial class Resource_ElearningMap_Controls_MapCourseList : System.Web.UI.UserControl
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

    protected void Page_Load(object sender, EventArgs e)
    {

        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();

        }
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Res_StudyMapReferCourseLogic studyMapRefLogic = new Res_StudyMapReferCourseLogic();

        DataTable dt = studyMapRefLogic.GetCourseListByStudyMapID(StudyMapID,pageIndex, pageSize, SortExpression, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

}