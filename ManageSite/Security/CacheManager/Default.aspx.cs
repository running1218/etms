using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Utility;
using ETMS.Controls;

public class CacheEnumItem
{
    public string CacheKey { get; set; }
    public string Description { get; set; }
    public int SubCacheItemCount { get; set; }
}
public partial class Admin_CacheManager_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.gvRoles, new IPageDataSource(this.QueryData));

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 分页数据源实现
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    private IList QueryData(int pageIndex, int pageSize, out int totalRecords)
    {
        IDictionary<string, int> cachedKeys = CacheHelper.GetCacheKeyGroups();
        totalRecords = cachedKeys.Count;
        List<CacheEnumItem> findCaches = new List<CacheEnumItem>();
        foreach (KeyValuePair<string, int> cachedKey in cachedKeys)
        {
            ETMS.Utility.BizCache.BizCacheItem configItem = ETMS.Utility.BizCache.BizCacheUtility.GetConfig(cachedKey.Key);
            findCaches.Add(new CacheEnumItem()
                {
                    CacheKey = cachedKey.Key,
                    Description = configItem.Description,
                    SubCacheItemCount = cachedKey.Value,
                });
        }
        //return findCaches;
        //由于当前的数据源非分页数据源，因此需要通过适配器进行转换。
        PageDataSourceProvider provider = new PageDataSourceProvider(findCaches, pageIndex, pageSize);
        return provider.PageDataSource;
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] clearKeys = CustomGridView.GetSelectedValues<string>(this.gvRoles);
        //1、清除选择的cache项
        foreach (string key in clearKeys)
        {
            CacheHelper.RemoveCacheItem(key);
        }
        //2、重新绑定数据 
        this.PageSet1.DataBind();
        JsUtility.SuccessMessageBox("清除选中的缓存操作成功！");
    }
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    { 
        //1、清除选择的cache项
        foreach (string key in CacheHelper.GetCacheKeyGroups().Keys)
        {
            CacheHelper.RemoveCacheItem(key);
        }
        //2、重新绑定数据 
        this.PageSet1.DataBind();
        JsUtility.SuccessMessageBox("清除全部的缓存操作成功！");
    }
}
