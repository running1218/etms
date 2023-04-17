
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告级别（系统字典表）业务实体
    /// </summary>
    [Serializable]
	public partial class Inf_dic_InfoLevel:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "InfoLevelID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.InfoLevelID; 
            }
            set
            {
                this.InfoLevelID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 公告级别
		/// </summary>
		public Int32 InfoLevelID{get;set;} 
		
		/// <summary>
		/// 公告级别名称
		/// </summary>
		public String InfoLevelName{get;set;} 
		
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
