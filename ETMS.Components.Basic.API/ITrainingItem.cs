using System;
using System.Data;

using ETMS.AppContext.Component;

namespace ETMS.Components.Basic.API
{
    /// <summary>
    /// 培训项目
    /// </summary>
    public interface ITrainingItem : IComponent
    {
        /// <summary>
        /// 获取某个组织机构下的所有培训项目
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns>DataTable</returns>
        DataTable GetTrainingItemListByOrgID(int orgID);


        /// <summary>
        /// 获取某个组织机构下,在某个时间还可以维护的所有培训项目
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="date">组织机构ID</param>
        /// <returns>DataTable</returns>
        DataTable GetTrainingItemListByOrgIDAndDate(int orgID, DateTime date);



    }
}
