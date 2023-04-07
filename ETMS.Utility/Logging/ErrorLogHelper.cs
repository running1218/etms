using System;
using System.Configuration;
using Common.Logging;
using System.Web;
using System.IO;

namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 异常日志记录器
    /// </summary>
    public abstract class ErrorLogHelper
    {
        private const string ApplicationNameKey = "System.ApplicationName";
        private const string LoggerName = "ApplicationSystemError";
        public static void Error(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            Exception exInner = ex.InnerException;
            Exception exBase = ex.GetBaseException();

            if (exInner is System.Reflection.TargetInvocationException)//如果是：目标调用发生异常，取内部异常
            {
                if (exInner.InnerException != null)
                {
                    exInner = exInner.InnerException;
                }
            }
            /*
             *  为了保证所有的异常都显示最终消息，内部消息则记录数据库。
             *  例如：有些出错信息不应该提供给用户，但日志需要详细记录。
             *        则通过页面捕获异常后，包装此异常到新异常（新异常描述：系统出错！）内部，然后抛出。
             * */
            string msg = exInner != null ? exInner.Message : ex.Message;
            ILog logger = LogManager.GetLogger(LoggerName);
            ErrorLog errLog = new ErrorLog()
            {
                ApplicationName = ConfigurationManager.AppSettings[ApplicationNameKey],
                BaseMessage = exBase.Message,
                Message = msg,
                StackTrace = exBase.StackTrace,
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl()
            };          
            //记录错误日志
            if (logger.IsErrorEnabled)
            {
                logger.Error(errLog);
            }
        }
        public static void WriteFileLog(Exception ex)
        {
            Exception exInner = ex.InnerException;
            Exception exBase = ex.GetBaseException();
            
            ErrorLog errLog = new ErrorLog()
            {
                ApplicationName = ConfigurationManager.AppSettings[ApplicationNameKey],
                BaseMessage = exBase.Message,
                Message = exInner != null ? exInner.Message : ex.Message,
                StackTrace = exBase.StackTrace,
                LoginName = UserHelper.GetUserIdentity(),
                ServerName = UserHelper.GetServerName(),
                ClientIP = UserHelper.GetUserIp(),
                PageUrl = UserHelper.GetRequestUrl()
            };
            WriteLog(JsonHelper.SerializeObject(errLog));
        }

        public static void WriteLog(string message)
        {
            string logPath = HttpContext.Current.Server.MapPath(string.Format("/log/{0}_log.txt", System.DateTime.Now.ToString("yyyy-MM")));
            if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
            if (!File.Exists(logPath))
                File.Create(logPath).Close();

            StreamWriter sw = File.AppendText(logPath);
            sw.WriteLine(string.Format("\r\n...{0}...............................................................................................", DateTime.Now));
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }
    }
}
