using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.ExOnlineTest.API.Entity;

using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity;

public partial class QuestionDB_ExOnlineTest_Controls_ExerciseView : System.Web.UI.UserControl
{
    private static readonly Ex_OnLineTestLogic onlineJobLogic = new Ex_OnLineTestLogic();

    /// <summary>
    /// 学习地图实体
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

    #region 页面条件参数存放


    private static Guid defaultGuidValue = new Guid();

    /// <summary>
    /// 学习地图ID
    /// </summary>
    public Guid OnlineTestID
    {
        get
        {
            if (ViewState["OnlineTestID"] == null)
            {
                ViewState["OnlineTestID"] = defaultGuidValue;
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

            InitControl();

        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        onlineTest = onlineJobLogic.GetById(OnlineTestID);

        ltlOnLineTestName.Text = onlineTest.OnLineTestName;
        lblIsShowAnswer.FieldIDValue = onlineTest.IsShowAnswer.ToString();
        lblOnLineTestStatus.FieldIDValue = onlineTest.OnLineTestStatus.ToString();
        ltlOnLineTestDesc.Text = onlineTest.OnLineTestDesc;

        Res_CourseResLogic courseResLogic = new Res_CourseResLogic();

        Guid CourseID = courseResLogic.getCourseIDByResID(OnlineTestID.ToString(), EnumResourcesType.OnLineTest);

        ltlCourseCode.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseCodeByID(CourseID);
        ltlCourseName.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);

    }
}