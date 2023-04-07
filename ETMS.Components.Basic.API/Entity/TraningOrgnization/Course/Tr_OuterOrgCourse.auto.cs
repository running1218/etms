//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-4-24 21:06:16.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TraningOrgnization.Course
{
    /// <summary>
    /// 外部培训机构课程表业务实体
    /// </summary>
    [Serializable]
	public partial class Tr_OuterOrgCourse:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "OuterOrgCourseID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.OuterOrgCourseID; 
            }
            set
            {
                this.OuterOrgCourseID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 外部培训机构课程ID
		/// </summary>
		public Guid OuterOrgCourseID{get;set;} 
		
		/// <summary>
		/// 外部培训机构ID
		/// </summary>
		public Guid OuterOrgID{get;set;} 
		
		/// <summary>
		/// 课程编码
		/// </summary>
		public String CourseCode{get;set;} 
		
		/// <summary>
		/// 课程名称
		/// </summary>
		public String CourseName{get;set;} 
		
		/// <summary>
		/// 课程类型
		/// </summary>
		public Int32 CourseTypeID{get;set;} 
		
		/// <summary>
		/// 缩略图URL
		/// </summary>
		public String ThumbnailURL{get;set;} 
		
		/// <summary>
		/// 试用地址
		/// </summary>
		public String AddrURL{get;set;} 
		
		/// <summary>
		/// 适用对象
		/// </summary>
		public String ForObject{get;set;} 
		
		/// <summary>
		/// 课程介绍
		/// </summary>
		public String CourseIntroduction{get;set;} 
		
		/// <summary>
		/// 课程大纲
		/// </summary>
		public String CourseOutline{get;set;} 
		
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
