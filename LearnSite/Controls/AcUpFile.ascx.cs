using ETMS.Utility;
using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace ETMS.Studying.Controls
{
    public partial class AcUpFile : System.Web.UI.UserControl
    {


        /// <summary>
        /// 上传请求的地址
        /// </summary>
        public string UpUrl
        {
            get
            {
                string url = (ViewState["UpUrl"] == null) ? WebUtility.AppPath + "/Controls/UploadHandler.ashx" : (string)ViewState["UpUrl"];

                return this.ActionHref(string.Format("{0}?type={1}&pr={2}"
                    , url
                    , HttpUtility.UrlEncode(ETMS.Utility.CrypProvider.Encryptor(FunctionType))
                    , HttpUtility.UrlEncode(ETMS.Utility.CrypProvider.Encryptor(DateTime.Now.ToString("yyyyMMddHHmmssfff")))));
            }
            set
            {
                ViewState["UpUrl"] = value;
            }
        }

        /// <summary>
        /// 此次上传对应的业务功能类型
        /// </summary>
        public string FunctionType
        {
            get
            {
                return (string)ViewState["FunctionType"];
            }
            set
            {
                ViewState["FunctionType"] = value;
            }
        }
        public bool FileTypeIsDisplay
        {
            get
            {
                return ViewState["FileTypeIsDisplay"] != null ? (bool)ViewState["FileTypeIsDisplay"] : true;
                //return (bool)ViewState["FileTypeIsDisplay"];
            }
            set
            {
                ViewState["FileTypeIsDisplay"] = value;
            }
        }
        /// <summary>
        /// 上传后回调js
        /// </summary>
        public string CallBack
        {
            get
            {
                return (ViewState["CallBack"] == null) ? "null" : (string)ViewState["CallBack"];
            }
            set
            {
                ViewState["CallBack"] = value;
            }
        }
        /// <summary>
        /// FilesPath
        /// </summary>
        public string FilesPath
        {
            get
            {
                return (ViewState["upFilesPath"] == null) ? "" : (string)ViewState["upFilesPath"];
            }
            set
            {
                ViewState["upFilesPath"] = value;
            }
        }
        /// <summary>
        /// 对话框高度（默认:200px)
        /// </summary>
        public int Height
        {
            get
            {
                return (ViewState["Height"] == null) ? 200 : (int)ViewState["Height"];
            }
            set
            {
                ViewState["Height"] = value;
            }
        }
        /// <summary>
        ///  对话框宽度（默认:400px)
        /// </summary>
        public int Width
        {
            get
            {
                return (ViewState["Width"] == null) ? 400 : (int)ViewState["Width"];
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        public FileUploadConfig fileUploadConfig;

        public List<FileUploadInfo> FileUrl { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy(FunctionType);
            if (!FileTypeIsDisplay)
            {
                FileTypePanel.Visible = false;
            }
            if (IsPostBack)
            {
                FileUrl = JsonHelper.JsonDeserialize<List<FileUploadInfo>>("[" + txt_file.Value + "]");
                string updir = string.Empty;
                string tempupdir = string.Empty;

                if (fileUploadConfig.UsingTempSave)
                {
                    tempupdir = ConfigurationManager.AppSettings["tempdir"] + "/" + DateTime.Now.ToString("yy-MM");
                    tempupdir = Server.MapPath(tempupdir);
                    updir = fileUploadConfig.Root.Replace("\\", "/");
                    updir = Server.MapPath(updir);

                }

                for (int i = 0; i < FileUrl.Count; i++)
                {
                    FileUrl[i].BizUrl = CrypProvider.Decryptor(FileUrl[i].BizUrl);
                    //如果使用了临时文件，把文件从临时目录移到正式目录
                    if (fileUploadConfig.UsingTempSave)
                    {
                        MoveToUpDir(tempupdir, updir, FileUrl[i].BizUrl);
                    }
                    // FileUrl[i].BizUrl = fileUploadConfig.Root + "\\" + FileUrl[i].BizUrl;
                    FileUrl[i].BizUrl = FileUrl[i].BizUrl;
                    FileUrl[i].BizUrl = FileUrl[i].BizUrl.Replace("\\", "/");
                    FileUrl[i].FileSizeStr = CrypProvider.Decryptor(FileUrl[i].FileSizeStr);
                    //FileUrl[i].FileName = string.Empty;
                    FileUrl[i].FileSize = 0;
                    FileUrl[i].FileType = FileUrl[i].FileType;
                    FileUrl[i].FullUrl = string.Empty;
                }


                txt_file.Value = string.Empty;

            }
        }
        //把文件从临时目录移到正式目录
        protected void MoveToUpDir(string tempupdir, string updir, string BizUrl)
        {
            string path = Path.GetDirectoryName(updir + "\\" + BizUrl);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo fi = new FileInfo(tempupdir + "\\" + BizUrl);
            fi.MoveTo(updir + "\\" + BizUrl);

            //File.Copy(tempupdir + "\\" + BizUrl, updir + "\\" + BizUrl, true);
        }

        public string GetFileTypes(string[] arr)
        {
            string fileTypes = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                fileTypes += arr[i].Replace(".", "") + ",";
            }

            return fileTypes.TrimEnd(',');
        }

    }
}