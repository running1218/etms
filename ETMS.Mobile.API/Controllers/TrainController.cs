using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Web.Http.Cors;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using System.Configuration;
using ETMS.Components.OnlinePlaying.Implement.BLL;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Train")]
    public class TrainController : ApiController
    {
        [Route("Course/{UserID}/{module}", Name = "获取培训项目的课程列表信息")]
        public HttpResponseMessage GetTrainCourseList(int UserID, int module)
        {
            DataTable dtTrainCourse = new Tr_ItemCourseLogic().GetTrainingItemCourseLisListByUserID(UserID);
            List<TrainItemCourse> itemCourse = new List<TrainItemCourse>();
            foreach (DataRow drTrainCourse in dtTrainCourse.Rows)
            {
                TrainItemCourse entity = new TrainItemCourse();
                entity.TrainingItemID = drTrainCourse["TrainingItemID"].ToGuid();
                entity.TrainingItemName= drTrainCourse["ItemName"].ToString();
                entity.CourseID = drTrainCourse["CourseID"].ToGuid();
                entity.CourseName = drTrainCourse["CourseName"].ToString();
                entity.ThumbnailURL = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(drTrainCourse["ThumbnailURL"].ToString()) ? "default.jpg" : drTrainCourse["ThumbnailURL"].ToString());
                entity.CourseBeginTime= drTrainCourse["CourseBeginTime"].ToDateTime().ToString("yyyy-MM-dd");
                entity.CourseEndTime = drTrainCourse["CourseEndTime"].ToDateTime().ToString("yyyy-MM-dd");
                entity.TrainingItemCourseID = drTrainCourse["TrainingItemCourseID"].ToGuid();
                entity.CourseModel = drTrainCourse["CourseModel"].ToInt();
                entity.LivingType = drTrainCourse["LivingType"].ToInt();
                //获取学习进度
                //entity.StudyProcessPercent = new Sty_UserStudyProgressLogic().GetStudyProcessPercent(UserID,entity.TrainingItemCourseID);
                itemCourse.Add(entity);
            }
            if (itemCourse.Count > 0)
                itemCourse = itemCourse.Where(f => f.CourseModel == module).ToList();
            return ResponseJson.GetSuccessJson(itemCourse);
        }

        [Route("Course/Resource/{TrainingItemCourseID}/{UserID}", Name = "获取培训项目课程的资源列表信息")]
        public HttpResponseMessage GetResourceByTrainingItemCourse(Guid TrainingItemCourseID, int UserID)
        {
            DataTable dt = new Res_ContentVideoLogic().GetResourceByCourse(TrainingItemCourseID, UserID);
            return ResponseJson.GetSuccessJson(dt);
        }

        [Route("Evaluation/{UserID}", Name = "获取培训项目的测评列表信息")]
        public HttpResponseMessage GetTrainEvaluationList(int UserID)
        {
            DataTable dtTrainEvaluation = new Ex_StudentEvaluationLogic().GetStudentEvaluationListByUserID(UserID);
            //设置测试的允许提交次数
            foreach (DataRow dr in dtTrainEvaluation.Rows)
            {
                if (dr["TestCount"].ToInt() == 0)
                {
                    dr["TestCount"] = string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrgMaxExamNum"]) ? 3 : ConfigurationManager.AppSettings["OrgMaxExamNum"].ToInt();
                }
            }
            return ResponseJson.GetSuccessJson(dtTrainEvaluation);
        }

        [Route("Evaluation/{ItemCourseID}/{UserID}", Name = "获取培训项目课程的测评列表信息")]
        public HttpResponseMessage GetTrainCourseEvaluationList(Guid ItemCourseID,int UserID)
        {
            DataTable dtTrainEvaluation = new Ex_StudentEvaluationLogic().GetStudentEvaluationListByItemCourseIDAndUserID(UserID, ItemCourseID);
            //设置测试的允许提交次数
            foreach (DataRow dr in dtTrainEvaluation.Rows)
            {
                if (dr["TestCount"].ToInt() == 0)
                {
                    dr["TestCount"] = string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrgMaxExamNum"]) ? 3 : ConfigurationManager.AppSettings["OrgMaxExamNum"].ToInt();
                }
            }
            return ResponseJson.GetSuccessJson(dtTrainEvaluation);
        }
        [Route("Living/{courseID}/{trainingItemCourseID}/{userID}", Name = "获取我的直播课程和学习状态")]
        public HttpResponseMessage GetUserCourseLivings(string courseID, Guid trainingItemCourseID, int userID)
        {
            OnlinePlayingLogic logic = new OnlinePlayingLogic();
            var list = logic.GetLivingByCourseID(courseID.ToGuid());
            var styResult = new StudentOnlinePlaying().GetStyLivingsByUserCourse(userID.ToInt(), trainingItemCourseID);
            foreach (var item in list)
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
                item.Type = 1;
            }
            return ResponseJson.GetSuccessJson(list);
        }
    }
}
