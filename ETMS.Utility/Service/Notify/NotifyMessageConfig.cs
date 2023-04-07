using System;
using Autumn.Expressions;
namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 提醒消息配置（动态表达式支持）
    /// 支持的模板参数：
    ///    #this.UserInfo  类型:Autumn.Business.Entity.SSO.UserInfo
    ///    #this.Context   类型：Object  推荐以匿名类的方式传入
    ///    #this.Receiver  类型：String 不同的消息格式对应不同，Email对应：用户邮箱，SMS对应：用户手机号，SiteInfo对应：用户ID
    /// </summary>
    [Serializable]
    public class NotifyMessageConfig
    {
        /// <summary>
        /// 接收者表达式
        /// </summary>
        public IExpression ReceiverTemplate { get; set; }

        /// <summary>
        /// 标题表达式
        /// </summary>
        public IExpression TitleTemplate { get; set; }

        /// <summary>
        /// 正文表达式
        /// </summary>
        public IExpression BodyTemplate { get; set; }

        /// <summary>
        /// 消息优先级表达式
        /// </summary>
        public IExpression LevelTemplate { get; set; }
    }
}
