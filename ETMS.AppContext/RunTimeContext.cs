namespace ETMS.AppContext
{
    /// <summary>
    /// 运行时环境
    /// </summary>
    public class RunTimeContext
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MachineName;
            }
        }

        /// <summary>
        /// 操作系统信息
        /// </summary>
        public string OS_DESCRIPTION
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// ASPNET运行时环境
        /// </summary>
        public string ASPNET_DESCRIPTION
        {

            get
            {
                return "";
            }
        }
        
        //待扩展：
        //内存使用情况
        //CPU使用情况
        //重启运行时
        //
    }
}
