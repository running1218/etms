using System;
using System.Collections.Generic;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// URL管理
    /// </summary>
    public class PageUrlLogic
    {
        private IDataAccess DAL = new PageUrlDataAccess();

        public void Save(PageUrl pageUrl)
        {
            if (pageUrl.PageID == 0)
            {
                DAL.Add(pageUrl);
            }
            else
            {
                DAL.Update(pageUrl);
            }
        }

        public void Remove(PageUrl pageUrl)
        {
            DAL.Delete(pageUrl);
        }

        public PageUrl GetPageUrlByID(int pageID)
        {
            return (PageUrl)DAL.Query((int)pageID);
        }

        public string[] GetAllRegisterUrls()
        {
            /*
             * 缓存支持
             */
            //config/BizCache.config中定义缓存过期策略
            string key = "PageUrls";
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<String[]>(key, () =>
            {
                List<string> list = new List<string>();
                foreach (PageUrl item in (PageUrl[])DAL.Query(" AND Status=1 "))
                {
                    list.Add(item.PageURL);
                }
                return list.ToArray();
            });
        }
    }
}
