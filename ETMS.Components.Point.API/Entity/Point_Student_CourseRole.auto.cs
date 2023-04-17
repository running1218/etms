//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-6 14:53:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Point.API.Entity
{
    /// <summary>
    /// 学员培训项目课程积分规则表业务实体
    /// </summary>
    [Serializable]
	public partial class Point_Student_CourseRole:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudentCoursePointRoleID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudentCoursePointRoleID; 
            }
            set
            {
                this.StudentCoursePointRoleID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学员培训项目课程积分规则ID
		/// </summary>
		public Guid StudentCoursePointRoleID{get;set;} 
		
		/// <summary>
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 课程属性
		/// </summary>
		public Int32 CourseAttrID{get;set;} 
		
		/// <summary>
		/// 学员培训项目课程积分分类
		/// </summary>
		public Int32 StudentCoursePointTypeID{get;set;} 
		
		/// <summary>
		/// 起始数
		/// </summary>
		public Int32 MinNum{get;set;} 
		
		/// <summary>
		/// 截止数
		/// </summary>
		public Int32 MaxNum{get;set;} 
		
		/// <summary>
		/// 赠送积分
		/// </summary>
		public Int32 GivePoints{get;set;} 
		
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
