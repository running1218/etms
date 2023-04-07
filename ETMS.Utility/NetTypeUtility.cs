
using System.Web;

namespace ETMS.Utility
{
    public static class NetTypeUtility
    {
        /// <summary>
        /// 获取当前请求网络类型
        /// </summary>
        public static NetType RequestNetType
        {
            get
            {
                if (HttpContext.Current.Request != null)
                {
                    string requestHost = HttpContext.Current.Request.Url.Host.ToLower();
                    if (requestHost.Contains(".edu.cn"))
                    {
                        return NetType.Edu;
                    }

                }

                return NetType.Com;
            }
        }
    }

    /// <summary>
    /// 网络类型：公网/教育网
    /// </summary>
    public enum NetType
    {
        Com = 0,
        Edu = 1
    }
}
