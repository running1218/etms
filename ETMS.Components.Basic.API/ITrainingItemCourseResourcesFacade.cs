using System;
using System.Data;
using ETMS.AppContext.Component;

namespace ETMS.Components.Basic.API
{

    /// <summary>
    /// 培训项目课程资源接口：
    /// huangzhf
    /// 2012-04-07
    /// </summary>
    public interface ITrainingItemCourseResourcesFacade : IComponent
    {


                /// <summary>
        /// 根据课程编号获取课件资源总数
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        Int32 GetResourcesTotal(Guid courseID);


        /// <summary>
        /// 获取某培训项目课程下的某资源数量
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        int GetTrainingItemResourcesTotal(Guid trainingItemCourseID);



        /// <summary>
        /// 获取某培训项目课程下的某资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords);


        /// <summary>
        /// 获取某个培训项目课程未使用的课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords);



    }

}
