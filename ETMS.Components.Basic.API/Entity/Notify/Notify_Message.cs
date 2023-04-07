using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息提醒清单业务实体
    /// </summary>
    public partial class Notify_Message : AbstractObject
    {
        public DateTime MessageEndTime { get; set; }
    }
}
