using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Threading;

namespace ETMS.Utility
{
    /// <summary>
    /// �ļ����ع���
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


        #region ҳ�����ݵ���
        private static void ExportToFile(string FileName, System.Web.UI.Control control, string FileType)
        {
            if (control != null)
            {
                //this.EnableViewState = false;
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                control.RenderControl(oHtmlTextWriter);//���������ؼ����������  
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
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;//���������Ϊ������Ĭ�ϱ���
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

        #region �����ļ�����

        /// <summary>
        /// ������չ����ȡ�ļ�Mime����
        /// </summary>
        /// <param name="Extension">�ļ���չ��</param>
        /// <returns>Mime����</returns>
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
        /// ���������ļ�
        /// </summary>
        /// <param name="FileName">�ļ�ȫ·��</param>
        /// <param name="FileReName">�ļ�������</param>
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
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//���������Ϊ��������
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.WriteFile(FileName);
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// ���������ļ� 
        /// edit zhangsz 2012-06-14 (�����ļ����ڻ��������) 
        /// "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Path.GetFileName(FileName), System.Text.Encoding.UTF8)); ��Ϊ "attachment;filename=" + EnCodeFileName(Path.GetFileName(FileName)));
        /// </summary>
        /// <param name="FileName">�ļ�ȫ·��</param>
        public static void ExportFile(string FileName)
        {
            string extension = Path.GetExtension(FileName);
            string MimeType = GetMimeType(extension);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + EnCodeFileName(Path.GetFileName(FileName)));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//���������Ϊ��������
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.WriteFile(FileName);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="FileContent">�ļ�����</param>
        public static void ExportFile(string FileContent, string FileName)
        {
            string extension = Path.GetExtension(FileName);
            string MimeType = GetMimeType(extension);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Path.GetFileName(FileName), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//���������Ϊ��������
            HttpContext.Current.Response.ContentType = MimeType;
            HttpContext.Current.Response.Write(FileContent);
            HttpContext.Current.Response.End();
        }

        public static bool DownFile(string FileFullName)
        {
            bool succ = false;
            Stream iStream = null;

            // ���� 10K
            byte[] buffer = new Byte[5000000];

            //��¼�ļ���С
            int length;

            // �ļ��ֽ���
            long dataToRead;

            // ��ȡ�ļ���
            string filename = Path.GetFileName(FileFullName);

            try
            {
                //���ļ�
                iStream = new System.IO.FileStream(FileFullName, FileMode.Open,FileAccess.Read, FileShare.Read);
                // �ܶ�ȡ�ֽ���:
                dataToRead = iStream.Length;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//���������Ϊ��������
            
                // ��ȡ�ļ�����
                while (dataToRead > 0)
                {

                    // ���ͻ����������:���������������,������ֹ����
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        //�򻺴��д������:һ�λ���5M
                        length = iStream.Read(buffer, 0, 5000000);

                        // ������д�������
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

                        // ����������͵��ͻ���
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
                // ����
                throw new Exception(string.Format("����ʧ��:{0}",ex.Message));
            }
            finally
            {
                if (iStream != null)
                {
                    //�ر�������
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

                //���ַ�������ʽ�����ļ�
                 fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                //֪ͨ����������ļ������Ǵ�
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
        /// �����ļ���֧�ִ��ļ����������ٶ����ơ�֧����������ӦͷAccept-Ranges��ETag������ͷRange ��
        /// Accept-Ranges����Ӧͷ����ͻ���ָ�����˽���֧�ֿɻָ�����.ʵ�ֺ�̨���ܴ������BITS����ֵΪ��bytes��
        /// ETag����Ӧͷ�����ڶԿͻ��˵ĳ�ʼ��200����Ӧ���Լ����Կͻ��˵Ļָ�����
        /// ����Ϊÿ���ļ��ṩһ��Ψһ��ETagֵ�������ļ������ļ�����޸ĵ�������ɣ�����ʹ�ͻ�������ܹ���֤�����Ѿ����ص��ֽڿ��Ƿ���Ȼ�����µġ�
        /// Range����������ʼλ�ã����Ѿ����ص��ͻ��˵��ֽ�����ֵ�磺bytes=1474560- ��
       /// ���⣺UrlEncode��������ļ����еĿո�ת����+��+ת��Ϊ%2b��������������ǲ������Ӻ�Ϊ�ո�ģ���������������صõ����ļ����ո�ͱ���˼Ӻţ�
       /// ����취��UrlEncode ֮��, �� "+" �滻�� "%20"����Ϊ�������%20ת��Ϊ�ո�
       /// </summary>
       /// <param name="httpContext">��ǰ�����HttpContext</param>
       /// <param name="filePath">�����ļ�������·������·�����ļ���</param>
       /// <param name="speed">�����ٶȣ�ÿ���������ص��ֽ���</param>
       /// <returns>true���سɹ���false����ʧ��</returns>
       public static bool DownloadFile(HttpContext httpContext, string filePath, long speed)
       {
           bool ret = true;
           try
           {
               #region--��֤��HttpMethod��������ļ��Ƿ����
               switch (httpContext.Request.HttpMethod.ToUpper())
               { //Ŀǰֻ֧��GET��HEAD����
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

               #region ����ֲ�����
               long startBytes = 0;
               int packSize = 1024 * 10; //�ֿ��ȡ��ÿ��10K bytes
               string fileName = Path.GetFileName(filePath);
               FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
               BinaryReader br = new BinaryReader(myFile);
               long fileLength = myFile.Length;

               int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//����������ȡ��һ���ݿ��ʱ����
               string lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
               string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//���ڻָ�����ʱ��ȡ����ͷ;
               #endregion

               #region--��֤���ļ��Ƿ�̫���Ƿ��������������ϴα����������֮���Ƿ��޸Ĺ�--------------
               if (myFile.Length > Int32.MaxValue)
               {//-------�ļ�̫����-------
                   httpContext.Response.StatusCode = 413;//����ʵ��̫��
                   return false;
               }

               if (httpContext.Request.Headers["If-Range"] != null)//��Ӧ��ӦͷETag���ļ���+�ļ�����޸�ʱ��
               {
                   //----------�ϴα����������֮���޸Ĺ�--------------
                   if (httpContext.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                   {//�ļ��޸Ĺ�
                       httpContext.Response.StatusCode = 412;//Ԥ����ʧ��
                       return false;
                   }
               }
               #endregion

               try
               {
                   #region -------�����Ҫ��Ӧͷ����������ͷ�������֤-------------------
                   httpContext.Response.Clear();
                   httpContext.Response.Buffer = false;
                   //httpContext.Response.AddHeader("Content-MD5", GetMD5Hash(myFile));//������֤�ļ�
                   httpContext.Response.AddHeader("Accept-Ranges", "bytes");//��Ҫ����������
                   httpContext.Response.AppendHeader("ETag", "\"" + eTag + "\"");//��Ҫ����������
                   httpContext.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//������޸�����д����Ӧ                
                   httpContext.Response.ContentType = "application/octet-stream";//MIME���ͣ�ƥ�������ļ�����
                   httpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                   httpContext.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                   httpContext.Response.AddHeader("Connection", "Keep-Alive");
                   httpContext.Response.ContentEncoding = Encoding.UTF8;
                   if (httpContext.Request.Headers["Range"] != null)
                   {//------����������������ȡ��������ʼλ�ã����Ѿ����ص��ͻ��˵��ֽ���------
                       httpContext.Response.StatusCode = 206;//��Ҫ���������룬��ʾ�ֲ���Χ��Ӧ����ʼ����ʱĬ��Ϊ200
                       string[] range = httpContext.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-"
                       startBytes = Convert.ToInt64(range[1]);//�Ѿ����ص��ֽ��������������صĿ�ʼλ��  
                       if (startBytes < 0 || startBytes >= fileLength)
                       {//��Ч����ʼλ��
                           return false;
                       }
                   }
                   if (startBytes > 0)
                   {//------������������󣬸��߿ͻ��˱��εĿ�ʼ�ֽ������ܳ��ȣ��Ա�ͻ��˽���������׷�ӵ�startBytesλ�ú�----------
                       httpContext.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                   }
                   #endregion

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//�ֿ����أ�ʣ�ಿ�ֿɷֳɵĿ���
                    for (int i = 0; i < maxCount && httpContext.Response.IsClientConnected; i++)
                    {//�ͻ����ж����ӣ�����ͣ
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
