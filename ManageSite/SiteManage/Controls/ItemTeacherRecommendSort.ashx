<%@ WebHandler Language="C#" Class="ItemTeacherRecommendSort" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;

public class ItemTeacherRecommendSort : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //获得推荐课程ID
            string ItemTeacherIDs = context.Request.Form["ItemTeacherIDs"];
            string[] itemTeachers = ItemTeacherIDs.Trim(',').Split(',');
            Rec_TeacherLogic itemTeacherLogic = new Rec_TeacherLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemTeachers.Length; i++)
            {
                itemTeacherLogic.UpdateOrderNumByTeacherID(itemTeachers[i].ToInt(), i + 1);
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