using System;
using System.Text;

using System.Net.Mail;

namespace ETMS.Utility
{
    /// <summary>
    /// SMTP����
    /// </summary>
    [Serializable]
    public class SMTPConfig
    {
        /// <summary>
        /// �ʼ���������ַ
        /// </summary>
        public string SMTP_ServerIP { get; set; }

        /// <summary>
        /// �ʼ��������˿�
        /// </summary>
        public string SMTP_ServerPort { get; set; }

        /// <summary>
        /// �ʼ���Դ
        /// </summary>
        public string SMTP_EmailFrom { get; set; }

        /// <summary>
        /// �ʼ��˻���
        /// </summary>
        public string SMTP_UserName { get; set; }

        /// <summary>
        ///�ʼ��˻�����
        /// </summary>
        public string SMTP_Password { get; set; }
    }

    /// <summary>
    /// �ʼ���Ϣ�嶨��
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
        /// �ʼ�����
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
        /// �ʼ�����
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
        /// �ʼ�������
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
    /// �ʼ����͹���
    /// </summary>
    public class EmailUtility
    {
        /// <summary>
        /// ϵͳĬ�ϵ�smtp�����·����ʼ�
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
        /// �ڲ�ͬ��smtp�ʼ������������·����ʼ�
        /// </summary>
        /// <param name="emailMsg">�ʼ���Ϣ��</param>
        /// <param name="config">smtp�ʼ���������</param>
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
