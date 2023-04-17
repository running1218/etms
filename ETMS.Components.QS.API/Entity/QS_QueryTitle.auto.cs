//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-24 10:33:04.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.QS.API.Entity
{
    /// <summary>
    /// 问卷调查题目表业务实体
    /// </summary>
    [Serializable]
	public partial class QS_QueryTitle:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "TitleID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.TitleID; 
            }
            set
            {
                this.TitleID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 问卷调查题目ID
		/// </summary>
		public Guid TitleID{get;set;} 
		
		/// <summary>
		/// 问卷调查ID
		/// </summary>
		public Guid QueryID{get;set;} 
		
		/// <summary>
		/// 问卷调查题型
		/// </summary>
		public Int32 TitleTypeID{get;set;} 
		
		/// <summary>
		/// 问卷调查题目名称
		/// </summary>
		public String TitleName{get;set;} 
		
		/// <summary>
		/// 问卷调查题目序号
		/// </summary>
		public Int32 TitleNo{get;set;} 
		
		/// <summary>
		/// 最少选择选项数
		/// </summary>
		public Int32 MinSelectNum{get;set;} 
		
		/// <summary>
		/// 最多选择选项数
		/// </summary>
		public Int32 MaxSelectNum{get;set;} 
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
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
