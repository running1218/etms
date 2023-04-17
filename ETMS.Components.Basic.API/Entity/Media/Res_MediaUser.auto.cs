
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity
{
    /// <summary>
    /// ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Res_MediaUser:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "MediaUserID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MediaUserID; 
            }
            set
            {
                this.MediaUserID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 
		/// </summary>
		public Guid MediaUserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Guid MediaID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime ViewTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
