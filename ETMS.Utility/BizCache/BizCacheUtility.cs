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
        /// ����ҵ�񻺴���
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
        /// �������ļ���ȡҵ�񻺴���
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
        /// ��ȡҵ�񻺴�����
        /// </summary>
        /// <param name="bizCacheItemName">ҵ�񻺴�������</param>
        /// <returns>ҵ�񻺴���</returns>
        public static BizCacheItem GetConfig(string bizCacheItemName)
        {
            if (!BizCacheItems.ContainsKey(bizCacheItemName))//��������ļ���δ���ã��򷵻�Ĭ�ϻ�����ڲ���
            {
                BizCacheItem defaultBizCacheItem = new BizCacheItem()
                {
                    CacheKey = bizCacheItemName,
                    CacheDuration = new TimeSpan(0, 5, 0), //Ĭ��5����
                    Enabled = true,
                    Name = bizCacheItemName
                };
                return defaultBizCacheItem;
                // throw new Exception(string.Format("ҵ�񻺴��{0}���������ļ���{1}���в����ڣ��������ã�", bizCacheItemName, FileName));
            }
            return (BizCacheItem)BizCacheItems[bizCacheItemName];
        }
    }
}
