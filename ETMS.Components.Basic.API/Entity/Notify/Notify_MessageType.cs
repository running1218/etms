

namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// ��Ϣ���ͣ�1���ʼ� 2������ 3��վ���ţ�ҵ��ʵ��
    /// </summary> 
    public partial class Notify_MessageType
    {
        /// <summary>
        /// �ʼ���Ϣ����
        /// </summary>
        public static Notify_MessageType EmailMessageType = new Notify_MessageType()
        {
            MessageTypeID = 1,
            MessageTypeName = "�ʼ�",
            IsUse = 1,
            OrderNum = 1,
        };
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public static Notify_MessageType SMSMessageType = new Notify_MessageType()
        {
            MessageTypeID = 2,
            MessageTypeName = "����",
            IsUse = 1,
            OrderNum = 2,
        };
        /// <summary>
        /// վ������Ϣ����
        /// </summary>
        public static Notify_MessageType SiteInfoMessageType = new Notify_MessageType()
        {
            MessageTypeID = 3,
            MessageTypeName = "վ����",
            IsUse = 1,
            OrderNum = 3,
        };
    }
}
