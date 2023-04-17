
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// ѧϰ��ͼ��γ̹�ϵ��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Res_StudyMapReferCourse:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "StudyMapReferCourseID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.StudyMapReferCourseID; 
            }
            set
            {
                this.StudyMapReferCourseID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ѧϰ��ͼ��γ�ID
		/// </summary>
		public Guid StudyMapReferCourseID{get;set;} 
		
		/// <summary>
		/// ѧϰ��ͼID
		/// </summary>
		public Guid StudyMapID{get;set;} 
		
		/// <summary>
		/// �γ�ID
		/// </summary>
		public Guid CourseID{get;set;}
        /// <summary>
        /// ѧϰ����
        /// </summary>
        public int StudyModelID { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string ChargeMan { get; set; }
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CreateTime{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public Int32 CreateUserID{get;set;} 
		
		/// <summary>
		/// ������
		/// </summary>
		public String CreateUser{get;set;} 
		
		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public DateTime ModifyTime{get;set;} 
		
		/// <summary>
		/// �޸���
		/// </summary>
		public String ModifyUser{get;set;} 
		
		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark{get;set;} 
		
		/// <summary>
		/// ɾ����ʶ
		/// </summary>
		public Boolean DelFlag{get;set;} 
		
		#endregion Fields, Properties

	}
}
