using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections;
using System.IO;
using System.Web;

namespace ETMS.Studying.Controls
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            UploadFile(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        public void UploadFile(HttpContext context)
        {
            string functionType = string.Empty;
            string pr = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.QueryString["type"]) && !string.IsNullOrEmpty(context.Request.QueryString["type"].Trim()))
            {
                functionType = context.Request.QueryString["type"].Trim();
            }
            else
            {
                return;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pr"]) && !string.IsNullOrEmpty(context.Request.QueryString["pr"].Trim()))
            {
                pr = context.Request.QueryString["pr"].Trim();
            }
            else
            {
                return;
            }
            pr = CrypProvider.Decryptor(pr);
            functionType = CrypProvider.Decryptor(functionType);
            FileUploadConfig fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy(functionType);


            context.Response.CacheControl = "no-cache";


            string updir = string.Empty;
            string reupdir = fileUploadConfig.UrlRoot;


            updir = fileUploadConfig.Root;
            //文件不存在就创建
            CreateDir(updir);

            string extname = string.Empty;
            string fullname = string.Empty;
            string filename = string.Empty;
            string FileOldName = string.Empty;
            if (context.Request.Files.Count > 0)
            {
                try
                {

                    for (int j = 0; j < context.Request.Files.Count; j++)
                    {
                        int offset = Convert.ToInt32(context.Request["chunk"]);
                        int total = Convert.ToInt32(context.Request["chunks"]);

                        HttpPostedFile uploadFile = context.Request.Files[j];

                        if (total <= 1)
                        {
                            FileOldName = uploadFile.FileName;
                        }
                        else
                        {
                            FileOldName = context.Request.Form["name"].Trim();
                        }
                        extname = Path.GetExtension(FileOldName);
                        //判断文件类型
                        if (!Array.Exists<string>(fileUploadConfig.FileTypes, new Predicate<string>(
                    delegate (string item)
                    {
                        return (item.Equals(extname, StringComparison.InvariantCultureIgnoreCase));
                    }
                    )))
                        {
                            context.Response.Write("文件格式不正确！");
                            return;
                        }

                        //文件没有分块
                        if (total <= 1)
                        {
                            if (uploadFile.ContentLength > 0)
                            {
                                if (uploadFile.ContentLength / 1024 / 1024 > fileUploadConfig.MaxFileSize)
                                {
                                    context.Response.Write("文件太大，超过了" + fileUploadConfig.MaxFileSize.ToString() + "MB！");
                                    return;
                                }
                                filename = GetNewFileName(fileUploadConfig, FileOldName);
                                string oldFullName = string.Format("{0}\\{1}", updir, filename);
                                DeleteOldFile(oldFullName);
                                uploadFile.SaveAs(oldFullName);

                                FileUploadInfo fileDefine = new FileUploadInfo();
                                fileDefine.BizUrl = CrypProvider.Encryptor(filename);
                                fileDefine.FileName = Path.GetFileName(filename);
                                fileDefine.FileNames = filename;
                                fileDefine.FileOldName = FileOldName;
                                fileDefine.FileType = extname;
                                fileDefine.FileSize = uploadFile.ContentLength;
                                fileDefine.FileSizeStr = CrypProvider.Encryptor(uploadFile.ContentLength.ToString());
                                fileDefine.FullUrl = string.Format("{0}/{1}", reupdir, filename.Replace('\\', '/'));
                                ResponseStr(context, fileDefine);

                            }
                        }
                        else
                        {
                            fullname = string.Format("{0}\\{1}.part", updir, pr + FileOldName);

                            if (System.IO.File.Exists(fullname))
                            {
                                System.IO.FileInfo tempfi = new System.IO.FileInfo(fullname);
                                if (tempfi.Length / 1024 / 1024 > fileUploadConfig.MaxFileSize)
                                {
                                    tempfi.Delete();
                                    context.Response.Write("文件太大，超过了" + fileUploadConfig.MaxFileSize.ToString() + "MB！");
                                    return;
                                }
                            }

                            //文件 分成多块上传
                            WriteTempFile(uploadFile, offset, fullname);

                            if (total - offset == 1)
                            {
                                //如果是最后一个分块文件 ，则把文件从临时文件夹中移到上传文件 夹中
                                System.IO.FileInfo fi = new System.IO.FileInfo(fullname);
                                filename = GetNewFileName(fileUploadConfig, FileOldName);
                                string oldFullName = string.Format("{0}\\{1}", updir, filename);
                                DeleteOldFile(oldFullName);

                                FileUploadInfo fileDefine = new FileUploadInfo();
                                fileDefine.BizUrl = CrypProvider.Encryptor(filename);
                                fileDefine.FileName = Path.GetFileName(filename);
                                fileDefine.FileNames = filename;
                                fileDefine.FileOldName = FileOldName;
                                fileDefine.FileType = extname;
                                fileDefine.FileSize = (int)fi.Length;
                                fileDefine.FileSizeStr = CrypProvider.Encryptor(fi.Length.ToString());
                                fileDefine.FullUrl = string.Format("{0}/{1}", reupdir, filename.Replace('\\', '/'));
                                fi.MoveTo(oldFullName);
                                ResponseStr(context, fileDefine);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write("Message" + ex.ToString());
                }



            }
        }

        public void ResponseStr(HttpContext context, FileUploadInfo fileDefine)
        {
            context.Response.Write(JsonHelper.JsonSerializer<FileUploadInfo>(fileDefine));
        }

        public void DeleteOldFile(string oldFullName)
        {
            string path = Path.GetDirectoryName(oldFullName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo oldFi = new FileInfo(oldFullName);
            if (oldFi.Exists)
            {
                //文件名存在则删除旧文件 
                oldFi.Delete();
            }
        }

        /// <summary>
        /// 动态表达式计算，获取新的文件名称
        /// </summary>
        /// <param name="functionConfigInfo">功能上传配置信息</param> 
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetNewFileName(FileUploadConfig functionConfigInfo, string fileName)
        {
            string newFileName = fileName;
            if (functionConfigInfo.FileNameExpression != null)
            {
                //通过动态表达式计算新的文件名
                Hashtable variables = new Hashtable();
                //fixed bug:如果filename中包含路径，则也要原始返回！
                variables.Add("FileName", Path.GetDirectoryName(fileName).Equals(string.Empty) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetDirectoryName(fileName) + @"\" + Path.GetFileNameWithoutExtension(fileName));
                variables.Add("FileType", Path.GetExtension(fileName));
                variables.Add("RequestParams", System.Web.HttpContext.Current.Request.Params);
                newFileName = (string)functionConfigInfo.FileNameExpression.GetValue(null, variables);
                /*if (Logger.IsDebugEnabled)
                {
                    //动态表达式计算
                    Logger.Debug(string.Format("上传文件名动态计算，原始名：{0}，新名：{1}", fileName, newFileName));
                }*/
            }

            return newFileName;
        }

        /// <summary>
        /// 保存临时文件 
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        private string WriteTempFile(HttpPostedFile uploadFile, int chunk, string fullName)
        {
            if (chunk == 0)
            {
                DeleteOldFile(fullName);
                //如果是第一个分块，则直接保存
                uploadFile.SaveAs(fullName);
            }
            else
            {
                //如果是其他分块文件 ，则原来的分块文件，读取流，然后文件最后写入相应的字节
                FileStream fs = new FileStream(fullName, FileMode.Append);
                if (uploadFile.ContentLength > 0)
                {
                    int FileLen = uploadFile.ContentLength;
                    byte[] input = new byte[FileLen];

                    // Initialize the stream.
                    System.IO.Stream MyStream = uploadFile.InputStream;

                    // Read the file into the byte array.
                    MyStream.Read(input, 0, FileLen);

                    fs.Write(input, 0, FileLen);

                }
                fs.Close();
            }


            return fullName;
        }

        /// <summary>
        /// 文件不存在就创建
        /// </summary>
        /// <param name="path"></param>
        public void CreateDir(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}