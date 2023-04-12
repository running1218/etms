using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.IDP.Implement.BLL.NotCourseData;
using ETMS.Utility;

public partial class Resource_ElearningMap_DataView : System.Web.UI.Page
{
    private static readonly IDP_NotCourseDataLogic logic = new IDP_NotCourseDataLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();
        }
    }

    void InitData()
    {
        var entity = logic.GetById(Request.ToparamValue<Guid>("NotCourseDataID"));
        if (null != entity)
        {
            ltlDataCode.Text = entity.DataCode;
            ltlDataName.Text = entity.DataName;
            ltlDataContent.Text = entity.DataCotent;
            ltlOutLine.Text = entity.DataOutline;
            lblCourseStatus.FieldIDValue = entity.DataStatus.ToString();
            lblStudyModel.FieldIDValue = entity.StudyModelID.ToString();
            ltlTimeLength.Text = entity.TimeLength.ToString();
            ltlTimes.Text = entity.StudyTimes.ToString();
            ltlImplementor.Text = entity.Implementor;
            ltlDutyMan.Text = entity.DutyMan;
            ltlDataURL.Text = entity.DataURL;
            ltlEvaluationMode.Text = entity.EvaluationMode;
        }
    }
}