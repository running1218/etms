using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Utility;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 考生答卷逻辑实现
    /// </summary>
    public class UserExamPaperLogic : IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        private static string Err_UserExam_ExamCompleted = "Test.UserExam.ExamCompleted";
        private static string Err_UserExam_PaperNotExist = "Test.UserExam.PaperNotExist";
        private static string Err_UserExam_OverExamTimes = "Test.UserExam.OverExamTimes";
        private static string Err_UserExam_ExamNotExist = "Test.UserExam.ExamNotExist";
        private static string Err_UserExam_Instance_Invalid = "Test.UserExam.Instance.Invalid";
        private static string Err_UserExam_NotStart = "Test.UserExam.NotStart";
        private static string Err_UserExam_NotTestOver = "Test.Exam.NotTestOver";
        private static string Err_UserExam_QuestionAnswerTypeErr = "Test.UserExam.QuestionAnswerTypeErr";
        private static string Err_UserExam_NotExistQuestion = "Test.UserExam.NotExistQuestion";

        //private static string Err_UserExam_ExamNotExist = "Test.UserExam.ExamNotExist";
        //private static string Err_UserExam_ExamNotExist = "Test.UserExam.ExamNotExist";
        //private static string Err_UserExam_ExamNotExist = "Test.UserExam.ExamNotExist";
        //private static string Err_UserExam_ExamNotExist = "Test.UserExam.ExamNotExist";
        #endregion

        public IUserQuestionDao UserQuestionDao { get; set; }
        public ITestPaperDao TestPaperDao { get; set; }

        public ExamQuestionsLogic QuestionsLogic { get; set; }
        private UserExamResultLogic ResultsLogic { get; set; }
        public UserTestStatusLogic TestStatusLogic { get; set; }
        public UserExamLogic ExamLogic { get; set; }
        public ITestFeedbackLogic TestFeedbackLogic { get; set; }

        public Guid UserExamID { get; set; }
        private ExamPaperSchema _PaperSchema = null;
        public ExamPaperSchema PaperSchema
        {
            get
            {
                if (this._PaperSchema == null)
                {
                    this._PaperSchema = this.LazyLoadPaperSchema(this.UserExamID);
                }
                return this._PaperSchema;
            }
        }

        private IList<UserQuestion> _Questions;
        public IList<UserQuestion> Questions
        {
            get
            {
                /* edit zhangsz 2013-09-03 把用户试题存入缓存中，以UserExamID为key
                缓存10分钟 */
                this._Questions = (List<UserQuestion>)CacheHelper.Get(UserExamID.ToString());
                if (this._Questions == null)
                {
                    this._Questions = QuestionsLogic.LoadQuestions(UserExamID);
                    CacheHelper.Add(UserExamID.ToString(), this._Questions, new TimeSpan(0, 10, 0));
                }
                return this._Questions;
            }
        }

        private UserExam _UserExam = null;
        public UserExam UserExam
        {
            get
            {
                if (this._UserExam == null)
                {
                    //得到值
                    this._UserExam = ExamLogic.GetUserExamByUserExamID(UserExamID);
                }

                return this._UserExam;
            }
        }

        public UserTestStatusType TestStatus
        {
            get
            {
                return this._TestStatus;
            }
        }
        /// <summary>
        /// 考生考试剩余时间
        /// </summary>
        public int RemainingTime
        {
            get
            {
                this.ThrowNotInitializedExeception();
                if (!this.UserExam.TimeLimit.HasValue)
                    return -1;

                int nTmp = 0;
                nTmp = this.UserExam.TimeLimit.Value * 60 - this.UserExam.ElapsedTime;
                return nTmp <= 0 ? 0 : nTmp;
            }
        }

        /// <summary>
        /// 试卷中试题ID列表
        /// </summary>
        private List<Guid> _LstQuestionsID = new List<Guid>();
        /// <summary>
        /// 考生答题状态
        /// </summary>
        private UserTestStatusType _TestStatus = UserTestStatusType.NotStart;
        private IList<TestFeedback> _TestFeedbacks = null;
        public IList<TestFeedback> TestFeedbacks
        {
            get
            {
                if (this._TestFeedbacks == null)
                {
                    TestFeedbackLogic.Load(this.UserExam.TestPaperID);
                    this._TestFeedbacks = TestFeedbackLogic.Feedbacks;
                }
                return this._TestFeedbacks;
            }
        }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (UserQuestionDao == null)
            {
                throw new Exception("please set TestFeedbackDao Property First!");
            }
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 得到某一考试答卷逻辑
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public void LoadExamPaper(Guid UserExamID)
        {
            this.ResetSelf();
            this.UserExamID = UserExamID;

            //初始作答结果的逻辑功能
            this.ResultsLogic.LoadUserExamResultPacks(UserExamID);

            //尽可能采用延迟加载的方法，不是一次性加载完成，而是在需要时再加载
            this._TestStatus = TestStatusLogic.GetTestStatusType(UserExamID);
        }
        /// <summary>
        /// 重置自己
        /// </summary>
        private void ResetSelf()
        {
            this._TestFeedbacks = null;
            this._Questions = null;
            this._UserExam = null;
            this._PaperSchema = null;
        }
        /// <summary>
        /// 为某一指定的试卷生成原始的结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        public void CreateExamResultForUserExam(Guid UserExamID)
        {
            this.ResultsLogic.CreateExamResultForUserExam(UserExamID);
        }

        /// <summary>
        /// 加载生成试卷的结构信息
        /// </summary>
        /// <param name="UserExamID"></param>
        private ExamPaperSchema LazyLoadPaperSchema(Guid UserExamID)
        {
            //形成一个试卷的结构信息
            ExamPaperSchema oPaperSchema = new ExamPaperSchema();

            //形成各个层次
            oPaperSchema.UserExam = this.UserExam;
            oPaperSchema.TestPaper = TestPaperDao.GetByID(this.UserExam.TestPaperID);

            Dictionary<int, string> LstChinaNum = new Dictionary<int, string>();
            LstChinaNum.Add(1, "一");
            LstChinaNum.Add(2, "二");
            LstChinaNum.Add(3, "三");
            LstChinaNum.Add(4, "四");
            LstChinaNum.Add(5, "五");
            LstChinaNum.Add(6, "六");
            LstChinaNum.Add(7, "七");
            LstChinaNum.Add(8, "八");
            LstChinaNum.Add(9, "九");
            LstChinaNum.Add(10, "十");

            //试题显示顺序,单选题、多选题、问答题、匹配题、归类题、判断题、填空题
            Dictionary<QuestionType, string> LstQuestionTypeNo = new Dictionary<QuestionType, string>();
            LstQuestionTypeNo.Add(QuestionType.SingleChoice, "单选题");
            LstQuestionTypeNo.Add(QuestionType.MultipleChoice, "多选题");
            LstQuestionTypeNo.Add(QuestionType.ExtendedText, "问答题");
            LstQuestionTypeNo.Add(QuestionType.Match, "匹配题");
            LstQuestionTypeNo.Add(QuestionType.Group, "归类题");
            LstQuestionTypeNo.Add(QuestionType.Judgement, "判断题");
            LstQuestionTypeNo.Add(QuestionType.TextEntry, "填空题");
            //分成不同的模块，按一定的顺序来定义
            int nModuleNo = 1;
            foreach (KeyValuePair<QuestionType, string> QuestionTypeItem in LstQuestionTypeNo)
            {
                var LstTmps = from QuestionItem in this.Questions
                              where QuestionItem.QuestionType == QuestionTypeItem.Key
                              orderby QuestionItem.QuestionNo
                              select QuestionItem;

                if (LstTmps != null && LstTmps.Count() > 0)
                {
                    Section oSection = new Section(string.Format("第{0}模块 {1}", LstChinaNum[nModuleNo], QuestionTypeItem.Value),
                        LstTmps.ToList<UserQuestion>());
                    //添中到结构中
                    oPaperSchema.Sections.Add(oSection);

                    nModuleNo++;
                }
            }

            return oPaperSchema;
        }

        /// <summary>
        /// 开始（或继续）考试
        /// </summary>
        public void StartTest()
        {
            //检查状态，只要是未交卷的，都可以开始考试
            //写开始考试时间，
            if (TestStatus == UserTestStatusType.TestOver || TestStatus == UserTestStatusType.Marked)
            {
                throw new ETMS.AppContext.BusinessException("Err_UserExam_ExamCompleted");
            }
            //开始考试
            this.ExamLogic.UpdateStartExamDateTime(this.UserExamID);
            //更改考试状态
            TestStatusLogic.UpdateTestStatusType(this.UserExamID, UserTestStatusType.Testing);
        }

        /// <summary>
        /// 检查考生是否允许进行某一次试卷定义的考试
        /// </summary>
        /// <remarks>
        /// 1,检查指定的试卷是否存在；<br></br>
        /// 2,检查考生的考试次数是否超过指定的最大次数;<br></br>
        /// 3,如果未通过验证，将抛出异常；
        /// </remarks>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        public void ValidateUserExamsTimes(int UserID, Guid TestPaperID)
        {
            TestPaper oTestPaper = TestPaperDao.GetByID(TestPaperID);
            if (oTestPaper == null)
            {
                throw new ETMS.AppContext.BusinessException("Err_UserExam_PaperNotExist");
            }

            int nTimes = this.ExamLogic.FindAllUserExamsCountFor(UserID, TestPaperID);
            if (nTimes <= 0)
                return;
            //得到试题定义，判断次数是否超出
            if (oTestPaper.MaxCount <= nTimes)
            {
                throw new ETMS.AppContext.BusinessException("Err_UserExam_OverExamTimes");
            }
        }
        ///<summary>
        /// 得到指定考生的测试信息
        ///</summary>
        /// <param name="studentID">考生ID</param>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testState">考试状态</param>
        public IList<StudentTestView> FindStudentTests(int studentID, Guid testPaperID, UserTestStatusType testState,
            int pageSize, int pageIndex, out int totalCount)
        {
            return this.ExamLogic.FindStudentTests(studentID, testPaperID, testState,
                pageSize, pageIndex, out totalCount);
        }
        public StudentTestView GetUserLastTest(int studentID, Guid testPaperID, UserTestStatusType testState, bool IsPreview)
        {
            return this.ExamLogic.GetUserLastTest(studentID, testPaperID, testState, IsPreview);
        }
        /// <summary>
        /// 得到考生，指定试卷的所有考试的统计信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TestPaperID"></param>
        /// <returns></returns>
        public UserExamStat GetUserExamStat(int UserID, Guid TestPaperID)
        {
            return this.ExamLogic.GetUserExamStat(UserID, TestPaperID);
        }
        /// <summary>
        /// 得到某一次考试结束后，显示的统计结果
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public UserTestResultStat GetUserTestResultStat()
        {
            UserTestResultStat oStat = new UserTestResultStat();
            int nTotalTimes = 0;
            if (this.UserExam == null)
            {
                throw new ETMS.AppContext.BusinessException("Err_UserExam_ExamNotExist");
            }

            TestPaper oTestPaper = TestPaperDao.GetByID(this.UserExam.TestPaperID);
            if (oTestPaper == null)
            {
                throw new ETMS.AppContext.BusinessException("Err_UserExam_PaperNotExist");
            }

            nTotalTimes = oTestPaper.MaxCount;
            oStat.RemainTestTimes = nTotalTimes;
            oStat.UserScore = this.UserExam.ExamScore;
            //得到某一次考试结果，
            //得到同一试卷的考试信息
            UserExamStat oExamStat = this.GetUserExamStat(this.UserExam.UserID,
                this.UserExam.TestPaperID);
            if (oExamStat != null)
            {
                oStat.MaxScoreInSamePaper = oExamStat.MaxUserScore;
                oStat.TestedTimes = oExamStat.ExamTimes;
                oStat.RemainTestTimes = nTotalTimes - oExamStat.ExamTimes;
            }
            return oStat;
        }

        public UserExamState GetUserExamState()
        {
            //得到考生答题的信息
            UserExamState oUserExamState = this.ResultsLogic.GetUserExamState(this.UserExamID);
            if (oUserExamState != null)
            {
                //得到其它信息
                oUserExamState.TimeLimit = this.UserExam.TimeLimit.HasValue ? this.UserExam.TimeLimit.Value : 0;
                oUserExamState.ElapsedTime = this.UserExam.ElapsedTime;

                //得到考生交卷时间
                oUserExamState.EndExamTime = this.UserExam.EndExamTime;
                //得到考生得分
                oUserExamState.UserScore = this.UserExam.ExamScore;
            }
            return oUserExamState;
        }
        ///<summary>
        /// 更新试题答案
        ///</summary>
        /// <param name="userTestPaperID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <param name="userAnswer">考生答案</param>
        /// <param name="nCurQuestionNo">当前试题的序号</param>
        /// <param name="totalTestTime">考试截止答该题时总用时（单位：秒）</param>
        /// <param name="isFeedback">是否需要评分，并返回答案反馈</param>
        /// <param name="lstFeedbacks">试题的答题反馈</param>
        /// <param name="lstOptionFeedbacks">试题的选项反馈</param>
        /// <remarks>考生该题得分。只有在评分时，才会得到正确的分数</remarks>
        public decimal UpdateQuestionAnswer(System.Guid questionID, AnswerBase userAnswer, int nCurQuestionNo,
            int totalTestTime, bool isFeedback,
            out string sQuestionFeedback,
            out IList<OptionFeedback> lstOptionFeedbacks)
        {
            sQuestionFeedback = "";
            lstOptionFeedbacks = null;
            decimal UserScore = 0;

            //初始化
            this.ThrowNotInitializedExeception();
            //判断状态
            AssertStartedTest();

            //判断答案实例是否正确
            //AssertAnswerType(userAnswer, oQuestion);
            //写入答案，时间
            //this.ResultsLogic.UpdateUserAnswerObject(questionID, userAnswer);
            bool temp = false;

            PingQuestion(questionID, userAnswer.Answer, out temp);


            this.ExamLogic.UpdateExamElapsedTime(this.UserExamID, totalTestTime, nCurQuestionNo);
            //组织反馈,可调用GetQuestionFeedback()
            if (isFeedback)
            {
                //评分得到反馈
                sQuestionFeedback = this.GetQuestionFeedback(questionID, out UserScore, out lstOptionFeedbacks);
            }

            return UserScore;
        }

        /// <summary>
        /// 对某一指定试题进行评分
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        private decimal PingQuestion(Guid QuestionID, string UserAnswer, out bool bUserCorrect)
        {
            ThrowNotInitializedExeception();

            UserExamResult p = this.ResultsLogic.AssertQuestionInUserExam(QuestionID);

            bUserCorrect = false;
            //对每一题型进行评分
            decimal userScore = 0;

            try
            {
                IQuestionPing QuestionPing = this.GetQuestionPingService(p.QuestionType, UserAnswer, p.QuestionAnswer, p.QuestionScore);
                userScore = QuestionPing.Ping(out bUserCorrect);
            }
            catch (Exception ex)
            {
                ;
            }
            //写入考生分数
            //this.ResultsLogic.up
            this.ResultsLogic.UpdateUserAnswer(this.UserExamID, QuestionID, UserAnswer, userScore);
            //QuestionResult.UserScore = userScore;

            return userScore;
        }

        /// <summary>
        /// 根据试题类型来创建不同的试题题型逻辑接口
        /// </summary>
        /// <param name="questionType"></param>
        /// <returns></returns>
        private IQuestionPing GetQuestionPingService(QuestionType questionType, string sUserAnswer,
            string sQuestioAnswer, decimal QuestionScore)
        {
            switch (questionType)
            {
                case QuestionType.SingleChoice:
                    return (IQuestionPing)(new SingleChoiceQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.MultipleChoice:
                    return (IQuestionPing)(new MultipleChoiceQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.Judgement:
                    return (IQuestionPing)(new JudgementQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.Match:
                    return (IQuestionPing)(new MatchQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.Group:
                    return (IQuestionPing)(new GroupQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.TextEntry:
                    return (IQuestionPing)(new TextEntryQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                case QuestionType.ExtendedText:
                    return (IQuestionPing)(new ExtendedTextQuestionPing(sUserAnswer, sQuestioAnswer, QuestionScore));
                default:
                    throw new NotImplementedException("暂不支持的题型");
            }
        }



        /// <summary>
        /// 检查答案的实例是否与试题的类型相一致
        /// </summary>
        /// <param name="oUserAnswer">考生答案实例</param>
        /// <param name="oQuestionType">试题类型</param>
        private void AssertAnswerType(AnswerBase oUserAnswer, QuestionType oQuestionType)
        {
            bool bErr = false;
            switch (oQuestionType)
            {
                case QuestionType.SingleChoice:
                    bErr = oUserAnswer is SingleChoiceAnswer;
                    break;
                case QuestionType.MultipleChoice:
                    bErr = oUserAnswer is MultipleChoiceAnswer;
                    break;
                case QuestionType.Judgement:
                    bErr = oUserAnswer is JudgementAnswer;
                    break;
                case QuestionType.TextEntry:
                    bErr = oUserAnswer is TextEntryAnswer;
                    break;
                case QuestionType.ExtendedText:
                    bErr = oUserAnswer is ExtendedTextAnswer;
                    break;
                case QuestionType.Match:
                    bErr = oUserAnswer is MatchAnswer;
                    break;
                case QuestionType.Group:
                    bErr = oUserAnswer is GroupAnswer;
                    break;
            }
            if (!bErr)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExam_QuestionAnswerTypeErr);
            }
        }
        ///<summary>
        /// 得到试卷中某一试题的答题反馈，包括选项反馈
        ///</summary>
        /// <param name="userTestPaperID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        /// <param name="UserScore">考生得分</param>
        /// <param name="lstOptionFeedbacks">试题选项反馈</param>
        public string GetQuestionFeedback(System.Guid questionID, out decimal UserScore,
            out IList<OptionFeedback> lstOptionFeedbacks)
        {
            lstOptionFeedbacks = null;
            bool bUserCorrect = false;

            UserExamResult oExamResult = this.ResultsLogic.GetUserExamResultByQuestionID(questionID);

            //判断是需要再次评分,课中试卷中的试题为0分，所以每次都需要重新评分
            if (this.TestStatus != UserTestStatusType.Marked || oExamResult.QuestionScore == 0)
            {
                //评分,并保存分数
                decimal userSocre = this.ResultsLogic.PingQuestion(questionID, out bUserCorrect);
            }

            //得到试题反馈（选项反馈和试题本身反馈）
            if (oExamResult.QuestionScore != 0)
            {
                bUserCorrect = (oExamResult.UserScore == oExamResult.QuestionScore);
            }

            UserScore = oExamResult.UserScore;

            string sQuestionFeedback = "";
            //得到试题选项反馈和试题反馈
            lstOptionFeedbacks = this.GetUserFeedbackByQuestionID(bUserCorrect,
                questionID, out sQuestionFeedback);

            return sQuestionFeedback;
        }

        ///<summary>
        /// 得到考生某一试题测试的结果
        ///</summary>
        /// <param name="questionID">试题ID，如果为Guid.Empty表示得到所有试题。</param>
        /// <param name="isFeedback">是否需要包含试题反馈</param>
        public ExamResult GetExamResult(Guid questionID, bool isFeedback)
        {
            string sQuestionFeedback = "";
            //得到答案
            UserExamResult oExamResult = this.ResultsLogic.GetUserExamResultByQuestionID(questionID);
            if (oExamResult == null)
                return null;

            UserQuestion oQuestion = this.GetQuestion(questionID);
            if (oQuestion == null)
                return null;

            ExamResult oResultRet = new ExamResult();
            oResultRet.QuestionID = questionID;
            oResultRet.QuestionType = oQuestion.QuestionType;
            oResultRet.UserAnswer = this.GetQuestionUserAnswer(questionID);
            oResultRet.QuestionAnswer = this.CreateQuestionTypeAnswer(oExamResult.QuestionAnswer, oQuestion.QuestionType);
            oResultRet.QuestionScore = oQuestion.QuestionScore;

            //判断是需要再次评分
            IList<OptionFeedback> LstOptionFeedbacks = null;
            decimal userScore = 0;
            sQuestionFeedback = this.GetQuestionFeedback(questionID, out userScore, out LstOptionFeedbacks);
            if (!isFeedback)
            {
                sQuestionFeedback = "";
                LstOptionFeedbacks = null;
                oResultRet.OptionFeedbacks = LstOptionFeedbacks;
                oResultRet.QuestionFeedback = sQuestionFeedback;
            }
            oResultRet.UserScore = oExamResult.UserScore;

            return oResultRet;
        }
        #region --根据考生答案得到试题及选项反馈--
        /// <summary>
        /// 根据考生答案得到试题的反馈
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="LstQuestionFeedbacks"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetUserFeedbackByQuestionID(bool bUserCorrect, Guid QuestionID,
            out string QuestionFeedbacks)
        {
            QuestionFeedbacks = "";
            IList<OptionFeedback> LstRet = null;

            //得到试题
            UserQuestion oQuestion = this.GetQuestion(QuestionID);
            if (oQuestion == null)
                return null;

            //得到试题的反馈
            IList<QuestionFeedback> LstAllQuestionsFeedback = this.QuestionsLogic.GetQuestionFeedbackByQuestion(this.UserExamID, QuestionID);

            //只有单选题、多选题类型存在选项反馈
            if (oQuestion.QuestionType == QuestionType.SingleChoice || oQuestion.QuestionType == QuestionType.MultipleChoice)
            {
                //得到选项反馈
                IList<OptionFeedback> LstAllOptions = this.QuestionsLogic.GetOptionFeedbackByQuestion(this.UserExamID, QuestionID);
                if (LstAllOptions != null)
                {
                    //依答案，得到一个合适的选项反馈信息
                    AnswerBase oUserAnswer = this.GetQuestionUserAnswer(QuestionID);
                    LstRet = this.GetOptionFeedbacksByUserAnswer(oUserAnswer, LstAllOptions);
                }
            }

            if (LstAllQuestionsFeedback != null && LstAllQuestionsFeedback.Count > 0)
            {
                //依答案，得到一个合适的试题反馈信息
                QuestionFeedbacks = bUserCorrect ? LstAllQuestionsFeedback[0].RightContent : LstAllQuestionsFeedback[0].WrongContent;
            }

            return LstRet;
        }
        /// <summary>
        /// 根据答案从试题的选项反馈列表中得到合适的反馈信息
        /// </summary>
        /// <param name="sAnswer"></param>
        /// <param name="LstOptionFeedbacks"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetOptionFeedbacksByUserAnswer(AnswerBase oUserAnswer,
            IList<OptionFeedback> LstOptionFeedbacks)
        {
            if (oUserAnswer is SingleChoiceAnswer || oUserAnswer is MultipleChoiceAnswer)
            { }
            else
                return null;
            if (LstOptionFeedbacks == null || LstOptionFeedbacks.Count <= 0)
                return null;

            IList<QuestionOption> LstUserAnswer = new List<QuestionOption>();
            if (oUserAnswer is SingleChoiceAnswer)
            {
                SingleChoiceAnswer oSingleAnswer = (SingleChoiceAnswer)oUserAnswer;
                LstUserAnswer.Add(oSingleAnswer.AnswerOption);
            }
            if (oUserAnswer is MultipleChoiceAnswer)
            {
                LstUserAnswer = ((MultipleChoiceAnswer)oUserAnswer).AnswerOptions;
            }

            if (LstUserAnswer == null || LstUserAnswer.Count <= 0)
                return null;

            //进行一下排序
            var LstUserTmps = (from item in LstUserAnswer
                               orderby item.OptionCode
                               select item).ToList<QuestionOption>();

            string sAnswer = "";
            foreach (QuestionOption AnswerOption in LstUserTmps)
            {
                sAnswer += string.IsNullOrEmpty(sAnswer) ? "" : ("," + AnswerOption.OptionCode);
            }

            var LstTmps = from item in LstOptionFeedbacks
                          where item.Options.ToUpper() == sAnswer.ToUpper()
                          select item;
            if (LstTmps == null || LstTmps.Count<OptionFeedback>() <= 0)
                return null;

            return LstTmps.ToList<OptionFeedback>();
        }
        #endregion
        ///<summary>
        /// 得到考生考试的试卷反馈信息
        ///</summary>
        public IList<TestFeedback> GetPaperFeedback()
        {
            IList<TestFeedback> LstFeedbacks = new List<TestFeedback>();

            this.ThrowNotInitializedExeception();
            int nStatus = (int)this.TestStatus;
            //判断是否已提交，
            if (nStatus < (int)UserTestStatusType.TestOver)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExam_NotTestOver);
            }
            decimal userScore = 0;
            //是否已评分，
            if (nStatus < (int)UserTestStatusType.Marked)
            {
                //未评分，完成评分
                userScore = this.ResultsLogic.Ping();
                ExamLogic.UpdateUserScore(this.UserExamID, userScore);
                this.UpdateTestStatus(UserTestStatusType.Marked);
            }
            //得到分数
            userScore = this.ResultsLogic.UserScore;

            //根据分数得到反馈，看分数落在那个反馈中
            if (this.TestFeedbacks != null && this.TestFeedbacks.Count > 0)
            {
                TestFeedback Feedback = this.TestFeedbacks.First<TestFeedback>(x => userScore >= x.BeginScore && userScore <= x.EndScore);
                if (Feedback != null)
                    LstFeedbacks.Add(Feedback);
            }

            return LstFeedbacks;
        }

        ///<summary>
        /// 提交试卷
        ///</summary>
        /// <param name="isReturnResult">是否返回结果</param>
        /// <param name="userScore">考试分数</param>
        public void SubmitPaper(bool isReturnResult, out decimal userScore)
        {
            userScore = 0;
            this.ThrowNotInitializedExeception();
            //modify hjy 修改了提价试卷平凡打开数据库的性能
            //完成交卷
            //this.ExamLogic.TestOver(this.UserExamID);
            //this.UpdateTestStatus(UserTestStatusType.TestOver);

            ////进行评分 修改部分 2013-8-30
            ////userScore = ResultsLogic.Ping();
            //ExamLogic.UpdateUserScore(this.UserExamID, userScore);

            //this.UpdateTestStatus(UserTestStatusType.Marked);


            this.UpdateUserScoreOver(UserTestStatusType.Marked);
            if (!isReturnResult)
            {
                userScore = 0;
            }
        }

        ///<summary>
        /// 得到试卷中指定试题的解题思路
        ///</summary>
        /// <param name="questionID">试题ID</param>
        public string GetQuestionSolution(System.Guid questionID)
        {
            this.ThrowNotInitializedExeception();

            QuestionExtend oTmp = QuestionsLogic.GetQuestionExtendByQuestionID(this.UserExamID, questionID);
            if (oTmp != null)
                return oTmp.Solution;

            return "";
        }

        ///<summary>
        /// 得到考生考试试题信息，包括试题的题面，选项，小题，考生答案等。
        ///</summary>
        /// <param name="userTestPaperID">考生考试ID</param>
        /// <param name="questionID">试题ID</param>
        public UserQuestion GetQuestion(System.Guid questionID)
        {
            UserQuestion Assess = null;
            //判断试题是否存在，
            this.ThrowNotInitializedExeception();
            //this.AssertExistQuestion(questionID);

            Assess = this.Questions.First<UserQuestion>(x => x.QuestionID == questionID);
            //设置考生答案
            Assess.UserAnswer = GetQuestionUserAnswer(questionID);

            return Assess;
        }

        /// <summary>
        /// 用于更新答题状态
        /// </summary>
        /// <param name="NewTestStatus"></param>
        private void UpdateTestStatus(UserTestStatusType NewTestStatus)
        {
            TestStatusLogic.UpdateTestStatusType(this.UserExamID, NewTestStatus);
            this._TestStatus = NewTestStatus;
        }


        /// <summary>
        /// 用于更新答题状态
        /// </summary>
        /// <param name="NewTestStatus"></param>
        private void UpdateUserScoreOver(UserTestStatusType NewTestStatus)
        {
            TestStatusLogic.UpdateUserScoreOver(this.UserExamID, NewTestStatus);
            this._TestStatus = NewTestStatus;
        }

        private AnswerBase GetQuestionUserAnswer(Guid QuestionID)
        {
            this.ThrowNotInitializedExeception();
            AnswerBase oAnswer = null;

            string sAnswer = "";
            UserExamResult oResult = this.ResultsLogic.GetUserExamResultByQuestionID(QuestionID);
            //得到答案字符串，
            if (oResult == null)
                return null;

            sAnswer = oResult.UserAnswer;
            oAnswer = this.CreateQuestionTypeAnswer(sAnswer, oResult.QuestionType);

            return oAnswer;
        }
        private AnswerBase CreateQuestionTypeAnswer(string sAnswer, QuestionType oQuestionType)
        {
            AnswerBase oAnswer = null;
            //转换为特定类型的答案实例
            if (string.IsNullOrEmpty(sAnswer))
                return null;
            switch (oQuestionType)
            {
                case QuestionType.SingleChoice:
                    oAnswer = new SingleChoiceAnswer(sAnswer);
                    break;
                case QuestionType.MultipleChoice:
                    oAnswer = new MultipleChoiceAnswer(sAnswer);
                    break;
                case QuestionType.Judgement:
                    oAnswer = new JudgementAnswer(sAnswer);
                    break;
                case QuestionType.TextEntry:
                    oAnswer = new TextEntryAnswer(sAnswer);
                    break;
                case QuestionType.ExtendedText:
                    oAnswer = new ExtendedTextAnswer(sAnswer);
                    break;
                case QuestionType.Match:
                    oAnswer = new MatchAnswer(sAnswer);
                    break;
                case QuestionType.Group:
                    oAnswer = new GroupAnswer(sAnswer);
                    break;
            }

            return oAnswer;
        }
        private void AssertExistQuestion(Guid QuestionID)
        {
            if (!_LstQuestionsID.Contains(QuestionID))
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExam_NotExistQuestion);
            }
        }
        /// <summary>
        /// 确认已开始考试
        /// </summary>
        private void AssertStartedTest()
        {
            int nStatus = (int)this.TestStatus;
            if (nStatus <= (int)UserTestStatusType.NotStart)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExam_NotStart);
            }
        }
        private bool ValidIsInitialized()
        {
            if (this.UserExamID == null || this.UserExamID == Guid.Empty)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 检查实例中数据是否完整，不完整直接抛出异常。
        /// </summary>
        private void ThrowNotInitializedExeception()
        {
            bool bIsInit = this.ValidIsInitialized();
            if (!bIsInit)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExam_Instance_Invalid);
            }
        }




    }
}
