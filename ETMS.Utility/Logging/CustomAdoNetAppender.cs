
using log4net.Appender;
using System.Configuration;
using System.Collections.Specialized;
namespace ETMS.Utility.Logging
{
    /// <summary>
    /// ֧����������
    /// </summary>
    public class CustomAdoNetAppender : AdoNetAppender
    {
        /// <summary>
        /// �����ַ�������
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
