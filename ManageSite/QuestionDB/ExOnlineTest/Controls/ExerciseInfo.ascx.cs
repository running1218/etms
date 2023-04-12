using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.ExOnlineTest.API.Entity;
using ETMS.Utility.Service.FileUpload;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement;

using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity;

using ETMS.Components.Basic.Implement.BLL;

public partial class QuestionDB_ExOnlineTest_Controls_ExerciseInfo : System.Web.UI.UserControl
{
    private static readonly Ex_OnLineTestLogic onlineTestLogic = new Ex_OnLineTestLogic();

   #region 页面条件参数存放

    /// <summary>
    /// 在线测试实体
    /// </summary>
    public Ex_OnLineTest onlineTest
    {
        get
        {
            return (Ex_OnLineTest)ViewState["onlineTest"];
        }
        set
        {
            ViewState["onlineTest"] = value;
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
    /// 在线测试ID
    /// </summary>
    public Guid OnlineTestID
    {
        get
        {
            if (ViewState["OnlineTestID"] == null)
            {
                ViewState["OnlineTestID"] = null;
            }
            return (Guid)ViewState["OnlineTestID"];
        }
        set
        {
            ViewState["OnlineTestID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Action == OperationAction.Add)
            {
                rbtnIsShowAnswer.SelectedValue = "1";
                rbtnOnLineTestStatus.SelectedValue = "1";

                if (null != CourseID && CourseID != Guid.Empty)
                {
                    ddlCourseID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ddlCourseID.isEnabled = false;
                }
            }
            if (Action == OperationAction.Edit)
            {
                InitControl();
                ddlCourseID.isEnabled = false;
            }
        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        onlineTest = onlineTestLogic.GetById(OnlineTestID);

        txtOnLineTestName.Text = onlineTest.OnLineTestName;
        rbtnIsShowAnswer.SelectedValue = onlineTest.IsShowAnswer.ToString();
        rbtnOnLineTestStatus.SelectedValue = onlineTest.OnLineTestStatus.ToString();
        txtOnLineTestDesc.Text = onlineTest.OnLineTestDesc;
        txtLimitTime.Text = onlineTest.LimitTime.ToString();
        txtTotalScore.Text = onlineTest.TotalScore.ToString();
        txtPassLine.Text = onlineTest.PassLine.ToString();

        Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
        courseRes = courseResLogic.getCourseResByResID(OnlineTestID.ToString(), EnumResourcesType.OnLineTest);

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
            onlineTest = new Ex_OnLineTest()
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
            onlineTest.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
            onlineTest.ModifyTime = DateTime.Now;
        }
        onlineTest.OnLineTestName = txtOnLineTestName.Text;
        onlineTest.OnLineTestDesc = txtOnLineTestDesc.Text;
        onlineTest.LimitTime = txtLimitTime.Text.ToInt();
        onlineTest.TotalScore = txtTotalScore.Text.ToDecimal();
        onlineTest.PassLine = txtPassLine.Text.ToDecimal();
        onlineTest.OnLineTestStatus = rbtnOnLineTestStatus.SelectedValue.ToInt();
        onlineTest.IsShowAnswer = rbtnIsShowAnswer.SelectedValue.ToInt();
        onlineTest.OrgID = UserContext.Current.OrganizationID;

        courseRes.IsUse = rbtnOnLineTestStatus.SelectedValue.ToInt();
        courseRes.ResName = onlineTest.OnLineTestName;
        courseRes.CourseID = ddlCourseID.getCourseID();
        courseRes.CourseResTypeID = (int)EnumResourcesType.OnLineTest;
    }

    //添加与修改
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (ddlCourseID.getCourseID() == Guid.Empty)
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请选择课程名称！");
            return;
        }
        if (string.IsNullOrEmpty(txtOnLineTestName.Text.Trim()))
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请填写作业名称！");
            return;
        }
        try
        {
            InitialEntity();
            onlineTestLogic.SaveOnlineTest(onlineTest, courseRes);
            string reURL = this.ActionHref(string.Format("~/QuestionDB/Testpaper/AddTestPaper.aspx?ExerciseID={0}&ExerciseType={1}", onlineTest.OnLineTestID, (int)BizExerciseType.ExOnlineTest));

            ETMS.Utility.JsUtility.SuccessMessageBox("", "在线测试信息保存成功！", "function(){window.parent.location.href='" + reURL + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}