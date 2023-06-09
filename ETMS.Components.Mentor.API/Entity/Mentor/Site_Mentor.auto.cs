//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xinyb.
//Date: 2012-05-02 11:00:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Mentor.API.Entity.Mentor
{
    /// <summary>
    /// 导师表业务实体
    /// </summary>
    [Serializable]
	public partial class Site_Mentor:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "MentorID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.MentorID; 
            }
            set
            {
                this.MentorID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 导师ID
		/// </summary>
		public Int32 MentorID{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// 修改人
		/// </summary>
		public String ModifyUser{get;set;} 
		
		#endregion Fields, Properties

	}
}
