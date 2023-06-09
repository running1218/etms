

namespace ETMS.Components.Basic.API.Entity.Notify
{
    /// <summary>
    /// 消息类别，指定了消息业务归类业务实体
    /// </summary> 
    public partial class Notify_MessageClass
    {
        /// <summary>
        /// 消息类别-demo
        /// </summary>
        public static Notify_MessageClass NotifyMessageClass_Demo = new Notify_MessageClass()
        {
            MessageClassID = 1,
            MessageClassName = "消息提醒Demo",
            IsUse = 1,
            OrderNum = 1,
        };

        /// <summary>
        /// 培训项目开始提醒
        /// </summary>
        public static Notify_MessageClass TrainingItemBeginNotify = new Notify_MessageClass()
        {
            MessageClassID = 2,
            MessageClassName = "培训项目开始提醒",
            IsUse = 1,
            OrderNum = 2,
        };

        /// <summary>
        /// 密码重置提醒
        /// </summary>
        public static Notify_MessageClass ResetPasswordNotify = new Notify_MessageClass()
        {
            MessageClassID = 3,
            MessageClassName = "密码重置提醒",
            IsUse = 1,
            OrderNum = 3,
        };

        /// <summary>
        /// 调查问卷未提交发邮件提醒 add 2013-1-10 胡俊义
        /// </summary>
        public static Notify_MessageClass SendEmailNotify = new Notify_MessageClass()
        {
            MessageClassID = 4,
            MessageClassName = "调查问卷未提交提醒",
            IsUse = 1,
            OrderNum = 4,
        };

        /// <summary>
        /// 培训项目开始提醒(教师)
        /// </summary>
        public static Notify_MessageClass TrainingItemBeginNotifyTeacher = new Notify_MessageClass()
        {
            MessageClassID = 5,
            MessageClassName = "培训项目开始提醒（教师）",
            IsUse = 1,
            OrderNum = 5,
        };
    }
}
