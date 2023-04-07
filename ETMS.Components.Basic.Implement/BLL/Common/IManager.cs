namespace ETMS.Components.Basic.Implement.BLL.Common
{
    /// <summary>
    /// 有关联关系数据管理类接口
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// 管理者
        /// </summary>
        ETMS.AppContext.IObject Manager { get;set;}

        /// <summary>
        /// 关联被管理者
        /// </summary>
        /// <param name="byManager">被管理者</param>
        void Associate(ETMS.AppContext.IObject member);

        /// <summary>
        /// 解除关联被管理者
        /// </summary>
        /// <param name="byManager">被管理者</param>
        void ReleaseAssociate(ETMS.AppContext.IObject member);
        
        /// <summary>
        /// 获取所有被管理者列表
        /// </summary>
        /// <returns></returns>
        ETMS.AppContext.IObject[] GetAllMembers(string filter);

        /// <summary>
        /// 获取所有被管理者列表(支持分页)
        /// </summary>
        /// <returns></returns>
        ETMS.AppContext.IObject[] GetAllMembers(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount);

        /// <summary>
        /// 根据被管理者主键值来获取被管理者
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        ETMS.AppContext.IObject GetMemberByPkValue(ETMS.AppContext.IObject pk);
    }
}
