
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Import
{
    /// <summary>
    /// ���ݵ�������ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Import_Task:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "TaskID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.TaskID; 
            }
            set
            {
                this.TaskID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ����ID
		/// </summary>
		public Int32 TaskID{get;set;} 
		
		/// <summary>
		/// ��������
		/// </summary>
		public String TaskName{get;set;} 
		
		/// <summary>
		/// ��������
		/// </summary>
		public Int32 OrganizationID{get;set;} 
		
		/// <summary>
		/// ��������
		/// </summary>
		public Int32 ImportTypeID{get;set;} 
		
		/// <summary>
		/// ����״̬
		/// </summary>
		public Int16 Status{get;set;} 
		
		/// <summary>
		/// ��������
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// �����ļ�
		/// </summary>
		public String FilePath{get;set;} 
		
		/// <summary>
		/// �ļ�����
		/// </summary>
		public String FilleName{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public Int32 CreatorID{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
