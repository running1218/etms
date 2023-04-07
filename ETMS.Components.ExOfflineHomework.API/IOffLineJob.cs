using System.Data;

using ETMS.AppContext.Component;
using ETMS.Components.ExOfflineHomework.API.Entity;

namespace ETMS.Components.ExOfflineHomework.API
{


    /// <summary>
    /// 离线作业接口
    /// </summary>
    public interface IOffLineJob : IComponent
    {
        /// <summary>
        /// 根据离线作业ID，获取其基本信息
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <returns>返回DataTable</returns>
        DataTable GetOfflinJobeListByPK(string jobID);

        /// <summary>
        /// 根据离线作业ID，获取其基本信息
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <returns>返回Res_e_OffLineJob实体</returns>
        Res_e_OffLineJob GetOfflineJobByPK(string jobID);


        /// <summary>
        /// 根据培训项目课程ID，获取其所有的离线作业
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回DataTable</returns>
        /// <returns></returns>
        DataTable GetOfflineJobListByItemCoursePK(string trainingItemCourseID);



        /// <summary>
        /// 设置某个离线作业到某个项目课程下
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>如果成功返回0，返回负数说明不成功</returns>
        int SetOfflineJobToItemCourse(string jobID, string trainingItemCourseID);


        /// <summary>
        /// 设置某个离线作业到某个项目课程下
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="beginTime">作业的开始时间</param>
        /// <param name="endTime">作业的结束时间</param>
        /// <returns>如果成功返回1，返回负数说明不成功</returns>
        //int SetOfflineJobToItemCourse(string jobID, string trainingItemCourseID, DateTime beginTime, DateTime endTime);



        /// <summary>
        /// 根据培训项目课程离线作业ID，获取其所有的基本信息
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns>返回DataTable</returns>
        /// <returns></returns>
        DataTable GetItemCourseOffLineJobByPK(string itemCourseOffLineJobID);


        /// <summary>
        /// 根据培训项目课程离线作业ID，获取其所有的基本信息
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns>返回Res_ItemCourseOffLineJob实体</returns>
        /// <returns></returns>
        //Res_ItemCourseOffLineJob GetItemCourseOffLineJobByPK(string itemCourseOffLineJobID);



    }
}
