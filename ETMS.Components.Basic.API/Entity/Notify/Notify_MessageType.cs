

namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息类型（1：邮件 2：短信 3：站内信）业务实体
    /// </summary> 
    public partial class Notify_MessageType
    {
        /// <summary>
        /// 邮件消息类型
        /// </summary>
        public static Notify_MessageType EmailMessageType = new Notify_MessageType()
        {
            MessageTypeID = 1,
            MessageTypeName = "邮件",
            IsUse = 1,
            OrderNum = 1,
        };
        /// <summary>
        /// 短信消息类型
        /// </summary>
        public static Notify_MessageType SMSMessageType = new Notify_MessageType()
        {
            MessageTypeID = 2,
            MessageTypeName = "短信",
            IsUse = 1,
            OrderNum = 2,
        };
        /// <summary>
        /// 站内信消息类型
        /// </summary>
        public static Notify_MessageType SiteInfoMessageType = new Notify_MessageType()
        {
            MessageTypeID = 3,
            MessageTypeName = "站内信",
            IsUse = 1,
            OrderNum = 3,
        };
    }
}
