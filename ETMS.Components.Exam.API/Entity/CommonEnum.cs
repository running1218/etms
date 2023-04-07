/// <summary>
/// 存放题库模块所用到的所有枚举
/// </summary>
/// 
namespace ETMS.Components.Exam.API.Entity
{
    /// <summary>
    /// 分享状态
    /// </summary>
    public enum ShareType
    {
        /*
        /// <summary>
        /// 空（作为查询条件时为全部）
        /// </summary>
        Null = 0,
        /// <summary>
        /// 私有(未分享)
        /// </summary>
        Private=1,
        /// <summary>
        /// 已分享给号友
        /// </summary>
        Public=2
        */
        /// <summary>
        /// 空|全部
        /// </summary>
        Null = 0,
        /// <summary>
        /// 未分享
        /// </summary>
        NotShare = 1,
        /// <summary>
        /// 完全公开
        /// </summary>
        ShareAll = 2,
        /// <summary>
        ///  已分享好友
        /// </summary>
        ShareFriend = 3,
    }

    /// <summary>
    /// 审核状态
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// 空（作为查询条件时为全部）
        /// </summary>
        Null = 0,
        /// <summary>
        /// 等待
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 批准(审核通过)
        /// </summary>
        Approve=99,
        /// <summary>
        /// 拒绝(审核不通过)
        /// </summary>
        Reject=98
    }

    /// <summary>
    /// 试卷状态
    /// </summary>
    public enum TestStatus
    {
        /// <summary>
        /// 空（作为查询条件时为全部）
        /// </summary>
        Null = 0,
        /// <summary>
        /// 等待
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 批准(审核通过)
        /// </summary>
        Approve = 99,
        /// <summary>
        /// 拒绝(审核不通过)
        /// </summary>
        Reject = 98
    }

    /// <summary>
    /// 生成试卷中的试题的来源
    /// </summary>
    public enum QuestionSource
    {
        /// <summary>
        /// 来源于原始试题表
        /// </summary>
        Origin = 1,
        /// <summary>
        /// 来源于备份试题表
        /// </summary>
        Backup = 2
    }
}
