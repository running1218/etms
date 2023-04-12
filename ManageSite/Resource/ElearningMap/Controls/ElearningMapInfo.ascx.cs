using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Utility.Service.FileUpload;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement;

public partial class Resource_ElearningMap_Controls_ElearningMapInfo : System.Web.UI.UserControl
{
    private static readonly Res_StudyMapLogic studyMapLogic = new Res_StudyMapLogic();

    /// <summary>
    /// 学习地图实体
    /// </summary>
    public Res_StudyMap studyMap
    {
        get
        {
            return (Res_StudyMap)ViewState["studyMap"];
        }
        set
        {
            ViewState["studyMap"] = value;
        }
    }

    #region 页面条件参数存放

    /// <summary>
    /// 操作
    /// </summary>
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
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

    protected string StudyMapType
    {
        get
        {
            if (null != ConfigurationManager.AppSettings["StudyMapType"])
            {
                return ConfigurationManager.AppSettings["StudyMapType"];
            }
            else
            {
                return "1";
            }
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Action == OperationAction.Add)
            {
                trStudyMapCode.Visible = false;
            }
            else if (Action == OperationAction.Edit)
            {
                InitControl();
            }

            ddlElearningMapType.DefaultValue = StudyMapType;
            if (StudyMapType == "1")
                trDepartment.Style.Add("display", "none");
            if (StudyMapType == "2")
                trPost.Style.Add("display", "none");
        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        studyMap = studyMapLogic.GetById(StudyMapID);

        ltlStudyMapCode.Text = studyMap.StudyMapCode;
        txtStudyMapName.Text = studyMap.StudyMapName;
        ddlElearningMapType.SelectedValue = studyMap.ELearningMapTypeID.ToString();
        ddlDepartment.SelectedValue = studyMap.DeptID.ToString();
        ddlPost.SelectedValue = studyMap.PostID.ToString();
        ddlRank.SelectedValue = studyMap.RankID.ToString();
        txtStudyMapDesc.Text = studyMap.StudyMapDesc;
    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private void InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            string Crieria = string.Format(" AND OrgID={0}",  UserContext.Current.OrganizationID);
            Res_StudyMapLogic studyMapLogic = new Res_StudyMapLogic();

            //新增实体
            studyMap = new Res_StudyMap()
            {
                StudyMapID = Guid.NewGuid(),
                StudyMapCode = "",
                CreateUser = UserContext.Current.RealName,
                CreateUserID = UserContext.Current.UserID,
                CreateTime = DateTime.Now
            };
        }
        else if (Action == OperationAction.Edit)
        {
            studyMap.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            studyMap.ModifyTime = DateTime.Now;
        }
        
        studyMap.StudyMapName = txtStudyMapName.Text.Trim();
        studyMap.ELearningMapTypeID = int.Parse(ddlElearningMapType.SelectedValue);
        studyMap.DeptID = ddlDepartment.SelectedValue.ToInt();
        studyMap.PostID = ddlPost.SelectedValue.ToInt();
        studyMap.RankID = ddlRank.SelectedValue.ToInt();
        studyMap.Status = 1;
        studyMap.StudyMapDesc = txtStudyMapDesc.Text;
        studyMap.Remark = "";
        studyMap.OrgID = UserContext.Current.OrganizationID;
    }

    //添加与修改
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        switch (StudyMapType)
        {
            case "1": //岗位、职级
                if (string.IsNullOrEmpty(ddlPost.SelectedValue) || string.IsNullOrEmpty(ddlRank.SelectedValue))
                {
                    ETMS.Utility.JsUtility.AlertMessageBox("请选择岗位和职级！");
                    return;
                }
                break;
            case "2": //部门、职级
                if (string.IsNullOrEmpty(ddlDepartment.SelectedValue) || string.IsNullOrEmpty(ddlRank.SelectedValue))
                {
                    ETMS.Utility.JsUtility.AlertMessageBox("请选择部门和职级！");
                    return;
                }

                break;
            case "3": //部门、岗位、职级
                if (string.IsNullOrEmpty(ddlDepartment.SelectedValue) || string.IsNullOrEmpty(ddlPost.SelectedValue) || string.IsNullOrEmpty(ddlRank.SelectedValue))
                {
                    ETMS.Utility.JsUtility.AlertMessageBox("请选择部门、岗位和职级！");
                    return;
                }
                break;
            default:
                break;
        }

        try
        {
            InitialEntity();
            studyMapLogic.SaveStudyMap(studyMap, Action);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("学习地图信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }    
    }
}