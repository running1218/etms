<%@ WebHandler Language="C#" Class="VideoTranscoding" %>

using System;
using System.Web;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;

public class VideoTranscoding : IHttpHandler
{
    private HttpContext contextCurrent;

    public void ProcessRequest(HttpContext context)
    {
        contextCurrent = context;
        string method = context.Request.QueryString["Method"].ToLower();
        switch (method)
        {
            case "transcoding":
                Transcoding();
                break;
            default:
                break;
        }
    }

    private void Transcoding()
    {
        try
        {
            Guid taskid = contextCurrent.Request.QueryString["taskid"].ToGuid();
            string status = contextCurrent.Request.QueryString["status"].ToString().ToLower();
            decimal duration = contextCurrent.Request.QueryString["duration"] != null ? contextCurrent.Request.QueryString["duration"].ToDecimal() : (decimal)0;
            string outpath = contextCurrent.Request.QueryString["outpath"] != null ? contextCurrent.Request.QueryString["outpath"].ToString() : "";
            outpath = outpath.Substring(outpath.IndexOf('/') + 1);
            if (status == "success")
            {
                Transcoding video = new ETMS.Components.Basic.API.Entity.Course.Transcoding();
                video.TaskID = taskid;
                video.Duration = (int)(duration / 1000);
                video.Outpath = outpath;
                Res_ContentVideoLogic logic = new Res_ContentVideoLogic();
                logic.Save(video);
            }
            else if (status == "error")
            {
                Res_TranscodingQueueLogic logic = new Res_TranscodingQueueLogic();
                logic.VideoTranscoding(taskid);
            }
        }
        catch (Exception ex)
        {
            throw new ETMS.AppContext.BusinessException(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}