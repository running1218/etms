using System;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 日志基础类
    /// </summary>
    [Serializable]
    public class BaseLog
    {
        public BaseLog()
        {
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 日志ID
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Web服务器名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 用户请求URL（包括URL参数）
        /// </summary>
        public string PageUrl { get; set; }

    }
}
