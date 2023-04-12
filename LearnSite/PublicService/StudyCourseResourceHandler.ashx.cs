using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System.Configuration;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for StudyCourseResourceHandler
    /// </summary>
    public class StudyCourseResourceHandler : IHttpHandler
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
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }
        public string GetStudyResource()
        {
            var ResourceID = currentContext.Request["ResourceID"].ToGuid();
            var CourseID = currentContext.Request["CourseID"].ToGuid();
            var ResourceType = currentContext.Request["ResourceType"];
            var code = ConfigurationManager.AppSettings["TransCodingStream"] != null ? ConfigurationManager.AppSettings["TransCodingStream"].ToString() : "";
            CourseContentStudyProgress obj = new Res_ContentVideoLogic().GetResourceByCourseAndContentID(CourseID, ResourceID,code).Rows[0].ToEntity<CourseContentStudyProgress>();
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