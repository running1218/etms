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
    /// 评价量表业务实体
    /// </summary>
    [Serializable]
	public partial class Evaluation_Plate:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "PlateID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.PlateID; 
            }
            set
            {
                this.PlateID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 评价量ID
		/// </summary>
		public Guid PlateID{get;set;} 
		
		/// <summary>
		/// 评价对象
		/// </summary>
		public Int32 ObjectTypeID{get;set;} 
		
		/// <summary>
		/// 评价量名称
		/// </summary>
		public String PlateName{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// 最多可评价次数
		/// </summary>
		public Int32 MaxRepeat{get;set;} 
		
		/// <summary>
		/// 是否可查看评价结果
		/// </summary>
		public Boolean IsViewResult{get;set;} 
		
		/// <summary>
		/// 是否含其他文本输入框
		/// </summary>
		public Boolean IsOther{get;set;} 
		
		/// <summary>
		/// 其他文本输入框名称
		/// </summary>
		public String OtherTitle{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// 修改人
		/// </summary>
		public String ModifyUser{get;set;} 
		
		/// <summary>
		/// 备注
		/// </summary>
		public String Remark{get;set;} 
		
		#endregion Fields, Properties

	}
}
