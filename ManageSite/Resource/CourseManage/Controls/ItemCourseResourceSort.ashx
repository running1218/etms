<%@ WebHandler Language="C#" Class="ItemCourseResourceSort" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;

public class ItemCourseResourceSort : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //获得课程资源ID
            string ItemCourseContentIDs = context.Request.Form["ItemCourseContentIDs"];
            string[] itemContents = ItemCourseContentIDs.Trim(',').Split(',');
            Res_ContentLogic contentLogic = new Res_ContentLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemContents.Length; i++)
            {
                contentLogic.UpdateOrderNumByCourseContentID(itemContents[i].ToGuid(), i + 1);
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