//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
	public partial class Poll_UserResourceQueryResult:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "BatchID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.BatchID; 
            }
            set
            {
                this.BatchID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 
		/// </summary>
		public Int32 BatchID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String UserName{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String UserType{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 QueryID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String ResourceTypeCode{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String ResourceCode{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
