//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-18 11:41:22.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Evaluation.API.Entity
{
    /// <summary>
    /// 评价文字记录表业务实体
    /// </summary>
    [Serializable]
	public partial class Evaluation_PlateResult:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ResultSubID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ResultSubID; 
            }
            set
            {
                this.ResultSubID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 评价文字信息ID
		/// </summary>
		public Guid ResultSubID{get;set;} 
		
		/// <summary>
		/// 评价量ID
		/// </summary>
		public Guid PlateID{get;set;} 
		
		/// <summary>
		/// 评价人ID
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// 评价对象ID
		/// </summary>
		public String ObjectID{get;set;} 
		
		/// <summary>
		/// 评价文字
		/// </summary>
		public String EvaluationContent{get;set;} 
		
		/// <summary>
		/// 评价时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
        
		/// <summary>
        /// 审批状态
		/// </summary>
        public Int32 ApproveStatus { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApproveTime { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public Int32 ApproveUserID { get; set; } 
		#endregion Fields, Properties

	}
}
