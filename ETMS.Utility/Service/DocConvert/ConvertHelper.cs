using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Forms;

using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using ETMS.Utility.Service.Common;
namespace ETMS.Utility.Service.DocConvert
{
    internal class ConvertHelper
    {
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
        public int Pdf2Swf_MaxTime { get; set; }

        private Timer timer = null;

        private Word._Document wordDocument = null;
        private Word.ApplicationClass wordApplication = null;
        object paramMissing = null;


        object missing = null;
        Excel.ApplicationClass excelApplication = null;
        Excel.Workbook workBook = null;

        PowerPoint.ApplicationClass pptApplication = null;
        PowerPoint.Presentation persentation = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public string WordToPDF(string sourcePath, string targetPath, int maxTime)
        {
            string result;

            timer = new Timer();
            timer.Interval = maxTime;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;

            paramMissing = Type.Missing;
            wordApplication = new Word.ApplicationClass();
            //Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            //Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Word.WdExportFormat paramExportFormat = Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                    Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                    ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                        paramExportFormat, paramOpenAfterExport,
                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                        paramEndPage, paramExportItem, paramIncludeDocProps,
                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                        paramBitmapMissingFonts, paramUseISO19005_1,
                        ref paramMissing);
                result = ConvertMessage.ConvertSuccess;
            }

            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                timer.Enabled = false;

                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public string ExcelToPDF(string sourcePath, string targetPath, int maxTime)
        {
            string result;
            missing = Type.Missing;
            //object missing = Type.Missing;
            //Excel.ApplicationClass application = null;
            //Excel.Workbook workBook = null;

            timer = new Timer();
            timer.Interval = maxTime;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
            try
            {
                excelApplication = new Excel.ApplicationClass();
                object target = targetPath;
                object type = Excel.XlFixedFormatType.xlTypePDF;
                workBook = excelApplication.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing);

                workBook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, target, Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);

                result = ConvertMessage.ConvertSuccess;
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                timer.Enabled = false;
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    excelApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public string PptToPDF(string sourcePath, string targetPath, int maxTime)
        {
            string result;
            missing = Type.Missing;
            //object missing = Type.Missing;
            //PowerPoint.ApplicationClass application = null;
            //PowerPoint.Presentation persentation = null;


            timer = new Timer();
            timer.Interval = maxTime;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
            try
            {
                pptApplication = new PowerPoint.ApplicationClass();
                persentation = pptApplication.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                persentation.SaveAs(targetPath, PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoTrue);

                result = ConvertMessage.ConvertSuccess;
            }
            catch (Exception e)
            {
                result = e.Message;
                ;
            }
            finally
            {
                timer.Enabled = false;
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (pptApplication != null)
                {
                    pptApplication.Quit();
                    pptApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// PDFToSwf
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string PDFToSwf(string sourcePath, string targetPath, int waitTime, bool poly2bitmap)
        {
            try
            {
                string command = Pdf2SwfFilePath;
                string swfPath = sourcePath.ToLower().Replace(".pdf", ".swf");
                string arguments = "";
                if (poly2bitmap)
                    arguments = " -o \"" + swfPath + "\" -i -s flashversion=9 -s disablelinks -s poly2bitmap -t \"" + sourcePath + "\" -s languagedir=\"" + XPDFPath + "\"";
                else
                    arguments = " -o \"" + swfPath + "\" -i -s flashversion=9 -s disablelinks -t \"" + sourcePath + "\" -s languagedir=\"" + XPDFPath + "\""; ;

                ProcessHelper.WaitExecuteForExit(command, arguments, waitTime * 1000);

                if (System.IO.File.Exists(swfPath))
                {
                    if (File.Exists(targetPath))
                        File.Delete(targetPath);
                    File.Move(swfPath, targetPath);
                    return ConvertMessage.ConvertSuccess;
                }
                else
                    return ConvertMessage.PdfConvertError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            StopOfficeConvert();
        }

        private void StopOfficeConvert()
        {
            ///关闭word
            try
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }


                ///关闭excel
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    excelApplication = null;
                }


                ///关闭ppt
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (pptApplication != null)
                {
                    pptApplication.Quit();
                    pptApplication = null;
                }
            }
            catch { }
        }
    }
}
