using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExContest.Implement.BLL;
using ETMS.Components.ExContest.API.Entity;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL;

public partial class QuestionDB_ExContest_Controls_ExerciseInfo : System.Web.UI.UserControl
{
    private static readonly Ex_ContestLogic ContestLogic = new Ex_ContestLogic();

    /// <summary>
    /// 闯关竞赛实体
    /// </summary>
    public Ex_Contest Contest
    {
        get
        {
            return (Ex_Contest)ViewState["Contest"];
        }
        set
        {
            ViewState["Contest"] = value;
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
    /// 闯关竞赛ID
    /// </summary>
    public Guid ContestID
    {
        get
        {
            if (ViewState["ContestID"] == null)
            {
                ViewState["ContestID"] = null;
            }
            return (Guid)ViewState["ContestID"];
        }
        set
        {
            ViewState["ContestID"] = value;
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
                rbtnContestStatus.SelectedValue = "1";

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
        Contest = ContestLogic.GetById(ContestID);

        txtContestName.Text = Contest.ContestName;
        //rbtnIsShowAnswer.SelectedValue = Contest.IsShowAnswer.ToString();
        rbtnContestStatus.SelectedValue = Contest.ContestStatus.ToString();
        txtContestDesc.Text = Contest.ContestDesc;

        Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
        courseRes = courseResLogic.getCourseResByResID(ContestID.ToString(), EnumResourcesType.Contest);

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
            Contest = new Ex_Contest()
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
            Contest.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            Contest.ModifyTime = DateTime.Now;
        }
        Contest.ContestName = txtContestName.Text;
        Contest.ContestDesc = txtContestDesc.Text;
        Contest.ContestStatus = rbtnContestStatus.SelectedValue.ToInt();
        Contest.IsShowAnswer = 1;//rbtnIsShowAnswer.SelectedValue.ToInt();
        Contest.OrgID = UserContext.Current.OrganizationID;

        courseRes.IsUse = rbtnContestStatus.SelectedValue.ToInt();
        courseRes.ResName = Contest.ContestName;
        courseRes.CourseID = ddlCourseID.getCourseID();
        courseRes.CourseResTypeID = (int)EnumResourcesType.Contest;
    }

    //添加与修改
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtContestName.Text.Trim()))
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请填写竞赛名称！");
            return;
        }
        try
        {
            InitialEntity();
            ContestLogic.SaveContest(Contest, courseRes);
            string reURL = this.ActionHref(string.Format("~/QuestionDB/Testpaper/AddTestPaper.aspx?ExerciseID={0}&ExerciseType={1}", Contest.ContestID, (int)BizExerciseType.ExContest));

            ETMS.Utility.JsUtility.SuccessMessageBox("", "闯关竞赛信息保存成功！", "function(){window.parent.location.href='" + reURL + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }

    }
}