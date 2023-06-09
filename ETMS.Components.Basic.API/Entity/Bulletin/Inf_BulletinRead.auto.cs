
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告已读记录表业务实体
    /// </summary>
    [Serializable]
	public partial class Inf_BulletinRead:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ArticleClickID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ArticleClickID; 
            }
            set
            {
                this.ArticleClickID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 公告已读记录ID
		/// </summary>
		public Int32 ArticleClickID{get;set;} 
		
		/// <summary>
		/// 公告ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// 用户ID
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
