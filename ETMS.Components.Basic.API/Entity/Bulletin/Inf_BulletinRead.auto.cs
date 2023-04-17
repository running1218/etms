
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// �����Ѷ���¼��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Inf_BulletinRead:AbstractObject
	{ 	
        #region ����ҵ�����
		 
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
		/// �����Ѷ���¼ID
		/// </summary>
		public Int32 ArticleClickID{get;set;} 
		
		/// <summary>
		/// ����ID
		/// </summary>
		public Int32 ArticleID{get;set;} 
		
		/// <summary>
		/// �û�ID
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
