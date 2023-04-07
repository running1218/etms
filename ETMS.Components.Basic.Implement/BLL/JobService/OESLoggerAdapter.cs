
using Common.Logging;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{

    public class OESLoggerAdapter : OES.Logger.ILog
    {
        ILog Logger = LogManager.GetLogger("JobService");

        /// <summary>
        /// 日志名称
        /// </summary>
        public string Name { get; set; }

        public OESLoggerAdapter(string name)
        {
            this.Name = name;
        }
        public void Debug(string msg)
        {
            Logger.Debug(this.Name + ":" + msg);
        }

        public void Error(string msg)
        {
            Logger.Error(this.Name + ":" + msg);
        }

        public void Info(string msg)
        {
            Logger.Info(this.Name + ":" + msg);
        }

        public bool IsDebug
        {
            get { return Logger.IsDebugEnabled; }
        }

        public bool IsError
        {
            get { return Logger.IsErrorEnabled; }
        }

        public bool IsInfo
        {
            get { return Logger.IsInfoEnabled; }
        }

        public OES.Logger.Config.LogConfig LogSetting
        {
            get { return new OES.Logger.Config.LogConfig(); }
        } 
    }
}
