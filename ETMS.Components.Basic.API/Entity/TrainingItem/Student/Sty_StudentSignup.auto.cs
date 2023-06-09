
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem.Student
{
    /// <summary>
    /// 学员报名表业务实体
    /// </summary>
    [Serializable]
	public partial class Sty_StudentSignup:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudentSignupID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudentSignupID; 
            }
            set
            {
                this.StudentSignupID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学员报名ID
		/// </summary>
		public Guid StudentSignupID{get;set;} 
		
		/// <summary>
		/// 报名方式
		/// </summary>
		public Int32 SignupModeID{get;set;} 
		
		/// <summary>
		/// 培训项目ID
		/// </summary>
		public Guid TrainingItemID{get;set;} 
		
		/// <summary>
		/// 用户ID
		/// </summary>
		public Int32 UserID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// 创建人
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// 修改人
		/// </summary>
		public String ModifyUser{get;set;} 
		
		/// <summary>
		/// 备注
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// 删除标识
		/// </summary>
		public Boolean DelFlag{get;set;} 

        public int IsUse { get; set; }
		
		#endregion Fields, Properties

	}
}
