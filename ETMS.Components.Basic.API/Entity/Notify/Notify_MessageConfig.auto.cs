
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息配置定义（消息模板及发送策略）业务实体
    /// </summary>
    [Serializable]
	public partial class Notify_MessageConfig:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "ConfigID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ConfigID; 
            }
            set
            {
                this.ConfigID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 消息配置ID
		/// </summary>
		public Int32 ConfigID{get;set;} 
		
		/// <summary>
		/// 消息类别编号
		/// </summary>
		public Int16 MessageClassID{get;set;} 
		
		/// <summary>
		/// 所属机构
		/// </summary>
		public Int32 OrganizationID{get;set;} 
		
		/// <summary>
		/// 邮件标题模板
		/// </summary>
		public String EmailSubjectTemplate{get;set;} 
		
		/// <summary>
		/// 邮件内容模板
		/// </summary>
		public String EmailBodyTemplate{get;set;} 
		
		/// <summary>
		/// 是否发送邮件
		/// </summary>
		public Boolean IsEnableEmail{get;set;} 
		
		/// <summary>
		/// 短信标题模板
		/// </summary>
		public String SMSSubjectTemplate{get;set;} 
		
		/// <summary>
		/// 短信内容模板
		/// </summary>
		public String SMSBodyTemplate{get;set;} 
		
		/// <summary>
		/// 是否发送短信
		/// </summary>
		public Boolean IsEnableSMS{get;set;} 
		
		/// <summary>
		/// 站内消息标题模板
		/// </summary>
		public String SiteInfoSubjectTemplate{get;set;} 
		
		/// <summary>
		/// 站内消息内容模板
		/// </summary>
		public String SiteInfoBodyTemplate{get;set;} 
		
		/// <summary>
		/// 是否发送站内信消息
		/// </summary>
		public Boolean IsEnableSiteInfo{get;set;} 
		
		/// <summary>
		/// 业务参数说明
		/// </summary>
		public String TemplateVariableDefine{get;set;} 
		
		/// <summary>
		/// 状态（1：启用，2：停用）
		/// </summary>
		public Int16 Status{get;set;} 
		
		/// <summary>
		/// 创建人ID
		/// </summary>
		public Int32 CreatorID{get;set;} 
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// 修改人ID
		/// </summary>
		public Int32 UpdaterID{get;set;} 
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime{get;set;} 
		
		#endregion Fields, Properties

	}
}
