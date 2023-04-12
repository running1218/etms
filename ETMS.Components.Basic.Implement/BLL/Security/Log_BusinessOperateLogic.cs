//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-13 20:34:59.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// ҵ�������־ҵ���߼�
    /// </summary>
    public partial class Log_BusinessOperateLogic
    {
        /// <summary>
        /// �������
        /// </summary>
        public void Save(Log_BusinessOperate log_BusinessOperate)
        {
            try
            {
                if (log_BusinessOperate.BizLogID == 0)
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������                    
                    Add(log_BusinessOperate);
                }
                else
                {
                    Update(log_BusinessOperate);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Log_BusinessOperateCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Log_BusinessOperate.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Log_BusinessOperateName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Log_BusinessOperate.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        protected void doRemove(Int64 bizLogID)
        {
            try
            {
                DAL.Remove(bizLogID);
                //��¼ɾ����־������IDɾ����
                BizLogHelper.Operate(bizLogID, "ɾ��");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Log_BusinessOperate.DataUsed");
                }
                //�����δ���������׳�
                throw ex;
            }
        }
    }


}
