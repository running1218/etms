//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-5-6 11:46:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.IDP.API.Entity
{
    /// <summary>
    /// IDP�ƻ�ѧϰ���ݱ�ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class IDP_PlanContent:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "IDPPlanContentID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.IDPPlanContentID; 
            }
            set
            {
                this.IDPPlanContentID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// IDP�ƻ�ѧϰ����ID
		/// </summary>
		public Guid IDPPlanContentID{get;set;} 
		
		/// <summary>
		/// IDP�ƻ�ID
		/// </summary>
		public Guid IDP_PlanID{get;set;} 
		
		/// <summary>
		/// ѧϰ����
		/// </summary>
		public String StudyContent{get;set;} 
		
		/// <summary>
		/// ѧϰ��ʽ
		/// </summary>
		public String StudyMode{get;set;} 
		
		/// <summary>
		/// �ƻ����ʱ��
		/// </summary>
		public DateTime PlanFinishingTime{get;set;} 
		
		/// <summary>
		/// ѧϰ����������
		/// </summary>
		public Int32 FinishedState{get;set;} 
		
		/// <summary>
		/// ʵ�����ʱ��
		/// </summary>
		public DateTime FinishedTime{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// ������ID
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
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