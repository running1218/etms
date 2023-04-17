//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2014/1/3 16:38:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity
{
    /// <summary>
    /// 课程订单表业务实体
    /// </summary>
    [Serializable]
	public partial class Order:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "OrderNo"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.OrderNo; 
            }
            set
            {
                this.OrderNo=(String)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 
		/// </summary>
		public String OrderNo{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String OrderDescription{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 OrderStatus{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 BuyNumber{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Decimal TotalPrice{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 Discount{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Decimal DiscountTotalPrice{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 PaymentStatus{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 PaymentModeID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String UserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String PayerName{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String PayerAlipayAccount{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String PayerPhone{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String PayerIP{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String CreateUser{get;set;} 
		
		#endregion Fields, Properties

	}
}
