using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 试卷答题反馈逻辑实现
    /// </summary>
    public class UserExamResultLogic : IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        private static string Err_UserExamResult_Instance_Invalid = "ItemBank.UserExamResult.Instance.Invalid";
        private static string Err_UserExamResult_ExamStarted = "Test.UserExam.ExamStarted";
        private static string Err_UserExamResult_NotExistQuestion = "Test.UserExam.NotExistQuestion";
        private static string Err_UserExamResult_QuestionExisted = "Test.UserExam.QuestionExisted";
        #endregion

        public IUserExamResultDao UserExamResultDao { get; set; }
        public UserTestStatusLogic UserTestStatusLogic { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (UserExamResultDao == null)
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

        #region --属性--
        private IList<UserExamResult> _Results;
        public IList<UserExamResult> Results
        {
            get
            {
                if (this._Results == null || this._Results.Count == 0)
                {
                    this._Results = this.UserExamResultDao.FindAllInUserExam(this.UserExamID);
                }

                return this._Results;
            }
            set
            {
                this._Results = value;
            }
        }
        public Guid UserExamID
        {
            get;
            set;
        }
        /// <summary>
        /// 获取答卷的考生得分
        /// </summary>
        public decimal UserScore
        {
            get
            {
                this.ThrowNotInitializedExeception();
                //得到总分
                if (this.Results == null || this.Results.Count <= 0)
                    return 0;
                return this.Results.Sum<UserExamResult>(item => item.UserScore);
            }
        }

        /// <summary>
        /// 获取答卷的总分
        /// </summary>
        public decimal PaperScore
        {
            get
            {
                this.ThrowNotInitializedExeception();
                if (this.Results == null || this.Results.Count <= 0)
                    return 0;

                return this.Results.Sum<UserExamResult>(item => item.QuestionScore);
            }
        }
        #endregion

        /// <summary>
        /// 获取当前试卷的状态
        /// </summary>
        /// <returns></returns>
        private UserTestStatusType GetTestStatus()
        {
            return GetTestStatus(this.UserExamID);
        }

        private UserTestStatusType GetTestStatus(Guid UserExamID)
        {
            return UserTestStatusLogic.GetTestStatusType(UserExamID);
        }
        /// <summary>
        /// 确认指定的答卷还没有开始考试
        /// </summary>
        /// <param name="UserExamID"></param>
        private void AssertTestNotStartExam(Guid UserExamID)
        {
            UserTestStatusType TestStatus = GetTestStatus(UserExamID);
            if (TestStatus != UserTestStatusType.NotStart)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_ExamStarted);
            }
        }
        /// <summary>
        /// 得到考生答题的统计结果
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public UserExamState GetUserExamState(Guid UserExamID)
        {
            UserExamState oRet = this.UserExamResultDao.GetUserExamState(UserExamID);
            if (oRet != null)
            {
                //得到各种类型的试题数与答对数
                oRet.LstQuestionTypeStat = (List<QuestionTypeUserResult>)this.GetQuestionTypeUserResult(UserExamID);
                //得到未批阅数,非客观题都是未批阅的
                int nNotRemarkCount = 0;
                nNotRemarkCount = oRet.LstQuestionTypeStat.Sum(x =>
                {
                    //当前系统中没有评阅功能，所以将所以填空题与问答题都称为未批阅试题
                    if (x.QuestionType == QuestionType.ExtendedText || x.QuestionType == QuestionType.TextEntry)
                    {
                        return x.QuestionsCount;
                    }
                    return 0;
                });

                oRet.NotRemarkCount = nNotRemarkCount;
            }

            return oRet;
        }
        /// <summary>
        /// 得到试卷中各种类型试题的总数及考生答对数
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public IList<QuestionTypeUserResult> GetQuestionTypeUserResult(Guid UserExamID)
        {
            IList<QuestionTypeUserResult> LstQuestionsType = this.UserExamResultDao.GetQuestionTypeUserResult(UserExamID);
            if (LstQuestionsType != null && LstQuestionsType.Count > 0)
            {
                //根据类型设置类型名称
                Array.ForEach<QuestionTypeUserResult>(LstQuestionsType.ToArray(), x =>
                {
                    x.QuestionTypeName = QuestionTypeHelper.GetQuestionTypeName(x.QuestionType);
                });
            }

            return LstQuestionsType;
        }
        /// <summary>
        /// 加载一个答案的结果包
        /// </summary>
        /// <param name="UserExamID"></param>
        public void LoadUserExamResultPacks(Guid UserExamID)
        {
            //UserExamResultLogic logic = new UserExamResultLogic();
            //延迟加载
            //logic.Results = GetAllInUserExam(UserExamID);
            this.UserExamID = UserExamID;
            this._Results = null;

            //return logic;
        }
        /// <summary>
        /// 为某一试卷生成初始作答结果信息
        /// </summary>
        /// <param name="UserExamID"></param>
        public void CreateExamResultForUserExam(Guid UserExamID)
        {
            UserExamResultDao.CreateExamResultForUserExam(UserExamID);
        }
        /// <summary>
        /// 新建一个新的答卷结果包
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="LstQuestions"></param>
        public UserExamResultLogic CreateNewUserExamResultPacks(Guid UserExamID, IList<UserExamResult> LstQuestions)
        {
            UserExamResultLogic logic = new UserExamResultLogic();
            AssertTestNotStartExam(UserExamID);

            //将所有项添加进去
            foreach (UserExamResult QuestionItem in LstQuestions)
            {
                QuestionItem.ResultID = Guid.NewGuid();
                QuestionItem.UserExamID = UserExamID;

                UserExamResultDao.Add(QuestionItem);
            }

            logic.Results = LstQuestions;
            logic.UserExamID = UserExamID;
            return logic;
        }
        /// <summary>
        /// 删除某一答卷中的所有试题答案信息
        /// </summary>
        /// <param name="UserExamID"></param>
        public void DeleteUserExamResultPacks(Guid UserExamID)
        {
            AssertTestNotStartExam(UserExamID);

            UserExamResultDao.DeleteAllInUserExam(UserExamID);
        }
        /// <summary>
        /// 向答卷结果包中添加一个试题
        /// </summary>
        /// <param name="ResultItem"></param>
        /// <returns></returns>
        public Guid AddQuestionItem(UserExamResult ResultItem)
        {
            ThrowNotInitializedExeception();

            IList<UserExamResult> LstTmps = new List<UserExamResult>();
            LstTmps.Add(ResultItem);

            //检查试题是否已存在
            AssertNewQuestionUniqueInUserExam(LstTmps);

            ResultItem.ResultID = Guid.NewGuid();
            ResultItem.UserExamID = this.UserExamID;

            //添加到数据库中
            UserExamResultDao.Add(ResultItem);
            //添加到本地属性中
            this.Results.Add(ResultItem);

            return ResultItem.ResultID;
        }
        /// <summary>
        /// 向答卷结果包添加多个试题
        /// </summary>
        /// <param name="Questions"></param>
        public void AddQuestions(IList<UserExamResult> Questions)
        {
            ThrowNotInitializedExeception();
            //检查试题是否已存在
            AssertNewQuestionUniqueInUserExam(Questions);

            //分别添加进去
            Array.ForEach<UserExamResult>(
                Questions.ToArray<UserExamResult>(),
                x =>
                {
                    x.ResultID = Guid.NewGuid();
                    x.UserExamID = this.UserExamID;
                    UserExamResultDao.Add(x);
                }
            );

            //添加到本地列表中
            Array.ForEach<UserExamResult>(Questions.ToArray<UserExamResult>(),
                x =>
                {
                    this.Results.Add(x);
                }
            );
        }
        /// <summary>
        /// 更新多个试题结果
        /// </summary>
        /// <param name="Questions"></param>
        public void UpdateQuestions(IList<UserExamResult> Questions)
        {
            ThrowNotInitializedExeception();
        }
        /// <summary>
        /// 更新指定试题的考生答案
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="sAnswer"></param>
        public void UpdateUserAnswer(Guid QuestionID, string sAnswer)
        {
            ThrowNotInitializedExeception();
            UserExamResult QuestionResult = AssertQuestionInUserExam(QuestionID);

            UserExamResultDao.UpdateUserAnswer(this.UserExamID, QuestionID, sAnswer);
            QuestionResult.UserAnswer = sAnswer;
        }

        /// <summary>
        /// 更新指定试题的考生答案
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="sAnswer"></param>
        public void UpdateUserAnswer(Guid ExamID, Guid QuestionID, string sAnswer, decimal userScore)
        {
            ThrowNotInitializedExeception();
            //UserExamResult QuestionResult = AssertQuestionInUserExam(QuestionID);

            UserExamResultDao.UpdateUserAnswer(this.UserExamID, QuestionID, sAnswer, userScore);
            //QuestionResult.UserAnswer = sAnswer;
        }
        /// <summary>
        /// 更新指定试题的考生答案
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="oAnswer"></param>
        public void UpdateUserAnswerObject(Guid QuestionID, AnswerBase oAnswer)
        {
            //转换为字符串
            string sUserAnswer = oAnswer.Answer;
            this.UpdateUserAnswer(QuestionID, sUserAnswer);
        }
        /// <summary>
        /// 对某一指定试题进行评分
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public decimal PingQuestion(Guid QuestionID, out bool bUserCorrect)
        {
            ThrowNotInitializedExeception();
            UserExamResult QuestionResult = AssertQuestionInUserExam(QuestionID);

            bUserCorrect = false;
            //对每一题型进行评分
            decimal userScore = 0;

            try
            {
                IQuestionPing QuestionPing = this.GetQuestionPingService(QuestionResult.QuestionType, QuestionResult.UserAnswer,
                    QuestionResult.QuestionAnswer, QuestionResult.QuestionScore);
                userScore = QuestionPing.Ping(out bUserCorrect);
            }
            catch (Exception ex)
            {
                ;
            }
            //写入考生分数
            UserExamResultDao.UpdateUserQuestionScore(this.UserExamID, QuestionID, userScore);
            QuestionResult.UserScore = userScore;

            return userScore;
        }
        /// <summary>
        /// 对整个答卷进行评分
        /// </summary>
        /// <returns></returns>
        public decimal Ping()
        {
            ThrowNotInitializedExeception();

            decimal totalScore = 0;
            bool bTmp = false;
            foreach (UserExamResult QuestionItem in this.Results)
            {
                totalScore += this.PingQuestion(QuestionItem.QuestionID, out bTmp);
            }

            return totalScore;
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

        public UserExamResult GetUserExamResultByQuestionID(Guid QuestionID)
        {
            var LstTmps = from QuestionItem in this.Results
                          where QuestionItem.QuestionID == QuestionID
                          select QuestionItem;
            if (LstTmps != null && LstTmps.Count<UserExamResult>() > 0)
            {
                return LstTmps.ToList<UserExamResult>()[0];
            }

            return null;
        }
        public UserExamResult AssertQuestionInUserExam(Guid QuestionID)
        {
            UserExamResult Tmp = this.GetUserExamResultByQuestionID(QuestionID);
            if (Tmp == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_NotExistQuestion);
            }
            else
                return Tmp;
        }
        /// <summary>
        /// 得到试卷中已存在试题（重复部分）
        /// </summary>
        /// <param name="Questions"></param>
        /// <returns></returns>
        private IList<UserExamResult> GetIntersetQuestions(IList<UserExamResult> Questions)
        {
            var LstTmps = from QuestionItem in this.Results
                          join Item in Questions
                          on QuestionItem.QuestionID equals Item.QuestionID
                          select QuestionItem;

            return LstTmps.ToList<UserExamResult>();
        }
        private void AssertNewQuestionUniqueInUserExam(IList<UserExamResult> NewQuestions)
        {
            IList<UserExamResult> LstTmps = this.GetIntersetQuestions(NewQuestions);
            if (LstTmps != null && LstTmps.Count > 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_QuestionExisted);
            }
        }

        private bool ValidIsInitialized()
        {
            if (this.UserExamID == null || this.UserExamID == Guid.Empty)
            {
                return false;
            }
            if (this.Results == null)
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
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_Instance_Invalid);
            }
        }
    }

    #region --各种题型评分--
    interface IQuestionPing
    {
        decimal Ping(out bool bUserCorrect);
    }
    abstract class QuestioinPingBase<T> : IQuestionPing
    {
        /// <summary>
        /// 考生答案
        /// </summary>
        public string UserAnswerStr { get; set; }
        /// <summary>
        /// 试题标准答案
        /// </summary>
        public string QuestionAnswerStr { get; set; }
        public decimal QuestionScore { get; set; }

        public T UserAnswer { get; set; }
        public T QuestionAnswer { get; set; }

        public QuestioinPingBase(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
        {
            this.UserAnswerStr = sUserAnswer;
            this.QuestionAnswerStr = sQuestioAnswer;
            this.QuestionScore = QuestionScore;

            //将字符串的答案转为对象
            //this.Init();
        }

        private void Init()
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = AnswerBase.Deserialize<T>(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = AnswerBase.Deserialize<T>(this.QuestionAnswerStr);


            }
        }
        /// <summary>
        /// 评分
        /// </summary>
        /// <returns></returns>
        public abstract decimal Ping(out bool bUserCorrect);
    }
    /// <summary>
    /// 单选题评分
    /// </summary>
    class SingleChoiceQuestionPing : QuestioinPingBase<SingleChoiceAnswer>
    {
        public SingleChoiceQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new SingleChoiceAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new SingleChoiceAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //单选题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null)
                return 0;

            if ((this.UserAnswer.AnswerOption.OptionCode == this.QuestionAnswer.AnswerOption.OptionCode)
                || (this.UserAnswer.AnswerOption.OptionID == this.QuestionAnswer.AnswerOption.OptionID))
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
                return 0;
        }
    }

    /// <summary>
    /// 多选题评分
    /// </summary>
    class MultipleChoiceQuestionPing : QuestioinPingBase<MultipleChoiceAnswer>
    {
        public MultipleChoiceQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new MultipleChoiceAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new MultipleChoiceAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //单选题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null)
                return 0;

            //全对才得分
            if (this.UserAnswer.AnswerOptions == null || this.QuestionAnswer.AnswerOptions == null)
                return 0;
            if (this.UserAnswer.AnswerOptions.Count != this.QuestionAnswer.AnswerOptions.Count)
                return 0;

            //对比二部分的值
            var LstTmps = from UserOption in this.UserAnswer.AnswerOptions
                          join QuestionOption in this.QuestionAnswer.AnswerOptions
                          on UserOption.OptionCode equals QuestionOption.OptionCode
                          select UserOption;
            //联接查询后，还相同就表示与标准答案一致
            if (LstTmps.ToList<QuestionOption>().Count == this.QuestionAnswer.AnswerOptions.Count)
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
            {
                LstTmps = from UserOption in this.UserAnswer.AnswerOptions
                          join QuestionOption in this.QuestionAnswer.AnswerOptions
                          on UserOption.OptionID equals QuestionOption.OptionID
                          select UserOption;
                if (LstTmps.ToList<QuestionOption>().Count == this.QuestionAnswer.AnswerOptions.Count)
                {
                    bUserCorrect = true;
                    return this.QuestionScore;
                }
                return 0;
            }
        }
    }

    /// <summary>
    /// 判断题评分
    /// </summary>
    class JudgementQuestionPing : QuestioinPingBase<JudgementAnswer>
    {
        public JudgementQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new JudgementAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new JudgementAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //判断题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null)
                return 0;

            //如果编码或ID相同都认为是考生做对了。因为手机部分无法设置编码
            if ((this.UserAnswer.AnswerOption.OptionCode == this.QuestionAnswer.AnswerOption.OptionCode)
                ||
                (this.UserAnswer.AnswerOption.OptionID == this.QuestionAnswer.AnswerOption.OptionID))
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
                return 0;
        }
    }

    /// <summary>
    /// 匹配题评分
    /// </summary>
    class MatchQuestionPing : QuestioinPingBase<MatchAnswer>
    {
        public MatchQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new MatchAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new MatchAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //匹配题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null)
                return 0;

            int nYes = 0;   //表示成功匹配的项
            //检查选项组，每一选项组中选项
            foreach (OptionGroupItemAnswer GroupItem in this.QuestionAnswer.OptionGroups)
            {
                //得到其中的一个选项
                if (GroupItem.Options == null || GroupItem.Options.Count <= 0)
                {
                    continue;
                }
                Guid QuestionOptionID = GroupItem.Options[0].OptionID;

                //从考生答案中得到匹配组
                var LstTmps = from UserGroupItem in this.UserAnswer.OptionGroups
                              where UserGroupItem.Options.TakeWhile(x => x.OptionID == QuestionOptionID).Count() > 0
                              select UserGroupItem;
                if (LstTmps == null || LstTmps.Count<OptionGroupItemAnswer>() == 0)
                {
                    continue;
                }
                OptionGroupItemAnswer UserGroupItemTmp = LstTmps.ToList<OptionGroupItemAnswer>()[0];
                //检查其中项是否一致
                if (GroupItem.Options == null || UserGroupItemTmp.Options == null ||
                    GroupItem.Options.Count != UserGroupItemTmp.Options.Count
                    )
                {
                    continue;
                }

                var LstOptionTmps = from OptionTmp in GroupItem.Options
                                    join UserOptionTmp in UserGroupItemTmp.Options
                                    on OptionTmp.OptionID equals UserOptionTmp.OptionID
                                    select OptionTmp;
                if (LstOptionTmps == null || LstOptionTmps.Count<OptionAnswer>() <= 0
                    || LstOptionTmps.Count<OptionAnswer>() != GroupItem.Options.Count)
                {
                    continue;
                }
                nYes++;
            }
            if (nYes == this.QuestionAnswer.OptionGroups.Count)
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
                return 0;
        }
    }

    /// <summary>
    /// 归类题评分
    /// </summary>
    class GroupQuestionPing : QuestioinPingBase<GroupAnswer>
    {
        public GroupQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new GroupAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new GroupAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //归类题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null)
                return 0;

            int nYes = 0;   //表示成功匹配的项
            //检查选项组，每一选项组中选项
            foreach (OptionGroupItemAnswer GroupItem in this.QuestionAnswer.OptionGroups)
            {
                var LstTmps = from UserGroupItem in this.UserAnswer.OptionGroups
                              where UserGroupItem.OptionGroupTitleID == GroupItem.OptionGroupTitleID
                              select UserGroupItem;
                if (LstTmps == null || LstTmps.Count<OptionGroupItemAnswer>() == 0)
                {
                    continue;
                }
                OptionGroupItemAnswer UserGroupItemTmp = LstTmps.ToList<OptionGroupItemAnswer>()[0];
                //检查其中项是否一致
                if (GroupItem.Options == null || UserGroupItemTmp.Options == null ||
                    GroupItem.Options.Count != UserGroupItemTmp.Options.Count
                    )
                {
                    continue;
                }

                var LstOptionTmps = from OptionTmp in GroupItem.Options
                                    join UserOptionTmp in UserGroupItemTmp.Options
                                    on OptionTmp.OptionID.ToString() equals UserOptionTmp.OptionID.ToString()
                                    select OptionTmp;
                if (LstOptionTmps == null || LstOptionTmps.Count<OptionAnswer>() <= 0
                    || LstOptionTmps.Count<OptionAnswer>() != GroupItem.Options.Count)
                {
                    continue;
                }
                nYes++;
            }

            if (nYes == this.QuestionAnswer.OptionGroups.Count)
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
                return 0;
        }
    }

    /// <summary>
    /// 填空题评分
    /// </summary>
    /// <remarks>
    ///  问题题也采用相同的方式
    /// </remarks>
    class TextEntryQuestionPing : QuestioinPingBase<TextEntryAnswer>
    {
        public TextEntryQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new TextEntryAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new TextEntryAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //匹配题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null ||
                this.UserAnswer.AnswerList == null || this.UserAnswer.AnswerList.Count <= 0)
            {
                return 0;
            }

            int nYes = 0;   //表示成功匹配的项
            foreach (TextEntry QuestionTextEntry in this.QuestionAnswer.AnswerList)
            {
                var LstTmps = from UserTextEntry in this.UserAnswer.AnswerList
                              where UserTextEntry.EntryID == QuestionTextEntry.EntryID
                              select UserTextEntry;
                if (LstTmps == null || LstTmps.Count<TextEntry>() <= 0)
                    continue;
                //内容比对
                TextEntry UserTextEntryTmp = LstTmps.ToList<TextEntry>()[0];
                if (UserTextEntryTmp.EntryValue.ToLower() == QuestionTextEntry.EntryValue.ToLower())
                {
                    nYes++;
                }
            }
            if (nYes == this.QuestionAnswer.AnswerList.Count)
            {
                bUserCorrect = true;
                return this.QuestionScore;
            }
            else
                return 0;
        }
    }

    /// <summary>
    /// 填空题评分
    /// </summary>
    /// <remarks>
    ///  问题题也采用相同的方式
    /// </remarks>
    class ExtendedTextQuestionPing : QuestioinPingBase<ExtendedTextAnswer>
    {
        public ExtendedTextQuestionPing(string sUserAnswer, string sQuestioAnswer, decimal QuestionScore)
            : base(sUserAnswer, sQuestioAnswer, QuestionScore)
        {
            if (!string.IsNullOrEmpty(this.UserAnswerStr))
            {
                this.UserAnswer = new ExtendedTextAnswer(this.UserAnswerStr);
            }
            if (!string.IsNullOrEmpty(this.QuestionAnswerStr))
            {
                this.QuestionAnswer = new ExtendedTextAnswer(this.QuestionAnswerStr);
            }
        }

        public override decimal Ping(out bool bUserCorrect)
        {
            bUserCorrect = false;
            //匹配题评分
            if (this.UserAnswer == null || this.QuestionAnswer == null ||
                string.IsNullOrEmpty(this.UserAnswer.Answer))
            {
                return 0;
            }

            bool bYes = false;   //表示成功匹配的项
            if (this.QuestionAnswer.Answer == this.UserAnswer.Answer)
                bYes = true;

            bUserCorrect = bYes;

            return bYes ? this.QuestionScore : 0;
        }
    }
    #endregion
}
