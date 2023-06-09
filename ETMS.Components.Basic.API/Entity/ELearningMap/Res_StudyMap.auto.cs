
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// 学习地图表业务实体
    /// </summary>
    [Serializable]
	public partial class Res_StudyMap:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudyMapID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudyMapID; 
            }
            set
            {
                this.StudyMapID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学习地图ID
		/// </summary>
		public Guid StudyMapID{get;set;} 
		
		/// <summary>
		/// 学习地图类型
		/// </summary>
		public Int32 ELearningMapTypeID{get;set;} 
		
		/// <summary>
		/// 学习地图编码
		/// </summary>
		public String StudyMapCode{get;set;} 
		
		/// <summary>
		/// 学习地图名称
		/// </summary>
		public String StudyMapName{get;set;} 
		
		/// <summary>
		/// 部门ID
		/// </summary>
		public int DeptID{get;set;} 
		
		/// <summary>
		/// 岗位ID
		/// </summary>
		public int PostID{get;set;} 
		
		/// <summary>
		/// 职级ID
		/// </summary>
		public int RankID{get;set;} 
		
		/// <summary>
		/// 能力描述
		/// </summary>
		public String StudyMapDesc{get;set;} 
		
		/// <summary>
		/// 是否启用
		/// </summary>
		public Int32 Status{get;set;} 
		
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
		/// 所属机构ID
		/// </summary>
		public Int32 OrgID{get;set;} 
		
		#endregion Fields, Properties

	}
}
