<%@ WebHandler Language="C#" Class="NoticeService" %>

using System;
using System.Web;
using ETMS.Utility;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.Implement.BLL.Bulletin;

public class NoticeService : IHttpHandler {
    HttpContext currentContext = null;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        currentContext = context;

        context.Response.Write(GetNotice());
        context.Response.End();
    }

    public string GetNotice()
    {
        string orgID = System.Configuration.ConfigurationManager.AppSettings["NoticeOrg"] ?? string.Empty;
        int pageIndex = currentContext.Request["PageIndex"].ToInt();
        int pageSize = currentContext.Request["PageSize"].ToInt();
        int totalRecord = 0;
        var source = new Inf_BulletinLogic().GetBulletinByOrgID(orgID, pageIndex, pageSize, out totalRecord);
        return JsonHelper.GetInvokeSuccessJson(source, totalRecord);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}