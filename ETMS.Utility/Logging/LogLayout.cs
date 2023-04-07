using System.IO;

using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;

namespace ETMS.Utility.Logging
{
    /// <summary>
    /// ҵ����־��ʽ����
    /// </summary>
    public class LogLayout : PatternLayout
    {
        /// <summary>
        /// ����ҵ����־��ʽ����
        /// </summary>
        public LogLayout(string pattern)
            : base(pattern)
        {


        }
        /// <summary>
        /// ���أ���֤�������ʼ��ʱ���ã�
        /// </summary>
        public override void ActivateOptions()
        {
            this.AddConverter("ModuleName", typeof(ModuleNamePatternConverter));
            this.AddConverter("MethodName", typeof(MethodPatternConverter));
            this.AddConverter("TargetID", typeof(TargetIDPatternConverter));
            this.AddConverter("Action", typeof(ActionPatternConverter));
            this.AddConverter("OrganizationID", typeof(OrganizationIDPatternConverter));

            this.AddConverter("LoginName", typeof(LoginNamePatternConverter));
            this.AddConverter("CreateTime", typeof(CreateTimePatternConverter));
            this.AddConverter("ServerName", typeof(ServerNamePatternConverter));
            this.AddConverter("ClientIP", typeof(ClientIPPatternConverter));
            this.AddConverter("PageUrl", typeof(PageUrlPatternConverter));

            this.AddConverter("ApplicationName", typeof(ApplicationNameConverter));
            this.AddConverter("Message", typeof(MessageConverter));
            this.AddConverter("BaseMessage", typeof(BaseMessageConverter));
            this.AddConverter("StackTrace", typeof(StackTraceConverter));

            base.ActivateOptions();
        }
    }
    #region ҵ����־��Ϣ
    internal sealed class ModuleNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BusinessLog errorLog = (BusinessLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.ModuleName);
        }
    }
    internal sealed class MethodPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BusinessLog errorLog = (BusinessLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.MethodName);
        }
    }
    internal sealed class TargetIDPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BusinessLog errorLog = (BusinessLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write((errorLog.TargetID == null ? "" : errorLog.TargetID.ToString()));
        }
    }
    internal sealed class ActionPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BusinessLog errorLog = (BusinessLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.Action);
        }
    }
    internal sealed class OrganizationIDPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BusinessLog errorLog = (BusinessLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.OrganizationID);
        }
    }
    #endregion

    #region ����ת����
    internal sealed class LoginNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BaseLog errorLog = (BaseLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.LoginName);
        }
    }
    internal sealed class CreateTimePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BaseLog errorLog = (BaseLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.CreateTime);
        }
    }
    internal sealed class ServerNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BaseLog errorLog = (BaseLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.ServerName);
        }
    }
    internal sealed class ClientIPPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BaseLog errorLog = (BaseLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.ClientIP);
        }
    }
    internal sealed class PageUrlPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            BaseLog errorLog = (BaseLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.PageUrl);
        }
    }
    #endregion
    #region �쳣����־ת����
    internal sealed class ApplicationNameConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent != null && loggingEvent.MessageObject != null)
            {
                ErrorLog errorLog = (ErrorLog)loggingEvent.MessageObject;
                if (errorLog != null)
                    writer.Write(errorLog.ApplicationName);
            }
        }
    }
    internal sealed class MessageConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            ErrorLog errorLog = (ErrorLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.Message);
        }
    }
    internal sealed class BaseMessageConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            ErrorLog errorLog = (ErrorLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.BaseMessage);
        }
    }
    internal sealed class StackTraceConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            ErrorLog errorLog = (ErrorLog)loggingEvent.MessageObject;
            if (errorLog != null)
                writer.Write(errorLog.StackTrace);
        }
    }
    #endregion


}
