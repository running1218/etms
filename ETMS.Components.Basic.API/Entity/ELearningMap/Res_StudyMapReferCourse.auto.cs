
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// 学习地图与课程关系表业务实体
    /// </summary>
    [Serializable]
	public partial class Res_StudyMapReferCourse:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudyMapReferCourseID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudyMapReferCourseID; 
            }
            set
            {
                this.StudyMapReferCourseID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学习地图与课程ID
		/// </summary>
		public Guid StudyMapReferCourseID{get;set;} 
		
		/// <summary>
		/// 学习地图ID
		/// </summary>
		public Guid StudyMapID{get;set;} 
		
		/// <summary>
		/// 课程ID
		/// </summary>
		public Guid CourseID{get;set;}
        /// <summary>
        /// 学习类型
        /// </summary>
        public int StudyModelID { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ChargeMan { get; set; }
		
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
		/// 删除标识
		/// </summary>
		public Boolean DelFlag{get;set;} 
		
		#endregion Fields, Properties

	}
}
