using System;
using System.Diagnostics;


namespace ETMS.Utility.Service.Common
{
    /// <summary>
    ///  ���̸�������������������,����ת��ʱ��־�޸Ĺ��࣬��ʱ�����ţ�
    /// </summary>
    public class ProcessHelper
    {
        public static string WaitExecuteForExit(string exefileName, string arguments, int milliseconds)
        {
            string executeResult = "";
            exefileName = string.Format("\"{0}\"", exefileName);
            Process process = new Process();     //�������̶���
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = exefileName;      //�趨��Ҫִ�е�����
            startInfo.Arguments = arguments;   //�趨����                                
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            //startInfo.RedirectStandardOutput = true;
            //startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            try
            {
                if (process.Start())       //��ʼ����
                {
                    if (milliseconds == 0)
                    {
                        //executeResult = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();     //�������޵ȴ����̽���
                    }
                    else
                    {
                        //executeResult = process.StandardOutput.ReadToEnd();
                        process.WaitForExit(milliseconds); //����ȴ����̽������ȴ�ʱ��Ϊָ���ĺ��� 
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
