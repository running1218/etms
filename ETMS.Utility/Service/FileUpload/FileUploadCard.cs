using System;
using System.Collections.Generic;

namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 上传文件定义
    /// </summary>
    public class UploadFileDefine
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
        /// 文件大小(单位字节)
        /// </summary>
        public int FileSize { get; set; }

    }
    /// <summary>
    /// 文件上传口令卡
    /// </summary>
    [Serializable]
    public class FileUploadCard
    {
        /// <summary>
        /// 口令卡ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 功能配置名称
        /// </summary>
        public string FunctionType { get; set; }
        /// <summary>
        /// 上传文件路径列表
        /// </summary>
        public List<string> Files { get; set; }

        /// <summary>
        /// 包含文件详情的列表
        /// </summary>
        public List<UploadFileDefine> FileDetails { get; set; }
    }
}
