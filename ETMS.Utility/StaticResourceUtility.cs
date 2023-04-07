using System;
using System.Collections.Generic;
using System.Web;

using System.Configuration;
using ETMS.Utility.Service.FileUpload;
namespace ETMS.Utility
{
    /// <summary>
    /// 静态资源配置信息
    /// 要求：appSettings中进行配置
    /// </summary>
    public static class StaticResourceUtility
    {

        #region 网站基本元素，css、js、图片、html、flash、media

        private static string DEFAULT_ROOT = "~/";
        private static string _ROOT;
        /// <summary>
        /// 静态资源配置根
        /// 可指定相对或绝对路径，如：~或http://resouce.com.cn
        /// </summary>
        public static string Root
        {
            get
            {
                if (string.IsNullOrEmpty(_ROOT))
                {
                    _ROOT = (ConfigurationManager.AppSettings["StaticResourceRoot"] ?? DEFAULT_ROOT);
                    if (_ROOT.StartsWith("~"))
                    {
                        _ROOT = _ROOT.Replace("~", HttpContext.Current.Request.ApplicationPath).Replace("//", "/");
                    }
                }
                return _ROOT;
            }
        }
        /// <summary>
        /// js脚本根路径
        /// </summary>
        public static string Root_JS
        {
            get
            {
                return string.Format("{0}/js", Root);
            }
        }
        /// <summary>
        /// 图片根路径
        /// </summary>
        public static string Root_Image
        {
            get
            {
                return string.Format("{0}/images", Root);
            }
        }
        /// <summary>
        /// 样式表根路径
        /// </summary>
        public static string Root_CSS
        {
            get
            {
                return string.Format("{0}/css", Root);
            }
        }
        /// <summary>
        /// 静态HTML根路径
        /// </summary>
        public static string Root_HTML
        {
            get
            {
                return string.Format("{0}/html", Root);
            }
        }
        /// <summary>
        /// 影音根路径
        /// </summary>
        public static string Root_Media
        {
            get
            {
                return string.Format("{0}/media", Root);
            }
        }

        /// <summary>
        /// flash根路径
        /// </summary>
        public static string Root_Flash
        {
            get
            {
                return string.Format("{0}/flash", Root);
            }
        }

        /// <summary>
        /// 获取js资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径如："base/jquery.js"</param>
        /// <returns></returns>
        public static string GetJsFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_JS, resoucePath);
        }

        /// <summary>
        /// 获取Css资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径如："default/base.css"</param>
        /// <returns></returns>
        public static string GetCssFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_CSS, resoucePath);
        }
        /// <summary>
        /// 获取Image资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径如："UI/head.jpg"</param>
        /// <returns></returns>
        public static string GetImageFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_Image, resoucePath);
        }

        /// <summary>
        /// 获取Html资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径如："help/index.html"</param>
        /// <returns></returns>
        public static string GetHtmlFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_HTML, resoucePath);
        }
        /// <summary>
        /// 获取Media资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径如："MP3/1.MP3"</param>
        /// <returns></returns>
        public static string GetMediaFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_Media, resoucePath);
        }

        /// <summary>
        /// 获取Flash资源全路径
        /// </summary>
        /// <param name="resoucePath">资源相对路径</param>
        /// <returns></returns>
        public static string GetFlashFullPath(string resoucePath)
        {
            return string.Format("{0}/{1}", Root_Flash, resoucePath);
        }
        #endregion

        #region 用户头像
        /// <summary>
        /// 默认用户头像
        /// </summary>
        private static string DefaultUserImage = "user.jpg";

        /// <summary>
        /// 获取用户头像全路径
        /// 增加默认头像逻辑
        /// </summary>
        /// <param name="imagePath">用户头像相对路径</param>
        /// <returns></returns>
        public static string GetUserImageFullPath(string imagePath)
        {
            return GetUserImageFullPath(imagePath, 0);
        }


        /// <summary>
        /// 获取用户头像全路径
        /// 增加默认图片逻辑
        /// 如果size小于等于0，则返回原始图！
        /// </summary>
        /// <param name="imagePath">用户头像相对路径</param>
        /// <param name="size">缩略图大小</param>
        /// <returns></returns>
        public static string GetUserImageFullPath(string imagePath, int size)
        {
            //如果未指定用户头像，则采用默认用户头像。
            if (string.IsNullOrEmpty(imagePath))
            {
                if (size <= 0)
                {
                    return GetImageFullPath(string.Format("Default/{0}", DefaultUserImage));
                }
                else
                {
                    string imageType = System.IO.Path.GetExtension(DefaultUserImage);
                    return GetImageFullPath(string.Format("Default/{0}_{1}x{1}{2}", DefaultUserImage, size, imageType));
                }
            }
            imagePath = imagePath.Replace(@"\", "/");
            if (size <= 0)
            {
                return string.Format("{0}/{1}", GetImagesUrlRoot(FileUploadFunctionType.UserIcon), imagePath);
            }
            else
            {
                string imageType = System.IO.Path.GetExtension(imagePath);
                string tmpstr = string.Format("{0}/{1}_{2}x{2}{3}", GetImagesUrlRoot(FileUploadFunctionType.UserIcon), imagePath, size, imageType);
                return tmpstr;
            }

        }
        #endregion

        #region 机构Logo
        /// <summary>
        /// 默认机构
        /// </summary>
        private static string DefaultOrgLogoImage = "logo.gif";

        /// <summary>
        /// 获取机构Logo全路径
        /// 增加默认机构Logo逻辑
        /// </summary>
        /// <param name="imagePath">机构Logo相对路径</param>
        /// <returns></returns>
        public static string GetOrgLogoFullPath(string imagePath)
        {
            return GetOrgLogoFullPath(imagePath, 0);
        }


        /// <summary>
        /// 获取机构Logo全路径
        /// 增加默认图片逻辑
        /// 如果size小于等于0，则返回原始图！
        /// </summary>
        /// <param name="imagePath">机构Logo相对路径</param>
        /// <param name="size">缩略图大小</param>
        /// <returns></returns>
        public static string GetOrgLogoFullPath(string imagePath, int size)
        {
            //如果未指定用户头像，则采用默认用户头像。
            if (string.IsNullOrEmpty(imagePath))
            {
                if (size <= 0)
                {
                    return GetImageFullPath(string.Format("Default/{0}", DefaultOrgLogoImage));
                }
                else
                {
                    string imageType = System.IO.Path.GetExtension(DefaultUserImage);
                    return GetImageFullPath(string.Format("Default/{0}_{1}x{1}{2}", DefaultUserImage, size, imageType));
                }
            }
            imagePath = imagePath.Replace(@"\", "/");
            if (size <= 0)
            {
                return string.Format("{0}/{1}", GetImagesUrlRoot("OrgLogo"), imagePath);
            }
            else
            {
                string imageType = System.IO.Path.GetExtension(imagePath);
                string tmpstr = string.Format("{0}/{1}_{2}x{2}{3}", GetImagesUrlRoot("OrgLogo"), imagePath, size, imageType);
                return tmpstr;
            }

        }
        #endregion

        #region 通用功能资源引用

        private static IDictionary<string, string> ImagesUrlRoots = new Dictionary<string, string>();
        private static IDictionary<string, string> ImagesRoots = new Dictionary<string, string>();
        public static string GetImagesUrlRoot(string functionType)
        {
            if (!ImagesUrlRoots.ContainsKey(functionType))
            {
                ImagesUrlRoots[functionType] = ETMS.Utility.Service.ServiceRepository.FileUploadStrategyService.GetStrategy(functionType).UrlRoot;
            }
            return ImagesUrlRoots[functionType];
        }

        /// <summary>
        /// 根据文件类型返回全路径
        /// </summary>
        /// <param name="type">功能类型</param>
        /// <param name="filePath">文件相对路径</param>
        /// <returns></returns>
        public static string GetFullPathByFileType(string type, string filePath)
        {
            string imageUrl = "";
            switch (type)
            {
                case FileUploadFunctionType.UserIcon:
                    imageUrl = GetUserImageFullPath(filePath);
                    break;
                case FileUploadFunctionType.OrgLogo:
                    imageUrl = GetOrgLogoFullPath(filePath);
                    break;
                default:
                    if (string.IsNullOrEmpty(filePath) || filePath.IndexOf(".") != -1)
                    {
                        imageUrl = string.Format("{0}/{1}", GetImagesUrlRoot(type), filePath);
                    }
                    else//针对富文本编辑器中浏览“我的资源”
                    {
                        imageUrl = string.Format("{0}/{1}/{2}", GetImagesUrlRoot(type), filePath, type);
                    }
                    break;
            }
            return imageUrl.Replace(@"\", "/");
        }

        /// <summary>
        /// 返回文件的物理路径
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFullRootPathByFileType(string functionType, string filePath)
        {
            return GetRootPathByFileType(functionType) + @"\" + filePath.Trim('/').Replace("/", @"\");
        }

        /// <summary>
        /// 根据文件类型返回根路径
        /// </summary>
        /// <param name="type">功能类型</param>
        /// <returns></returns>
        public static string GetRootPathByFileType(string functionType)
        {
            if (!ImagesRoots.ContainsKey(functionType))
            {
                ImagesRoots[functionType] = ETMS.Utility.Service.ServiceRepository.FileUploadStrategyService.GetStrategy(functionType).Root;
            }
            return ImagesRoots[functionType];
        }

        /// <summary>
        /// 根据文件类型返回用户根路径
        /// </summary>
        /// <param name="type">功能类型</param>
        /// <returns></returns>
        public static string GetUserRootPathByFileType(string type, Guid userID)
        {
            ////针对富文本编辑器中浏览“我的资源”
            //要求：GetRootPathByFileType(type)=d:\ipllapp\Autumn.Store\Autumn.Store.Web\Content\Upload
            //返回:d:\ipllapp\Autumn.Store\Autumn.Store.Web\Content\Upload\{userID}\{type}
            return string.Format(@"{0}\{1}\{2}", GetRootPathByFileType(type), userID.ToString("n"), type);
        }
        #endregion

    }
}