using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace ETMS.Utility.Configuration
{
    /// <summary>
    /// ���ù���
    /// </summary>
    public class ConfigurationUtility<T>
    {
        /// <summary>
        /// ��ȡ���ö�
        /// </summary>
        /// <param name="fileName">�����ļ�</param>
        /// <param name="section">���ö�</param>
        /// <returns>NameValueCollection</returns>
        public static NameValueCollection GetConfigSection(string fileName, string section)
        {
            NameValueCollection cacheCollection = new NameValueCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(fileName));
            XmlNode node = doc.SelectSingleNode(section);
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    cacheCollection.Add(n.Attributes["key"].Value.Trim(), n.Attributes["value"].Value.Trim());
                }
            }

            return cacheCollection;
        }
        /// <summary>
        /// ��ȡ���ö�
        /// </summary>
        /// <param name="fileName">�����ļ�</param>
        /// <param name="section">���ö�</param>
        /// <param name="cacheKey">�����</param>
        /// <returns>NameValueCollection</returns>
        public static NameValueCollection GetConfigSection(string fileName, string section, string cacheKey)
        {
            NameValueCollection cacheCollection = (NameValueCollection)CacheHelper.Get(cacheKey);
            if (cacheCollection == null)
            {
                cacheCollection = GetConfigSection(fileName, section);

                CacheDependency dependency = new CacheDependency(HttpContext.Current.Server.MapPath(fileName));
                CacheHelper.AddPermanent(cacheKey, cacheCollection, dependency);
            }
            return cacheCollection;
        }
    }
}
