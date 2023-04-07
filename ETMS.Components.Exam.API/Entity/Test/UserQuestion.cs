// File:    UserQuestion.cs
// Author:  Administrator
// Created: 2011��12��15�� 19:53:18
// Purpose: Definition of Class UserQuestion

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Linq;

namespace ETMS.Components.Exam.API.Entity.Test
{
    ///<summary>
    /// �û�����
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
        /// �Ծ�����ID
        /// </summary>
        public Guid ExamQuestionID { get; set; }
        /// <summary>
        /// ��������ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// �Ծ���ID
        /// </summary>
        public Guid TestPaperID { get; set; }
        public Guid QuestionID { get; set; }
        public Guid ParentQuestionID { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int SubItemIndex { get; set; }
        ///<summary>
        /// ��������
        ///</summary>
        public ETMS.Components.Exam.API.Entity.ItemBank.QuestionType QuestionType
        {
            get;
            set;
        }
        /// <summary>
        /// �������ݣ����棩
        /// </summary>
        public string QuestionTitle { get; set; }
        /// <summary>
        /// ��Ӧ����ID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// ����ѧ��
        /// </summary>
        public string Subject { get; set; }
        public string KnowledgePoints { get; set; }
        public int Difficulty { get; set; }

        ///<summary>
        /// ����𰸣��ַ�����
        ///</summary>
        public string AnswerStr
        {
            get;
            set;
        }
        /// <summary>
        /// �������ַ���
        /// </summary>
        public string UserAnswerStr { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public AnswerBase UserAnswer { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public decimal QuestionScore { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public int QuestionNo { get; set; }

        ///<summary>
        /// ����
        ///</summary>
        public IList<TestQuestionBase> SubItems
        {
            get;
            set;
        }
    }

    #region --���������ʾ�ĸ�������ʵ����--
    /// <summary>
    /// ������ѡ����
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
        /// ѡ�����.
        /// </summary>
        public SingleChoiceAnswer Answer { get; set; }

        ///<summary>
        /// ��ѡ�����ѡ��
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// ��ѡ�������ʵ����
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
        /// �ж����.
        /// </summary>
        public MultipleChoiceAnswer Answer { get; set; }

        ///<summary>
        /// ��ѡ�����ѡ��
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// �ж��������ʵ����
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
        /// �ж����.
        /// </summary>
        public JudgementAnswer Answer { get; set; }

        ///<summary>
        /// �ж������ѡ�ֻ�������ѡ�
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
    }

    /// <summary>
    /// �����ʵ����
    /// </summary>
   [Serializable]
    public class TestTextEntryQuestion : UserQuestion
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public TestTextEntryQuestion()
        {
        }

        /// <summary>
        /// ���캯��
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
        /// �ʴ����.
        /// </summary>
        public TextEntryAnswer Answer { get; set; }
    }

    /// <summary>
    /// �ʴ���ʵ����
    /// </summary>
   [Serializable]
    public class TestExtendedTextQuestion : UserQuestion
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public TestExtendedTextQuestion()
        {
        }

        /// <summary>
        /// ���캯��
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
        /// �ʴ����.
        /// </summary>
        public ExtendedTextAnswer Answer { get; set; }
    }

    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public class TestGroupQuestion : UserQuestion
    {
        /// <summary>
        /// ����ѡ����
        /// �����⣺�����飬ÿ����ѡ��
        /// </summary>
        public IList<TestOptionGroup> OptionGroups { get; set; }
        public IList<TestQuestionOption> Options { get; set; }

        /// <summary>
        /// �������.
        /// </summary>
        public GroupAnswer Answer { get; set; }
        /// <summary>
        /// ���캯��
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
    /// ƥ����
    /// </summary>
    [Serializable]
    public class TestMatchQuestion : UserQuestion
    {

        private IList<TestOptionGroup> _OptionGroups = null;
        private IList<TestQuestionOption> _Options = null;
        /// <summary>
        /// ����ѡ����
        /// ƥ���⣺�ֶ��飬��A�� B�� C��ȣ�ÿ������ѡ��
        /// </summary>
        public IList<TestOptionGroupItem> OptionGroups { get; set; }
        /// <summary>
        /// ƥ�����
        /// </summary>
        public MatchAnswer Answer { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public TestMatchQuestion(UserQuestion commQuestion, string sAnswer,
            IList<TestQuestionOption> LstOptions, IList<TestOptionGroup> LstGroups)
            : base(commQuestion)
        {
            //this.CommonQuestion = new CommonQuestion(QuestionType.Match);
            this._OptionGroups = LstGroups;
            
            this._Options = LstOptions;
            //����OptionGroups
            this.OptionGroups= this.CreateOptionGroups();

            //this.Answer = AnswerBase.Deserialize<MatchAnswer>(sAnswer);
            this.Answer = new MatchAnswer(sAnswer);
            this.UserAnswer = new MatchAnswer(commQuestion.UserAnswerStr);
        }
        /// <summary>
        /// ��������ѡ�������
        /// </summary>
        /// <returns></returns>
        private IList<TestOptionGroupItem> CreateOptionGroups()
        {
            IList<TestOptionGroupItem> LstGroups = new List<TestOptionGroupItem>();
            if (this._OptionGroups == null || this._OptionGroups.Count <= 0
                || this._Options == null || this._Options.Count <= 0)
                return LstGroups;

            //���
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

    #region --ѡ����ѡ����--
    /// <summary>
    /// ��������ѡ��
    /// </summary>
    [Serializable]
    public class TestQuestionOption : QuestionOption
    {
        /// <summary>
        /// �Ծ�ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// ����ԭ����
        /// </summary>
        public string QuestionOptionCode { get; set; }
    }
    /// <summary>
    /// ��������ѡ����
    /// </summary>
    [Serializable]
    public class TestOptionGroup : OptionGroup
    {
        /// <summary>
        /// �Ծ�ID
        /// </summary>
        public Guid UserExamID { get; set; }
    }
    /// <summary>
    /// ѡ����,���ڹ�����
    /// </summary>
    [Serializable]
    public class TestOptionGroupItem
    {
        /// <summary>
        /// ѡ������Ϣ
        /// </summary>
        public TestOptionGroup OptionGroup { get; set; }
        ///<summary>
        /// �����е�ѡ��
        ///</summary>
        public IList<TestQuestionOption> Options
        {
            get;
            set;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        public TestOptionGroupItem()
        {
            this.Options = new List<TestQuestionOption>();
        }
    }
    #endregion
}