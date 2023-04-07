using System;
using System.Text;

using System.Net.Mail;

namespace ETMS.Utility
{
    /// <summary>
    /// SMTP配置
    /// </summary>
    [Serializable]
    public class SMTPConfig
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string SMTP_ServerIP { get; set; }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public string SMTP_ServerPort { get; set; }

        /// <summary>
        /// 邮件来源
        /// </summary>
        public string SMTP_EmailFrom { get; set; }

        /// <summary>
        /// 邮件账户名
        /// </summary>
        public string SMTP_UserName { get; set; }

        /// <summary>
        ///邮件账户密码
        /// </summary>
        public string SMTP_Password { get; set; }
    }

    /// <summary>
    /// 邮件消息体定义
    /// </summary>
    [Serializable]
    public class EmailMsg
    {
        public EmailMsg(String subject, String body, String receiver)
        {
            this.subjectField = subject;
            this.bodyField = body;
            this.receiverField = receiver;
        }
        private String subjectField;
        /// <summary>
        /// 邮件主题
        /// </summary>
        public String Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        private String bodyField;
        /// <summary>
        /// 邮件内容
        /// </summary>
        public String Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }

        private String receiverField;
        /// <summary>
        /// 邮件接收人
        /// </summary>
        public String Receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }
    }

    /// <summary>
    /// 邮件发送工具
    /// </summary>
    public class EmailUtility
    {
        /// <summary>
        /// 系统默认的smtp配置下发送邮件
        /// </summary>
        /// <param name="emailMsg"></param>
        public static void SendEmail(EmailMsg emailMsg)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(emailMsg.Receiver));
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Subject = emailMsg.Subject;
            mailMessage.Body = emailMsg.Body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = false; ;
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// 在不同的smtp邮件服务器配置下发送邮件
        /// </summary>
        /// <param name="emailMsg">邮件消息体</param>
        /// <param name="config">smtp邮件服务配置</param>
        public static void SendEmail(EmailMsg emailMsg, SMTPConfig config)
        {
            MailMessage mailMessage = new MailMessage(config.SMTP_EmailFrom, emailMsg.Receiver, emailMsg.Subject, emailMsg.Body);          
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;          
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(config.SMTP_ServerIP, config.SMTP_ServerPort.ToInt());
            smtpClient.Credentials = new System.Net.NetworkCredential(config.SMTP_UserName, config.SMTP_Password);
            smtpClient.EnableSsl = false; ;
            smtpClient.Send(mailMessage);
        }
    }
}
