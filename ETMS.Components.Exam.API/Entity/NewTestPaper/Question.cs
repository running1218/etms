using System;
using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.NewTestPaper
{
    /// <summary>
    /// 试题实体类 add 2013-9-26 hjy
    /// </summary>
    public class Question
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperID { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 试题名称
        /// </summary>
        public string QuestionTitle { get; set; }

        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }

        /// <summary>
        /// 试题类型
        /// </summary>
        public int QuestionType { get; set; }

        /// <summary>
        /// 试题答案
        /// </summary>
        public string QuestionAnswer { get; set; }

        /// <summary>
        /// 试题顺序号
        /// </summary>
        public int ItemSequence { get; set; }

        /// <summary>
        /// 考生答案
        /// </summary>
        public string UserAnswer { get; set; }

        /// <summary>
        /// 考生得分
        /// </summary>
        public decimal UserScore { get; set; }

 
        /// <summary>
        /// 试题选项信息
        /// </summary>
        public List<Option> QuestionOption { get; set; }

    }
}
