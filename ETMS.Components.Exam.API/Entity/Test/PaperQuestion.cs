using System;

using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷-试题关联表
    /// </summary>
    [Serializable]
    public class PaperQuestion
    {
        /// <summary>
        /// 试卷定义ID
        /// </summary>
        public Guid TestPaperID { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 试题类型(模块)
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// 模块内序号
        /// </summary>
        public int ItemSequence { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public Decimal QuestionScore { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }

         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="questionID">试题ID（题库试题ID)</param>
        public PaperQuestion(Guid testPaperID, Guid questionID)
        {
            this.TestPaperID = testPaperID;
            this.QuestionID = questionID;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PaperQuestion()
        {
            this.TestPaperID = Guid.Empty;
            this.QuestionID = Guid.Empty;
            this.QuestionType = ItemBank.QuestionType.Null;
        }
    }
}
