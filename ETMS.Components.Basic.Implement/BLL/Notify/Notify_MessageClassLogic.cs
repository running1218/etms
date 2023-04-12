//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-11 9:06:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;

using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Notify;
namespace ETMS.Components.Basic.Implement.BLL.Notify
{
    /// <summary>
    /// ��Ϣ���ָ������Ϣҵ�����ҵ���߼�
    /// </summary>
    public partial class Notify_MessageClassLogic
    {
        /// <summary>
        /// �������
        /// </summary>
        public void Save(Notify_MessageClass notify_MessageClass)
        {
            try
            {
                if (notify_MessageClass.MessageClassID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    notify_MessageClass.MessageClassID = notify_MessageClass.MessageClassID.NewID(); ;
                    Add(notify_MessageClass);
                }
                else
                {
                    Update(notify_MessageClass);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Notify_MessageClassCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageClass.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Notify_MessageClassName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageClass.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        protected void doRemove(Int16 messageClassID)
        {
            try
            {
                DAL.Remove(messageClassID);
                //��¼ɾ����־������IDɾ����
                BizLogHelper.Operate(messageClassID, "ɾ��");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Notify.Notify_MessageClass.DataUsed");
                }
                //�����δ���������׳�
                throw ex;
            }
        }

        /// <summary>
        /// ������Ϣ������ƻ�ȡ��Ϣ���ʵ��
        /// </summary>
        /// <param name="className">��Ϣ�������</param>
        /// <returns>��Ϣ���ʵ��</returns>
        public Notify_MessageClass GetMessageClassByClassName(string className)
        {
            //config/BizCache.config�ж��建����ڲ���
            string key = "Notify_MessageClassDefines";
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<Notify_MessageClass>(key, className, () =>
              {
                  int totalRecords = 0;
                  IList<Notify_MessageClass> messageClassItems = GetEntityList(1, 1, "", string.Format(" AND MessageClassName='{0}'", className), out totalRecords);
                  if (totalRecords == 0)
                  {
                      throw new BusinessException("Notify.Notify_MessageClass.NotFoundByClassName", new object[] { className });
                  }
                  return messageClassItems[0];
              });
        }


    }


}
