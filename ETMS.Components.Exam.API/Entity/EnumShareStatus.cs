using System;

namespace ETMS.Components.Exam.API.Entity
{
    /// <summary>
    /// 分享状态
    /// 0：空|全部；1：未分享；2：完全公开；3：已分享给好友
    /// </summary>
    [Serializable]
    public enum EnumShareStatus
    {
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
}
