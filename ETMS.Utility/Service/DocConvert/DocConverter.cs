using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;


namespace ETMS.Utility.Service.DocConvert
{
    internal class ConvertMessage
    {
        public static string DangerousTypeError = "文件疑似可执行文件！";
        public static string DocTypeError = "不支持该文件格式转码！";
        public static string PdfConvertError = "PDF文件转码失败！";
        public static string PdfCopyError = "PDF文件复制失败！";
        public static string GetPDFPageCountError = "获取PDF页数失败！";
        public static string ConvertSuccess = "success";

    }
    /// <summary>
    /// 文档转化器
    /// 支持下列文档转换为swf格式
    /// {.doc,.docx,.dotx,.dotm,.dot,.txt,.rtf,.xlsx,.xlsm,.xml, .ppt,.pptx,.pptm,.potx,.ppsx,.ppsm,.potm,.pdf}
    /// </summary>
    public class DocConverter
    {
        public DocConverter()
        {
            //设置默认值
            Temp_PDFPath = AppDomain.CurrentDomain.BaseDirectory + @"Temp\PdfFiles";
            Pdf2SwfFilePath = AppDomain.CurrentDomain.BaseDirectory + @"bin\DOC2SWF\pdf2swf.exe";
            XPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"bin\DOC2SWF\xpdf-chinese-simplified";
            MaxTimes_Doc2PDF = 10;
            MaxTimes_Pdf2Swf = 120;
        }

        /// <summary>
        /// PDF临时文件
        /// </summary>
        public string Temp_PDFPath { get; set; }
        /// <summary>
        /// 各种doc转pdf最长持续时间（单位分钟）
        /// </summary>
        public int MaxTimes_Doc2PDF { get; set; }
        /// <summary>
        /// PDF转SWFexe文件地址
        /// </summary>
        public string Pdf2SwfFilePath { get; set; }
        /// <summary>
        /// xpdf-chinese-simplified
        /// </summary>
        public string XPDFPath { get; set; }
        /// <summary>
        /// pdf转swf最大时间（单位秒）
        /// </summary>
        public int MaxTimes_Pdf2Swf { get; set; }
        /// <summary>
        /// 各种文档==》SWF类型
        /// </summary>
        /// <param name="DocDefine">待转文档</param>
        public void Convert(DocDefine doc)
        {
            string fileType = Path.GetExtension(doc.OriginalFullFilePath);
            //判断文件是否是伪装的exe或dll
            if (IsExeDllFile(doc.OriginalFullFilePath))
            {
                throw new Exception("this file is realy a exe or dll File!");
            }

            //0、准备工作：临时的PDF文件名
            if (!Directory.Exists(this.Temp_PDFPath))
            {
                Directory.CreateDirectory(this.Temp_PDFPath);
            }
            //转换辅助器
            ConvertHelper ct = new ConvertHelper();
            ct.Pdf2Swf_MaxTime = this.MaxTimes_Pdf2Swf;
            ct.Pdf2SwfFilePath = this.Pdf2SwfFilePath;
            ct.XPDFPath = this.XPDFPath;

            string tempPDFFilePath = string.Format(@"{0}\{1}.PDF", this.Temp_PDFPath, Guid.NewGuid().ToString("n"));
            try
            {

                #region 第一步：文件转PDF过程

                //1、文件归类：不同的文件选择不同的加工方式
                string convertResult = "";
                switch (fileType)
                {
                    case ".doc":
                    case ".docx":
                    case ".dotx":
                    case ".dotm":
                    case ".dot":
                    case ".txt":
                    case ".rtf":
                        convertResult = ct.WordToPDF(doc.OriginalFullFilePath, tempPDFFilePath, this.MaxTimes_Doc2PDF);
                        break;
                    case ".xls":
                    case ".xlsx":
                    case ".xlsm":
                    case ".xml":
                        convertResult = ct.ExcelToPDF(doc.OriginalFullFilePath, tempPDFFilePath, this.MaxTimes_Doc2PDF);
                        break;
                    case ".ppt":
                    case ".pptx":
                    case ".pptm":
                    case ".potx":
                    case ".ppsx":
                    case ".ppsm":
                    case ".potm":
                        convertResult = ct.PptToPDF(doc.OriginalFullFilePath, tempPDFFilePath, this.MaxTimes_Doc2PDF);
                        break;
                    case ".pdf":
                        //tempPDFFilePath = doc.OriginalFullFilePath;
                        if (File.Exists(doc.OriginalFullFilePath))
                        {
                            File.Copy(doc.OriginalFullFilePath, tempPDFFilePath);
                        }
                        convertResult = ConvertMessage.ConvertSuccess;
                        break;
                    default:
                        convertResult = string.Format("不支持的文档类型{0}!", fileType);
                        break;
                }
                //核实第一步pdf转换的结果，如果转换失败，则异常终止！
                if (!convertResult.Equals(ConvertMessage.ConvertSuccess))
                {
                    throw new Exception(convertResult);
                }

                #endregion

                #region 第二步：尝试多长2此转换（PDF-->SWF）（1）矢量图pdf转换 （2）源生态pdf转换
                convertResult = "";//重置转换消息，进行第二阶段转换
                bool poly2bitmap = false;
                do
                {
                    convertResult = ct.PDFToSwf(tempPDFFilePath, doc.SWFFullFilePath, 0, poly2bitmap);
                    poly2bitmap = !poly2bitmap;
                }
                while (!convertResult.Equals(ConvertMessage.ConvertSuccess) && poly2bitmap);
                //核实第二步swf转换的结果，如果转换失败，则异常终止！
                if (!convertResult.Equals(ConvertMessage.ConvertSuccess))
                {
                    throw new Exception(convertResult);
                }
                #endregion
            }
            catch
            {
                throw;
            }
            finally
            {
                //释放临时资源pdf
                if (!fileType.Equals(".pdf", StringComparison.InvariantCultureIgnoreCase) && File.Exists(tempPDFFilePath))
                {
                    try
                    {
                        //PDF做为手机端输出
                        //if (!Path.GetExtension(doc.OriginalFullFilePath).Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
                        //{
                        //    File.Copy(tempPDFFilePath, doc.OriginalFullFilePath + ".pdf", true);
                        //}
                        File.Delete(tempPDFFilePath);
                    }
                    catch { }
                }
            }
        }

        #region helper
        /// <summary>
        /// 深度检测文件是否是exe、dll等可执行内容
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        internal static bool IsExeDllFile(string sFileName)
        {
            bool xx = false; //default the "sFileName" is not a .exe or .dll file; 
            FileStream fs = null;
            BinaryReader r = null;

            try
            {
                fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
                r = new BinaryReader(fs);
            }
            catch
            { }
            string bx = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx = bx + buffer.ToString();
            }
            catch
            {

            }

            if (r != null)
                r.Close();
            if (fs != null)
                fs.Close();
            // if (bx == "7790" || bx == "8297" || bx == "8075")//7790:exe 8297:rar 8075:pk 
            if (bx == "7790" || bx == "8297")
            {
                xx = true;
            }

            return xx;

        }
        #endregion
    }
}