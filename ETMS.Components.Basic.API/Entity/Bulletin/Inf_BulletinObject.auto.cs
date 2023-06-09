
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告发布对象表业务实体
    /// </summary>
    [Serializable]
	public partial class Inf_BulletinObject:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "BulletinObjectID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.BulletinObjectID; 
            }
            set
            {
                this.BulletinObjectID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 公告发布对象ID
		/// </summary>
		public Guid BulletinObjectID{get;set;} 
		
		/// <summary>
		/// 公告ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// 公告发布对象类型
		/// </summary>
		public Int32 BulletinObjectTypeID{get;set;} 
		
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
