using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Threading;

namespace ETMS.Utility
{
    /// <summary>
    /// 文件下载工具
    /// </summary>
    public class FileDownLoadUtility
    {
        private const string rar="application/x-rar-compressed";
        private const string zip= "application/zip";
        private const string pdf = "application/pdf";

        private const string xls="application/ms-excel";
        private const string doc="application/ms-word";
        private const string ppt = "application/vnd.ms-powerpoint";

        private const string docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private const string xlsx="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";


        #region 页面数据导出
        private static void ExportToFile(string FileName, System.Web.UI.Control control, string FileType)
        {
            if (control != null)
            {
                //this.EnableViewState = false;
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                control.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
                string content = oStringWriter.ToString();
                if (control is GridView)
                    content = content.Replace("border=\"0\"", "border=\"1\"");
                ExportToFile(FileName, content, FileType);
            }
        }
        private static void ExportToFile(string FileName, string content, string FileType)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = System.Text.Encoding.Default.HeaderName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + EnCodeFileName(FileName));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;//设置输出流为服务器默认编码
            HttpContext.Current.Response.ContentType = FileType;
            //HttpContext.Current.Response.Write("<style>td{mso-number-format:\"@\";BACKGROUND-COLOR:white}</style>");
            //modify by liuyx . solved the # problem
            HttpContext.Current.Response.Write("<style>td{BACKGROUND-COLOR:white}</style>");
            HttpContext.Current.Response.Write(string.Format("<meta http-equiv=Content-Type content=text/html;charset={0}>", System.Text.Encoding.Default.HeaderName));
            HttpContext.Current.Response.Write(content);
            HttpContext.Current.Response.End();
        }
        public static void ExportToExcel(string FileName, string content)
        {
            ExportToFile(FileName, content, "application/ms-excel");
        }
        public static void ExportToExcel(string FileName, System.Web.UI.Control control)
        {
            ExportToFile(FileName, control, "application/ms-excel");
        }
        public static void ExportToExcel(string FileName, GridView gwSource)
        {
            if (gwSource.Rows.Count > 0)
            {
                ExportToExcel(FileName, (System.Web.UI.Control)gwSource);
            }
        }
        public static void ExportToWord(string FileName, System.Web.UI.Control control)
        {
            ExportToFile(FileName, control, "application/ms-word");
        }
        public static void ExportToWord(string FileName, GridView gwSource)
        {
            if (gwSource.Rows.Count > 0)
            {
                ExportToWord(FileName, (System.Web.UI.Control)gwSource);
            }
        }

        public static string EnCodeFileName(string fileName)
        {
            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                return fileName;
            }
            else
            {
                return HttpUtility.UrlEncode(System.Text.UTF8Encoding.UTF8.GetBytes(fileName));
            }
        }
        #endregion

        #region 物理文件导出

        /// <summary>
        /// 根据扩展名获取文件Mime类型
        /// </summary>
        /// <param name="Extension">文件扩展名</param>
        /// <returns>Mime类型</returns>
        private static string GetMimeType(string Extension)
        {
            string returnResult = string.Empty;
            switch (Extension)
            { 
                case "rar":
                    returnResult = rar;
                    break;
                case "zip":
                    returnResult = zip;
                    break;
                case "pdf":
                    returnResult = pdf;
                    break;
                case "xls":
                    returnResult = xls;
                    break;
                case "doc":
                    returnResult = doc;
                    break;
                case "ppt":
                    returnResult = ppt;
                    break;
                case "docx":
                    returnResult = docx;
                    break;
                case "xlsx":
                    returnResult = xlsx;
                    break;
                case "pptx":
                    returnResult = pptx;
                    break;
                default:
                    returnResult = rar;
                    break;

            }
            return returnResult;
        }
        /// <summary>
        /// 导出物理文件
        /// </summary>
        /// <param name="FileName">文件全路径</param>
        /// <param name="FileReName">文件重命名</param>
        public static void ExportFileRename(string FileName, string FileReName)
        {
            string extension = Path.GetExtension(FileName);
            string MimeType = GetMimeType(extension);

            if (string.IsNullOrEmpty(FileReName))
            {
                FileReName = Path.GetFileName(FileName);
            }
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileReName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.WriteFile(FileName);
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 导出物理文件 
        /// edit zhangsz 2012-06-14 (导出文件名在火狐下乱码) 
        /// "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Path.GetFileName(FileName), System.Text.Encoding.UTF8)); 改为 "attachment;filename=" + EnCodeFileName(Path.GetFileName(FileName)));
        /// </summary>
        /// <param name="FileName">文件全路径</param>
        public static void ExportFile(string FileName)
        {
            string extension = Path.GetExtension(FileName);
            string MimeType = GetMimeType(extension);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + EnCodeFileName(Path.GetFileName(FileName)));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.WriteFile(FileName);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出物理文件
        /// </summary>
        /// <param name="FileContent">文件内容</param>
        public static void ExportFile(string FileContent, string FileName)
        {
            string extension = Path.GetExtension(FileName);
            string MimeType = GetMimeType(extension);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Path.GetFileName(FileName), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.Write(FileContent);
            HttpContext.Current.Response.End();
        }

        public static bool DownFile(string FileFullName)
        {
            bool succ = false;
            Stream iStream = null;

            // 缓存 10K
            byte[] buffer = new Byte[5000000];

            //记录文件大小
            int length;

            // 文件字节数
            long dataToRead;

            // 获取文件名
            string filename = Path.GetFileName(FileFullName);

            try
            {
                //打开文件
                iStream = new System.IO.FileStream(FileFullName, FileMode.Open,FileAccess.Read, FileShare.Read);
                // 总读取字节数:
                dataToRead = iStream.Length;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            
                // 读取文件数据
                while (dataToRead > 0)
                {

                    // 检查客户端连接情况:如果连接则传送数据,否则终止传送
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        //向缓存中存放数据:一次缓存5M
                        length = iStream.Read(buffer, 0, 5000000);

                        // 将数据写入输出流
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

                        // 将输出流推送到客户端
                        HttpContext.Current.Response.Flush();

                        buffer = new Byte[5000000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误
                throw new Exception(string.Format("下载失败:{0}",ex.Message));
            }
            finally
            {
                if (iStream != null)
                {
                    //关闭数据流
                    iStream.Close();
                }
            }


            return succ;
        }

        public static void DownFileStream(string fileName, string filePath)
        {

            FileStream fs = null;
            try
            {

                //以字符流的形式下载文件
                 fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
                        
        }

         /// <summary>
        /// 下载文件，支持大文件、续传、速度限制。支持续传的响应头Accept-Ranges、ETag，请求头Range 。
        /// Accept-Ranges：响应头，向客户端指明，此进程支持可恢复下载.实现后台智能传输服务（BITS），值为：bytes；
        /// ETag：响应头，用于对客户端的初始（200）响应，以及来自客户端的恢复请求，
        /// 必须为每个文件提供一个唯一的ETag值（可由文件名和文件最后被修改的日期组成），这使客户端软件能够验证它们已经下载的字节块是否仍然是最新的。
        /// Range：续传的起始位置，即已经下载到客户端的字节数，值如：bytes=1474560- 。
       /// 另外：UrlEncode编码后会把文件名中的空格转换中+（+转换为%2b），但是浏览器是不能理解加号为空格的，所以在浏览器下载得到的文件，空格就变成了加号；
       /// 解决办法：UrlEncode 之后, 将 "+" 替换成 "%20"，因为浏览器将%20转换为空格
       /// </summary>
       /// <param name="httpContext">当前请求的HttpContext</param>
       /// <param name="filePath">下载文件的物理路径，含路径、文件名</param>
       /// <param name="speed">下载速度：每秒允许下载的字节数</param>
       /// <returns>true下载成功，false下载失败</returns>
       public static bool DownloadFile(HttpContext httpContext, string filePath, long speed)
       {
           bool ret = true;
           try
           {
               #region--验证：HttpMethod，请求的文件是否存在
               switch (httpContext.Request.HttpMethod.ToUpper())
               { //目前只支持GET和HEAD方法
                   case "GET":
                   case "HEAD":
                       break;
                   //default:
                   //    httpContext.Response.StatusCode = 501;
                   //    return false;
               }
               if (!File.Exists(filePath))
               {
                   httpContext.Response.StatusCode = 404;
                   return false;
               }
               #endregion

               #region 定义局部变量
               long startBytes = 0;
               int packSize = 1024 * 10; //分块读取，每块10K bytes
               string fileName = Path.GetFileName(filePath);
               FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
               BinaryReader br = new BinaryReader(myFile);
               long fileLength = myFile.Length;

               int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//毫秒数：读取下一数据块的时间间隔
               string lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
               string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//便于恢复下载时提取请求头;
               #endregion

               #region--验证：文件是否太大，是否是续传，且在上次被请求的日期之后是否被修改过--------------
               if (myFile.Length > Int32.MaxValue)
               {//-------文件太大了-------
                   httpContext.Response.StatusCode = 413;//请求实体太大
                   return false;
               }

               if (httpContext.Request.Headers["If-Range"] != null)//对应响应头ETag：文件名+文件最后修改时间
               {
                   //----------上次被请求的日期之后被修改过--------------
                   if (httpContext.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                   {//文件修改过
                       httpContext.Response.StatusCode = 412;//预处理失败
                       return false;
                   }
               }
               #endregion

               try
               {
                   #region -------添加重要响应头、解析请求头、相关验证-------------------
                   httpContext.Response.Clear();
                   httpContext.Response.Buffer = false;
                   //httpContext.Response.AddHeader("Content-MD5", GetMD5Hash(myFile));//用于验证文件
                   httpContext.Response.AddHeader("Accept-Ranges", "bytes");//重要：续传必须
                   httpContext.Response.AppendHeader("ETag", "\"" + eTag + "\"");//重要：续传必须
                   httpContext.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//把最后修改日期写入响应                
                   httpContext.Response.ContentType = "application/octet-stream";//MIME类型：匹配任意文件类型
                   httpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                   httpContext.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                   httpContext.Response.AddHeader("Connection", "Keep-Alive");
                   httpContext.Response.ContentEncoding = Encoding.UTF8;
                   if (httpContext.Request.Headers["Range"] != null)
                   {//------如果是续传请求，则获取续传的起始位置，即已经下载到客户端的字节数------
                       httpContext.Response.StatusCode = 206;//重要：续传必须，表示局部范围响应。初始下载时默认为200
                       string[] range = httpContext.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-"
                       startBytes = Convert.ToInt64(range[1]);//已经下载的字节数，即本次下载的开始位置  
                       if (startBytes < 0 || startBytes >= fileLength)
                       {//无效的起始位置
                           return false;
                       }
                   }
                   if (startBytes > 0)
                   {//------如果是续传请求，告诉客户端本次的开始字节数，总长度，以便客户端将续传数据追加到startBytes位置后----------
                       httpContext.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                   }
                   #endregion

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//分块下载，剩余部分可分成的块数
                    for (int i = 0; i < maxCount && httpContext.Response.IsClientConnected; i++)
                    {//客户端中断连接，则暂停
                        httpContext.Response.BinaryWrite(br.ReadBytes(packSize));
                        httpContext.Response.Flush();
                        if (sleep > 1) Thread.Sleep(sleep);
                    }


                   #endregion

               }
               catch
               {
                   ret = false;
               }
               finally
               {
                   br.Close();
                   myFile.Close();
               }
           }
           catch
           {
               ret = false;
           }
           return ret;
       }
        

    }
}
