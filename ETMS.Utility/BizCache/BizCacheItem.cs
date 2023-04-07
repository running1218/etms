using System;

namespace ETMS.Utility.BizCache
{
    /// <summary>
    /// ҵ�񻺴���
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
        /// �������ƣ�Ҫ��Ψһ��
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// �Ƿ����ã�Ĭ��ͣ�ã�
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        public string CacheKey { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public TimeSpan CacheDuration { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Description { get; set; }
    }
}
