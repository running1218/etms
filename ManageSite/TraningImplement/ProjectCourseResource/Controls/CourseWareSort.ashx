<%@ WebHandler Language="C#" Class="CourseWareSort" %>

using System;
using System.Web;
using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;

public class CourseWareSort : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //ID
            string CourseWareIDs = context.Request.Form["CourseWareIDName"];
            string[] itemCourseWareIDs = CourseWareIDs.Trim(',').Split(',');
            Res_ItemCourse_CoursewareLogic CoursewareLogic = new Res_ItemCourse_CoursewareLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemCourseWareIDs.Length; i++)
            {
                CoursewareLogic.ItemCourseResUpdateOrderNum(itemCourseWareIDs[i].ToGuid(), i + 1);
            }
        }
        catch (Exception ex)
        {
            states = "fail" + ex.Message;
        }
        context.Response.Write(states);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}