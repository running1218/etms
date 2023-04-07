using System;
using System.IO;
using System.Xml.Serialization;

namespace ETMS.Components.Reporting.Implement
{
    public class XmlProvider
    {
        /// <summary>
        /// 反序列化返回xml所有节点信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlPath)
        {
            T entity = default(T);

            try
            {
                using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    entity = (T)xmlSerializer.Deserialize((Stream)fs);
                }
            }
            catch (Exception ex)
            {

            }
            return entity;
        }
    }
}
