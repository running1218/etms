using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileAPI.Common
{
    public class LoggerHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="level">日志级别：Error， Operation</param>
        public static void WriteLog(string message, string level)
        {            
            string logPath = HttpContext.Current.Server.MapPath(string.Format("/log/{0}_log.txt", System.DateTime.Now.ToString("yyyy-MM-dd")));
            if (!File.Exists(logPath))
                File.Create(logPath).Close();

            StreamWriter sw = File.AppendText(logPath);
            sw.WriteLine(string.Format("{0} # {1} # {2}", System.DateTime.Now.ToString(), level, message));            
            sw.Flush();
            sw.Close();
        }
    }
}
