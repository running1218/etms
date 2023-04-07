using System;
namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 考生试题答案信息
    /// </summary>
    [Serializable]
    public class UserExamResult 
    {
        /// <summary>
        /// 考试试题答案ID
        /// </summary>
        public Guid ResultID { get; set; }
        /// <summary>
        /// 考生答卷ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// 题目名称
        /// </summary>
        public string QuestionName { get; set; }
        /// <summary>
        /// 试题类型
        /// </summary>
        public ItemBank.QuestionType QuestionType { get; set; }
        /// <summary>
        /// 考生答案
        /// </summary>
        public string UserAnswer { get; set; }
        /// <summary>
        /// 考生得分
        /// </summary>
        public decimal UserScore { get; set; }
        /// <summary>
        /// 试题答案
        /// </summary>
        public string QuestionAnswer { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }
    }
}
