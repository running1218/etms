//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-5-19 9:14:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.ExContest.API.Entity.StudentContest
{
    /// <summary>
    /// 学生闯关竞赛结果表业务实体
    /// </summary>
    [Serializable]
	public partial class Ex_StudentContest:AbstractObject
	{ 	
        #region 所有业务基类
		 
        public override string DefaultKeyName
        {
            get { return "StudentContestID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudentContestID; 
            }
            set
            {
                this.StudentContestID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// 学生闯关竞赛结果ID
		/// </summary>
		public Guid StudentContestID{get;set;} 
		
		/// <summary>
		/// 闯关竞赛ID
		/// </summary>
		public Guid ContestID{get;set;} 
		
		/// <summary>
		/// 学生ID
		/// </summary>
		public Int32 StudentID{get;set;} 
		
		/// <summary>
		/// 培训项目课程ID
		/// </summary>
		public Guid TrainingItemCourseID{get;set;} 
		
		/// <summary>
		/// 考试成绩
		/// </summary>
		public Decimal Score{get;set;} 
		
		/// <summary>
		/// 开始考试时间
		/// </summary>
		public DateTime BeginTime{get;set;} 
		
		/// <summary>
		/// 结束考试时间
		/// </summary>
		public DateTime EndTime{get;set;} 
		
		/// <summary>
		/// 学生考试答卷ID
		/// </summary>
		public Guid UserExamID{get;set;} 
		
		/// <summary>
		/// 学生选课ID
		/// </summary>
		public Guid StudentCourseID{get;set;} 
		
		#endregion Fields, Properties

	}
}
