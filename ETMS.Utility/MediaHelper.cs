using Microsoft.WindowsAPICodePack.Shell;

namespace ETMS.Utility
{
    public class MediaHelper
    {
        /// <summary>
        /// 获取视频播放时间, 单位：秒
        /// </summary>
        /// <param name="mediaFile"></param>
        /// <returns></returns>
        public static double GetDuration(string mediaFile)
        {
            ShellFile shellFile = ShellFile.FromFilePath(mediaFile);
            double nanoseconds = default(double);
            double seconds = default(double);

            if (null != shellFile && null != shellFile.Properties && null != shellFile.Properties.System.Media && null != shellFile.Properties.System.Media.Duration)
            {
                nanoseconds = shellFile.Properties.System.Media.Duration.Value.ToString().ToDouble();
                seconds = nanoseconds * 0.0000001;
            }

            return seconds;
        }
    }
}
