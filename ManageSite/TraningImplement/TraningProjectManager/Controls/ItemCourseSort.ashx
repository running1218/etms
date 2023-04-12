<%@ WebHandler Language="C#" Class="ItemCourseSort" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;

public class ItemCourseSort : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //获得项目课程ID
            string ItemCourseIDs = context.Request.Form["ItemCourseIDName"];
            string[] itemCourses = ItemCourseIDs.Trim(',').Split(',');
            Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemCourses.Length; i++)
            {
                itemCourseLogic.UpdateOrderNumByItemCourseID(itemCourses[i].ToGuid(), i + 1);
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