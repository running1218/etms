using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;
using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;

using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.API.Entity;

public partial class QuestionDB_ExOnlineHomework_Controls_ExerciseView : System.Web.UI.UserControl
{
    private static readonly Ex_OnLineJobLogic onlineJobLogic = new Ex_OnLineJobLogic();

    /// <summary>
    /// 学习地图实体
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

    #region 页面条件参数存放


    private static Guid defaultGuidValue = new Guid();

    /// <summary>
    /// 学习地图ID
    /// </summary>
    public Guid OnlineJobID
    {
        get
        {
            if (ViewState["OnlineJobID"] == null)
            {
                ViewState["OnlineJobID"] = defaultGuidValue;
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

            InitControl();

        }
    }

    /// <summary>
    /// 初始化控件值
    /// </summary>
    private void InitControl()
    {
        onlineJob = onlineJobLogic.GetById(OnlineJobID);

        ltlOnlineJobName.Text = onlineJob.OnLineJobName;
        lblIsShowAnswer.FieldIDValue = onlineJob.IsShowAnswer.ToString();
        lblOnLineJobStatus.FieldIDValue = onlineJob.OnLineJobStatus.ToString();
        ltlOnLineJobDesc.Text = onlineJob.OnLineJobDesc;

        Res_CourseResLogic courseResLogic = new Res_CourseResLogic();

        Guid CourseID = courseResLogic.getCourseIDByResID(OnlineJobID.ToString(), EnumResourcesType.OnLineJob);

        ltlCourseCode.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseCodeByID(CourseID);
        ltlCourseName.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);

    }
}