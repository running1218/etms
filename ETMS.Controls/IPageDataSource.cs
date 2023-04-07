using System.Collections;
namespace ETMS.Controls
{
    /// <summary>
    /// 分页数据源接口定义
    /// </summary>
    /// <param name="pageIndex">页面索引，从1开始。</param>
    /// <param name="pageSize">每页显示的数据条数</param>
    /// <param name="total">记录总数</param>
    /// <returns>提取到的数据集合<typeparamref name="System.Collections.IList"/></returns>
    public delegate IList IPageDataSource(int pageIndex, int pageSize, out int total);
}
