//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-1 16:00:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob
{
    /// <summary>
    /// ������ҵ��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Ex_OnLineJob:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "OnLineJobID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.OnLineJobID; 
            }
            set
            {
                this.OnLineJobID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ������ҵID
		/// </summary>
		public Guid OnLineJobID{get;set;} 
		
		/// <summary>
		/// ��������ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// ������ҵ����
		/// </summary>
		public String OnLineJobName{get;set;} 
		
		/// <summary>
		/// ������ҵ˵��
		/// </summary>
		public String OnLineJobDesc{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 IsShowAnswer{get;set;} 
		
		/// <summary>
		/// ������ҵ״̬
		/// </summary>
		public Int32 OnLineJobStatus{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ������ID
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
		
		/// <summary>
		/// ɾ����ʶ
		/// </summary>
		public Boolean DelFlag{get;set;} 
		
		/// <summary>
		/// �Ծ�ID
		/// </summary>
		public String TestPaperID{get;set;} 
		
		#endregion Fields, Properties

	}
}