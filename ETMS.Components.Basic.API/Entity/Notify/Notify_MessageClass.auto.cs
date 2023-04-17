
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// ��Ϣ���ָ������Ϣҵ�����ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Notify_MessageClass:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "MessageClassID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MessageClassID; 
            }
            set
            {
                this.MessageClassID=(Int16)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// �������ͱ��
		/// </summary>
		public Int16 MessageClassID{get;set;} 
		
		/// <summary>
		/// ������������
		/// </summary>
		public String MessageClassName{get;set;} 
		
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
