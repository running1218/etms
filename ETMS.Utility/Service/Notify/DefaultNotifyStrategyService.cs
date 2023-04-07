using System.Collections.Specialized;
using Autumn.Objects.Factory;
namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 默认的消息提醒策略服务实现
    /// </summary> 
    public class DefaultNotifyStrategyService : INotifyStrategy, IInitializingObject
    {
        private static string DefaultStrategyName = "Default";
        private static string DefaultStrategyValue = "000";
        /// <summary>
        /// 策略定义
        /// 默认
        /// </summary>
        public NameValueCollection Strategys { get; set; }

        /// <summary>
        /// 获取特定类型消息对应的提醒策略
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <returns>返回此消息对应的提醒策略（支持email？支持sms？支持SiteInfo?)以{‘1','0','0'}格式返回</returns>
        public char[] GetStrategy(string messageType)
        {
            string value = Strategys[messageType.ToString()];
            if (string.IsNullOrEmpty(value) || value.Length != 3)//如果为找到对应的策略或策略配置非000，格式，则采用默认策略
            {
                value = Strategys[DefaultStrategyName];
            }
            return value.ToCharArray();
        }

        public void AfterPropertiesSet()
        {
            if (Strategys == null)
            {
                Strategys = new NameValueCollection();
            }
            //如果没有指定默认策略，则追加
            if (string.IsNullOrEmpty(Strategys[DefaultStrategyName]))
            {
                Strategys.Add(DefaultStrategyName, DefaultStrategyValue);
            }
        }
    }
}
