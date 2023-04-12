using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Utility;
using ETMS.Components.Exam.API.Interface.Test;

public partial class QuestionDB_TestPaper_Controls_TestpaperInfoSenior : System.Web.UI.UserControl
{
    #region 页面条件参数存放
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
    /// CourseID
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
    /// 题库ID
    /// </summary>
    public QuestionBank eQuestionBank
    {
        get
        {
            return (QuestionBank)ViewState["eQuestionBank"];
        }
        set
        {
            ViewState["eQuestionBank"] = value;
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

    #endregion

    protected int SingleChoiceEasySum = 0;
    protected int SingleChoiceMediumSum = 0;
    protected int SingleChoiceHardSum = 0;

    protected int SingleChoiceLowSelectQty = 0;
    protected int SingleChoiceMediumSelectQty = 0;
    protected int SingleChoiceHighSelectQty = 0;
    
    protected int MultipleChoiceEasySum = 0;
    protected int MultipleChoiceMediumSum = 0;
    protected int MultipleChoiceHardSum = 0;

    protected int MultipleChoiceLowSelectQty = 0;
    protected int MultipleChoiceMediumSelectQty = 0;
    protected int MultipleChoiceHighSelectQty = 0;

    protected int JudgementEasySum = 0;
    protected int JudgementMediumSum = 0;
    protected int JudgementHardSum = 0;

    protected int JudgementLowSelectQty = 0;
    protected int JudgementMediumSelectQty = 0;
    protected int JudgementHighSelectQty = 0;

    protected int SingleChoiceQuestionScore = 0;
    protected int MultipleChoiceQuestionScore = 0;
    protected int JudgementQuestionScore = 0;

    protected int SingleChoiceSum = 0;
    protected int MultipleChoiceSum = 0;
    protected int JudgementSum = 0;

    protected int QuestionSum = 0;
    protected int QuestionScoreSum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbtnSave.Attributes.Add("onclick", string.Format("javascript:return checkltlQuestionScoreSum('{0}');", hfldTestPaperRule.ClientID));

        SetQuestionBank();
        InitLiteral();
    }

    /// <summary>
    /// 获取课程的题库实体，如果没有，插入一条课程题库关联数据和题库信息。
    /// </summary>
    private void SetQuestionBank()
    {
        string CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
        int OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;

        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        eQuestionBank = bankService.GetQuestionBankByCourseID(CourseID, CourseName, OrgID);

    }

    /// <summary>
    /// 初始化题目数量
    /// </summary>
    private void InitLiteral()
    {
        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        QuestionSearch questionSearch = new QuestionSearch();

        questionSearch.QuestionBankID = eQuestionBank.QuestionBankID;

        questionSearch.Type = QuestionType.SingleChoice;
        questionSearch.Difficulty = 1;
        IList<Question> questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out SingleChoiceEasySum);

        questionSearch.Difficulty = 2;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out SingleChoiceMediumSum);

        questionSearch.Difficulty = 3;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out SingleChoiceHardSum);

        questionSearch.Type = QuestionType.MultipleChoice;
        questionSearch.Difficulty = 1;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out MultipleChoiceEasySum);

        questionSearch.Difficulty = 2;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out MultipleChoiceMediumSum);

        questionSearch.Difficulty = 3;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out MultipleChoiceHardSum);

        questionSearch.Type = QuestionType.Judgement;
        questionSearch.Difficulty = 1;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out JudgementEasySum);

        questionSearch.Difficulty = 2;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out JudgementMediumSum);

        questionSearch.Difficulty = 3;
        questionList = bankService.GetQuestionByCourseID(CourseID, questionSearch, 1, 1, out JudgementHardSum);
        

        ITestPaperRuleLogic iTestPaperRuleLogic = ServiceRepository.TestPaperRuleService;
        IList<TestPaperRule> TestPaperRuleList = iTestPaperRuleLogic.GetQuestionStat(TestPaperID);

        foreach (TestPaperRule eTestPaperRule in TestPaperRuleList)
        {
            if (eTestPaperRule.QuestionType == QuestionType.SingleChoice)
            {
                //SingleChoiceEasySum = eTestPaperRule.LowTotalQty;
                //SingleChoiceMediumSum = eTestPaperRule.MediumTotalQty;
                //SingleChoiceHardSum = eTestPaperRule.HighTotalQty;

                SingleChoiceLowSelectQty = eTestPaperRule.LowSelectQty;
                SingleChoiceMediumSelectQty =  eTestPaperRule.MediumSelectQty;
                SingleChoiceHighSelectQty = eTestPaperRule.HighSelectQty;

                SingleChoiceQuestionScore = eTestPaperRule.QuestionScore;
               
            }
            if (eTestPaperRule.QuestionType == QuestionType.MultipleChoice)
            {
                //MultipleChoiceEasySum = eTestPaperRule.LowTotalQty;
                //MultipleChoiceMediumSum = eTestPaperRule.MediumTotalQty;
                //MultipleChoiceHardSum = eTestPaperRule.HighTotalQty;

                MultipleChoiceLowSelectQty = eTestPaperRule.LowSelectQty;
                MultipleChoiceMediumSelectQty = eTestPaperRule.MediumSelectQty;
                MultipleChoiceHighSelectQty = eTestPaperRule.HighSelectQty;

                MultipleChoiceQuestionScore = eTestPaperRule.QuestionScore;

            }
            if (eTestPaperRule.QuestionType == QuestionType.Judgement)
            {
                //JudgementEasySum = eTestPaperRule.LowTotalQty;
                //JudgementMediumSum = eTestPaperRule.MediumTotalQty;
                //JudgementHardSum = eTestPaperRule.HighTotalQty;

                JudgementLowSelectQty = eTestPaperRule.LowSelectQty;
                JudgementMediumSelectQty = eTestPaperRule.MediumSelectQty;
                JudgementHighSelectQty = eTestPaperRule.HighSelectQty;

                JudgementQuestionScore = eTestPaperRule.QuestionScore;
            }
        }

        SingleChoiceSum = SingleChoiceLowSelectQty + SingleChoiceMediumSelectQty + SingleChoiceHighSelectQty;
        MultipleChoiceSum = MultipleChoiceLowSelectQty + MultipleChoiceMediumSelectQty + MultipleChoiceHighSelectQty;
        JudgementSum = JudgementLowSelectQty + JudgementMediumSelectQty + JudgementHighSelectQty;
        QuestionSum = SingleChoiceSum + MultipleChoiceSum + JudgementSum;
        QuestionScoreSum = SingleChoiceSum * SingleChoiceQuestionScore + MultipleChoiceSum * MultipleChoiceQuestionScore + JudgementSum * JudgementQuestionScore;
    }

    /// <summary>
    /// 保存策略
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        TestPaperRule eTestPaperRule = new TestPaperRule();
        ITestPaperRuleLogic iTestPaperRuleLogic = ServiceRepository.TestPaperRuleService;

        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        QuestionSearch questionSearch = new QuestionSearch();

        eTestPaperRule.TestPaperID = TestPaperID;
        eTestPaperRule.QuestionBankID = eQuestionBank.QuestionBankID;
        eTestPaperRule.CreatedUserID = ETMS.AppContext.UserContext.Current.UserID;
        //SingleChoice,2,1,1,10;MultipleChoice,2,1,1,10;Judgement,1,1,0,10
        string[] strList = hfldTestPaperRule.Value.Split(';');
        foreach (string item in strList)
        {
            string[] strs = item.Split(',');

            eTestPaperRule.QuestionType = getType(strs[0]);
            eTestPaperRule.LowSelectQty = StringUtility.ToInt(strs[1]);
            eTestPaperRule.MediumSelectQty = StringUtility.ToInt(strs[2]);
            eTestPaperRule.HighSelectQty = StringUtility.ToInt(strs[3]);
            eTestPaperRule.QuestionScore = StringUtility.ToInt(strs[4]);

            switch (eTestPaperRule.QuestionType)
            {
                case QuestionType.Null:
                    break;
                case QuestionType.SingleChoice:
                    eTestPaperRule.LowTotalQty = SingleChoiceEasySum;
                    eTestPaperRule.MediumTotalQty = SingleChoiceMediumSum;
                    eTestPaperRule.HighTotalQty = SingleChoiceHardSum;
                    break;
                case QuestionType.MultipleChoice:
                    eTestPaperRule.LowTotalQty = MultipleChoiceEasySum;
                    eTestPaperRule.MediumTotalQty = MultipleChoiceMediumSum;
                    eTestPaperRule.HighTotalQty = MultipleChoiceHardSum;
                    break;
                case QuestionType.TextEntry:
                    break;
                case QuestionType.Judgement:
                    eTestPaperRule.LowTotalQty = JudgementEasySum;
                    eTestPaperRule.MediumTotalQty = JudgementMediumSum;
                    eTestPaperRule.HighTotalQty = JudgementHardSum;
                    break;
                case QuestionType.ExtendedText:
                    break;
                case QuestionType.Match:
                    break;
                case QuestionType.Group:
                    break;
                default:
                    break;
            }
            
            iTestPaperRuleLogic.SaveTestPaperRule(eTestPaperRule);
            iTestPaperRuleLogic.UpdateQuestionTypeScore(TestPaperID, eTestPaperRule.QuestionType, eTestPaperRule.QuestionScore);
        }

        ITestPaperLogic iTestPaperLogic = ServiceRepository.TestPaperService;
        IList<TestPaperUnit> testPaperUnitList = iTestPaperLogic.GetTestPaperSchema(TestPaperID, TestPaperType.AdvancedRandom);

        IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
        IList<KeyValuePair<Guid, QuestionType>> selectedQuestions = new List<KeyValuePair<Guid, QuestionType>>();
                
        foreach (TestPaperUnit testPaperUnit in testPaperUnitList)
        {
            foreach (BasePriview eBasePriview in testPaperUnit.QuestionList)
            {
                PaperQuestion ePaperQuestion = new PaperQuestion();

                ePaperQuestion.QuestionID = eBasePriview.QuestionID;
                ePaperQuestion.QuestionScore = eBasePriview.QuestionScore;
                ePaperQuestion.QuestionType = testPaperUnit.QuestionType;
                ePaperQuestion.TestPaperID = TestPaperID;

                iPaperQuestionLogic.AddQuestion(ePaperQuestion);
            }
        }
        //InitLiteral();
        string url = this.ActionHref(string.Format("~/QuestionDB/TestPaper/AddTestPaper.aspx?ExerciseType={0}&ExerciseID={1}",(int)ExerciseType, ExerciseID));

        ETMS.Utility.JsUtility.SuccessMessageBox("提示信息", "策略设置成功！", "function(){window.location='" + url + "'}");
        
    }

    private QuestionType getType(string strType)
    {
        switch (strType)
        {
            case "SingleChoice":
                return QuestionType.SingleChoice;

            case "MultipleChoice":
                return QuestionType.MultipleChoice;

            case "Judgement":
                return QuestionType.Judgement;
            default:
                return QuestionType.Null;
        }
    }
    
}
