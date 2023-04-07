
using System.Web;

namespace ETMS.Utility
{
    public static class NetTypeUtility
    {
        /// <summary>
        /// ��ȡ��ǰ������������
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
    /// �������ͣ�����/������
    /// </summary>
    public enum NetType
    {
        Com = 0,
        Edu = 1
    }
}
