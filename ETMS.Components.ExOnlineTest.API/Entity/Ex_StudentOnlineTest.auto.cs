//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-12 9:56:35.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.ExOnlineTest.API.Entity.ExOnlineTest
{
    /// <summary>
    /// 业务实体
    /// </summary>
    [Serializable]
	public partial class Ex_StudentOnlineTest:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudentOnlineTestID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudentOnlineTestID; 
            }
            set
            {
                this.StudentOnlineTestID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 
		/// </summary>
		public Guid StudentOnlineTestID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Guid OnlineTestID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Guid TrainingItemCourseID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Int32 StudentID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Decimal Score{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime BeginTime{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public DateTime EndTime{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Guid UserExamID{get;set;} 
		
		/// <summary>
		/// 
		/// </summary>
		public Guid StudentCourseID{get;set;} 
		
		#endregion Fields, Properties

	}
}
