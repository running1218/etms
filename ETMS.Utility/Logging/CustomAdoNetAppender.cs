
using log4net.Appender;
using System.Configuration;
using System.Collections.Specialized;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 支持链接名称
    /// </summary>
    public class CustomAdoNetAppender : AdoNetAppender
    {
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public string ConnectionStringName
        {
            set
            {
                this.ConnectionString = ((NameValueCollection)ConfigurationManager.GetSection("dataBaseSettings"))[value];
            }
        }

    }
}
