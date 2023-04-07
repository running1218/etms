using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 考生某一次考试结束后，显示的结果信息
    /// </summary>
    [Serializable]
    public class UserTestResultStat
    {
        public UserTestResultStat() { }
        /// <summary>
        /// 本次考试最高分
        /// </summary>
        public decimal UserScore { get; set; }
        /// <summary>
        /// 同一试卷考试中最高分
        /// </summary>
        public decimal MaxScoreInSamePaper { get; set; }
        /// <summary>
        /// 已进行的答题次数
        /// </summary>
        public int TestedTimes { get; set; }
        /// <summary>
        /// 剩余可以答题的次数
        /// </summary>
        public int RemainTestTimes { get; set; }
    }
    /// <summary>
    /// 考生指定试卷的考试统计
    /// </summary>
    [Serializable]
    public class UserExamStat
    {
        public UserExamStat() { }

        /// <summary>
        /// 考试最高分
        /// </summary>
        public decimal MaxUserScore { get; set; }
        /// <summary>
        /// 考试最低分
        /// </summary>
        public decimal MinUserScore { get; set; }
        /// <summary>
        /// 已进行考试次数
        /// </summary>
        public int ExamTimes { get; set; }
    }
    /// <summary>
    /// 考生，某一次考试的状态
    /// </summary>
    [Serializable]
    public class UserExamState
    {
        public UserExamState() 
        {
            this.LstQuestionTypeStat = new List<QuestionTypeUserResult>();
        }

        /// <summary>
        /// 试卷总题数
        /// </summary>
        public int TotalQuestionCnt { get; set; }
        /// <summary>
        /// 考生已作答总题数
        /// </summary>
        public int TestedQuestionCnt { get; set; }
        /// <summary>
        /// 考生已用时，单位：秒
        /// </summary>
        public int ElapsedTime { get; set; }
        /// <summary>
        /// 试卷限制答题的总时间,单位：分钟
        /// </summary>
        public int TimeLimit { get; set; }
        /// <summary>
        /// 剩余答题时间。如果试卷不限时，返回-1。单位：秒
        /// </summary>
        public int RemainTime
        {
            get
            {
                if (this.TimeLimit <= 0)
                    return -1;

                int nTmp = this.TimeLimit * 60 - this.ElapsedTime;
                return nTmp <= 0 ? 0 : nTmp;
            }
        }

        /// <summary>
        /// 试卷中各种题型的考生答题的统计结果
        /// </summary>
        public List<QuestionTypeUserResult> LstQuestionTypeStat { get; set; }
        /// <summary>
        /// 考生得分
        /// </summary>
        public decimal UserScore { get; set; }
        /// <summary>
        /// 试卷中考生回答正确的试题数
        /// </summary>
        public int CorrectCount
        {
            get
            {
                if(this.LstQuestionTypeStat==null || this.LstQuestionTypeStat.Count<=0)
                    return 0;

                int nTotal = this.LstQuestionTypeStat.Sum(x => { return x.CorrectCount; });
                return nTotal ;
            }
        }
        /// <summary>
        /// 考生考试交卷时间
        /// </summary>
        public DateTime EndExamTime { get; set; }
        /// <summary>
        /// 考生答题正确率
        /// </summary>
        public float CorrectRate {
            get
            {
                //答题正确率为，考生答对的试题数/试题总数
                if (this.CorrectCount == 0 || this.TotalQuestionCnt == 0)
                    return 0;
                else
                {
                    float nTmp=this.CorrectCount / this.TotalQuestionCnt;
                    return nTmp > 1 ? 1 : nTmp;
                }
            }
        }
        /// <summary>
        /// 未批阅试题数.
        /// </summary>
        public int NotRemarkCount { get; set; }
    }
    /// <summary>
    /// 某一试题类型的考生答题结果
    /// </summary>
    [Serializable]
    public class QuestionTypeUserResult
    {
        /// <summary>
        /// 试题类型名称
        /// </summary>
        public string QuestionTypeName { set; get; }
        /// <summary>
        /// 试题类型枚举
        /// </summary>
        public QuestionType QuestionType { set; get; }
        /// <summary>
        /// 试卷中该类型中所有试题数
        /// </summary>
        public int QuestionsCount { get; set; }
        /// <summary>
        /// 考生答题的试题数
        /// </summary>
        public int CorrectCount { get; set; }
        /// <summary>
        /// 考生答错题数
        /// </summary>
        public int ErrorCount
        {
            get
            {
                int nTmp = this.QuestionsCount - this.CorrectCount;
                return (nTmp >= 0 && nTmp <= this.QuestionsCount) ? nTmp : 0;
            }
        }
    }
}
