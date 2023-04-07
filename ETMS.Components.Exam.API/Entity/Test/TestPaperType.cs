using System;

namespace ETMS.Components.Exam.API.Entity.Test
{
    /// <summary>
    /// 考试试卷类型
    /// </summary>
    [Serializable]
    public enum TestPaperType
    {
        /// <summary>
        /// 空（作为查询条件时为全部）
        /// </summary>
        Null = 0,
        /// <summary>
        /// 简单固定
        /// </summary>
        SimpleFix,
        /// <summary>
        /// 高级固定
        /// </summary>
        AdvancedFix,
        /// <summary>
        /// 高级随机
        /// </summary>
        AdvancedRandom
    }
}
