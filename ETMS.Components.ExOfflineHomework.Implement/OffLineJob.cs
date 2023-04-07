using System;
using ETMS.AppContext.Component;
using ETMS.Components.ExOfflineHomework.API;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.ExOfflineHomework.Implement.BLL;


namespace ETMS.Components.ExOfflineHomework.Implement
{
    public class OffLineJob : DefaultComponent,IOffLineJob
    {




        #region IOffLineJob 成员


        /// <summary>
        /// 根据离线作业ID，获取其基本信息
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <returns>返回DataTable</returns>
        public System.Data.DataTable GetOfflinJobeListByPK(string jobID)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据离线作业ID，获取其基本信息
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <returns>返回Res_e_OffLineJob实体</returns>
        public Res_e_OffLineJob GetOfflineJobByPK(string jobID)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 根据培训项目课程ID，获取其所有的离线作业
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回DataTable</returns>
        /// <returns></returns>
        public System.Data.DataTable GetOfflineJobListByItemCoursePK(string trainingItemCourseID)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 设置某个离线作业到某个项目课程下
        /// </summary>
        /// <param name="jobID">离线作业ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>如果成功返回0，返回负数说明不成功</returns>
        public int SetOfflineJobToItemCourse(string jobID, string trainingItemCourseID)
        {
            //初始化一个项目课程离线作业实体
            Res_ItemCourseOffLineJob itemCourseOffLineJobEntity = new Res_ItemCourseOffLineJob();
            itemCourseOffLineJobEntity.ItemCourseOffLineJobID = new Guid();
            itemCourseOffLineJobEntity.JobID = new Guid(jobID);
            itemCourseOffLineJobEntity.TrainingItemCourseID = new Guid(trainingItemCourseID);

            //获取离线作业对象
            Res_e_OffLineJobLogic jobLogic = new Res_e_OffLineJobLogic();
            Res_e_OffLineJob jobEntity = jobLogic.GetById(itemCourseOffLineJobEntity.ItemCourseOffLineJobID);
            //将离线作业的内容复制到项目课程离线作业实体上
            itemCourseOffLineJobEntity.IsUse = jobEntity.IsUse;
            itemCourseOffLineJobEntity.BeginTime = jobEntity.BeginTime;
            itemCourseOffLineJobEntity.EndTime = jobEntity.EndTime;
            itemCourseOffLineJobEntity.CreateTime = jobEntity.CreateTime;

            Res_ItemCourseOffLineJobLogic action = new Res_ItemCourseOffLineJobLogic();
            action.Add(itemCourseOffLineJobEntity);
            return 0;
        }



        /// <summary>
        /// 根据培训项目课程离线作业ID，获取其所有的基本信息
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns>返回DataTable</returns>
        /// <returns></returns>
        public System.Data.DataTable GetItemCourseOffLineJobByPK(string itemCourseOffLineJobID)
        {
            throw new NotImplementedException();
        }





        #endregion
    }
}
