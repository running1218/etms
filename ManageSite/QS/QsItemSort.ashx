<%@ WebHandler Language="C#" Class="QsItemSort" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ETMS.Components.QS.API.Entity;
using System.Collections.Generic;
using ETMS.Components.QS.Implement.BLL;

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
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<QS_QueryTitle> queryTitleEntitylist = new List<QS_QueryTitle>();
        try
        {
            queryTitleEntitylist = serializer.Deserialize<List<QS_QueryTitle>>(queryTitleList);
            new QS_QueryTitleLogic().UpdateQSTitleSort(queryTitleEntitylist);
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