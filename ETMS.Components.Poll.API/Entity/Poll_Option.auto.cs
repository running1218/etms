//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;

using ETMS.AppContext;
namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// ѡ���ҵ��ʵ��
    /// </summary>
    [Serializable]
	public partial class Poll_Option:AbstractObject
	{ 	
        #region ����ҵ�����
		 
        public override string DefaultKeyName
        {
            get { return "OptionID"; }
        }
         public override object KeyValue
        {
            get
            {
                return this.OptionID; 
            }
            set
            {
                this.OptionID=(Int32)value;
            }
        }    
	    #endregion

		#region Fields, Properties
		/// <summary>
		/// ѡ����
		/// </summary>
		public Int32 OptionID{get;set;} 
		
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public Int32 TitleID{get;set;} 
		
		/// <summary>
		/// ѡ������
		/// </summary>
		public String OptionName{get;set;} 
		
		/// <summary>
		/// ����ѡ���ı�����
		/// </summary>
		public Int32 OtherLength{get;set;} 
		
		#endregion Fields, Properties

	}
}