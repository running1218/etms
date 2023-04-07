using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷的一个结构信息
    /// </summary>
    [Serializable]
    public class ExamPaperSchema
    {
        public ExamPaperSchema() 
        {
            this.Sections = new List<Section>();
        }

        /// <summary>
        /// 试卷定义信息
        /// </summary>
        public TestPaper TestPaper { get; set; }
        /// <summary>
        /// 试卷本身信息
        /// </summary>
        public UserExam UserExam { get; set; }
        /// <summary>
        /// 考生考试剩余时间
        /// </summary>
        public int RemainingTime
        {
            get
            {
                if (this.UserExam == null)
                    return 0;

                if (!this.UserExam.TimeLimit.HasValue)
                    return -1;

                int nTmp = 0;
                nTmp = this.UserExam.TimeLimit.Value * 60  - this.UserExam.ElapsedTime;
                return nTmp <= 0 ? 0 : nTmp;
            }
        }

        /// <summary>
        /// 试卷中各个模块信息
        /// </summary>
        public IList<Section> Sections { get; set; }
    }
    /// <summary>
    /// 试卷中的章节部分（用于表示模块信息） 
    /// </summary>
    [Serializable]
    public  class Section
    {
        public Section() 
        {
            this.Assessments = new List<Assessment>();
        }

        public Section(string sTitle, IList<UserQuestion> LstQuestions)
        {
            this.Title = sTitle;

            this.Assessments = new List<Assessment>();
            foreach (UserQuestion item in LstQuestions)
            {
                Assessment ass = new Assessment(item);
                this.Assessments.Add(ass);
            }
        }
        /// <summary>
        /// 模块名称信息
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 模块中各个试题信息
        /// </summary>
        public IList<Assessment> Assessments { get; set; }
    }

    /// <summary>
    /// 试卷中的试题
    /// </summary>
    [Serializable]
    public class Assessment
    {
        public Assessment()
        { }

        public Assessment(UserQuestion question)
        {
            this.QuestionType = question.QuestionType;
            this.Score = question.QuestionScore;
            this.QuestionNo = question.QuestionNo;
            this.QuestionID = question.QuestionID;
        }
        /// <summary>
        /// 试题类型
        /// </summary>
        public QuestionType QuestionType { get; set; }
        /// <summary>
        /// 试题的ID
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 试题在试卷中序号
        /// </summary>
        public int QuestionNo { get; set; }
    }
}
