//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012/3/29 10:29:27.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// ���漶��ϵͳ�ֵ����ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Inf_dic_InfoLevel:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "InfoLevelID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.InfoLevelID; 
            }
            set
            {
                this.InfoLevelID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ���漶��
		/// </summary>
		public Int32 InfoLevelID{get;set;} 
		
		/// <summary>
		/// ���漶������
		/// </summary>
		public String InfoLevelName{get;set;} 
		
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