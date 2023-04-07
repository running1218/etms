using System.Collections.Generic;

using Common.Logging;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 账户策略逻辑层
    /// </summary>
    public class AccountStrategyLogic
    {

        #region 内部成员
        private ILog Logger = LogManager.GetLogger(typeof(AccountStrategyLogic));
        #endregion

        /// <summary>
        /// 获取当前用户所在机构定义账户策略
        /// </summary>
        /// <returns>账户策略</returns>
        public AccountStrategy GetAccountStrategy()
        {
            //取当前用户所在机构
            return GetAccountStrategy(ETMS.AppContext.UserContext.Current.OrganizationID);
        }

        /// <summary>
        /// 获取机构定义账户策略
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <returns>账户策略</returns>
        public AccountStrategy GetAccountStrategy(int organizationID)
        {
            Site_SysConfigLogic logic = new Site_SysConfigLogic();
            IList<Site_SysConfig> sysConfigItems = logic.GetOrganizationConfig(organizationID, Site_SysConfigGroup.AccountConfigGroup.ConfigGroupID);

            AccountStrategy config = new AccountStrategy();
            //将数据库中配置项集合转换为配置对象属性赋值
            foreach (Site_SysConfig item in sysConfigItems)
            {
                //获取配置属性并赋值
                System.Reflection.PropertyInfo propertyInfo = config.GetType().GetProperty(item.Name);
                if (propertyInfo != null)
                {
                    //如果用户自定义值为空，则取默认值
                    string value = string.IsNullOrEmpty(item.UserValue) ? item.DefaultValue : item.UserValue;
                    propertyInfo.SetValue(config, value, null);
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.Debug(string.Format("数据库配置项“{0}”，赋值“{1}”到类型{2}中对应属性!", item.Name, value, config.GetType()));
                    }
                }
                else
                {
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.Debug(string.Format("数据库配置项“{0}”，在类型{1}中未找到对应属性!", item.Name, config.GetType()));
                    }
                }
            }
            return config;

        }
    }
}
