<%@ WebHandler Language="C#" Class="ItemCourseRecommendSort" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;

public class ItemCourseRecommendSort : IHttpHandler {
    
   public void ProcessRequest(HttpContext context)
    {
        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //获得推荐课程ID
            string ItemCourseIDs = context.Request.Form["ItemCourseIDs"];
            string[] itemCourses = ItemCourseIDs.Trim(',').Split(',');
            Rec_CourseLogic itemCourseLogic = new Rec_CourseLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemCourses.Length; i++)
            {
                itemCourseLogic.UpdateOrderNumByCourseID(itemCourses[i].ToGuid(), i + 1);
            }
        }
        catch(Exception ex)
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