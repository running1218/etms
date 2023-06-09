
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 项目课程导学资料表业务实体
    /// </summary>
    [Serializable]
	public partial class Tr_ItemCourseMentorData:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ItemCourseMentorDataID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ItemCourseMentorDataID; 
            }
            set
            {
                this.ItemCourseMentorDataID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 项目课程导学资料ID
		/// </summary>
		public Guid ItemCourseMentorDataID{get;set;} 
		
		/// <summary>
		/// 培训项目课程ID
		/// </summary>
		public Guid TrainingItemCourseID{get;set;} 
		
		/// <summary>
		/// 公告ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime BeginTime{get;set;} 
		
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
