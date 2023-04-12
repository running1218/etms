using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.WebApp.Manage;
using ETMS.AppContext;
using ETMS.Components.Fee.API.Entity;
using ETMS.Components.Fee.Implement.BLL;
using ETMS.Utility;

public partial class Fee_CourseFeeSetting_Controls_FeeSetting : System.Web.UI.UserControl
{
    #region 页面参数

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

    /// <summary>
    /// ID
    /// </summary>
    public Guid CourseFeeSettingID
    {
        get
        {
            if (ViewState["CourseFeeSettingID"] == null)
            {
                ViewState["CourseFeeSettingID"] = Guid.Empty;
            }
            return (Guid)ViewState["CourseFeeSettingID"];
        }
        set
        {
            ViewState["CourseFeeSettingID"] = value;
        }
    }


    /// <summary>
    ///  实体
    /// </summary>
    public Fee_CourseFeeSetting CourseFeeSetting
    {
        get
        {
            return (Fee_CourseFeeSetting)ViewState["CourseFeeSetting"];
        }
        set
        {
            ViewState["CourseFeeSetting"] = value;
        }
    }

    #endregion

    private static readonly Fee_CourseFeeSettingLogic CourseFeeSettingLogic = new Fee_CourseFeeSettingLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Action == OperationAction.Edit)
            {
                InitControl();
            }
        }
    }
    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        CourseFeeSetting = CourseFeeSettingLogic.GetById(CourseFeeSettingID);

        txtCourseFee.Text = CourseFeeSetting.CourseFee.ToString();
        ddlTeacherLevelID.SelectedValue = CourseFeeSetting.TeacherLevelID.ToString();
        ddlTrainingTimeDesc.SelectedValue = CourseFeeSetting.TrainingTimeDescID.ToString();
        txtRemark.Text = CourseFeeSetting.Remark;
    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private void InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            //新增实体
            CourseFeeSetting = new Fee_CourseFeeSetting()
            {   
                
            };
        }
        else if (Action == OperationAction.Edit)
        {
            CourseFeeSetting.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            CourseFeeSetting.ModifyTime = DateTime.Now;
        }

        CourseFeeSetting.CourseFee = txtCourseFee.Text.Trim().ToDecimal();
        CourseFeeSetting.TeacherLevelID = ddlTeacherLevelID.SelectedValue.ToInt();
        CourseFeeSetting.TrainingTimeDescID = ddlTrainingTimeDesc.SelectedValue.ToInt();
        CourseFeeSetting.Remark = txtRemark.Text.Trim();
    }

    //添加与修改
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InitialEntity();
            CourseFeeSettingLogic.Save(CourseFeeSetting);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("课酬标准保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}