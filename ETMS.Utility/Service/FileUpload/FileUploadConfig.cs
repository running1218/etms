using System;

using Autumn.Expressions;
namespace ETMS.Utility.Service.FileUpload
{
    [Serializable]
    public class FileUploadConfig
    {
        public FileUploadConfig()
        {
            this.Root = "";
            this.UrlRoot = "";
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string[] FileTypes { get; set; }
        /// <summary>
        /// 最大的文件大小（单位：KB)
        /// </summary>
        public int MaxFileSize { get; set; }
        /// <summary>
        /// 最大的文件数量
        /// </summary>
        public int MaxFileCount { get; set; }
        /// <summary>
        /// 文件物理根路径
        /// </summary>
        public string Root { get; set; }
        /// <summary>
        /// 文件Url根路径
        /// </summary>
        public string UrlRoot { get; set; }
        /// <summary>
        /// 文件命名表达式
        /// </summary>
        public IExpression FileNameExpression { get; set; }
        /// <summary>
        /// 是否启用临时文件保存机制（默认不启用）
        /// 上传过程1：支持临时文件存储
        ///  优点：如果用户业务上未确认保存，则上传文件被以垃圾文件的方式处理
        ///  缺点：涉及文件多次拷贝，性能影响！
        ///  场景：用户没有对应的网络磁盘（即无法限制用户个人空间的大小），需防治垃圾数据！
        ///     步骤1、申请令牌
        ///     步骤2、上传文件（临时保存）
        ///     步骤3、确认保存（将临时数据Copy至正式文件服务器）
        /// 上传过程2：文件即时保存
        ///  优点：用户文件上传后立即存入正式文件服务器，一次文件保存操作
        ///  缺点：如果用户业务上未确认保存，则此上传文件即没有被引用，实际成为垃圾文件。
        ///  场景：用户有对应的个人存储空间（可限制大小），用户可以管理自己上传的所有资源，如果空间不足时，可删除之前的垃圾文件。
        ///     步骤1、申请令牌
        ///     步骤2、文件上传（即时保存至文件服务器）
        ///     步骤3、确认保存，仅返回此处上传的文件列表信息，供业务使用。
        /// </summary>
        public bool UsingTempSave { get; set; }
    }
}
