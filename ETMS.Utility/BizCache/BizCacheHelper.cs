using System;

namespace ETMS.Utility.BizCache
{
    public delegate T CacheItemNotExistsHandler<T>();
    /// <summary>
    /// cacheHelper
    /// </summary>
    public static class BizCacheHelper
    {
        public static void AddCacheItem<T>(String cacheItemEnum, T cacheItem)
        {
            AddCacheItem<T>(cacheItemEnum, null, cacheItem);
        }

        public static void AddCacheItem<T>(String cacheItemEnum, string cacheItemKey, T cacheItem)
        {
            BizCacheItem bizCacheItem = BizCacheUtility.GetConfig(cacheItemEnum);
            if (bizCacheItem.Enabled)
            {
                if (string.IsNullOrEmpty(cacheItemKey))
                {
                    CacheHelper.Add(bizCacheItem.CacheKey, cacheItem, bizCacheItem.CacheDuration);
                }
                else
                {
                    string cacheKeyFormat = bizCacheItem.CacheKey;
                    if (cacheKeyFormat.IndexOf("{0}") == -1)
                    {
                        cacheKeyFormat += "/{0}";//自动追加"/{0}"参数
                    }
                    CacheHelper.Add(string.Format(cacheKeyFormat, cacheItemKey), cacheItem, bizCacheItem.CacheDuration);
                }
            }
        }

        public static T GetCacheItem<T>(String cacheItemEnum)
        {
            return GetCacheItem<T>(cacheItemEnum, null);
        }

        public static T GetCacheItem<T>(String cacheItemEnum, string cacheItemKey)
        {
            BizCacheItem bizCacheItem = BizCacheUtility.GetConfig(cacheItemEnum);
            if (bizCacheItem.Enabled)
            {
                object result = null;
                if (string.IsNullOrEmpty(cacheItemKey))
                {
                    result = CacheHelper.Get(bizCacheItem.CacheKey);
                }
                else
                {
                    string cacheKeyFormat=bizCacheItem.CacheKey;
                    if(cacheKeyFormat.IndexOf("{0}")==-1)
                    {
                        cacheKeyFormat += "/{0}";//自动追加"/{0}"参数
                    }
                    result = CacheHelper.Get(string.Format(cacheKeyFormat, cacheItemKey));
                }
                if (result == null && typeof(T).IsValueType)//如果只为null，值类型
                {
                    return default(T);
                }
                else
                {
                    return (T)result;
                }
            }
            return default(T);
        }

        public static T GetOrInsertItem<T>(String cacheItemEnum, CacheItemNotExistsHandler<T> handler)
        {
            return GetOrInsertItem<T>(cacheItemEnum, "", handler);
        }

        public static T GetOrInsertItem<T>(String cacheItemEnum, string cacheItemKey, CacheItemNotExistsHandler<T> handler)
        {
            T obj = GetCacheItem<T>(cacheItemEnum, cacheItemKey);
            if ((typeof(T).IsValueType && obj.Equals(default(T))) || obj == null)
            {
                obj = handler();
                AddCacheItem<T>(cacheItemEnum, cacheItemKey, obj);
            }
            return obj;
        }

        public static void RemoveCacheItem(String cacheItemEnum)
        {
            RemoveCacheItem(cacheItemEnum, null);
        }

        public static void RemoveCacheItem(String cacheItemEnum, string cacheItemKey)
        {
            if (string.IsNullOrEmpty(cacheItemKey))
            {
                CacheHelper.Remove(cacheItemEnum);
            }
            else
            {
                BizCacheItem bizCacheItem = BizCacheUtility.GetConfig(cacheItemEnum.ToString());
                string cacheKeyFormat = bizCacheItem.CacheKey;
                if (cacheKeyFormat.IndexOf("{0}") == -1)
                {
                    cacheKeyFormat += "/{0}";//自动追加"/{0}"参数
                }
                CacheHelper.Remove(string.Format(cacheKeyFormat, cacheItemKey));
            }
        }

    }
}
