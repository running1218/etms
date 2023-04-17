
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
	public partial class Res_Media:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "MediaID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MediaID; 
            }
            set
            {
                this.MediaID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 
		/// </summary>
		public Guid MediaID{get;set;} 
		
		/// <summary>
		/// 1:视频；2：音频
		/// </summary>
		public Int32 MediaType{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String MediaName{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public String MediaInstroduce{get;set;} 
		
		/// <summary>
		/// 视频、音频播放时长
		/// </summary>
		public Int32 PlayTime{get;set;} 
		
		/// <summary>
		/// 图片地址
		/// </summary>
		public String ImagePath{get;set;} 
		
		/// <summary>
		/// 视频地址
		/// </summary>
		public String MediaPath{get;set;} 
		
		/// <summary>
		/// 点击次数
		/// </summary>
		public Int32 PlayRate{get;set;} 
		
		/// <summary>
		/// 是否推荐
		/// </summary>
		public Boolean IsRecommend{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime RecommendTime{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 ModifyUserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
