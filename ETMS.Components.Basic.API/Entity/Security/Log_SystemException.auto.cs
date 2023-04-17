
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 系统异常日志业务实体
    /// </summary>
    [Serializable]
	public partial class Log_SystemException:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "SysExLogID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.SysExLogID; 
            }
            set
            {
                this.SysExLogID=(Int64)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 系统异常日志编号
		/// </summary>
		public Int64 SysExLogID{get;set;} 
		
		/// <summary>
		/// 应用名称
		/// </summary>
		public String ApplicationName{get;set;} 
		
		/// <summary>
		/// 错误消息
		/// </summary>
		public String Message{get;set;} 
		
		/// <summary>
		/// 上级错误消息
		/// </summary>
		public String BaseMessage{get;set;} 
		
		/// <summary>
		/// 堆栈信息
		/// </summary>
		public String StackTrace{get;set;} 
		
		/// <summary>
		/// 操作人
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
		
		/// <summary>
		/// 请求URL
		/// </summary>
		public String PageUrl{get;set;} 
		
		#endregion Fields, Properties

	}
}
