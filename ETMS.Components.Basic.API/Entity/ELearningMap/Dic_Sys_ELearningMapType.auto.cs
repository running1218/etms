
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// 学习地图类型（系统字典表）业务实体
    /// </summary>
    [Serializable]
	public partial class Dic_Sys_ELearningMapType:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ELearningMapTypeID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ELearningMapTypeID; 
            }
            set
            {
                this.ELearningMapTypeID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学习地图类型
		/// </summary>
		public Int32 ELearningMapTypeID{get;set;} 
		
		/// <summary>
		/// 学习地图类型名称
		/// </summary>
		public String ELearningMapTypeName{get;set;} 
		
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
