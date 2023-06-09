
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 系统日志消息业务实体
    /// </summary>
    [Serializable]
	public partial class Log_SystemInfo:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "SysInfoLogID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.SysInfoLogID; 
            }
            set
            {
                this.SysInfoLogID=(Int64)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 系统消息日志编号
		/// </summary>
		public Int64 SysInfoLogID{get;set;} 
		
		/// <summary>
		/// 目标
		/// </summary>
		public String Target{get;set;} 
		
		/// <summary>
		/// 日志类型
		/// </summary>
		public String LogType{get;set;} 
		
		/// <summary>
		/// 消息内容
		/// </summary>
		public String Message{get;set;} 
		
		/// <summary>
		/// 当前用户
		/// </summary>
		public String LoginName{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 服务器名称
		/// </summary>
		public String ServerName{get;set;} 
		
		/// <summary>
		/// 客户端IP
		/// </summary>
		public String ClientIP{get;set;} 
		
		#endregion Fields, Properties

	}
}
