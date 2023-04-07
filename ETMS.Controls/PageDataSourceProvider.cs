using System;
using System.Collections;
namespace ETMS.Controls
{

    /// <summary>
    /// ��ҳ����Դ������
    /// ʹ�ó���������ṩ������Դ��֧�ַ�ҳ������Ҫʹ�÷�ҳ�ؼ�����ʾ����ʱ��ͨ������������������Դת����
    /// </summary>
    public class PageDataSourceProvider
    {
        private IList m_DataSource;
        private int m_PageIndex;
        private int m_PageSize;
        /// <summary>
        /// ԭʼ����Դ
        /// </summary>
        public IList OriginalDataSource
        {
            get
            {
                return this.m_DataSource;
            }
        }
        /// <summary>
        /// ����ҳ��������
        /// Ҫ�󣺴�1��ʼ
        /// </summary>
        public int PageIndex
        {
            get
            {
                return this.m_PageIndex;
            }
        }
        /// <summary>
        /// ҳ���С
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
        }

        /// <summary>
        /// ��ҳ����Դ���������캯��
        /// </summary>
        /// <param name="dataSource">�����������Դ</param>
        /// <param name="pageIndex">����ҳ��������</param>
        /// <param name="pageSize">����ҳ���С</param>
        public PageDataSourceProvider(System.Data.DataTable dataSource, int pageIndex, int pageSize)
            : this(((System.ComponentModel.IListSource)dataSource).GetList(), pageIndex, pageSize)
        {
        }
        /// <summary>
        /// ��ҳ����Դ���������캯��
        /// </summary>
        /// <param name="dataSource">�����������Դ</param>
        /// <param name="pageIndex">����ҳ��������</param>
        /// <param name="pageSize">����ҳ���С</param>
        public PageDataSourceProvider(IList dataSource, int pageIndex, int pageSize)
        {
            this.m_DataSource = dataSource;
            this.m_PageIndex = pageIndex;
            this.m_PageSize = pageSize;
        }
        /// <summary>
        /// �����ķ�ҳ����Դ
        /// </summary>
        public IList PageDataSource
        {
            get
            {
                if (this.m_DataSource.Count <= this.m_PageSize)//�������Դ��¼�����ҳ��ʾ����ֱ�ӷ���
                    return this.m_DataSource;

                int startIndex = (PageIndex - 1) * PageSize;
                int length = PageSize;
                if (startIndex > this.m_DataSource.Count - 1)
                {
                    startIndex = this.m_DataSource.Count - 1;
                    length = 0;
                }
                else
                {
                    //������ȡ�����鳤�ȡ�
                    length = (this.m_DataSource.Count - startIndex) > this.m_PageSize ? this.m_PageSize : (this.m_DataSource.Count - startIndex);
                }
                object[] sourceArray = new object[this.m_DataSource.Count];
                this.m_DataSource.CopyTo(sourceArray, 0);
                object[] destinationArray = new object[length];

                Array.Copy(sourceArray, startIndex, destinationArray, 0, length);
                return destinationArray;
            }
        }
    }

}
