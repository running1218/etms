// File:    UserQuestion.cs
// Author:  Administrator
// Created: 2011年12月15日 19:53:18
// Purpose: Definition of Class UserQuestion

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Linq;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// 用户试题
    ///</summary>
    [Serializable]
    public class UserQuestion
    {
        public UserQuestion()
        { }
        public UserQuestion(UserQuestion question)
        {
            this.AnswerStr = question.AnswerStr;
            this.UserAnswerStr = question.UserAnswerStr;

            this.Difficulty = question.Difficulty;
            this.ExamQuestionID = question.ExamQuestionID;
            this.KnowledgePoints = question.KnowledgePoints;
            this.ObjectID = question.ObjectID;
            this.ParentQuestionID = question.ParentQuestionID;
            this.QuestionID = question.QuestionID;
            this.QuestionScore = question.QuestionScore;
            this.QuestionTitle = question.QuestionTitle;
            this.QuestionType = question.QuestionType;
            this.SubItemIndex = question.SubItemIndex;
            this.QuestionNo = question.QuestionNo;

            this.SubItems = question.SubItems;
            this.Subject = question.Subject;
            this.TestPaperID = question.TestPaperID;
            this.UserExamID = question.UserExamID;

        }
        /// <summary>
        /// 试卷生成ID
        /// </summary>
        public Guid ExamQuestionID { get; set; }
        /// <summary>
        /// 考生考试ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// 试卷定义ID
        /// </summary>
        public Guid TestPaperID { get; set; }
        public Guid QuestionID { get; set; }
        public Guid ParentQuestionID { get; set; }
        /// <summary>
        /// 子题序号
        /// </summary>
        public int SubItemIndex { get; set; }
        ///<summary>
        /// 试题类型
        ///</summary>
        public ETMS.Components.Exam.API.Entity.ItemBank.QuestionType QuestionType
        {
            get;
            set;
        }
        /// <summary>
        /// 试题内容（题面）
        /// </summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// 适应对象ID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// 所属学科
        /// </summary>
        public string Subject { get; set; }
        public string KnowledgePoints { get; set; }
        public int Difficulty { get; set; }

        ///<summary>
        /// 试题答案（字符串）
        ///</summary>
        public string AnswerStr
        {
            get;
            set;
        }
        /// <summary>
        /// 考生答案字符串
        /// </summary>
        public string UserAnswerStr { get; set; }
        /// <summary>
        /// 考生答案
        /// </summary>
        public AnswerBase UserAnswer { get; set; }

        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }
        /// <summary>
        /// 试题序号
        /// </summary>
        public int QuestionNo { get; set; }

        ///<summary>
        /// 子题
        ///</summary>
        public IList<TestQuestionBase> SubItems
        {
            get;
            set;
        }
    }

    #region --定义答题显示的各个题型实体类--
    /// <summary>
    /// 考生单选答题
    /// </summary>
    [Serializable]
    public class TestSingleChoiceQuestion : UserQuestion
    {
        public TestSingleChoiceQuestion()
        { }
        public TestSingleChoiceQuestion(UserQuestion commQuestion)
            : base(commQuestion)
        {
        }
        public TestSingleChoiceQuestion(UserQuestion commQuestion, string sAnswer, IList<TestQuestionOption> LstOptions)
            :this(commQuestion)
        {
            this.Options = LstOptions;
            //this.Answer = AnswerBase.Deserialize<SingleChoiceAnswer>(sAnswer);
            this.Answer = new SingleChoiceAnswer(sAnswer);
            this.UserAnswer = new SingleChoiceAnswer(commQuestion.UserAnswerStr);
        }
        /// <summary>
        /// 选择题答案.
        /// </summary>
        public SingleChoiceAnswer Answer { get; set; }

        ///<summary>
        /// 单选题各个选项
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 多选题试题的实体类
    /// </summary>
    [Serializable]
    public class TestMultipleChoiceQuestion : UserQuestion
    {
        public TestMultipleChoiceQuestion()
        { }
        public TestMultipleChoiceQuestion(UserQuestion commQuestion)
            : base(commQuestion)
        {
        }
        public TestMultipleChoiceQuestion(UserQuestion commQuestion, string sAnswer, IList<TestQuestionOption> LstOptions)
            : this(commQuestion)
        {
            this.Options = LstOptions;
            //this.Answer = AnswerBase.Deserialize<MultipleChoiceAnswer>(sAnswer);
            this.Answer = new MultipleChoiceAnswer(sAnswer);
            this.UserAnswer = new MultipleChoiceAnswer(commQuestion.UserAnswerStr);
        }

        /// <summary>
        /// 判断题答案.
        /// </summary>
        public MultipleChoiceAnswer Answer { get; set; }

        ///<summary>
        /// 多选题各个选项
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 判断题试题的实体类
    /// </summary>
    [Serializable]
    public class TestJudgementQuestion : UserQuestion
    {
        public TestJudgementQuestion(UserQuestion commQuestion, string sAnswer, IList<TestQuestionOption> LstOptions)
            :base(commQuestion)
        {
            this.Options = LstOptions;
            //this.Answer = AnswerBase.Deserialize<JudgementAnswer>(sAnswer);
            this.Answer = new JudgementAnswer(sAnswer);
            this.UserAnswer = new JudgementAnswer(commQuestion.UserAnswerStr);
        }

        /// <summary>
        /// 判断题答案.
        /// </summary>
        public JudgementAnswer Answer { get; set; }

        ///<summary>
        /// 判断题各个选项（只允许二个选项）
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 填空题实体类
    /// </summary>
   [Serializable]
    public class TestTextEntryQuestion : UserQuestion
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestTextEntryQuestion()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commQuestion"></param>
        public TestTextEntryQuestion(UserQuestion commQuestion)
            : base(commQuestion)
        {
        }
        public TestTextEntryQuestion(UserQuestion commQuestion, string sAnswer)
            :this(commQuestion)
        {
            //this.Answer = AnswerBase.Deserialize<TextEntryAnswer>(sAnswer);
            this.Answer = new TextEntryAnswer(sAnswer);
            this.UserAnswer = new TextEntryAnswer(commQuestion.UserAnswerStr);
        }

        /// <summary>
        /// 问答题答案.
        /// </summary>
        public TextEntryAnswer Answer { get; set; }
    }

    /// <summary>
    /// 问答题实体类
    /// </summary>
   [Serializable]
    public class TestExtendedTextQuestion : UserQuestion
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestExtendedTextQuestion()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commQuestion"></param>
        public TestExtendedTextQuestion(UserQuestion commQuestion)
            : base(commQuestion)
        {

        }
        public TestExtendedTextQuestion(UserQuestion commQuestion, string sAnswer)
            : this(commQuestion)
        {
            //this.Answer = AnswerBase.Deserialize<ExtendedTextAnswer>(sAnswer);
            this.Answer = new ExtendedTextAnswer(sAnswer);
            this.UserAnswer = new ExtendedTextAnswer(commQuestion.UserAnswerStr);
        }
        /// <summary>
        /// 问答题答案.
        /// </summary>
        public ExtendedTextAnswer Answer { get; set; }
    }

    /// <summary>
    /// 归类题
    /// </summary>
    [Serializable]
    public class TestGroupQuestion : UserQuestion
    {
        /// <summary>
        /// 试题选项组
        /// 归类题：分两组，每组多个选项
        /// </summary>
        public IList<TestOptionGroup> OptionGroups { get; set; }
        public IList<TestQuestionOption> Options { get; set; }

        /// <summary>
        /// 归类题答案.
        /// </summary>
        public GroupAnswer Answer { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestGroupQuestion(UserQuestion commQuestion,string sAnswer,
            IList<TestQuestionOption> LstOptions,IList<TestOptionGroup> LstGroups)
            :base(commQuestion)
        {
            this.OptionGroups = LstGroups;
            this.Options = LstOptions;

            //this.Answer = AnswerBase.Deserialize<GroupAnswer>(sAnswer);
            this.Answer = new GroupAnswer(sAnswer);
            this.UserAnswer = new GroupAnswer(commQuestion.UserAnswerStr);
        }
    }

    /// <summary>
    /// 匹配题
    /// </summary>
    [Serializable]
    public class TestMatchQuestion : UserQuestion
    {

        private IList<TestOptionGroup> _OptionGroups = null;
        private IList<TestQuestionOption> _Options = null;
        /// <summary>
        /// 试题选项组
        /// 匹配题：分多组，如A组 B组 C组等，每组两个选项
        /// </summary>
        public IList<TestOptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// 匹配题答案
        /// </summary>
        public MatchAnswer Answer { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TestMatchQuestion(UserQuestion commQuestion, string sAnswer,
            IList<TestQuestionOption> LstOptions, IList<TestOptionGroup> LstGroups)
            : base(commQuestion)
        {
            //this.CommonQuestion = new CommonQuestion(QuestionType.Match);
            this._OptionGroups = LstGroups;
            
            this._Options = LstOptions;
            //生成OptionGroups
            this.OptionGroups= this.CreateOptionGroups();

            //this.Answer = AnswerBase.Deserialize<MatchAnswer>(sAnswer);
            this.Answer = new MatchAnswer(sAnswer);
            this.UserAnswer = new MatchAnswer(commQuestion.UserAnswerStr);
        }
        /// <summary>
        /// 生成试题选项组对象
        /// </summary>
        /// <returns></returns>
        private IList<TestOptionGroupItem> CreateOptionGroups()
        {
            IList<TestOptionGroupItem> LstGroups = new List<TestOptionGroupItem>();
            if (this._OptionGroups == null || this._OptionGroups.Count <= 0
                || this._Options == null || this._Options.Count <= 0)
                return LstGroups;

            //组合
            foreach (TestOptionGroup itemGroup in this._OptionGroups)
            {
                TestOptionGroupItem oGroupItem = new TestOptionGroupItem();
                oGroupItem.OptionGroup = itemGroup;
                var LstTmp = (from OptionItem in this._Options
                             where OptionItem.OptionGroupTitleID == itemGroup.OptionGroupTitleID
                             select OptionItem).ToList<TestQuestionOption>();
                oGroupItem.Options = LstTmp;
                LstGroups.Add(oGroupItem);
            }
            return LstGroups;
        }
    }

    #endregion

    #region --选项与选项组--
    /// <summary>
    /// 考生答题选项
    /// </summary>
    [Serializable]
    public class TestQuestionOption : QuestionOption
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// 试题原编码
        /// </summary>
        public string QuestionOptionCode { get; set; }
    }
    /// <summary>
    /// 考生答题选项组
    /// </summary>
    [Serializable]
    public class TestOptionGroup : OptionGroup
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid UserExamID { get; set; }
    }
    /// <summary>
    /// 选项组,用于归类题
    /// </summary>
    [Serializable]
    public class TestOptionGroupItem
    {
        /// <summary>
        /// 选项组信息
        /// </summary>
        public TestOptionGroup OptionGroup { get; set; }
        ///<summary>
        /// 所具有的选项
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestOptionGroupItem()
        {
            this.Options = new List<TestQuestionOption>();
        }
    }
    #endregion
}