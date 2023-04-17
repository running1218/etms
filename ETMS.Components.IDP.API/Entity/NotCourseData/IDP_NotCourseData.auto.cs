//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-9 11:37:55.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.IDP.API.Entity.NotCourseData
{
    /// <summary>
    /// IDP非课程资料表业务实体
    /// </summary>
    [Serializable]
	public partial class IDP_NotCourseData:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "IDP_NotCourseDataID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.IDP_NotCourseDataID; 
            }
            set
            {
                this.IDP_NotCourseDataID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// IDP非课程资料ID
		/// </summary>
		public Guid IDP_NotCourseDataID{get;set;} 
		
		/// <summary>
		/// IDP学习内容来源
		/// </summary>
		public Int32 IDPSourceID{get;set;} 
		
		/// <summary>
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		/// <summary>
		/// 资料编码
		/// </summary>
		public String DataCode{get;set;} 
		
		/// <summary>
		/// 资料名称
		/// </summary>
		public String DataName{get;set;} 
		
		/// <summary>
		/// 学习内容
		/// </summary>
		public String DataCotent{get;set;} 
		
		/// <summary>
		/// 学习纲要
		/// </summary>
		public String DataOutline{get;set;} 
		
		/// <summary>
		/// 预计时长
		/// </summary>
		public Decimal TimeLength{get;set;} 
		
		/// <summary>
		/// 资料状态
		/// </summary>
		public Int32 DataStatus{get;set;} 
		
		/// <summary>
		/// 学习方式
		/// </summary>
		public Int32 StudyModelID{get;set;} 
		
		/// <summary>
		/// 次数
		/// </summary>
		public Int32 StudyTimes{get;set;} 
		
		/// <summary>
		/// 实施人
		/// </summary>
		public String Implementor{get;set;} 
		
		/// <summary>
		/// 培训资料所在
		/// </summary>
		public String DataURL{get;set;} 
		
		/// <summary>
		/// 责任方
		/// </summary>
		public String DutyMan{get;set;} 
		
		/// <summary>
		/// 学习效果评量方式
		/// </summary>
		public String EvaluationMode{get;set;} 
		
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
		/// 删除标识
		/// </summary>
		public Boolean DelFlag{get;set;} 
		
		/// <summary>
		/// 备注
		/// </summary>
		public String Remark{get;set;} 
		
		#endregion Fields, Properties

	}
}
