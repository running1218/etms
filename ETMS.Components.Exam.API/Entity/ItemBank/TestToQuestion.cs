using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 试卷-试题定义实体
    /// </summary>
    [Serializable]
    public class TestToQuestion
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
        /// 试题序号
        /// </summary>
        public int ItemSequence { get; set; }

        /// <summary>
        /// 试题分数
        /// </summary>
        public decimal QuestionScore { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreatedUserID { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
