//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-25 16:15:18.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Fee.API.Entity
{
    /// <summary>
    /// ��ѵ��Ŀ������ˮ��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Fee_FeeCostDetails:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "FeeCostDetailID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.FeeCostDetailID; 
            }
            set
            {
                this.FeeCostDetailID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ������ˮID
		/// </summary>
		public Guid FeeCostDetailID{get;set;} 
		
		/// <summary>
		/// ��ѵ��ĿID
		/// </summary>
		public Guid TrainingItemID{get;set;} 
		
		/// <summary>
		/// ������ˮ��
		/// </summary>
		public String FeeCostDetailNo{get;set;} 
		
		/// <summary>
		/// ������ˮ����
		/// </summary>
		public String FeeCostDetailName{get;set;} 
		
		/// <summary>
		/// ���÷�������
		/// </summary>
		public DateTime CostDate{get;set;} 
		
		/// <summary>
		/// ���
		/// </summary>
		public Decimal Amount{get;set;} 
		
		/// <summary>
		/// ��;
		/// </summary>
		public String Purpose{get;set;} 
		
		/// <summary>
		/// PR����
		/// </summary>
		public String PRNo{get;set;} 
		
		/// <summary>
		/// �Ƿ��õ���Ʊ
		/// </summary>
		public Boolean IsGetInvoice{get;set;} 
		
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime ReimbursementDate{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String Handler{get;set;} 
		
		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark{get;set;} 
		
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
		/// ɾ����ʶ
		/// </summary>
		public Boolean DelFlag{get;set;} 
		
		#endregion Fields, Properties

	}
}