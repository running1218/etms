namespace ETMS.Components.Order.Implement.DAL
{
    /// <summary>
    /// ���ݿ����Ӷ���
    /// </summary>
    public static class ConnectionString
    {
        /// <summary>
        /// ���ݿ��д����󣬶������
        /// ���û�����ã���Ĭ��ȡ��LMS_Read��
        /// </summary>
        public static string ETMSRead = ETMS.AppContext.ApplicationContext.Current.DataSources["ETMS_Read"];
        public static string ETMSWrite = ETMS.AppContext.ApplicationContext.Current.DataSources["ETMS_Write"];
    }
}


