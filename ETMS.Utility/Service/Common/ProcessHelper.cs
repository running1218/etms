using System;
using System.Diagnostics;


namespace ETMS.Utility.Service.Common
{
    /// <summary>
    ///  进程辅助器，方便启动进程,由于转换时日志修改过多，暂时不开放！
    /// </summary>
    public class ProcessHelper
    {
        public static string WaitExecuteForExit(string exefileName, string arguments, int milliseconds)
        {
            string executeResult = "";
            exefileName = string.Format("\"{0}\"", exefileName);
            Process process = new Process();     //创建进程对象
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = exefileName;      //设定需要执行的命令
            startInfo.Arguments = arguments;   //设定参数                                
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            //startInfo.RedirectStandardOutput = true;
            //startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            try
            {
                if (process.Start())       //开始进程
                {
                    if (milliseconds == 0)
                    {
                        //executeResult = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();     //这里无限等待进程结束
                    }
                    else
                    {
                        //executeResult = process.StandardOutput.ReadToEnd();
                        process.WaitForExit(milliseconds); //这里等待进程结束，等待时间为指定的毫秒 
                    }
                }
            }
            catch (Exception ex)
            {
                executeResult = ex.StackTrace;
            }
            finally
            {
                if (process != null)
                    process.Close();
            }
            return executeResult;
        }
    }
}
