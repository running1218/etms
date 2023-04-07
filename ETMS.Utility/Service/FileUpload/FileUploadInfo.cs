namespace ETMS.Utility.Service.FileUpload
{
    public class FileUploadInfo
    {
        /// <summary>
        /// 业务保持的Url
        /// </summary>
        public string BizUrl { get; set; }
        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullUrl { get; set; }

        /// <summary>
        /// 文件类型，如:.jpg,.rar
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件名如：readme
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件名如：readme
        /// </summary>
        public string FileNames { get; set; }
        /// <summary>
        /// 文件名如：readme
        /// </summary>
        public string FileOldName { get; set; }

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 文件大小(单位字节)
        /// </summary>
        public string FileSizeStr { get; set; }
    }
}
