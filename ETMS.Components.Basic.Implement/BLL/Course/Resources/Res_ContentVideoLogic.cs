using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.DAL.Course.Resources;
using ETMS.Utility.Logging;
using System;
using System.Data;

namespace ETMS.Components.Basic.Implement.BLL.Course.Resources
{
    public partial class Res_ContentVideoLogic
    {
        private static readonly Res_ContentVideoDataAccess DAL = new Res_ContentVideoDataAccess();
        /// <summary>
        /// 增加
        /// </summary>
        public void Save(Transcoding transcoding)
        {
            try
            {
                Res_TranscodingQueueDataAccess TranscodingDAL = new Res_TranscodingQueueDataAccess();
                Res_TranscodingQueue resTranscodingQueue = TranscodingDAL.GetById(transcoding.TaskID);
                if (resTranscodingQueue != null)
                {
                    //更新Res_Content表视频时长 
                    //转码后视频插入Res_ContentVideo
                    //删除Res_TranscodingQueue表对应数据

                    DAL.Insert(transcoding);
                    BizLogHelper.AddOperate(transcoding);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException("保存视频转码结果失败");
                throw;
            }

        }

        public ResContent GetById(Guid ContentID,string code)
        {
            ResContent res_Content = DAL.GetTranscodingById(ContentID, code);
            if (res_Content == null)
            {
                throw new ETMS.AppContext.BusinessException("Course.Resources.ResContent.NotFoundException", new object[] { ContentID });
            }

            return res_Content;
        }

        public DataTable GetResourceByCourse(Guid TrainingItemCourseID,int UserID) {
            return DAL.GetResourceByCourse(TrainingItemCourseID, UserID);
        }
        /// <summary>
        /// 根据课程ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        public DataTable GetResourceByCourseID(Guid CourseID)
        {
            return DAL.GetResourceByCourseID(CourseID);
        }

        /// <summary>
        /// 根据课程ID和资源ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="ContentID"></param> 
        /// <returns></returns>
        public DataTable GetResourceByContentID(Guid CourseID,Guid ContentID)
        {
            return DAL.GetResourceByCourseID(CourseID, ContentID);
        }

        /// <summary>
        /// 根据课程ID和资源ID查询资源
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="ContentID"></param> 
        /// <returns></returns>
        public DataTable GetResourceByCourseAndContentID(Guid CourseID, Guid ContentID,String Code)
        {
            return DAL.GetResourceByCourseAndContentID(CourseID, ContentID,Code);
        }
        /// <summary>
        /// 查询课程下视频资源总时间
        /// </summary>
        /// <param name="TrainingItemCourseID">项目课程关系ID</param>
        /// <returns></returns>
        public int GetContentVideoTotalByCourse(Guid TrainingItemCourseID)
        {
            return DAL.GetContentVideoTotalByCourse(TrainingItemCourseID);
        }

        /// <summary>
        /// 学生一门课总学习时长
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetStudentStudyTimeByCourse(Guid trainingItemCourseID, int userID)
        {
            return DAL.GetStudentStudyTimeByCourse(trainingItemCourseID, userID);
        }
        }
}
