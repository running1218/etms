
using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.ELearningMap
{
    /// <summary>
    /// ѧϰ��ͼ���ͣ�ϵͳ�ֵ��ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Dic_Sys_ELearningMapType:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "ELearningMapTypeID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.ELearningMapTypeID; 
            }
            set
            {
                this.ELearningMapTypeID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ѧϰ��ͼ����
		/// </summary>
		public Int32 ELearningMapTypeID{get;set;} 
		
		/// <summary>
		/// ѧϰ��ͼ��������
		/// </summary>
		public String ELearningMapTypeName{get;set;} 
		
		/// <summary>
		/// ��ʾ���
		/// </summary>
		public Int32 OrderNum{get;set;} 
		
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public Int32 IsUse{get;set;} 
		
		#endregion Fields, Properties

	}
}
