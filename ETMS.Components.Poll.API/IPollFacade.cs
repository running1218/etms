using System.Collections.Generic;
using ETMS.AppContext.Component;
using ETMS.Components.Poll.API.Entity;
namespace ETMS.Components.Poll.API
{
    /// <summary>
    /// Poll模块，门面接口定义，继承IComponent
    /// </summary>
    public interface IPollFacade : IComponent
    {
        /// <summary>
        /// 获取用户可看到的调查列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="organizationID">用户所处机构ID</param>
        /// <returns></returns>
        System.Data.DataTable GetQueryListForUser(int userID, int organizationID);


        /// <summary>
        /// 获取用户可看到的调查列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="organizationID">用户所处机构ID</param>
        /// <param name="topSize">头几条数据</param>
        /// <returns></returns>
        IList<Poll_Query> GetQueryListForUser(int userID, int organizationID, int topSize);
    }
}

