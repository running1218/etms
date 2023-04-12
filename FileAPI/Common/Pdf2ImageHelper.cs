using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using GhostScriptAPI;

namespace FileAPI.Common
{
    public class Pdf2ImageHelper
    {
        private GhostScriptSetting _setting;
        public GhostScriptSetting Setting
        {
            get
            {
                return _setting;
            }
            set
            {
                _setting = value;
            }
        }

        public Pdf2ImageHelper()
        {
            Setting = new GhostScriptSetting();
        }

        public Pdf2ImageHelper(GhostScriptSetting setting)
        {
            Setting = setting;
        }

        public bool ExportImage(string pdfFile, string saveDirectory, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, null, null, null, out pageCount);
        }

        public bool ExportImage(string pdfFile, string saveDirectory, string password, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, null, null, password, out pageCount);
        }

        public bool ExportImage(string pdfFile, string saveDirectory, int pageNumber, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, pageNumber, pageNumber, null, out pageCount);
        }

        public bool ExportImage(string pdfFile, string saveDirectory, int pageNumber, string password, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, pageNumber, pageNumber, password, out pageCount);
        }

        public bool ExportImage(string pdfFile, string saveDirectory, int firstPage, int lastPage, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, firstPage, lastPage, null, out pageCount);
        }

        public bool ExportImage(string pdfFile, string saveDirectory, int firstPage, int lastPage, string password, out int pageCount)
        {
            return ExportPdfToImage(pdfFile, saveDirectory, firstPage, lastPage, null, out pageCount);
        }

        private bool ExportPdfToImage(string pdfFile, string saveDirectory, int? firstPage, int? lastPage, string password, out int pageCount)
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            else
            {
                Directory.Delete(saveDirectory, true);
                Directory.CreateDirectory(saveDirectory);
            }

            bool result = false;
            if ((firstPage == null) && (lastPage == null))
            {
                result = GhostScriptHelper.Execute(pdfFile, saveDirectory, password, Setting, out pageCount);
            }
            else
            {
                if ((firstPage != null) && (lastPage != null))
                {
                    if (firstPage <= 0)
                    {
                        throw new GhostScriptConvertException(-2, "起始页面数值设置错误。");
                    }
                    if (lastPage <= 0)
                    {
                        throw new GhostScriptConvertException(-3, "结束页面数值设置错误。");
                    }
                    if (lastPage < firstPage)
                    {
                        throw new GhostScriptConvertException(-4, "起始页面不能大于结束页面。");
                    }

                    for (int i = (int)firstPage; i <= (int)lastPage; i++)
                    {
                        result = GhostScriptHelper.Execute(pdfFile, saveDirectory, i, password, Setting);
                        if (!result)
                            break;
                    }

                    pageCount = (int)lastPage - (int)firstPage + 1;
                }
                else
                {
                    throw new GhostScriptConvertException(-1, "起始页面、结束页面不能为空。");
                }
            }

            if (!result)
                throw new GhostScriptConvertException(-100, "转换文档失败。");
            else
                return true;
        }
    }
}
