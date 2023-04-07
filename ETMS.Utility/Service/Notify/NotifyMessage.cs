using System;

namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 提醒消息
    /// </summary>
    [Serializable]
    public class NotifyMessage
    {
        /// <summary>
        /// 消息类别
        /// </summary>
        public short MessageClassID { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 消息优先级
        /// </summary>
        public NotifyLevel Level { get; set; }

        /// <summary>
        /// 项目或者其它消息的停止发送时间
        /// </summary>
        public DateTime MessageEndTime { get; set; }
    }
}
