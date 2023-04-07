using System;
using System.Collections.Generic;
using Autumn.Util;
using Autumn.Objects.Factory;
namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 默认的消息提醒源服务实现
    /// 通过读取配置文件中的消息配置，输出标准的消息内容。
    /// </summary>
    public class DefaultNotifyMessageSourceService : INotifyMessageSourceService, IInitializingObject
    {
        /// <summary>
        /// 消息模板
        /// </summary>
        public IDictionary<string, NotifyMessageConfig> MessageTemplates { get; set; }

        /// <summary>
        /// 获取提醒消息
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="context">消息上下文</param>
        /// <returns>返回提醒消息</returns>
        public NotifyMessage GetMessage(string messageType, object context)
        {
            if (!MessageTemplates.ContainsKey(messageType.ToString()))
            {
                throw new Exception(string.Format("未找到名为“{0}”的消息配置模板！", messageType));
            }
            //找到配置
            NotifyMessageConfig config = MessageTemplates[messageType.ToString()];
            NotifyMessage message = new NotifyMessage();
            //通过动态表达式计算方式获取最终内容
            if (config.ReceiverTemplate != null)
            {
                message.Receiver = (string)config.ReceiverTemplate.GetValue(context);
            }
            if (config.TitleTemplate != null)
            {
                message.Title = (string)config.TitleTemplate.GetValue(context);
            }
            if (config.BodyTemplate != null)
            {
                message.Body = (string)config.BodyTemplate.GetValue(context);
            }
            if (config.LevelTemplate != null)
            {
                message.Level = (NotifyLevel)config.LevelTemplate.GetValue(context);
            }
            return message;
        }

        /// <summary>
        /// 应用上下文加载，对象属性应用后，检查属性是否完全初始化！
        /// </summary>
        public void AfterPropertiesSet()
        {
            AssertUtils.ArgumentNotNull(this.MessageTemplates, "请先设置属性MessageTemplates！");
        }
    }
}
