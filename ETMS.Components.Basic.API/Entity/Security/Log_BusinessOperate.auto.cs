
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 业务操作日志业务实体
    /// </summary>
    [Serializable]
	public partial class Log_BusinessOperate:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "BizLogID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.BizLogID; 
            }
            set
            {
                this.BizLogID=(Int64)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 日志编号
		/// </summary>
		public Int64 BizLogID{get;set;} 
		
		/// <summary>
		/// 操作模块
		/// </summary>
		public String ModuleName{get;set;} 
		
		/// <summary>
		/// 模块内方法名称
		/// </summary>
		public String MethodName{get;set;} 
		
		/// <summary>
		/// 操作对象ID
		/// </summary>
		public String TargetID{get;set;} 
		
		/// <summary>
		/// 动作
		/// </summary>
		public String Action{get;set;} 
		
		/// <summary>
		/// 操作人
		/// </summary>
		public String LoginName{get;set;} 
		
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 所在服务器名称
		/// </summary>
		public String ServerName{get;set;} 
		
		/// <summary>
		/// 客户端IP
		/// </summary>
		public String ClientIP{get;set;} 
		
		/// <summary>
		/// 客户请求页面
		/// </summary>
		public String PageUrl{get;set;} 
		
		/// <summary>
		/// 组织机构ID
		/// </summary>
		public Int32 OrganizationID{get;set;} 
		
		#endregion Fields, Properties

	}
}
