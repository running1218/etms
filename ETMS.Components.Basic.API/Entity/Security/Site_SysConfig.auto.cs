
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 系统配置业务实体
    /// </summary>
    [Serializable]
	public partial class Site_SysConfig:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ConfigID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ConfigID; 
            }
            set
            {
                this.ConfigID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 配置ID
		/// </summary>
		public Int32 ConfigID{get;set;} 
		
		/// <summary>
		/// 所属机构
		/// </summary>
		public Int32 OrganizationID{get;set;} 
		
		/// <summary>
		/// 配置标识
		/// </summary>
		public String Name{get;set;} 
		
		/// <summary>
		/// 配置组ID
		/// </summary>
		public Int32 ConfigGroupID{get;set;} 
		
		/// <summary>
		/// 配置名称
		/// </summary>
		public String DisplayName{get;set;} 
		
		/// <summary>
		/// 系统默认值
		/// </summary>
		public String DefaultValue{get;set;} 
		
		/// <summary>
		/// 排列序号
		/// </summary>
		public Int32 OrderNo{get;set;} 
		
		/// <summary>
		/// 用户自定义值
		/// </summary>
		public String UserValue{get;set;} 
		
		/// <summary>
		/// 修改人
		/// </summary>
		public String Modifier{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime ModifyTime{get;set;}

        /// <summary>
        /// 说明
        /// </summary>
        public String Description { get; set; } 
		#endregion Fields, Properties

	}
}
