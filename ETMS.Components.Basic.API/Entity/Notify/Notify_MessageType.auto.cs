
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// ��Ϣ���ͣ�1���ʼ� 2������ 3��վ���ţ�ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Notify_MessageType:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "MessageTypeID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MessageTypeID; 
            }
            set
            {
                this.MessageTypeID=(Int16)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// �������ͱ��
		/// </summary>
		public Int16 MessageTypeID{get;set;} 
		
		/// <summary>
		/// ������������
		/// </summary>
		public String MessageTypeName{get;set;} 
		
		/// <summary>
		/// ��ʾ���
		/// </summary>
		public Int32 OrderNum{get;set;} 
		
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		#endregion Fields, Properties

	}
}
