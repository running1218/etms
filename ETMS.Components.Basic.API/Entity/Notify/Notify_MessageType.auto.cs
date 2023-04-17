
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息类型（1：邮件 2：短信 3：站内信）业务实体
    /// </summary>
    [Serializable]
	public partial class Notify_MessageType:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "MessageTypeID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MessageTypeID; 
            }
            set
            {
                this.MessageTypeID=(Int16)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 导入类型编号
		/// </summary>
		public Int16 MessageTypeID{get;set;} 
		
		/// <summary>
		/// 导入类型名称
		/// </summary>
		public String MessageTypeName{get;set;} 
		
		/// <summary>
		/// 显示序号
		/// </summary>
		public Int32 OrderNum{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		#endregion Fields, Properties

	}
}
