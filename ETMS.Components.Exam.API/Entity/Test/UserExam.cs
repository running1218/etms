using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 考生考试试卷表
    /// </summary>
    [Serializable]
    public class UserExam
    {
        /// <summary>
        /// 答卷ID
        /// </summary>
        public Guid UserExamID { get; set; }
        /// <summary>
        /// 考生ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 试卷定义ID
        /// </summary>
        public Guid TestPaperID { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public string TestPaperName { get; set; }
        /// <summary>
        /// 试卷满分数
        /// </summary>
        public decimal TestPaperScore { get; set; }
        /// <summary>
        /// 试卷及格分数
        /// </summary>
        public decimal PassingScore { get; set; }
        /// <summary>
        /// 试卷规定时长
        /// </summary>
        public int? TimeLimit { get; set; }
        /// <summary>
        /// 考生答卷分数
        /// </summary>
        public decimal ExamScore { get; set; }
        /// <summary>
        /// 最终调整分数
        /// </summary>
        public decimal AdjustedScore { get; set; }
        /// <summary>
        /// 首次答卷时间(第一次)
        /// </summary>
        public DateTime BeginExamTime { get; set; }
        /// <summary>
        /// 最后一次答题开始时间
        /// </summary>
        public DateTime LastBeginExamTime { get; set; }
        /// <summary>
        /// 更新时间(保存)
        /// </summary>
        public DateTime LastSaveTime { get; set; }
        /// <summary>
        /// 交卷时间(完成)
        /// </summary>
        public DateTime EndExamTime { get; set; }
        /// <summary>
        /// 当前答题的试题序号
        /// </summary>
        public int CurrQuestionNo { get; set; }
        /// <summary>
        /// 考生总用时
        /// </summary>
        public int ElapsedTime { get; set; }
        /// <summary>
        /// 试卷快照文件名
        /// </summary>
        public string TestPapertlFileName { get; set; }
        /// <summary>
        /// 答卷状态
        /// </summary>
        public UserTestStatusType Status { get; set; }
        /// <summary>
        /// 调整人
        /// </summary>
        public int AdjustedUserID { get; set; }
        /// <summary>
        /// 调整日期
        /// </summary>
        public DateTime AdjustedDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedUserID { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
