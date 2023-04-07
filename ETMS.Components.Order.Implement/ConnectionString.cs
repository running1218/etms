namespace ETMS.Components.Order.Implement.DAL
{
    /// <summary>
    /// 数据库连接定义
    /// </summary>
    public static class ConnectionString
    {
        /// <summary>
        /// 数据库读写分离后，读库访问
        /// 如果没有设置，在默认取（LMS_Read）
        /// </summary>
        public static string ETMSRead = ETMS.AppContext.ApplicationContext.Current.DataSources["ETMS_Read"];
        public static string ETMSWrite = ETMS.AppContext.ApplicationContext.Current.DataSources["ETMS_Write"];
    }
}


