//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-04-18 22:30:53.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    /// <summary>
    /// �༶Ⱥ��ѧԱ��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Sty_ClassSubgroupStudent:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "SubgroupStudentID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.SubgroupStudentID; 
            }
            set
            {
                this.SubgroupStudentID=(Guid)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// �༶Ⱥ��ѧԱID
		/// </summary>
		public Guid SubgroupStudentID{get;set;} 
		
		/// <summary>
		/// �༶Ⱥ��ID
		/// </summary>
		public Guid ClassSubgroupID{get;set;} 
		
		/// <summary>
		/// �༶ѧԱID
		/// </summary>
		public Guid ClassStudentID{get;set;} 
		
		/// <summary>
		/// �Ƿ��Ƕӳ�
		/// </summary>
		public Boolean IsLeader{get;set;} 
		
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
		
		#endregion Fields, Properties

	}
}