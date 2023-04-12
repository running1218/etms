using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace GhostScriptAPI
{
    public class GhostScriptHelper
    {
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance")]
        private static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args")]
        private static extern int gsapi_init_with_args(IntPtr instance, int argc, string[] argv);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit")]
        private static extern int gsapi_exit(IntPtr instance);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance")]
        private static extern void gsapi_delete_instance(IntPtr instance);

        private static object gsInstanceLock = new object();

        public static bool Execute(string inputFile, string outputDirectory, string password, GhostScriptSetting setting, out int pageCount)
        {
            pageCount = 0;
            string imageExtension = "." + GetImageFormatExtension(setting.ImageFormat);

            List<string> args = GetSettingParameters(setting);

            string parameterFormat = "{0}={1}";

            //-sPDFPassword
            if (!string.IsNullOrEmpty(password))
            {
                args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sPDFPassword), password));
            }

            //-sOutputFile
            args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sOutputFile), Path.Combine(outputDirectory, "%d" + imageExtension)));

            //InputFile
            args.Add(inputFile);

            bool result = Execute(args.ToArray());
            if (result)
            {
                pageCount = Directory.GetFiles(outputDirectory, "*" + imageExtension, SearchOption.TopDirectoryOnly).Length;
                for (int i = 1; i <= pageCount; i++)
                {
                    string imagePath = Path.Combine(outputDirectory, i.ToString() + imageExtension);
                    string thumbPath = Path.Combine(outputDirectory, i.ToString() + "_t" + imageExtension);
                    ImageHelper.MakeThumbnail(imagePath, thumbPath, 64, 64, ImageThumbnailMode.Equimultiple);
                }
            }

            return result;
        }

        public static bool Execute(string inputFile, string outputDirectory, int pageNumber, string password, GhostScriptSetting setting)
        {
            string imageExtension = "." + GetImageFormatExtension(setting.ImageFormat);

            List<string> args = GetSettingParameters(setting);

            string parameterFormat = "{0}={1}";

            //-sPDFPassword
            if (!string.IsNullOrEmpty(password))
            {
                args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sPDFPassword), password));
            }

            //-dFirstPage=1
            args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dFirstPage), pageNumber));

            //-dLastPage=10
            args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dLastPage), pageNumber));

            //-sOutputFile
            args.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sOutputFile), Path.Combine(outputDirectory, pageNumber.ToString() + imageExtension)));

            //InputFile
            args.Add(inputFile);

            bool result = Execute(args.ToArray());
            if (result)
            {
                string imagePath = Path.Combine(outputDirectory, pageNumber.ToString() + imageExtension);
                string thumbPath = Path.Combine(outputDirectory, pageNumber.ToString() + "_t" + imageExtension);
                ImageHelper.MakeThumbnail(imagePath, thumbPath, 64, 64, ImageThumbnailMode.Equimultiple);
            }

            return result;
        }

        private static bool Execute(string[] args)
        {
            IntPtr gsInstanceIntPtr;
            lock (gsInstanceLock)
            {
                gsapi_new_instance(out gsInstanceIntPtr, IntPtr.Zero);
                try
                {
                    int result = gsapi_init_with_args(gsInstanceIntPtr, args.Length, args);
                    return result < 0 ? false : true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    gsapi_exit(gsInstanceIntPtr);
                    gsapi_delete_instance(gsInstanceIntPtr);
                }
            }
        }

        private static string GetParameterString(GhostScriptParameter parameter)
        {
            switch (parameter)
            {
                case GhostScriptParameter.dFIXEDRESOLUTION:
                    return "-r";
                case GhostScriptParameter.dIgnoreNumCopies:
                    return "-d.IgnoreNumCopies";
                default:
                    return "-" + parameter.ToString();
            }
        }

        private static string GetPaperSizeString(GhostScriptPaperSize paperSize)
        {
            switch (paperSize)
            {
                case GhostScriptPaperSize.portrait_11x17:
                    return "11x17";
                default:
                    return paperSize.ToString();
            }
        }

        private static string GetImageFormatString(GhostScriptImageFormat imageFormat)
        {
            return imageFormat.ToString();
        }

        public static string GetImageFormatExtension(GhostScriptImageFormat imageFormat)
        {
            switch (imageFormat)
            {
                case GhostScriptImageFormat.png16m:
                case GhostScriptImageFormat.png256:
                case GhostScriptImageFormat.pnggray:
                case GhostScriptImageFormat.pngalpha:
                    return "png";

                case GhostScriptImageFormat.jpeg:
                case GhostScriptImageFormat.jpeggray:
                    return "jpg";

                case GhostScriptImageFormat.tiff24nc:
                case GhostScriptImageFormat.tiff32nc:
                case GhostScriptImageFormat.tiffgray:
                    return "tiff";

                default:
                    throw new ArgumentException("图片格式错误。");
            }
        }

        private static int GetAntiAliasLevel(GhostScriptAntiAlias antiAlias)
        {
            return (int)antiAlias;
        }

        public static List<string> GetSettingParameters(GhostScriptSetting setting)
        {
            List<string> parameters = new List<string>();
            //-q 
            parameters.Add(GetParameterString(GhostScriptParameter.dQUIET));
            //-dSAFER
            parameters.Add(GetParameterString(GhostScriptParameter.dSAFER));
            //-dBATCH
            parameters.Add(GetParameterString(GhostScriptParameter.dBATCH));
            //-dNOPAUSE
            parameters.Add(GetParameterString(GhostScriptParameter.dNOPAUSE));
            //-dUseCIEColor
            parameters.Add(GetParameterString(GhostScriptParameter.dUseCIEColor));

            string parameterFormat = "{0}={1}";

            //-dGridFitTT=2
            parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dGridFitTT), 2));

            //-sDEVICE=png16m
            parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sDEVICE), GetImageFormatString(setting.ImageFormat)));

            //-dGraphicsAlphaBits=4
            parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dGraphicsAlphaBits), GetAntiAliasLevel(setting.AntiAlias)));

            //-dTextAlphaBits=4
            parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dTextAlphaBits), GetAntiAliasLevel(setting.AntiAlias)));

            //-r150
            parameters.Add(string.Format("{0}{1}", GetParameterString(GhostScriptParameter.dFIXEDRESOLUTION), setting.DPI));

            switch (setting.PaperSizeType)
            {
                case GhostScriptPaperSizeType.Default:
                    //-dUseCropBox=true
                    parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dUseCropBox), "true"));
                    break;
                case GhostScriptPaperSizeType.Define: 
                    //-dPDFFitPage
                    parameters.Add(GetParameterString(GhostScriptParameter.dPDFFitPage));
                    //-sPAPERSIZE=a5
                    parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.sPAPERSIZE), GetPaperSizeString(setting.PaperSize)));
                    break;
                case GhostScriptPaperSizeType.Custom:
                    //-dPDFFitPage
                    parameters.Add(GetParameterString(GhostScriptParameter.dPDFFitPage));
                    //-dDEVICEWIDTHPOINTS=720
                    parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dDEVICEWIDTHPOINTS), setting.PaperWidth));
                    //-dDEVICEHEIGHTPOINTS=1440
                    parameters.Add(string.Format(parameterFormat, GetParameterString(GhostScriptParameter.dDEVICEHEIGHTPOINTS), setting.PaperHeight));
                    break;
            }

            return parameters;
        }

        //public static int GetPDFPageTotalCount(string pdfFile)
        //{
        //    try
        //    {
        //        Process p = new Process();

        //        p.StartInfo.UseShellExecute = false;
        //        p.StartInfo.RedirectStandardOutput = true;
        //        p.StartInfo.CreateNoWindow = true;
        //        p.StartInfo.FileName = ConfigurationManager.AppSettings["PdfConvertGSWin32CPath"] ?? "gswin32c.exe";
        //        p.StartInfo.Arguments = string.Format(" -q -dNODISPLAY -c \"({0}) (r) file runpdfbegin pdfpagecount = quit\")", pdfFile.Replace(@"\", @"\\"));
        //        p.Start();

        //        string pagecountString = p.StandardOutput.ReadToEnd();
        //        p.WaitForExit();

        //        pagecountString = pagecountString.Replace("\n", "");

        //        int pagecount = 0;
        //        if (int.TryParse(pagecountString, out pagecount))
        //        {
        //            return pagecount;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
    }

    /// <summary>
    /// 执行参数
    /// </summary>
    public enum GhostScriptParameter
    {
        //Rendering parameters
        dCOLORSCREEN,
        dDITHERPPI,
        dDOINTERPOLATE,
        dTextAlphaBits,
        dGraphicsAlphaBits,
        dAlignToPixels,
        dGridFitTT,
        dUseCIEColor,
        dNOCIE,
        dNOSUBSTDEVICECOLORS,
        dNOPSICC,
        dNOINTERPOLATE,
        dNOTRANSPARENCY,
        dNO_TN5044,
        dDOPS,

        //Page parameters
        dFIXEDMEDIA,
        dFIXEDRESOLUTION,
        dPSFitPage,
        dORIENT1,
        dDEVICEWIDTHPOINTS,
        dDEVICEHEIGHTPOINTS,
        sDEFAULTPAPERSIZE,
        dFitPage,

        //Font-related parameters
        dDISKFONTS,
        dLOCALFONTS,
        dNOCCFONTS,
        dNOFONTMAP,
        dNOFONTPATH,
        dNOPLATFONTS,
        dNONATIVEFONTMAP,
        sFONTMAP,
        sFONTPATH,
        sSUBSTFONT,
        dOLDCFF,

        //Resource-related parameters
        sGenericResourceDir,
        sFontResourceDir,

        //Interaction-related parameters
        dBATCH,
        dNOPAGEPROMPT,
        dNOPAUSE,
        dNOPROMPT,
        dQUIET,
        dSHORTERRORS,
        sstdout,
        dTTYPAUSE,

        //Device parameters
        dNODISPLAY,
        sDEVICE,
        sOutputFile,
        dIgnoreNumCopies,

        //EPS parameters
        dEPSCrop,
        dEPSFitPage,
        dNOEPS,

        ///ICC color
        sDefaultGrayProfile,
        sDefaultRGBProfile,
        sDefaultCMYKProfile,
        sDeviceNProfile,
        sOutputICCProfile,
        sICCOutputColors,
        sProofProfile,
        sDeviceLinkProfile,
        sNamedProfile,
        dRenderIntent,
        dBlackPtComp,
        dKPreserve,
        sGraphicICCProfile,
        dGraphicIntent,
        dGraphicBlackPt,
        dGraphicKPreserve,
        sImageICCProfile,
        dImageIntent,
        dImageBlackPt,
        dImageKPreserve,
        sTextICCProfile,
        dTextIntent,
        dTextBlackPt,
        dTextKPreserve,
        dOverrideICC,
        sSourceObjectICC,
        dDeviceGrayToK,
        dUseFastColor,
        dSimulateOverprint,
        dUsePDFX3Profile,
        sICCProfilesDir,

        //Other parameters
        dDELAYBIND,
        dDOPDFMARKS,
        dJOBSERVER,
        dNOBIND,
        dNOCACHE,
        dNOGC,
        dNOOUTERSAVE,
        dNOSAFER,
        dSAFER,
        dPreBandThreshold,
        dSTRICT,
        dWRITESYSTEMDICT,

        //PDF parameters
        dFirstPage,
        dLastPage,
        dPDFFitPage,
        dPrinted,
        dUseBleedBox,
        dUseTrimBox,
        dUseArtBox,
        dUseCropBox,
        sPDFPassword,
        dShowAnnots,
        dShowAcroForm,
        dNoUserUnit,
        dRENDERTTNOTDEF,

        sPAPERSIZE
    }

    /// <summary>
    /// 图片输出页面大小
    /// </summary>
    public enum GhostScriptPaperSize
    {
        //U.S. standard
        portrait_11x17,
        ledger,
        legal,
        letter,
        lettersmall,
        archE,
        archD,
        archC,
        archB,
        archA,

        //ISO standard
        a0,
        a1,
        a2,
        a3,
        a4,
        a4small,
        a5,
        a6,
        a7,
        a8,
        a9,
        a10,
        isob0,
        isob1,
        isob2,
        isob3,
        isob4,
        isob5,
        isob6,
        c0,
        c1,
        c2,
        c3,
        c4,
        c5,
        c6,

        //JIS standard
        jisb0,
        jisb1,
        jisb2,
        jisb3,
        jisb4,
        jisb5,
        jisb6,

        //ISO / JIS switchable
        b0,
        b1,
        b2,
        b3,
        b4,
        b5,

        //Other
        flsa,
        flse,
        halfletter,
        hagaki
    }

    /// <summary>
    /// 图片输出大小模式
    /// </summary>
    public enum GhostScriptPaperSizeType
    {
        Default,
        Define,
        Custom
    }

    /// <summary>
    /// 图片输出格式
    /// </summary>
    public enum GhostScriptImageFormat
    {
        /// <summary>
        /// PNG 8位色彩
        /// </summary>
        png256,

        /// <summary>
        /// PNG 24位色彩
        /// </summary>
        png16m,

        /// <summary>
        /// PNG 灰度
        /// </summary>
        pnggray,

        /// <summary>
        /// PNG 透明
        /// </summary>
        pngalpha,

        /// <summary>
        /// JPG 
        /// </summary>
        jpeg,

        /// <summary>
        /// JPG 灰度
        /// </summary>
        jpeggray,

        /// <summary>
        /// TIFF 8位灰度
        /// </summary>
        tiffgray,
        
        /// <summary>
        /// TIFF 24位色彩
        /// </summary>
        tiff24nc,
        
        /// <summary>
        /// TIFF 32位色彩
        /// </summary>
        tiff32nc
    }

    /// <summary>
    /// 图片输出质量
    /// </summary>
    public enum GhostScriptAntiAlias
    {
        Low = 1,
        Medium = 2,
        High = 4
    }

    /// <summary>
    /// 输出设置
    /// </summary>
    public class GhostScriptSetting
    {
        //-r150
        private GhostScriptImageFormat _imageFormat = GhostScriptImageFormat.jpeg;
        private GhostScriptAntiAlias _antiAlias = GhostScriptAntiAlias.High;
        private int _dpi = 200;
        private GhostScriptPaperSizeType _paperSizeType = GhostScriptPaperSizeType.Default;
        private GhostScriptPaperSize _paperSize = GhostScriptPaperSize.a4;
        private int _paperWidth = 0;
        private int _paperHeight = 0;

        /// <summary>
        /// 图片输出格式
        /// </summary>
        public GhostScriptImageFormat ImageFormat
        {
            get
            {
                return _imageFormat;
            }
            set
            {
                _imageFormat = value;
            }
        }

        /// <summary>
        /// 图片输出质量
        /// </summary>
        public GhostScriptAntiAlias AntiAlias
        {
            get
            {
                return _antiAlias;
            }
            set
            {
                _antiAlias = value;
            }
        }

        /// <summary>
        /// 图片输出每英寸点数
        /// </summary>
        public int DPI
        {
            get
            {
                return _dpi;
            }
            set
            {
                _dpi = value;
            }
        }

        /// <summary>
        /// 图片输出大小模式
        /// </summary>
        public GhostScriptPaperSizeType PaperSizeType
        {
            get
            {
                return _paperSizeType;
            }
            set
            {
                _paperSizeType = value;
            }
        }

        /// <summary>
        /// 图片输出页面大小
        /// </summary>
        public GhostScriptPaperSize PaperSize
        {
            get
            {
                return _paperSize;
            }
            set
            {
                _paperSize = value;
            }
        }

        /// <summary>
        /// 图片输出宽度
        /// </summary>
        public int PaperWidth
        {
            get
            {
                return _paperWidth;
            }
            set
            {
                _paperWidth = value;
            }
        }

        /// <summary>
        /// 图片输出高度
        /// </summary>
        public int PaperHeight
        {
            get
            {
                return _paperHeight;
            }
            set
            {
                _paperHeight = value;
            }
        }
    }

    public class GhostScriptConvertException : Exception
    {
        public int ErrorCode
        {
            get;
            set;
        }

        public GhostScriptConvertException(int errorCode, string message):base(message)
        {
            this.ErrorCode = errorCode;
        }
    }

    public class ConvertEventArgs : EventArgs
    {
        public ConvertEventArgs(int currentPage, int convertCount)
        {
            _currentPage = currentPage;
            _convertCount = convertCount;
        }

        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
        }
        private int _convertCount;
        public int ConvertCount
        {
            get
            {
                return _convertCount;
            }
        }
    }

    public delegate void ConvertEventHandler(object sender, ConvertEventArgs e);
}
