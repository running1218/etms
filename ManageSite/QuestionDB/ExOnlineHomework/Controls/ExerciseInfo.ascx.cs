using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;
using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;
using ETMS.Utility.Service.FileUpload;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement;

using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity;

using ETMS.Components.Basic.Implement.BLL;

public partial class QuestionDB_ExOnlineHomework_Controls_ExerciseInfo : System.Web.UI.UserControl
{
    private static readonly Ex_OnLineJobLogic onlineJobLogic = new Ex_OnLineJobLogic();

    /// <summary>
    /// 在线作业实体
    /// </summary>
    public Ex_OnLineJob onlineJob
    {
        get
        {
            return (Ex_OnLineJob)ViewState["onlineJob"];
        }
        set
        {
            ViewState["onlineJob"] = value;
        }
    }

    /// <summary>
    /// 学习资源实体
    /// </summary>
    public Res_CourseRes courseRes
    {
        get
        {
            return (Res_CourseRes)ViewState["courseRes"];
        }
        set
        {
            ViewState["courseRes"] = value;
        }
    }

    public Guid CourseID
    {
        get;
        set;
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

    /// <summary>
    /// 在线作业ID
    /// </summary>
    public Guid OnlineJobID
    {
        get
        {
            if (ViewState["OnlineJobID"] == null)
            {
                ViewState["OnlineJobID"] = null;
            }
            return (Guid)ViewState["OnlineJobID"];
        }
        set
        {
            ViewState["OnlineJobID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Action == OperationAction.Add)
            {
                //rbtnIsShowAnswer.SelectedValue = "1";
                rbtnOnLineJobStatus.SelectedValue = "1";

                if (null != CourseID && CourseID != Guid.Empty)
                {
                    ddlCourseID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ddlCourseID.isEnabled = false;
                }
            }
            if (Action == OperationAction.Edit)
            {
                ddlCourseID.isEnabled = false;
                InitControl();
            }
        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        onlineJob = onlineJobLogic.GetById(OnlineJobID);

        txtOnlineJobName.Text = onlineJob.OnLineJobName;
        //rbtnIsShowAnswer.SelectedValue = onlineJob.IsShowAnswer.ToString();
        rbtnOnLineJobStatus.SelectedValue = onlineJob.OnLineJobStatus.ToString();
        txtOnLineJobDesc.Text = onlineJob.OnLineJobDesc;

        Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
        courseRes = courseResLogic.getCourseResByResID(OnlineJobID.ToString(), EnumResourcesType.OnLineJob);

        ddlCourseID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(courseRes.CourseID);
    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private void InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            //新增实体
            onlineJob = new Ex_OnLineJob()
            {
                CreateUser = UserContext.Current.RealName,
                CreateUserID = UserContext.Current.UserID,
                CreateTime = DateTime.Now
            };
            courseRes = new Res_CourseRes()
            {
                CreateUser = UserContext.Current.RealName,
                CreateUserID = UserContext.Current.UserID,
                CreateTime = DateTime.Now
            };
        }
        else if (Action == OperationAction.Edit)
        {
            onlineJob.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            onlineJob.ModifyTime = DateTime.Now;
        }
        onlineJob.OnLineJobName = txtOnlineJobName.Text;
        onlineJob.OnLineJobDesc = txtOnLineJobDesc.Text;
        onlineJob.OnLineJobStatus = rbtnOnLineJobStatus.SelectedValue.ToInt();
        onlineJob.IsShowAnswer = 1;//rbtnIsShowAnswer.SelectedValue.ToInt();
        onlineJob.OrgID = UserContext.Current.OrganizationID;

        courseRes.IsUse = rbtnOnLineJobStatus.SelectedValue.ToInt();
        courseRes.ResName = onlineJob.OnLineJobName;
        courseRes.CourseID = ddlCourseID.getCourseID();
        courseRes.CourseResTypeID = (int)EnumResourcesType.OnLineJob;
    }

    //添加与修改
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (ddlCourseID.getCourseID() == Guid.Empty)
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请选择课程名称！");
            return;
        }
        if (string.IsNullOrEmpty(txtOnlineJobName.Text.Trim()))
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请填写作业名称！");
            return;
        }
        try
        {
            InitialEntity();
            onlineJobLogic.SaveOnlineJob(onlineJob, courseRes);
            string reURL = this.ActionHref(string.Format("~/QuestionDB/Testpaper/AddTestPaper.aspx?ExerciseID={0}&ExerciseType={1}", onlineJob.OnLineJobID,(int)BizExerciseType.ExOnlineHomework));

            ETMS.Utility.JsUtility.SuccessMessageBox("", "在线作业信息保存成功！", "function(){window.parent.location.href='" + reURL + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}