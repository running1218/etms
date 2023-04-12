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
using System.Text;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.ExContest.API.Entity;
using ETMS.Components.ExContest.Implement.BLL;

public partial class QuestionDB_TestPaper_TestpaperView : System.Web.UI.Page
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

    #endregion

    protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {   
            ExerciseType = (BizExerciseType)Enum.Parse(typeof(BizExerciseType), Request.QueryString["ExerciseType"].ToString());
            ExerciseID = Request.QueryString["ExerciseID"].ToString();

            Res_CourseResLogic courseResLogic = new Res_CourseResLogic();

            switch (ExerciseType)
            {
                case BizExerciseType.ExContest:
                    Ex_Contest Contest = new Ex_Contest();
                    Ex_ContestLogic ContestLogic = new Ex_ContestLogic();
                    Contest = ContestLogic.GetById(new Guid(ExerciseID));

                    CourseID = courseResLogic.getCourseIDByResID(Contest.ContestID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.Contest);
                    CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ExerciseName = Contest.ContestName;

                    ltlExerciseName.Text = string.Format("闯关竞赛：{0}", ExerciseName);

                    break;
                case BizExerciseType.ExOfflineHomework:
                    break;
                case BizExerciseType.ExOnlineHomework:
                    Ex_OnLineJob onlineJob = new Ex_OnLineJob();
                    Ex_OnLineJobLogic onlineJobLogic = new Ex_OnLineJobLogic();
                    onlineJob = onlineJobLogic.GetById(new Guid(ExerciseID));

                    CourseID = courseResLogic.getCourseIDByResID(onlineJob.OnLineJobID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.OnLineJob);
                    CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ExerciseName = onlineJob.OnLineJobName;

                    ltlExerciseName.Text = string.Format("在线作业：{0}", ExerciseName);

                    break;
                case BizExerciseType.ExOnlinePractice:
                    break;
                case BizExerciseType.ExOnlineTest:
                    Ex_OnLineTest onlineTest = new Ex_OnLineTest();
                    Ex_OnLineTestLogic onlineTestLogic = new Ex_OnLineTestLogic();
                    onlineTest = onlineTestLogic.GetById(new Guid(ExerciseID));

                    CourseID = courseResLogic.getCourseIDByResID(onlineTest.OnLineTestID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.OnLineTest);
                    CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                    ExerciseName = onlineTest.OnLineTestName;

                    ltlExerciseName.Text = string.Format("在线测试：{0}  【时长：{1}】 【及格线：{2}】【总分：{3}】", ExerciseName, (int)onlineTest.LimitTime, (int)onlineTest.PassLine,(int)onlineTest.TotalScore);
                    break;
                case BizExerciseType.ExRandomPractice:
                    break;
                default:
                    break;
            }
            
            SetTestPaper();
        }
    }

    /// <summary>
    /// 获取试卷ID值，如果没有，则先建立课程卷库关系，建立试卷ID，再获取
    /// </summary>
    private void SetTestPaper()
    {
        //课程ID:CourseID 课程名称CourseName 机构ID:OrgID 作业IDExerciseID 作业名称ExerciseName 考试资源类型ExerciseType
        ITreeCategoryLogic iTreeCategoryLogic = ServiceRepository.TreeCategoryService;

        eTestPaper = iTreeCategoryLogic.GetCourseExamResTestPaperForManage(CourseID, CourseName, ETMS.AppContext.UserContext.Current.OrganizationID, new Guid(ExerciseID), ExerciseName, (int)ExerciseType);
        TestPaperID = eTestPaper.TestPaperID;

        
        int totalSize = 0;
        IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
        IList<PaperQuestionView> paperQuestionViewList = iPaperQuestionLogic.FindQuestionView(TestPaperID, ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Null, 1000, 1, out totalSize);
        
        SingleChoiceQuestion eSingleChoiceQuestion = new SingleChoiceQuestion();
        IQuestionServiceLogic iSingleChoiceQuestionLogic = ServiceRepository.SingleChoiceQuestionService;

        MultipleChoiceQuestion eMultipleChoiceQuestion = new MultipleChoiceQuestion();
        IQuestionServiceLogic iMultipleChoiceQuestionLogic = ServiceRepository.MultipleChoiceQuestionService;

        JudgementQuestion eJudgementQuestion = new JudgementQuestion();
        IQuestionServiceLogic iJudgementQuestionLogic = ServiceRepository.JudgementQuestionService;

        StringBuilder paperInfo = new StringBuilder();

        int i = 1;
        int j = 0;
        string strCheck = "";
        string strb = "";
        string strb2 = "";
        foreach (PaperQuestionView eBasePriview in paperQuestionViewList)
            {
                paperInfo.Append(string.Format("<tr><td width='20' align='right' valign='top'><b>{0}.</b></td><td valign='top'><b>{1}</b></td><td align='right' width='100'>（{2}分）</td></tr>", i, eBasePriview.QuestionTitle, (int)eBasePriview.QuestionScore));
                j = 0;
                strCheck = "";
                strb = "";
                strb2 = "";

                if (eBasePriview.QuestionType == QuestionType.SingleChoice)
                {
                    eSingleChoiceQuestion = (SingleChoiceQuestion)iSingleChoiceQuestionLogic.GetByID(eBasePriview.QuestionID);

                    foreach (QuestionOption questionOption in eSingleChoiceQuestion.Options)
                    {
                        strCheck = "";
                        strb = "";
                        strb2 = "";
                        if (eSingleChoiceQuestion.Answer.AnswerOption.OptionCode == letterlist[j])
                        {
                            strCheck = " checked";
                            strb = "<b><font class='RightAnswer'>";
                            strb2 = "</font></b>";
                        }

                        paperInfo.Append(string.Format("<tr><td></td><td colspan='2'><input disabled type='radio' id='radio{0}' name='radio{1}' {2} />{3}</td></tr>", i.ToString() + j.ToString(), i, strCheck, strb + letterlist[j] + ". " + questionOption.OptionContent + strb2));
                        j++;
                    }
                }

                if (eBasePriview.QuestionType == QuestionType.MultipleChoice)
                {
                    eMultipleChoiceQuestion = (MultipleChoiceQuestion)iMultipleChoiceQuestionLogic.GetByID(eBasePriview.QuestionID);

                    foreach (QuestionOption questionOption in eMultipleChoiceQuestion.Options)
                    {
                        strCheck = "";
                        strb = "";
                        strb2 = "";
                        for (int k = 0; k < eMultipleChoiceQuestion.Answer.AnswerOptions.Count; k++)
                        {
                            if (eMultipleChoiceQuestion.Answer.AnswerOptions[k].OptionCode == letterlist[j])
                            {
                                strCheck = " checked";
                                strb = "<b><font class='RightAnswer'>";
                                strb2 = "</font></b>";
                            }
                        }

                        paperInfo.Append(string.Format("<tr><td></td><td colspan='2'><input disabled type='checkbox' id='checkbox{0}' name='checkbox{1}' {2} />{3}</td></tr>", i.ToString() + j.ToString(), i, strCheck, strb + letterlist[j] + ". " + questionOption.OptionContent + strb2));
                        j++;
                    }
                }

                if (eBasePriview.QuestionType == QuestionType.Judgement)
                {
                    eJudgementQuestion = (JudgementQuestion)iJudgementQuestionLogic.GetByID(eBasePriview.QuestionID);

                    foreach (QuestionOption questionOption in eJudgementQuestion.Options)
                    {
                        strCheck = "";
                        strb = "";
                        strb2 = "";
                        if (eJudgementQuestion.Answer.OptionAnswer.OptionCode == letterlist[j])
                        {
                            strCheck = " checked";
                            strb = "<b><font class='RightAnswer'>";
                            strb2 = "</font></b>";
                        }

                        paperInfo.Append(string.Format("<tr><td></td><td colspan='2'><input disabled type='radio' id='radio{0}' name='radio{1}'  {2}  />{3}</td></tr>", i.ToString() + j.ToString(), i, strCheck, strb + questionOption.OptionContent + strb2));
                        j++;
                    }
                }
                paperInfo.Append("<tr><td colspan='3' style=' line-height:5px; height:5px;'></td></tr>");

                i++;
                
            }

        ltlTestpaperInfo.Text = paperInfo.ToString();
    }

}