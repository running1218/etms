//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-3-28 14:31:33.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Courseware.API.Entity
{
    /// <summary>
    /// 课件表业务实体
    /// </summary>
    [Serializable]
	public partial class Res_Courseware:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "CoursewareID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.CoursewareID; 
            }
            set
            {
                this.CoursewareID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 课件ID
		/// </summary>
		public Guid CoursewareID{get;set;} 
		
		/// <summary>
		/// 课件类型
		/// </summary>
		public Int32 CoursewareTypeID{get;set;} 
		
		/// <summary>
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 课件名称
		/// </summary>
		public String CoursewareName{get;set;} 
		
		/// <summary>
		/// 课件地址
		/// </summary>
		public String CoursewarePath{get;set;} 
		
		/// <summary>
		/// 课件来源
		/// </summary>
		public String CoursewareSource{get;set;} 
		
		/// <summary>
		/// 课件状态
		/// </summary>
		public Int32 CoursewareStatus{get;set;} 
		
		/// <summary>
		/// 课件时长
		/// </summary>
		public Int32 ShowHoures{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人
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

        /// <summary>
        /// 封面图片
        /// </summary>
        public String CoverImg { get; set; } 
		
		/// <summary>
		/// 删除标识
		/// </summary>
		public Boolean DelFlag{get;set;}

        public Boolean IsURL { get; set; }
		
		#endregion Fields, Properties

	}
}
