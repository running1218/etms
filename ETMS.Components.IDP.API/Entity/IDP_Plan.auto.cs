//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-5-6 11:46:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.IDP.API.Entity
{
    /// <summary>
    /// IDP计划表业务实体
    /// </summary>
    [Serializable]
	public partial class IDP_Plan:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "IDP_PlanID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.IDP_PlanID; 
            }
            set
            {
                this.IDP_PlanID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// IDP计划ID
		/// </summary>
		public Guid IDP_PlanID{get;set;} 
		
		/// <summary>
		/// 导师ID
		/// </summary>
		public Int32 MentorID{get;set;} 
		
		/// <summary>
		/// 学员ID
		/// </summary>
		public Int32 StudentID{get;set;} 
		
		/// <summary>
		/// IDP类型
		/// </summary>
		public Int32 IDPTypeID{get;set;} 
		
		/// <summary>
		/// IDP计划开始时间
		/// </summary>
		public DateTime IDPPlanBeginTime{get;set;} 
		
		/// <summary>
		/// IDP计划结束时间
		/// </summary>
		public DateTime IDPPlanEndTime{get;set;} 
		
		/// <summary>
		/// 直接上级姓名
		/// </summary>
		public String SuperiorName{get;set;} 
		
		/// <summary>
		/// 上级职务
		/// </summary>
		public String SuperiorPosition{get;set;} 
		
		/// <summary>
		/// 填表日期
		/// </summary>
		public DateTime FillingDate{get;set;} 
		
		/// <summary>
		/// 学习计划完成率
		/// </summary>
		public Int32 CompletionRate{get;set;} 
		
		/// <summary>
		/// 评价
		/// </summary>
		public String Evaluation{get;set;} 
		
		/// <summary>
		/// 是否关闭
		/// </summary>
		public Boolean IsClose{get;set;} 
		
		/// <summary>
		/// 关闭时间
		/// </summary>
		public DateTime CloseTime{get;set;} 
		
		/// <summary>
		/// 关闭人
		/// </summary>
		public String CloseUser{get;set;} 
		
		/// <summary>
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 创建人ID
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
		
		#endregion Fields, Properties

	}
}
