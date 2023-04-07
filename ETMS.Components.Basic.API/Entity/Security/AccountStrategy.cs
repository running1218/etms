namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 账户策略
    /// </summary>
    public class AccountStrategy
    {
        public AccountStrategy()
        {
            this.Security_PassWord_Default = "888888";
            this.Security_PassWord_Strategy = "^(?![0-9]+$)[a-zA-Z0-9_]{6,15}$";
        }
        /// <summary>
        /// 系统默认密码
        /// </summary>
        public string Security_PassWord_Default { get; set; }

        /// <summary>
        /// 密码策略
        /// </summary>
        public string Security_PassWord_Strategy { get; set; }
    }
}
