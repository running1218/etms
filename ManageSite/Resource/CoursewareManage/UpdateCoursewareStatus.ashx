<%@ WebHandler Language="C#" Class="UpdateCoursewareStatus" %>

using System;
using System.Web;
using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;

public class UpdateCoursewareStatus : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        UpdateStatus(context);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public void UpdateStatus(HttpContext context)
    { 
        Guid coursewareId = context.Request["CoursewareID"].ToGuid();
        int resourceStatus = context.Request["ResourceStatus"].ToInt();
        string resourcePath = context.Request["ResourcePath"].ToString();

        new Res_CoursewareLogic().UpdateResourceStatus(coursewareId, resourceStatus, resourcePath);
    }
}