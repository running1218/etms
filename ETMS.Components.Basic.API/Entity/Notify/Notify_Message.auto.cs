
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// ��Ϣ�����嵥ҵ��ʵ��
    /// </summary>
    [Serializable]
    public partial class Notify_Message : AbstractObject
    {
        #region ����ҵ�����

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
        /// ��ϢID
        /// </summary>
        public Int32 MessageID { get; set; }

        /// <summary>
        /// �������ͱ��
        /// </summary>
        public Int16 MessageClassID { get; set; }

        /// <summary>
        /// �������ͱ��
        /// </summary>
        public Int16 MessageTypeID { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public Int32 OrganizationID { get; set; }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public String Body { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String Receiver { get; set; }

        /// <summary>
        /// ״̬(0:δ���� 1�������� 2������ʧ�� 3�����ͳɹ�)
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
        /// ��Ϣ�Ķ�ʱ��
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        #endregion Fields, Properties

    }
}
