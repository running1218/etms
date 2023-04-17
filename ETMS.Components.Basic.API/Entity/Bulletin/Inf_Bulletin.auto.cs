using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告表业务实体
    /// </summary>
    [Serializable]
	public partial class Inf_Bulletin:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ArticleID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ArticleID; 
            }
            set
            {
                this.ArticleID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 公告ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// 公告级别
		/// </summary>
		public Int32 InfoLevelID{get;set;} 
		
		/// <summary>
		/// 公告类别
		/// </summary>
		public Int32 ArticleTypeID{get;set;} 
		
		/// <summary>
		/// 标题
		/// </summary>
		public String MainHead{get;set;} 
		
		/// <summary>
		/// 简介
		/// </summary>
		public String Brief{get;set;} 
		
		/// <summary>
		/// 关键字
		/// </summary>
		public String Keyword{get;set;} 
		
		/// <summary>
		/// 信息内容
		/// </summary>
		public String ArticleContent{get;set;}

        /// <summary>
        /// 图片
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 信息有效开始时间
		/// </summary>
		public DateTime BeginDate{get;set;} 
		
		/// <summary>
		/// 信息有效结束时间
		/// </summary>
		public DateTime EndDate{get;set;} 
		
		/// <summary>
		/// 是否置顶
		/// </summary>
		public Boolean IsTop{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateMan{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 更新人
		/// </summary>
		public String UpdateMan{get;set;} 
		
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
