using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// �����ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Inf_Bulletin:AbstractObject
	{ 	
        #region ����ҵ�����
		 
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
		/// ����ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// ���漶��
		/// </summary>
		public Int32 InfoLevelID{get;set;} 
		
		/// <summary>
		/// �������
		/// </summary>
		public Int32 ArticleTypeID{get;set;} 
		
		/// <summary>
		/// ����
		/// </summary>
		public String MainHead{get;set;} 
		
		/// <summary>
		/// ���
		/// </summary>
		public String Brief{get;set;} 
		
		/// <summary>
		/// �ؼ���
		/// </summary>
		public String Keyword{get;set;} 
		
		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public String ArticleContent{get;set;}

        /// <summary>
        /// ͼƬ
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// ��������ID
        /// </summary>
        public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// ��Ϣ��Ч��ʼʱ��
		/// </summary>
		public DateTime BeginDate{get;set;} 
		
		/// <summary>
		/// ��Ϣ��Ч����ʱ��
		/// </summary>
		public DateTime EndDate{get;set;} 
		
		/// <summary>
		/// �Ƿ��ö�
		/// </summary>
		public Boolean IsTop{get;set;} 
		
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String CreateMan{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String UpdateMan{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime UpdateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
