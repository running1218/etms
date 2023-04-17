
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ϵͳ�쳣��־ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Log_SystemException:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "SysExLogID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.SysExLogID; 
            }
            set
            {
                this.SysExLogID=(Int64)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ϵͳ�쳣��־���
		/// </summary>
		public Int64 SysExLogID{get;set;} 
		
		/// <summary>
		/// Ӧ������
		/// </summary>
		public String ApplicationName{get;set;} 
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public String Message{get;set;} 
		
		/// <summary>
		/// �ϼ�������Ϣ
		/// </summary>
		public String BaseMessage{get;set;} 
		
		/// <summary>
		/// ��ջ��Ϣ
		/// </summary>
		public String StackTrace{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String LoginName{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ����������
		/// </summary>
		public String ServerName{get;set;} 
		
		/// <summary>
		/// �ͻ���IP
		/// </summary>
		public String ClientIP{get;set;} 
		
		/// <summary>
		/// ����URL
		/// </summary>
		public String PageUrl{get;set;} 
		
		#endregion Fields, Properties

	}
}
