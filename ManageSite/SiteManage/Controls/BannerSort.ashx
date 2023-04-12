<%@ WebHandler Language="C#" Class="BannerSort" %>

using System;
using System.Web;
using ETMS.Components.Basic.Implement.BLL.Operation;
using ETMS.Utility;
public class BannerSort : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string states = "success";
        context.Response.ContentType = "text/plain";
        try
        {
            //获得推荐课程ID
            string ItemBannerIDs = context.Request.Form["ItemBannerIDs"];
            string[] itemBanners = ItemBannerIDs.Trim(',').Split(',');
            BannerSpreadLogic bannerSpreadLogic = new BannerSpreadLogic();
            //新的排序ID更新到表中
            for (int i = 0; i < itemBanners.Length; i++)
            {
                bannerSpreadLogic.UpdateOrderNumByBannerSpreadID(itemBanners[i].ToGuid(), i + 1);
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