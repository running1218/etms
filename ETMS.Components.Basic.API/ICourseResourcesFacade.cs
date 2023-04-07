using System;
using System.Data;
using ETMS.AppContext.Component;

using ETMS.Components.Basic.API.Entity;

namespace ETMS.Components.Basic.API
{
    public interface ICourseResourcesFacade : IComponent
    {
        /// <summary>
        /// 获取某课程下的某资源数量(返回可用的资源，即状态为“启用”）
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        int GetResourcesTotal(Guid courseID);
        
        /// <summary>
        /// 获取某课程下的所有资源总数
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        int GetALLResourcesTotal(Guid courseID);

        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <returns>EnumResourcesType枚举</returns>
        EnumResourcesType GetResourcesType();


        /// <summary>
        /// 获取某课程下的可用资源列表(返回可用的资源，即状态为“启用”）
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        DataTable GetResourcesList(Guid courseID,int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords);



    }
}
