using System;

using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace ETMS.Utility.BizCache
{
    public class BizCacheUtility
    {
        const string FileName = "~/Config/BizCache.config";
        const string SectionName = "bizCache";
        const string ItemName = "bizCacheItem";
        const string CacheKey = "CacheKey::BizCache";

        /// <summary>
        /// 所有业务缓存项
        /// </summary>
        private static Hashtable BizCacheItems
        {
            get
            {
                Hashtable _BizCacheItems = (Hashtable)CacheHelper.Get(CacheKey);
                if (_BizCacheItems == null)
                {
                    _BizCacheItems = GetBizCacheConfig();

                    string phyFileName = "";
                    if (HttpContext.Current != null)
                    {
                        phyFileName = HttpContext.Current.Server.MapPath(FileName);
                    }
                    else
                    {
                        phyFileName = FileName.Replace("~/", AppDomain.CurrentDomain.BaseDirectory);
                    }
                    CacheDependency dependency = new CacheDependency(phyFileName);
                    CacheHelper.AddPermanent(CacheKey, _BizCacheItems, dependency);
                }

                return _BizCacheItems;
            }
        }

        /// <summary>
        /// 从配置文件获取业务缓存项
        /// </summary>
        /// <returns></returns>
        private static Hashtable GetBizCacheConfig()
        {
            Hashtable _BizCacheItems = new Hashtable();

            XmlDocument doc = new XmlDocument();
            string phyFileName = "";
            if (HttpContext.Current != null)
            {
                phyFileName = HttpContext.Current.Server.MapPath(FileName);
            }
            else
            {
                phyFileName = FileName.Replace("~/", AppDomain.CurrentDomain.BaseDirectory);
            }
            doc.Load(phyFileName);
            XmlNode node = doc.SelectSingleNode(SectionName);
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.NodeType == XmlNodeType.Element && n.Name == ItemName)
                {
                    BizCacheItem bizCacheItem = new BizCacheItem(
                        n.Attributes["name"].Value.Trim(),
                        n.Attributes["enabled"].Value.Trim() == "true",
                        n.Attributes["cacheKey"].Value.Trim(),
                        TimeSpan.Parse(n.Attributes["cacheDuration"].Value.Trim()),
                        n.Attributes["description"] != null ? n.Attributes["description"].Value : null
                        );

                    _BizCacheItems.Add(bizCacheItem.Name, bizCacheItem);
                }
            }

            return _BizCacheItems;
        }

        /// <summary>
        /// 获取业务缓存配置
        /// </summary>
        /// <param name="bizCacheItemName">业务缓存项名称</param>
        /// <returns>业务缓存项</returns>
        public static BizCacheItem GetConfig(string bizCacheItemName)
        {
            if (!BizCacheItems.ContainsKey(bizCacheItemName))//如果配置文件中未配置，则返回默认缓存过期策略
            {
                BizCacheItem defaultBizCacheItem = new BizCacheItem()
                {
                    CacheKey = bizCacheItemName,
                    CacheDuration = new TimeSpan(0, 5, 0), //默认5分钟
                    Enabled = true,
                    Name = bizCacheItemName
                };
                return defaultBizCacheItem;
                // throw new Exception(string.Format("业务缓存项“{0}”在配置文件“{1}”中不存在，请检查配置！", bizCacheItemName, FileName));
            }
            return (BizCacheItem)BizCacheItems[bizCacheItemName];
        }
    }
}
