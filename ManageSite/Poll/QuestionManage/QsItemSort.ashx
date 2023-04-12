<%@ WebHandler Language="C#" Class="QsItemSort" %>

using System;
using System.Web;
using System.Collections.Generic;
using ETMS.Components.Poll.API.Entity;

public class QsItemSort : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string actionName = context.Request.Params["ActionName"];
        string returnMsg = "";
        switch (actionName)
        {
            case "qsItemSort":
                {
                    returnMsg = qsItemSort(context);
                    break;
                }
        }
        context.Response.Write(returnMsg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private string qsItemSort(HttpContext context)
    {
        string msg = "";
        bool temp = false;
        string queryTitleList = context.Request.Params["queryTitleList"];
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<Poll_Title> queryTitleEntitylist = new List<Poll_Title>();
        try
        {
            var p = serializer.Deserialize<List<Poll_Title>>(queryTitleList);
            new ETMS.Components.Poll.Implement.BLL.Poll_TitleLogic().UpdateQSTitleSort(p);
            msg = "";
            temp = true;
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
            temp = false;
        }
        return serializer.Serialize(new ReturnValue(temp, msg));
    }
}

public class ReturnValue
{
    public ReturnValue(bool err, object obj)
    {
        error = err;
        msg = obj;
    }
    public bool error { get; set; }

    /// <summary>
    /// 数据主体
    /// </summary>
    public object msg { get; set; }
}