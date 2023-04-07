using System;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷-试题定义
    /// </summary>
    [Serializable]
    public class TestPaperToQuestion
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperID { get; set; }
        /// <summary>
        /// 试题在题库中的ID
        /// </summary>
        public Guid QuestionID { get; set; }
        /// <summary>
        /// 试题在整个试卷中的序号
        /// </summary>
        public int ItemSequence { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }
        /// <summary>
        /// 试题实体
        /// </summary>
        public Question Question { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID</param>
        public TestPaperToQuestion(Guid testPaperID, Guid questionID)
        {
            this.TestPaperID = testPaperID;
            this.QuestionID = questionID;
            this.Question = new Question(QuestionType.SingleChoice);
        }
    }
}
