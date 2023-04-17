//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-5-23 9:54:48.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Fee.API.Entity
{
    /// <summary>
    /// 课酬标准表业务实体
    /// </summary>
    [Serializable]
	public partial class Fee_CourseFeeSetting:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "CourseFeeSettingID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.CourseFeeSettingID; 
            }
            set
            {
                this.CourseFeeSettingID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 课酬标准ID
		/// </summary>
		public Guid CourseFeeSettingID{get;set;} 
		
		/// <summary>
		/// 讲师等级
		/// </summary>
		public Int32 TeacherLevelID{get;set;} 
		
		/// <summary>
		/// 培训时间说明
		/// </summary>
		public Int32 TrainingTimeDescID{get;set;} 
		
		/// <summary>
		/// 课酬标准
		/// </summary>
		public Decimal CourseFee{get;set;} 
		
		/// <summary>
		/// 备注
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// 修改人
		/// </summary>
		public String ModifyUser{get;set;} 
		
		/// <summary>
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		#endregion Fields, Properties

	}
}
