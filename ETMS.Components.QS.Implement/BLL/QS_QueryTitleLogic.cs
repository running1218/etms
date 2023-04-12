//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-23 11:37:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;

using ETMS.Components.QS.API.Entity;


namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// �ʾ�������Ŀ��ҵ���߼�
    /// </summary>
    public partial class QS_QueryTitleLogic
    {
        /// <summary>
        /// �ʾ�������Ŀ����
        /// </summary>
        /// <param name="entity">�ʾ�������Ŀʵ��</param>
        /// <param name="action">�������������ӻ����޸�</param>
        public void Save(QS_QueryTitle entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                {
                    //ȡ��ǰ���ӵ��ʾ�����������Ŀ���
                    int maxTitleNo = GetMaxTitleNo(entity.QueryID);
                    //�Զ��������Ϊ��һ��
                    entity.TitleNo = maxTitleNo + 1;
                    Add(entity);
                }
                else if (action == OperationAction.Edit)
                {
                    Update(entity);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// ɾ��
        /// </summary>
        protected void doRemove(Guid titleID)
        {
            try
            {
                DAL.Remove(titleID);
                //��¼ɾ����־������IDɾ����
                BizLogHelper.Operate(titleID, "ɾ��");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message.ToUpper();
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (errorMsg.IndexOf("FK_QS_QUERY_REF_1972_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("���ʾ�������Ŀ���С�ѡ����ѡ�������ɾ����");
                }
                else if (errorMsg.IndexOf("FK_QS_QUERY_REF_1971_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("���ʾ�������Ŀ���С���������������ɾ����");
                }
                //�����δ���������׳�
                throw ex;
            }
        }


        /// <summary>
        /// ��ȡĳ�������ʾ��ĵ�ǰ�����Ŀ���
        /// </summary>
        /// <param name="queryID">�����ʾ�ID</param>
        /// <returns></returns>
        public Int32 GetMaxTitleNo(Guid queryID)
        {
            return DAL.GetMaxTitleNo(queryID);
        }



        /// <summary>
        /// ��ȡ�ʾ���������б�����ϸ��Ϣ
        /// </summary>
        /// <param name="pageIndex">��ʼҳ</param>
        /// <param name="pageSize">ÿҳ�ļ�¼��</param>
        /// <param name="sortExpression">����ʽ</param>
        /// <param name="criteria">�� AND ��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�����ܵ����������ļ�¼��</param>
        public DataTable GetQueryTitleAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetQueryTitleAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// ��ȡĳ���ʾ���������б�����ϸ��Ϣ(��ҳ)
        /// </summary>
        /// <param name="queryID">�����ʾ�ID</param>
        /// <param name="pageIndex">��ʼҳ</param>
        /// <param name="pageSize">ÿҳ�ļ�¼��</param>
        /// <param name="sortExpression">����ʽ</param>
        /// <param name="criteria">�� AND ��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�����ܵ����������ļ�¼��</param>
        public DataTable GetQueryTitleAllInfoByQueryID(Guid queryID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND qt.QueryID='{0}'", queryID);
            return GetQueryTitleAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// ��ȡĳ���ʾ���������б�����ϸ��Ϣ,�������˳������(����ҳ)
        /// </summary>
        /// <param name="queryID">�����ʾ�ID</param>
        /// <param name="pageIndex">��ʼҳ</param>
        /// <param name="pageSize">ÿҳ�ļ�¼��</param>
        /// <param name="sortExpression">����ʽ</param>
        /// <param name="criteria">�� AND ��ͷ�Ĳ�ѯ����</param>
        /// <param name="totalRecords">�����ܵ����������ļ�¼��</param>
        public DataTable GetQueryTitleAllInfoByQueryID(Guid queryID)
        {
            int totalRecords = 0;
            string criteria = string.Format(" AND qt.QueryID='{0}'", queryID);
            string sortExpression = "qt.TitleNo";
            return GetQueryTitleAllInfo(1, 10000, sortExpression, criteria, out  totalRecords);
        }

        public void UpdateQSTitleSort(List<QS_QueryTitle> list)
        {
            foreach (QS_QueryTitle entity in list)
            {
                DAL.UpdateQSTitleSort(entity);
            }
        }

    }


}
