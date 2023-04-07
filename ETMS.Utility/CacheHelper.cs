using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Caching;

namespace ETMS.Utility
{
    /// <summary>
    /// ����
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// �����Թ���ʱ�䷽ʽ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param> ��
        /// <param name="expireSeconds">���Թ�������</param>
        public static void Add(string key, object value, TimeSpan cacheDuration)
        {
            Add(key, value, cacheDuration, CacheItemPriority.Normal);
        }

        /// <summary>
        /// �����Թ���ʱ�䷽ʽ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="expireSeconds">���Թ�������</param>
        /// <param name="priority">���ȼ�</param>
        public static void Add(string key, object value, TimeSpan cacheDuration, CacheItemPriority priority)
        {
            Add(key, value, null, DateTime.Now.Add(cacheDuration), TimeSpan.Zero, priority, null);
        }

        /// <summary>
        /// �����Թ���ʱ�䷽ʽ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="absoluteExpiration">���Թ���ʱ��</param>
        /// <param name="priority">���ȼ�</param>
        public static void Add(string key, object value, DateTime absoluteExpiration, CacheItemPriority priority)
        {
            Add(key, value, null, absoluteExpiration, TimeSpan.Zero, priority, null);
        }

        /// <summary>
        /// ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="dependencies">������</param>
        /// <param name="absoluteExpiration">���Թ���ʱ��</param>
        /// <param name="slidingExpiration">��Թ���ʱ��</param>
        /// <param name="priority">���ȼ�</param>
        /// <param name="onRemovedCallback">�����Ƴ��ص�</param>
        public static void Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
        }

        /// <summary>
        /// �������ڷ�ʽ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        public static void AddPermanent(string key, object value)
        {
            AddPermanent(key, value, null);
        }

        /// <summary>
        /// �������ڷ�ʽ��ӻ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="dependencies">������</param>
        public static void AddPermanent(string key, object value, CacheDependency dependencies)
        {
            Add(key, value, dependencies, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>����ֵ</returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// �Ƴ�������
        /// </summary>
        /// <param name="key">��</param>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        #region ���������ְ��

        /// <summary>
        /// ��ȡ���л������
        /// ���飺��Ϊһ�黺��Ŀ�ʼ��־
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, int> GetCacheKeyGroups()
        {
            Dictionary<string, int> list = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            foreach (System.Collections.DictionaryEntry item in HttpRuntime.Cache)
            {
                string key = item.Key.ToString();
                //keyGroup ���򣬲�����"/"�� �ų�����ģ������ʹ�õ�
                if (key.Contains("::") || key.StartsWith("__"))
                {
                    continue;
                }
                //key ������ʱ���
                string[] strs = key.Split('/');
                int splitCount = strs.Length - 1;
                //if (splitCount <= 1)
                {
                    if (splitCount == 0)//��һ��cache��
                    {
                        if (!list.ContainsKey(key))
                        {
                            list.Add(key, 0);
                        }
                    }
                    else//�ڶ���cache�������ڸ���һ���ṩ����
                    {
                        key = strs[0];
                        if (!list.ContainsKey(key))
                        {
                            list.Add(key, 1);
                        }
                        else
                        {
                            list[key] = list[key]++;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// ��ȡ��ǰָ��������Ļ������б�
        /// </summary>
        /// <param name="cache">�������</param>
        /// <param name="cacheKey">�����</param>
        /// <returns>�������б�</returns>
        public static IList<string> GetCacheDetails(string cacheKeyGroup)
        {
            return doCacheItemOperate(cacheKeyGroup, "query");
        }

        /// <summary>
        /// ��������ָ��cacheKey�Ļ�����
        /// </summary>
        /// <param name="cache">�������</param>
        /// <param name="cacheKey"></param>
        private static IList<string> doCacheItemOperate(string cacheKey, string operate)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            List<string> findKeys = new List<string>();
            foreach (System.Collections.DictionaryEntry cacheItem in cache)
            {
                //����ҵ��Ե�ǰcachekey��ͷ�����ʾΪҪ�Ƴ�cache��
                if (cacheItem.Key.ToString().StartsWith(cacheKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    findKeys.Add(cacheItem.Key.ToString());
                }
            }
            if (operate == "delete")
            {
                //�Ƴ�cacheItem
                foreach (string key in findKeys)
                {
                    cache.Remove(key);
                }
            }
            return findKeys;
        }

        /// <summary>
        /// �Ƴ�BizCacheItemEnum��Ӧ�����л���
        /// </summary>
        /// <param name="cache">�������</param>
        public static void RemoveAllCaches()
        {
            Cache cache = HttpRuntime.Cache;
            List<string> findKeys = new List<string>();
            foreach (System.Collections.DictionaryEntry cacheItem in cache)
            {
                findKeys.Add(cacheItem.Key.ToString());
            }
            //�Ƴ�cacheItem
            foreach (string key in findKeys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// �����Ƴ�ָ��cacheKey�Ļ�����
        /// </summary>
        /// <param name="cache">�������</param>
        /// <param name="cacheKey"></param>
        public static IList<string> RemoveCacheItem(string cacheKey)
        {
            return doCacheItemOperate(cacheKey, "delete");
        }


        #endregion
    }
}
