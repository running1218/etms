

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// �����飨ϵͳ�ֵ䣩ҵ��ʵ��
    /// </summary>
    public partial class Site_SysConfigGroup
    {
        /// <summary>
        /// �ʼ�ϵͳ����
        /// </summary>
        public static Site_SysConfigGroup SMTPConfigGroup = new Site_SysConfigGroup()
        {
            ConfigGroupID = 1,
            ConfigGroupName = "�ʼ�ϵͳ",
            IsUse = 1,
            OrderNum = 1
        };

        /// <summary>
        /// �˻���������
        /// </summary>
        public static Site_SysConfigGroup AccountConfigGroup = new Site_SysConfigGroup()
        {
            ConfigGroupID = 2,
            ConfigGroupName = "�˻�����",
            IsUse = 1,
            OrderNum = 2
        };
    }
}
