namespace ETMS.Utility
{
    public static class ObjectUtility
    {
        /// <summary>
        /// 对象进行xml序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectXmlSerialization(this object obj)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                new System.Xml.Serialization.XmlSerializerFactory().CreateSerializer(obj.GetType()).Serialize(ms, obj);
                System.IO.StreamReader reader = new System.IO.StreamReader(ms);
                ms.Position = 0;//重置流位置
                return reader.ReadToEnd();
            }
        } /// <summary>
        /// 对象进行xml序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectJSONSerialization(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);            
        }
    }
}
