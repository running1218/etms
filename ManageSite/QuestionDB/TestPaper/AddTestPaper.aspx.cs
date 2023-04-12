using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;

using ETMS.Components.Basic.Implement.BLL;
using ETMS.WebApp;

using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;
using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;

using ETMS.Components.ExOnlineTest.API.Entity;
using ETMS.Components.ExOnlineTest.Implement.BLL;

using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;

using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;
using ETMS.Utility;
using ETMS.Components.ExContest.Implement.BLL;
using ETMS.Components.ExContest.API.Entity;

public partial class QuestionDB_TestPaper_AddTestPaper : System.Web.UI.Page
{
    #region 页面条件参数存放
    /// <summary>
    /// 试卷实体
    /// </summary>
    public TestPaper eTestPaper
    {
        get;
        set;
    }

    /// <summary>
    /// TestPaperID
    /// </summary>
    public Guid TestPaperID
    {
        get
        {
            return (Guid)ViewState["TestPaperID"];
        }
        set
        {
            ViewState["TestPaperID"] = value;
        }
    }

    /// <summary>
    /// 作业类型
    /// </summary>
    public BizExerciseType ExerciseType
    {
        get
        {
            return (BizExerciseType)ViewState["ExerciseType"];
        }
        set
        {
            ViewState["ExerciseType"] = value;
        }
    }

    /// <summary>
    /// 作业ID
    /// </summary>
    public string ExerciseID
    {
        get
        {
            return ViewState["ExerciseID"].ToString();
        }
        set
        {
            ViewState["ExerciseID"] = value;
        }
    }

    /// <summary>
    /// 作业Name
    /// </summary>
    public string ExerciseName
    {
        get
        {
            return ViewState["ExerciseName"].ToString();
        }
        set
        {
            ViewState["ExerciseName"] = value;
        }
    }

    /// <summary>
    /// 课程ID，用来到题库中查询相应的题目
    /// </summary>
    public Guid CourseID
    {
        get
        {
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    /// <summary>
    /// CourseName
    /// </summary>
    public string CourseName
    {
        get
        {
            return ViewState["CourseName"].ToString();
        }
        set
        {
            ViewState["CourseName"] = value;
        }
    }

    /// <summary>
    /// 总分
    /// </summary>
    public int TotalScore
    {
        get
        {
            if (ViewState["TotalScore"] == null)
                ViewState["TotalScore"] = 100;
            return ViewState["TotalScore"].ToInt();
        }
        set
        {
            ViewState["TotalScore"] = value;
        }
    }
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string sKey = "keyTestPaper";
            string sScript = @"<script language='javascript' type='text/javascript'>
                                   setCookie('Tab_0');
                                </script>";

            Page.ClientScript.RegisterStartupScript(Page.GetType(), sKey, sScript);


            ExerciseType = (BizExerciseType)Enum.Parse(typeof(BizExerciseType), Request.QueryString["ExerciseType"].ToString());
            ExerciseID = Request.QueryString["ExerciseID"].ToString();

            Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
            
            switch (ExerciseType)
            {
                case BizExerciseType.ExContest:
                    Ex_Contest contest = new Ex_Contest();
                    Ex_ContestLogic contestLogic = new Ex_ContestLogic();
                    contest = contestLogic.GetById(new Guid(ExerciseID));

                    CourseID = courseResLogic.getCourseIDByResID(contest.ContestID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.Contest);
                    ltlCourseCode.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseCodeByID(CourseID);
                    ltlCourseName.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ltlExerciseTypeName.Text = new QuestionInfo().ExerciseName(ExerciseType);
                    ltlExerciseName.Text = contest.ContestName;
                    lblOnLineJobStatus.FieldIDValue = contest.ContestStatus.ToString();
                    
                    TotalScore = 100;
                    break;
                case BizExerciseType.ExOfflineHomework:
                    break;
                case BizExerciseType.ExOnlineHomework:
                    Ex_OnLineJob onlineJob = new Ex_OnLineJob();
                    Ex_OnLineJobLogic onlineJobLogic = new Ex_OnLineJobLogic();
                    onlineJob = onlineJobLogic.GetById(new Guid(ExerciseID));
                    
                    CourseID = courseResLogic.getCourseIDByResID(onlineJob.OnLineJobID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.OnLineJob);
                    ltlCourseCode.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseCodeByID(CourseID);
                    ltlCourseName.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ltlExerciseTypeName.Text = new QuestionInfo().ExerciseName(ExerciseType);
                    ltlExerciseName.Text = onlineJob.OnLineJobName;
                    lblOnLineJobStatus.FieldIDValue = onlineJob.OnLineJobStatus.ToString();
                    
                    TotalScore = 100;//作业不设置总分，默认就是100
                    break;
                case BizExerciseType.ExOnlinePractice:
                    break;
                case BizExerciseType.ExOnlineTest:
                    Ex_OnLineTest onlineTest = new Ex_OnLineTest();
                    Ex_OnLineTestLogic onlineTestLogic = new Ex_OnLineTestLogic();
                    onlineTest = onlineTestLogic.GetById(new Guid(ExerciseID));

                    CourseID = courseResLogic.getCourseIDByResID(onlineTest.OnLineTestID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.OnLineTest);
                    ltlCourseCode.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseCodeByID(CourseID);
                    ltlCourseName.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ltlExerciseTypeName.Text = new QuestionInfo().ExerciseName(ExerciseType);
                    ltlExerciseName.Text = onlineTest.OnLineTestName;
                    lblOnLineJobStatus.FieldIDValue = onlineTest.OnLineTestStatus.ToString();
                   
                    TotalScore = onlineTest.TotalScore.ToInterger();

                    ltlTotalScore.Text = TotalScore.ToString();
                    break;
                case BizExerciseType.ExRandomPractice:
                    break;
                default:
                    break;
            }

           
            ItemDB2BeSelect1.CourseID = CourseID;
            TestpaperInfoSenior1.CourseID = CourseID;
            SetTestPaper();
        }
        string sKeyTotalscore = "keyTotalscore";
        string sScriptTotalscore = @"<script language='javascript' type='text/javascript'>
                                   totalscore = {0}
                                </script>";

        Page.ClientScript.RegisterStartupScript(Page.GetType(), sKeyTotalscore, string.Format(sScriptTotalscore, TotalScore));

    }

    /// <summary>
    /// 获取试卷ID值，如果没有，则先建立课程卷库关系，建立试卷ID，再获取
    /// </summary>
    private void SetTestPaper()
    {
        //课程ID:CourseID 课程名称CourseName 机构ID:OrgID 作业IDExerciseID 作业名称ExerciseName 考试资源类型ExerciseType
        ExerciseName = ltlExerciseName.Text;
        CourseName = ltlCourseName.Text;

        ITreeCategoryLogic iTreeCategoryLogic = ServiceRepository.TreeCategoryService;
     
        eTestPaper = iTreeCategoryLogic.GetCourseExamResTestPaperForManage(CourseID, CourseName, ETMS.AppContext.UserContext.Current.OrganizationID, new Guid(ExerciseID), ExerciseName, (int)ExerciseType);

        TestPaperID = eTestPaper.TestPaperID;

        //获取到TestPaper实体后，将TestPaperID存入ExOnline表等
        switch (ExerciseType)
        {
            case BizExerciseType.ExContest:
                Ex_Contest exContest = new Ex_Contest();
                Ex_ContestLogic exContestLogic = new Ex_ContestLogic();

                exContest = exContestLogic.GetById(ExerciseID.ToGuid());
                exContest.TestPaperID = TestPaperID.ToString();

                exContestLogic.Update(exContest);
                break;
            case BizExerciseType.ExOfflineHomework:
                break;
            case BizExerciseType.ExOnlineHomework:
                Ex_OnLineJob exOnlineJob = new Ex_OnLineJob();
                Ex_OnLineJobLogic exOnlineJobLogic = new Ex_OnLineJobLogic();

                exOnlineJob = exOnlineJobLogic.GetById(ExerciseID.ToGuid());
                exOnlineJob.TestPaperID = TestPaperID.ToString();

                exOnlineJobLogic.Update(exOnlineJob);

                break;
            case BizExerciseType.ExOnlinePractice:
                break;
            case BizExerciseType.ExOnlineTest:

                Ex_OnLineTest exOnlineTest = new Ex_OnLineTest();
                Ex_OnLineTestLogic exOnlineTestLogic = new Ex_OnLineTestLogic();

                exOnlineTest = exOnlineTestLogic.GetById(ExerciseID.ToGuid());
                exOnlineTest.TestPaperID = TestPaperID.ToString();

                exOnlineTestLogic.Update(exOnlineTest);

                break;
            case BizExerciseType.ExRandomPractice:
                break;
            default:
                break;
        }

        ItemDB2BeSelect1.TestPaperID = TestPaperID;
        TestpaperInfoSenior1.TestPaperID = TestPaperID;
        ItemDBSelected1.TestPaperID = TestPaperID;

        ItemDB2BeSelect1.ExerciseID = ExerciseID;
        ItemDB2BeSelect1.ExerciseType = ExerciseType;

        TestpaperInfoSenior1.ExerciseID = ExerciseID;
        TestpaperInfoSenior1.ExerciseType = ExerciseType;
    }

    protected string getBackUrl()
    {
        string backUrl = "";
        switch (ExerciseType)
        {
            case BizExerciseType.ExContest:
                backUrl = string.Format("{0}/QuestionDB/ExContest/ExerciseList.aspx", ETMS.Utility.WebUtility.AppPath);
                break;
            case BizExerciseType.ExOfflineHomework:
                
                break;
            case BizExerciseType.ExOnlineHomework:
                backUrl = string.Format("{0}/QuestionDB/ExOnlineHomework/ExerciseList.aspx", ETMS.Utility.WebUtility.AppPath);
                break;
            case BizExerciseType.ExOnlinePractice:
                break;
            case BizExerciseType.ExOnlineTest:
                backUrl = string.Format("{0}/QuestionDB/ExOnlineTest/ExerciseList.aspx", ETMS.Utility.WebUtility.AppPath);
                break;
            case BizExerciseType.ExRandomPractice:
                break;
            default:
                break;
        }
        return backUrl;
    }

}