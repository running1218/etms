
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity
{
    /// <summary>
    /// ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Res_Media:AbstractObject
	{ 	
        #region ����ҵ�����
		 
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
		/// 1:��Ƶ��2����Ƶ
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
		/// ��Ƶ����Ƶ����ʱ��
		/// </summary>
		public Int32 PlayTime{get;set;} 
		
		/// <summary>
		/// ͼƬ��ַ
		/// </summary>
		public String ImagePath{get;set;} 
		
		/// <summary>
		/// ��Ƶ��ַ
		/// </summary>
		public String MediaPath{get;set;} 
		
		/// <summary>
		/// �������
		/// </summary>
		public Int32 PlayRate{get;set;} 
		
		/// <summary>
		/// �Ƿ��Ƽ�
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
