//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-04-18 22:30:52.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    /// <summary>
    /// ��ί��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Sty_ClassMonitor:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "ClassMonitorID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ClassMonitorID; 
            }
            set
            {
                this.ClassMonitorID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ��ίID
		/// </summary>
		public Guid ClassMonitorID{get;set;} 
		
		/// <summary>
		/// �༶ѧԱID
		/// </summary>
		public Guid ClassStudentID{get;set;} 
		
		/// <summary>
		/// ѧԱ����
		/// </summary>
		public Int32 StudentTypeID{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// �޸���
		/// </summary>
		public String ModifyUser{get;set;} 
		
		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark{get;set;} 
		
		#endregion Fields, Properties

	}
}