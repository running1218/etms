

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 配置组（系统字典）业务实体
    /// </summary>
    public partial class Site_SysConfigGroup
    {
        /// <summary>
        /// 邮件系统配置
        /// </summary>
        public static Site_SysConfigGroup SMTPConfigGroup = new Site_SysConfigGroup()
        {
            ConfigGroupID = 1,
            ConfigGroupName = "邮件系统",
            IsUse = 1,
            OrderNum = 1
        };

        /// <summary>
        /// 账户策略配置
        /// </summary>
        public static Site_SysConfigGroup AccountConfigGroup = new Site_SysConfigGroup()
        {
            ConfigGroupID = 2,
            ConfigGroupName = "账户策略",
            IsUse = 1,
            OrderNum = 2
        };
    }
}
