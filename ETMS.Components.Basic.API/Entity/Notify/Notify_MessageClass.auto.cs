
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息类别，指定了消息业务归类业务实体
    /// </summary>
    [Serializable]
	public partial class Notify_MessageClass:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "MessageClassID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MessageClassID; 
            }
            set
            {
                this.MessageClassID=(Int16)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 导入类型编号
		/// </summary>
		public Int16 MessageClassID{get;set;} 
		
		/// <summary>
		/// 导入类型名称
		/// </summary>
		public String MessageClassName{get;set;} 
		
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
