using System;

using ETMS.Components.Exam.API.Entity.ItemBank;
namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 试题汇总
    /// </summary>
    [Serializable]
    public class QuestionStat
    {
        /*
        /// <summary>
        /// 试题来源(题库ID)
        /// </summary>
        public Guid QuestionBankID { get; set; }

        /// <summary>
        /// 试题来源(题库名称)
        /// </summary>
        public String QuestionBankName { get; set; }

        /// <summary>
        /// 试题类型(题型)
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// 难度(易)试题数
        /// </summary>
        public int LowDifficult { get; set; }

        /// <summary>
        /// 难度(中)试题数
        /// </summary>
        public int MediumDifficult { get; set; }

        /// <summary>
        /// 难度(难)试题数
        /// </summary>
        public int HighDifficult { get; set; }
         */

        /// <summary>
        /// 试题类型
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// 剩余数量
        /// </summary>
        public int RemainQty { get; set; }

        /// <summary>
        /// 总题数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 初始分数
        /// </summary>
        public decimal Score { get; set; }

        /*
        /// <summary>
        /// 试题难度
        /// </summary>
        public short Difficulty { get; set; }


        */
    }
}
