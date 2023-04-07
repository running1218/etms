using System;
using System.Configuration;
using System.IO;

namespace ETMS.Utility
{
    public class TransferPatternProvider
    {
        /// <summary>
        /// 将上传文件转换成swf格式
        /// </summary>
        /// <param name="fileName"></param>
        public static string TransferPattern(string fileName)
        {
            string phyPath = string.Format(@"DisScorm\{0}.swf", Guid.NewGuid().ToString());

            //try
            //{
            //    string root = (ServiceRepository.FileUploadStrategyService as DefaultFileUploadStrategyService).Root;
            //    DocConverter docConverter = new DocConverter();
            //    DocDefine define = new DocDefine()
            //    {
            //        OriginalFullFilePath = string.Format(@"{3}\UploadFiles\{0}\{1}\{2}", DateTime.Now.Year, DateTime.Now.Month, fileName, root).Replace("\\", @"\"),
            //        SWFFullFilePath = string.Format(@"{0}\{1}", root, phyPath).Replace("\\", @"\")
            //    };

            //    docConverter.Convert(define);
            //}
            //catch (Exception ex)
            //{
            //    ETMS.Utility.Logging.ErrorLogHelper.Error(ex);
            //    throw ex;
            //}

            return phyPath;
        }

        /// <summary>
        /// 上传文件是否在转换的范围内
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsUsingPattern(string fileName)
        {
            var flag = false;
            var extention = FileExtention(fileName);
            
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransferPattern"]))
            { 
                var patterns = ConfigurationManager.AppSettings["TransferPattern"].ToString();
                if (patterns.ToLower().Contains(extention))
                    flag = true;
            }

            return flag;
        }

        private static string FileExtention(string fileName)
        {
            return Path.GetExtension(fileName);
        }
    }
}
