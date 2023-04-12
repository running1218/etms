<%@ WebHandler Language="C#" Class="Produciton" %>

using System;
using System.Web;
using ETMS.Activity.Implement.BLL;
using ETMS.Utility;
using ETMS.AppContext;

public class Produciton : IHttpHandler {

    private static readonly AppraisalLogic logic = new AppraisalLogic();
    private HttpContext currentContext = null;
    public void ProcessRequest(HttpContext context)
    {
        currentContext = context;
        string method = currentContext.Request["Method"];
        if (string.IsNullOrEmpty(method))
        {
            ReturnResponseContent(JsonHelper.GetParametersInValidJson());
        }
        switch (method.ToLower())
        {
            case "getproductioninfo":
                ReturnResponseContent(GetProductionInfo());
                break;
            case "approveactivity":
                ReturnResponseContent(ApproveActivity());
                break;
            default:
                break;
        }
    }

    public string GetProductionInfo()
    {
        var producitonID = currentContext.Request["ID"].ToGuid();
        var entity = new ProductionLogic().GetProduction(producitonID);
        return JsonHelper.GetInvokeSuccessJson(entity);
    }

    public string ApproveActivity()
    {
        Guid producitonID = currentContext.Request["ID"].ToGuid();
        string score = currentContext.Request["Score"];
        string comment = currentContext.Request["Comment"];

        var entity = new ProductionLogic().GetProduction(producitonID);
        entity.Score = score.ToDecimal();
        entity.Comment = comment;
        entity.AppraiseStatus = 1;
        entity.AppraiseTime = DateTime.Now;
        entity.Appraiser = UserContext.Current.UserID;
        new ProductionLogic().Mark(entity);

        return JsonHelper.GetInvokeSuccessJson();
    }
    private void ReturnResponseContent(string content)
    {
        currentContext.Response.Clear();
        currentContext.Response.ContentType = "text/json";
        currentContext.Response.Write(content);
        currentContext.Response.End();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}