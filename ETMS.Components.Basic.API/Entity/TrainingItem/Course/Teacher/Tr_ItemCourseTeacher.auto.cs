
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem.Course.Teacher
{
    /// <summary>
    /// 培训项目课程讲师表业务实体
    /// </summary>
    [Serializable]
	public partial class Tr_ItemCourseTeacher:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ItemCourseTeacherID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ItemCourseTeacherID; 
            }
            set
            {
                this.ItemCourseTeacherID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 培训项目课程讲师ID
		/// </summary>
		public Guid ItemCourseTeacherID{get;set;} 
		
		/// <summary>
		/// 培训项目课程ID
		/// </summary>
		public Guid TrainingItemCourseID{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// 讲师手机
		/// </summary>
		public String TeacherMobile{get;set;} 
		
		/// <summary>
		/// 讲师邮箱
		/// </summary>
		public String TeacherEMAIL{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 TeacherID{get;set;} 
		
		#endregion Fields, Properties

	}
}
