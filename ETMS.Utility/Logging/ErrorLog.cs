using System;
using System.Text;

namespace ETMS.Utility.Logging
{
    /// <summary>
    /// 错误日志
    /// </summary>
    [Serializable]
    public class ErrorLog : BaseLog
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get;
            set;
        }
        /// <summary>
        /// 消息名称
        /// </summary>
        public string Message
        {
            get;
            set;
        }        
        /// <summary>
        /// 内部消息名称
        /// </summary>
        public string BaseMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder writer = new StringBuilder();
            writer.AppendLine();
            writer.AppendFormat("应用名称：{0}\r\n", this.ApplicationName);
            writer.AppendFormat("消息名称：{0}\r\n", this.Message);
            writer.AppendFormat("堆栈内容：{0}\r\n", this.StackTrace);
            writer.AppendFormat("当前用户：{0}\r\n", this.LoginName);
            writer.AppendFormat("Web服务器地址：{0}\r\n", this.ServerName);
            writer.AppendFormat("客户端IP：{0}\r\n", this.ClientIP);
            writer.AppendFormat("用户请求URL：{0}\r\n", this.PageUrl);
            return writer.ToString();
        }
    }
}