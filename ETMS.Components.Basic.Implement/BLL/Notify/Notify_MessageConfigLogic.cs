//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-11 9:06:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Notify;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// ��Ϣ���ö��壨��Ϣģ�弰���Ͳ��ԣ�ҵ���߼�
    /// </summary>
    public partial class Notify_MessageConfigLogic
	{
 		/// <summary>
		/// �������
		/// </summary>
		public void Save(Notify_MessageConfig notify_MessageConfig)
		{
            try
            {
			    if(notify_MessageConfig.ConfigID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    notify_MessageConfig.ConfigID=notify_MessageConfig.ConfigID.NewID();;
                    Add(notify_MessageConfig);
                }
                else
                {
                    Update(notify_MessageConfig);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Notify_MessageConfigCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageConfig.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Notify_MessageConfigName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageConfig.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            } 
		} 

        /// <summary>
		/// ɾ��
		/// </summary>
		protected void doRemove(Int32 configID)
		{
            try
            {
			     DAL.Remove(configID);
                 //��¼ɾ����־������IDɾ����
                 BizLogHelper.Operate(configID,"ɾ��");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageConfig.DataUsed");
                } 
                //�����δ���������׳�
                throw ex;
            }  
		}  
	}
	
	
}
