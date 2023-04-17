//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-19 21:15:42.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Import
{
    /// <summary>
    /// 项目学员导入明细表业务实体
    /// </summary>
    [Serializable]
	public partial class Import_StudentSignup:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "DetailID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.DetailID; 
            }
            set
            {
                this.DetailID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 任务明细ID
		/// </summary>
		public Int32 DetailID{get;set;} 
		
		/// <summary>
		/// 任务ID
		/// </summary>
		public Int32 TaskID{get;set;} 
		
		/// <summary>
		/// 数据项导入状态
		/// </summary>
		public Int16 Status{get;set;} 
		
		/// <summary>
		/// 数据项导入说明
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// 项目编码
		/// </summary>
		public String ItemCode{get;set;} 
		
		/// <summary>
		/// 项目名称
		/// </summary>
		public String ItemName{get;set;} 
		
		/// <summary>
		/// 培训项目ID
		/// </summary>
		public Guid TrainingItemID{get;set;} 
		
		/// <summary>
		/// 组织机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 用户ID
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// 学员账户
		/// </summary>
		public String LoginName{get;set;} 
		
		/// <summary>
		/// 学员姓名
		/// </summary>
		public String RealName{get;set;} 
		
		/// <summary>
		/// 部门名称
		/// </summary>
		public String DepartmentName{get;set;} 
		
		/// <summary>
		/// 职级名称
		/// </summary>
		public String RankName{get;set;} 
		
		/// <summary>
		/// 岗位名称
		/// </summary>
		public String PostName{get;set;} 
		
		#endregion Fields, Properties

	}
}
