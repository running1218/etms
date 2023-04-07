using System;

using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试卷策略
    /// </summary>
    [Serializable]
    public class TestPaperRule
    {
        /// <summary>
        /// 策略ID
        /// </summary>
        //public Guid RuleID { get; set; }

        /// <summary>
        /// 试卷定义ID
        /// </summary>
        public Guid TestPaperID { get; set; }

        /// <summary>
        /// 题库ID
        /// </summary>
        public Guid QuestionBankID { get; set; }

        /// <summary>
        /// 题库名称
        /// </summary>
        public string QuestionBankName { get; set; }

        /// <summary>
        /// 试题类型
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /*
        /// <summary>
        /// 试题难度
        /// </summary>
        public short Difficulty { get; set; }
        /// <summary>
        /// 挑题数量
        /// </summary>
        public int SelectQty { get; set; }

        /// <summary>
        /// 总题数量
        /// </summary>
        public int TotalQty { get; set; }      
        */

        public int LowSelectQty { get; set; }
        public int MediumSelectQty { get; set; }
        public int HighSelectQty { get; set; }

        /// <summary>
        /// 简易题总数
        /// </summary>
        public int LowTotalQty { get; set; }
        /// <summary>
        /// 中等题总数
        /// </summary>
        public int MediumTotalQty { get; set; }
        /// <summary>
        /// 难度题总数
        /// </summary>
        public int HighTotalQty { get; set; } 

        /// <summary>
        /// 初始分数
        /// </summary>
        public int QuestionScore { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }
    }
}
