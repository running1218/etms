using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Components.Basic.API.Entity.ELearningMap;

public partial class Resource_ElearningMap_Controls_ElearningMapInfoView : System.Web.UI.UserControl
{
    public string StudyMapType
    {
        get
        {
            return ConfigurationManager.AppSettings["StudyMapType"] ?? string.Empty;
        }
    }
    private static readonly Res_StudyMapLogic studyMapLogic = new Res_StudyMapLogic();

    #region 页面条件参数存放

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
    /// 是否可见能力描述
    /// </summary>
    public Boolean StudyMapDescVisible
    {
        get
        {
            if (ViewState["StudyMapDescVisible"] == null)
            {
                ViewState["StudyMapDescVisible"] =true;
            }
            return (Boolean)ViewState["StudyMapDescVisible"];
        }
        set
        {
            ViewState["StudyMapDescVisible"] = value;
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {   
            InitControl();
        }

    }

    /// <summary>
    /// 初始化页面数据
    /// </summary>
    private void InitControl()
    {
        Res_StudyMap studyMap = new Res_StudyMap();

        studyMap = studyMapLogic.GetById(StudyMapID);

        ltlStudyMapCode.Text = studyMap.StudyMapCode;
        ltlStudyMapName.Text = studyMap.StudyMapName;
        lblDepartment.FieldIDValue = studyMap.DeptID.ToString();
        lblPost.FieldIDValue = studyMap.PostID.ToString();
        lblRank.FieldIDValue = studyMap.RankID.ToString();
        ltlStudyMapDesc.Text = studyMap.StudyMapDesc;

        trStudyMapDesc.Visible = StudyMapDescVisible;
    }
}