using System;

namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 文件上传功能类型
    /// 应用中各类功能上传均在此定义
    /// 具体上传策略配置请参考“config/Service-plugins-config.xml"
    /// </summary>
    [Serializable]
    public abstract class FileUploadFunctionType
    {
        /// <summary>
        /// 用户头像上传
        /// </summary>
        public const string UserIcon = "UserIcon"; 
        public const string CourseLogo  = "CourseLogo";
        public const string OrgLogo = "OrgLogo";
        public const string ItemLogo = "ItemLogo";
        public const string MediaLogo = "MediaLogo";
        public const string BulletinImage = "BulletinImage";
        public const string BannerImage = "BannerImage";
       
    }
}
