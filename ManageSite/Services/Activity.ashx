<%@ WebHandler Language="C#" Class="Activity" %>

using System;
using System.Web;
using ETMS.Utility;
using ETMS.Activity.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Dictionary;

public class Activity : IHttpHandler {

    private static readonly ActivityDirectoryLogic logic = new ActivityDirectoryLogic();
    private HttpContext context = null;
    public void ProcessRequest (HttpContext context) {
        this.context = context;
        context.Response.ContentType = "text/plain";
        context.Response.Write(GetCities());
        context.Response.End();
    }

    private string GetCities()
    {
        string parentID = context.Request["ParentID"].ToString();
        var list = logic.GetAreaListByParent(parentID);
        return JsonHelper.GetInvokeSuccessJson(list);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}