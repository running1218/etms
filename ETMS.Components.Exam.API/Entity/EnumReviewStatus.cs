using System;

namespace ETMS.Components.Exam.API.Entity
{
    /// <summary>
    /// 审核状态
    /// 0：空|全部；1: 等待审核；
    /// 2：一审不通过；3：一审通过；
    /// 4：二审不通过；5：二审通过；
    /// 6：三审不通过；7：三审通过；
    /// 99：审核通过
    /// </summary>
    [Serializable]
    public enum EnumReviewStatus
    {
        /// <summary>
        /// 空|全部
        /// </summary>
        Null = 0,
        /// <summary>
        /// 等待审核
        /// </summary>
        WaitReview = 1,
        /// <summary>
        /// 一审不通过
        /// </summary>
        NotPassFirstReview = 2,
        /// <summary>
        /// 一审通过
        /// </summary>
        PassFirstReview = 3,
        /// <summary>
        /// 二审不通过
        /// </summary>
        NotPassSecendReview = 4,
        /// <summary>
        /// 二审通过
        /// </summary>
        PassSecendReview = 5,
        /// <summary>
        /// 三审不通过
        /// </summary>
        NotPassThirdReview = 6,
        /// <summary>
        /// 三审通过
        /// </summary>
        PassThirdReview = 7,
        /// <summary>
        /// 审核不通过(试题/试卷使用)
        /// </summary>
        NotPassReview = 98,
        /// <summary>
        /// 审核通过
        /// </summary>
        PassReview = 99
    }
}
