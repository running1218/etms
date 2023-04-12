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

public partial class Resource_ElearningMap_MapCourseList : System.Web.UI.Page
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
        PageSet1.pageInit(this.gvList, PageDataSource);
        
        if (!Page.IsPostBack)
        {
            StudyMapID = new Guid(Request.QueryString["StudyMapID"]);
            ElearningMapInfoView1.StudyMapID = StudyMapID;
            ElearningMapInfoView1.StudyMapDescVisible = false;

            lbtAdd.PostBackUrl = this.ActionHref(string.Format("~/Resource/ElearningMap/ChooseCourse.aspx?StudyMapID={0}", StudyMapID));           
            
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
        Res_StudyMapReferCourseLogic studyMapRefLogic = new Res_StudyMapReferCourseLogic();

        DataTable dt = studyMapRefLogic.GetCourseListByStudyMapID(StudyMapID, pageIndex, pageSize, SortExpression, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    //批量删除学习地图与课程的关系
    protected void btnDeletes_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.gvList);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("请选择要删除的课程！");
            return;
        }
        try
        {
            Res_StudyMapReferCourseLogic studyMapRefLogic = new Res_StudyMapReferCourseLogic();
            studyMapRefLogic.Remove(selectedValues);
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }

        JsUtility.SuccessMessageBox(string.Format("成功删除[{0}]条课程信息！", selectedValues.Length));
        

        //刷新数据
        this.PageSet1.DataBind();
        //upList.Update();
    }

    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Res_StudyMapReferCourseLogic studyMapRefLogic = new Res_StudyMapReferCourseLogic();
        Guid mapcourseId = (Guid) gvList.DataKeys[e.RowIndex].Value;
        DropDownList ddlStudyModel = (DropDownList)gvList.Rows[e.RowIndex].FindControl("ddlStudyModel");
        TextBox txtChargeMan = (TextBox)gvList.Rows[e.RowIndex].FindControl("txtActualMan");
        var entity = studyMapRefLogic.GetById(mapcourseId);
        if (null != entity)
        {
            entity.StudyModelID = ddlStudyModel.SelectedValue.ToInt();
            entity.ChargeMan = txtChargeMan.Text;

            try
            {
                studyMapRefLogic.Save(entity);
                gvList.EditIndex = -1;
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }

            JsUtility.SuccessMessageBox("保存成功!");
        }
    }
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {       
        gvList.EditIndex = e.NewEditIndex;        
        this.PageSet1.QueryChange();
        gvList.Rows[e.NewEditIndex].Cells[5].CssClass = "visibleS";
    }
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvList.EditIndex = -1;
        this.PageSet1.DataBind();
    }
}