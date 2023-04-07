using System;

namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 提醒级别
    /// </summary>
    [Serializable]
    public enum NotifyLevel
    {
        /// <summary>
        /// 默认优先级
        /// </summary>
        Default = 0,
        /// <summary>
        /// 中优先级
        /// </summary>        
        Medium = 1,
        /// <summary>
        /// 高优先级
        /// </summary>
        High = 2

    }
}
