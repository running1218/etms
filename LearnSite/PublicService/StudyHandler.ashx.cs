using University.Mooc.AppContext;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Configuration;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for StudyHandler
    /// </summary>
    public class StudyHandler : IHttpHandler
    {
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "resource"://课程分类
                    ReturnResponseContent(GetStudyResource());
                    break;
                case "initialize":
                    ReturnResponseContent(StudyInitialize());
                    break;
                case "terminate":
                    ReturnResponseContent(StudyTerminate());
                    return;
                case "completed":
                    ReturnResponseContent(StudyCompleted());
                    return;
                case "time":
                    ReturnResponseContent(GetCurrentTime());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }

        public string GetCurrentTime()
        {
            return JsonHelper.GetInvokeSuccessJson(DateTime.Now);
        }

        /// <summary>
        /// 初始化学习
        /// </summary>
        public string StudyInitialize()
        {
            if (UserContext.Current.UserID == 0) {
                return JsonHelper.GetInvokeFailedJson(-1, "未登录");
            }
            Guid ResourceID = currentContext.Request["ResourceID"].ToGuid();
            Guid trainingItemCourseID = currentContext.Request["trainingItemCourseID"].ToGuid();
            if (ResourceID == Guid.Empty || trainingItemCourseID == Guid.Empty)
            {
                return JsonHelper.GetParametersInValidJson();
            }
            try
            {
                new Sty_UserStudyProgressLogic().StudyInitialize(ResourceID, trainingItemCourseID, UserContext.Current.UserID);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch(Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存学习进度失败");
            }

        }

        /// <summary>
        /// 终止学习
        /// </summary>
        public string StudyTerminate()
        {
            if (UserContext.Current.UserID == 0)
                return JsonHelper.GetInvokeFailedJson(-1, "未登录");

            Guid ResourceID = currentContext.Request["ResourceID"].ToGuid();
            Guid trainingItemCourseID = currentContext.Request["trainingItemCourseID"].ToGuid();
            DateTime startTime = currentContext.Request["StartTime"].ToDateTime();

            if (ResourceID == Guid.Empty)
            {
                return JsonHelper.GetParametersInValidJson();
            }

            if (currentContext.Request["Progress"] == null)
            {
                return JsonHelper.GetParametersInValidJson();
            }

            int progress = -1;
            if (!int.TryParse(currentContext.Request["Progress"].Trim(), out progress))
            {
                return JsonHelper.GetParametersInValidJson();
            }

            try
            {
                new Sty_UserStudyProgressLogic().StudyTerminate(ResourceID, trainingItemCourseID, UserContext.Current.UserID, progress);
                new Sty_UserStudyProgressDetailsLogic().Insert(ResourceID, trainingItemCourseID, UserContext.Current.UserID, startTime, progress);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch(Exception ex)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存学习进度失败");
            }
        }

        /// <summary>
        /// 完成学习
        /// </summary>
        public string StudyCompleted()
        {
            if (UserContext.Current.UserID == 0)
                return JsonHelper.GetInvokeFailedJson(-1, "未登录");

            Guid ResourceID = currentContext.Request["ResourceID"].ToGuid();
            Guid trainingItemCourseID = currentContext.Request["trainingItemCourseID"].ToGuid();
            if (ResourceID == Guid.Empty)
            {
                return JsonHelper.GetParametersInValidJson();
            }
            if (trainingItemCourseID == Guid.Empty)
            {
                return JsonHelper.GetParametersInValidJson();
            }
            int progress = -1;
            if (!int.TryParse(currentContext.Request["Progress"].Trim(), out progress))
            {
                return JsonHelper.GetParametersInValidJson();
            }

            try
            {
                new Sty_UserStudyProgressLogic().StudyCompleted(ResourceID, trainingItemCourseID,UserContext.Current.UserID, progress);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch
            {
                return JsonHelper.GetInvokeFailedJson(-1, "保存学习进度失败");
            }
        }

        public string GetStudyResource()
        {
            var ResourceID = currentContext.Request["ResourceID"].ToGuid();
            var TrainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();
            var ResourceType = currentContext.Request["ResourceType"];
            string code = ConfigurationManager.AppSettings["TransCodingStream"] != null ? ConfigurationManager.AppSettings["TransCodingStream"].ToString() : "";
            CourseContentStudyProgress obj = new Sty_UserStudyProgressLogic().GetUserStudyProgress(UserContext.Current.UserID, ResourceID, TrainingItemCourseID, code);
            FileUploadConfig fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");
            obj.UrlRoot = fileUploadConfig.UrlRoot;
            return JsonHelper.GetInvokeSuccessJson(obj);
        }
        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}