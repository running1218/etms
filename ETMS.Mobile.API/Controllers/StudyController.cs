using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility.Service.FileUpload;
using System.Configuration;
using ETMS.Utility.Service;
using ETMS.Utility.Cryptography;
using System.Data;
using ETMS.Components.OnlinePlaying.Implement.BLL;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Study")]
    public class StudyController : ApiController
    {
        /// <summary>
        /// 初始化学习进度
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="StudentID">学生ID</param>
        /// <returns></returns>
        [Route("Initialize/{TrainingItemCourseID}/{ResourceID}/{StudentID}", Name = "初始化学习进度")]
        public HttpResponseMessage PostInitializeStudyProgress(Guid TrainingItemCourseID, Guid ResourceID, int StudentID)
        {
            try
            {
                new Sty_UserStudyProgressLogic().StudyInitialize(ResourceID, TrainingItemCourseID, StudentID);
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }
            //返回当前服务器时间
            return ResponseJson.GetSuccessJson(DateTime.Now);

        }
        /// <summary>
        /// 终止学习进度
        /// </summary>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="StudyProgress">学习进度</param>
        /// <param name="StartTime">开始学习时间（精确到秒）</param>
        /// <param name="StudentID">学生ID</param>
        /// <returns></returns>
        [Route("Terminate/{TrainingItemCourseID}/{ResourceID}/{StudyProgress}/{StartTime}/{StudentID}", Name = "终止学习进度")]
        public HttpResponseMessage PostTerminateStudyProgress(Guid TrainingItemCourseID, Guid ResourceID, int StudyProgress, string StartTime, int StudentID)
        {
            try
            {
                DateTime startTime = Base64Utility.Base64Decode(StartTime).ToDateTime();
                new Sty_UserStudyProgressLogic().StudyTerminate(ResourceID, TrainingItemCourseID, StudentID, StudyProgress);
                new Sty_UserStudyProgressDetailsLogic().Insert(ResourceID, TrainingItemCourseID, StudentID, startTime, StudyProgress);
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }
            return ResponseJson.GetSuccessJson();
        }
        /// <summary>
        /// 完成学习进度
        /// </summary>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="StudyProgress">学习进度</param>
        /// <param name="StudentID">学生ID</param>
        /// <returns></returns>
        [Route("Completed/{TrainingItemCourseID}/{ResourceID}/{StudyProgress}/{StudentID}", Name = "完成学习进度")]
        public HttpResponseMessage PostCompletedStudyProgress(Guid TrainingItemCourseID, Guid ResourceID, int StudyProgress, int StudentID)
        {
            try
            {
                new Sty_UserStudyProgressLogic().StudyCompleted(ResourceID, TrainingItemCourseID, StudentID, StudyProgress);
            }
            catch (Exception ex)
            {
                return ResponseJson.GetFailedJson(ex);
            }
            return ResponseJson.GetSuccessJson();
        }
        /// <summary>
        /// 获取学生学习资源的进度信息
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="ResourceID">资源ID</param>
        /// <param name="ResourceType">资源类型【1-视频；2-文档】</param>
        /// <param name="StudentID">学生ID</param>
        /// <returns></returns>
        [Route("Resource/{TrainingItemCourseID}/{ResourceID}/{ResourceType}/{StudentID}", Name = "获取学生学习资源的进度信息")]
        public HttpResponseMessage GetStudyResourceContent(Guid TrainingItemCourseID, Guid ResourceID, int ResourceType, int StudentID)
        {
            string code = ConfigurationManager.AppSettings["TransCodingStream"] ?? string.Empty;
            CourseContentStudyProgress obj = new Sty_UserStudyProgressLogic().GetUserStudyProgress(StudentID, ResourceID, TrainingItemCourseID, code);
            FileUploadConfig fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");
            obj.UrlRoot = fileUploadConfig.UrlRoot;
            obj.ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(obj.ThumbnailURL) ? "default.jpg" : obj.ThumbnailURL);
            return ResponseJson.GetSuccessJson(obj);
        }
        //TrainingItemCourseID
        /// <summary>
        /// 获取用户最后学习的资源信息
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="StudentID">学生ID</param>
        /// <returns></returns>
        [Route("Resource/{TrainingItemCourseID}/{StudentID}", Name = "获取用户最后学习的资源信息")]
        public HttpResponseMessage GetUserStudyLastContent(Guid TrainingItemCourseID,int StudentID)
        {
            string ResourceID = string.Empty;
            DataTable dt= new Sty_UserStudyProgressLogic().GetUserStudyLastContent(TrainingItemCourseID, StudentID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ResourceID = dt.Rows[0]["ContentID"].ToString();
            }
            return ResponseJson.GetSuccessJson(ResourceID);
        }

        [Route("Livings/{courseID}/{userID}/{trainingItemCourseID}", Name ="获取学生直播课程列表和学习情况")]
        public HttpResponseMessage GetUserStudyLiving(string courseID, int userID, string trainingItemCourseID)
        {
            var result = new OnlinePlayingLogic().GetLivingByCourseID(courseID.ToGuid());
            var styResult = new StudentOnlinePlaying().GetStyLivingsByUserCourse(userID, trainingItemCourseID.ToGuid());

            foreach (var item in result)
            {
                if (styResult != null)
                {
                    if (styResult.Count(f => f.LivingID == item.LivingID) > 0)
                    {
                        item.StyStatus = 1;
                    }
                    else
                    {
                        item.StyStatus = -1;
                    }
                }
            }

            return ResponseJson.GetSuccessJson(result);
        }
    }
}
