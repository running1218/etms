using System;

namespace ETMS.Utility.BizCache
{
    /// <summary>
    /// 业务缓存项
    /// </summary>
    public class BizCacheItem
    {
        public BizCacheItem()
        {
        }
        public BizCacheItem(string name, bool enabled, string cacheKey, TimeSpan cacheDuration, string description)
        {
            this.Name = name;
            this.Enabled = enabled;
            this.CacheKey = cacheKey;
            this.CacheDuration = cacheDuration;
            this.Description = description;
        }
        /// <summary>
        /// 缓存名称（要求唯一）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否启用（默认停用）
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 缓存键定义
        /// </summary>
        public string CacheKey { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan CacheDuration { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
