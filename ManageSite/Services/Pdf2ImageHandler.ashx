<%@ WebHandler Language="C#" Class="Pdf2ImageHandler" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;

public class Pdf2ImageHandler : IHttpHandler {
    private HttpContext context = null;

    public void ProcessRequest (HttpContext context) {
        this.context = context;
        context.Response.ContentType = "text/plain";
        context.Response.Write(Pdf2ImageHandlerMethod());
        context.Response.End();
    }

    private string Pdf2ImageHandlerMethod()
    {
        string contentID = context.Request["ID"].ToString();
        string pages = context.Request["Pages"].ToString();

        var entity = new Res_ContentLogic().GetByID(contentID.ToGuid());
        entity.PlayTime = pages.ToInt();
        entity.ModifyTime = DateTime.Now;
        new Res_ContentLogic().Update(entity);
        return "ok";
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}