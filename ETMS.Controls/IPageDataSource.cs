using System.Collections;
namespace ETMS.Controls
{
    /// <summary>
    /// ��ҳ����Դ�ӿڶ���
    /// </summary>
    /// <param name="pageIndex">ҳ����������1��ʼ��</param>
    /// <param name="pageSize">ÿҳ��ʾ����������</param>
    /// <param name="total">��¼����</param>
    /// <returns>��ȡ�������ݼ���<typeparamref name="System.Collections.IList"/></returns>
    public delegate IList IPageDataSource(int pageIndex, int pageSize, out int total);
}
