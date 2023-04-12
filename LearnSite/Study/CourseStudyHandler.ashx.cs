using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility;
using System;
using System.Web;

namespace ETMS.Studying.Study
{
    /// <summary>
    /// Summary description for CourseStudy1
    /// </summary>
    public class CourseStudyHandler : IHttpHandler
    {
        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = context.Request["Method"];
            switch (method.ToLower())
            {
                case "addcourse":
                    ReturnResponseContent(AddCourse());
                    break;
            }
        }
        
        private string AddCourse()
        {
            try
            {
                var trainingItemCourseID = currentContext.Request["TrainingItemCourseID"].ToGuid();
                var studentSignupID = currentContext.Request["SignupID"].ToString();
                Sty_StudentCourseLogic logic = new Sty_StudentCourseLogic();
                logic.AddStudentSelectCourse(trainingItemCourseID, studentSignupID, UserContext.Current.UserID, UserContext.Current.RealName);
                return JsonHelper.GetInvokeSuccessJson();
            }
            catch (Exception)
            {
                return JsonHelper.GetInvokeFailedJson(-1, "选修课程失败");
            }
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