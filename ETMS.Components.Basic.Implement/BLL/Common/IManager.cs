namespace ETMS.Components.Basic.Implement.BLL.Common
{
    /// <summary>
    /// �й�����ϵ���ݹ�����ӿ�
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// ������
        /// </summary>
        ETMS.AppContext.IObject Manager { get;set;}

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="byManager">��������</param>
        void Associate(ETMS.AppContext.IObject member);

        /// <summary>
        /// ���������������
        /// </summary>
        /// <param name="byManager">��������</param>
        void ReleaseAssociate(ETMS.AppContext.IObject member);
        
        /// <summary>
        /// ��ȡ���б��������б�
        /// </summary>
        /// <returns></returns>
        ETMS.AppContext.IObject[] GetAllMembers(string filter);

        /// <summary>
        /// ��ȡ���б��������б�(֧�ַ�ҳ)
        /// </summary>
        /// <returns></returns>
        ETMS.AppContext.IObject[] GetAllMembers(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount);

        /// <summary>
        /// ���ݱ�����������ֵ����ȡ��������
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        ETMS.AppContext.IObject GetMemberByPkValue(ETMS.AppContext.IObject pk);
    }
}
