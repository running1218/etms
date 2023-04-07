using System;
using System.Collections;
namespace ETMS.Controls
{

    /// <summary>
    /// 分页数据源适配器
    /// 使用场景：如果提供的数据源不支持分页，且需要使用分页控件来显示数据时，通过此适配器进行数据源转换。
    /// </summary>
    public class PageDataSourceProvider
    {
        private IList m_DataSource;
        private int m_PageIndex;
        private int m_PageSize;
        /// <summary>
        /// 原始数据源
        /// </summary>
        public IList OriginalDataSource
        {
            get
            {
                return this.m_DataSource;
            }
        }
        /// <summary>
        /// 请求页面索引号
        /// 要求：从1开始
        /// </summary>
        public int PageIndex
        {
            get
            {
                return this.m_PageIndex;
            }
        }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
        }

        /// <summary>
        /// 分页数据源适配器构造函数
        /// </summary>
        /// <param name="dataSource">待适配的数据源</param>
        /// <param name="pageIndex">请求页面索引号</param>
        /// <param name="pageSize">请求页面大小</param>
        public PageDataSourceProvider(System.Data.DataTable dataSource, int pageIndex, int pageSize)
            : this(((System.ComponentModel.IListSource)dataSource).GetList(), pageIndex, pageSize)
        {
        }
        /// <summary>
        /// 分页数据源适配器构造函数
        /// </summary>
        /// <param name="dataSource">待适配的数据源</param>
        /// <param name="pageIndex">请求页面索引号</param>
        /// <param name="pageSize">请求页面大小</param>
        public PageDataSourceProvider(IList dataSource, int pageIndex, int pageSize)
        {
            this.m_DataSource = dataSource;
            this.m_PageIndex = pageIndex;
            this.m_PageSize = pageSize;
        }
        /// <summary>
        /// 适配后的分页数据源
        /// </summary>
        public IList PageDataSource
        {
            get
            {
                if (this.m_DataSource.Count <= this.m_PageSize)//如果数据源记录不足多页显示，则直接返回
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
                    //设置提取的数组长度。
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
