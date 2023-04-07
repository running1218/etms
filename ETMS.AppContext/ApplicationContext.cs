#define PRODUCT_SINGLE      //单机构版本
//#define PRODUCT_MULTI     //多机构（机构数不限）
//#define PRODUCT_MULTI_10  //多机构（机构数10）
//#define PRODUCT_MULTI_50  //多机构（机构数50）
using System.Configuration;
using Common.Logging;
using System.Collections.Specialized;
namespace ETMS.AppContext
{
    /// <summary>
    /// 应用上下文环境
    /// </summary>
    public class ApplicationContext
    {
        private static ApplicationContext Instance = new ApplicationContext();
        /// <summary>
        /// 数据源定义
        /// </summary>
        public NameValueCollection DataSources
        {
            get
            {
                return (ConfigurationManager.GetSection("dataBaseSettings") as NameValueCollection);
            }
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILog Logger { get; internal set; }

        /// <summary>
        /// 组件仓库
        /// </summary>
        public AppContext.Component.IComponentRepository ComponentRepository { get; internal set; }

        /// <summary>
        /// 应用配置
        /// </summary>
        public System.Collections.Specialized.NameValueCollection AppSettings
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings;
            }
        }

        /// <summary>
        /// 当前应用上下文
        /// </summary>
        public static ApplicationContext Current
        {
            get
            {
                return Instance;
            }
            internal set
            {
                Instance = value;
            }
        }
    }
}
