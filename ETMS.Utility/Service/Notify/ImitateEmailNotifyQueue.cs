namespace ETMS.Utility.Service.Notify
{
    /// <summary>
    /// 模拟邮件发送队列，调用SMTP之间发送邮件！
    /// </summary>
    public class ImitateEmailNotifyQueue : INotifyQueueLogic
    {
        public int Send(string reciever, string subject, string body, int level, string appMan)
        {
            ETMS.Utility.EmailUtility.SendEmail(new EmailMsg(subject, body, reciever));
            return 1;
        }
    }
}
