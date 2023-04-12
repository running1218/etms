using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.API.Entity;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Components.Basic.API.Entity.ELearningMap;

public partial class Resource_ElearningMap_ChooseData : System.Web.UI.Page
{
    public Guid StudyMapID
    {
        get
        {
            return Request.ToparamValue<Guid>("StudyMapID");
        }
    }
    protected static readonly Res_StudyMapReferIDPLogic studyMapIDPLogic = new Res_StudyMapReferIDPLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!IsPostBack)
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
        var entity = new Res_StudyMapReferIDP(){
            OrgID = UserContext.Current.OrganizationID,
            StudyMapID = StudyMapID,
            DataCode = txt_DataCode.Text,
            DataName = txt_DataName.Text,
            DataCotent = txtDataCotent.Text,
            DataOutline = txtDataOutline.Text
        };
        var source = studyMapIDPLogic.GetMapDataChoseList(entity, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(source, pageIndex, pageSize);
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

    /// <summary>
    /// 批量建立关系
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("请选择要关联的课程！");
            return;
        }
        try
        {
            studyMapIDPLogic.Save(StudyMapID, selectedValues);            
            
            JsUtility.SuccessMessageBoxAndRedirectToParent(string.Format("成功关联[{0}]条课程信息！", selectedValues.Length),getCancelUrl());
            this.PageSet1.QueryChange();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }        
    }

    protected string getCancelUrl()
    {
        return this.ActionHref(string.Format("~/Resource/ElearningMap/MapDataList.aspx?StudyMapID={0}", StudyMapID));
    }
}