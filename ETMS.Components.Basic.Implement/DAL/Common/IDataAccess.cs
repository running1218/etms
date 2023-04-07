using System;

namespace ETMS.Components.Basic.Implement.DAL.Common
{
    public interface IDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="obj"></param>
        void Add(Object obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        void Update(Object obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        void Delete(Object obj);
         
        /// <summary>
        /// 根据ID获取对象实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Object Query(Object id);

        /// <summary>
        /// 获取对象实体列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        Object[] Query(string filter);
        /// <summary>
        /// 获取对象实体列表（支持分页操作）
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        Object[] Query(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount);

    }
}
