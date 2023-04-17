
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息提醒清单业务实体
    /// </summary>
    [Serializable]
    public partial class Notify_Message : AbstractObject
    {
        #region 所有业务基类

        public override string DefaultKeyName
        {
            get { return "MessageID"; }
        }
        public override object KeyValue
        {
            get
            {
                return this.MessageID;
            }
            set
            {
                this.MessageID = (Int32)value;
            }
        }
        #endregion

        #region Fields, Properties
        /// <summary>
        /// 消息ID
        /// </summary>
        public Int32 MessageID { get; set; }

        /// <summary>
        /// 导入类型编号
        /// </summary>
        public Int16 MessageClassID { get; set; }

        /// <summary>
        /// 导入类型编号
        /// </summary>
        public Int16 MessageTypeID { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public Int32 OrganizationID { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public String Body { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public String Receiver { get; set; }

        /// <summary>
        /// 状态(0:未发送 1：发送中 2：发送失败 3：发送成功)
        /// </summary>
        public Int16 Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 CreatorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 消息阅读时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        #endregion Fields, Properties

    }
}
