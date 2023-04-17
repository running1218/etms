
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Import
{
    /// <summary>
    /// 数据导入任务业务实体
    /// </summary>
    [Serializable]
	public partial class Import_Task:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "TaskID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.TaskID; 
            }
            set
            {
                this.TaskID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 任务ID
		/// </summary>
		public Int32 TaskID{get;set;} 
		
		/// <summary>
		/// 任务名称
		/// </summary>
		public String TaskName{get;set;} 
		
		/// <summary>
		/// 所属机构
		/// </summary>
		public Int32 OrganizationID{get;set;} 
		
		/// <summary>
		/// 导入类型
		/// </summary>
		public Int32 ImportTypeID{get;set;} 
		
		/// <summary>
		/// 导入状态
		/// </summary>
		public Int16 Status{get;set;} 
		
		/// <summary>
		/// 导入描述
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// 导入文件
		/// </summary>
		public String FilePath{get;set;} 
		
		/// <summary>
		/// 文件名称
		/// </summary>
		public String FilleName{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public Int32 CreatorID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
