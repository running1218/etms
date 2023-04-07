using System;

namespace ETMS.Components.Basic.Implement.DAL.Common
{
    public interface IDataAccess
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        void Add(Object obj);
        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="obj"></param>
        void Update(Object obj);
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="obj"></param>
        void Delete(Object obj);
         
        /// <summary>
        /// ����ID��ȡ����ʵ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Object Query(Object id);

        /// <summary>
        /// ��ȡ����ʵ���б�
        /// </summary>
        /// <param name="filter">��������</param>
        /// <returns></returns>
        Object[] Query(string filter);
        /// <summary>
        /// ��ȡ����ʵ���б�֧�ַ�ҳ������
        /// </summary>
        /// <param name="pageIndex">ҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <param name="filter">��������</param>
        /// <param name="orderBy">��������</param>
        /// <param name="recordCount">��¼����</param>
        /// <returns></returns>
        Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount);

    }
}
