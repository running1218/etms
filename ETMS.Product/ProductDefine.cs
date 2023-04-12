//#define PRODUCT_SINGLE      //单机构版本
#define PRODUCT_MULTI     //多机构（机构数不限）
//#define PRODUCT_MULTI_10  //多机构（机构数10）
//#define PRODUCT_MULTI_50  //多机构（机构数50）


//#define PRODUCT_CONFIGLEVEL_LOWER //产品-低配
//#define PRODUCT_CONFIGLEVEL_STANDARD //产品-标配
#define PRODUCT_CONFIGLEVEL_HIGHEST //产品-高配
using System;

namespace ETMS.Product
{
    /// <summary>
    /// 产品定义
    /// </summary>
    public class ProductDefine
    {
#if PRODUCT_CONFIGLEVEL_LOWER
        /// <summary>
        /// 产品配置级别
        /// </summary>
        public const ProductConfigLevel ConfigLevel = ProductConfigLevel.Lower;

#elif PRODUCT_CONFIGLEVEL_STANDARD
        /// <summary>
        /// 产品配置级别
        /// </summary>
        public const ProductConfigLevel ConfigLevel = ProductConfigLevel.Standard;

#elif PRODUCT_CONFIGLEVEL_HIGHEST
        /// <summary>
        /// 产品配置级别
        /// </summary>
        public const ProductConfigLevel ConfigLevel = ProductConfigLevel.Highest;

#endif

        /// <summary>
        /// 产品-名称
        /// </summary>
        public static string ProductName
        {
            get
            {
                return Resource.Product_Name;
            }
        }

        /// <summary>
        /// 产品-版本号
        /// </summary>
        public static string VersionNumber
        {
            get
            {
                return Resource.Product_VersionNember;
            }
        }

        /// <summary>
        /// 产品-版权
        /// </summary>
        public static string Copyright
        {
            get
            {
                return string.Format(Resource.Product_Copyright, DateTime.Now.ToString("yyyy"));
            }
        }

        /// <summary>
        /// 产品-是否是单机构版本
        /// </summary>
        public static bool IsSingleOrganizationVersion
        {
            get
            {
                return SupportMaxOrganizationCount == 1;
            }
        }
#if PRODUCT_SINGLE
        /// <summary>
        /// 产品-授权的最大机构数量
        /// </summary>
        public const int SupportMaxOrganizationCount = 1;

        /// <summary>
        /// 版本信息
        /// </summary>
        public static string VersionInfo
        {
            get
            {
                return string.Format("{0}，版本号:{1}", Resource.Product_SingleTitle, VersionNumber);
            }
        }
#elif  PRODUCT_MULTI
        /// <summary>
        /// 产品-授权的最大机构数量
        /// </summary>
        public const int SupportMaxOrganizationCount = int.MaxValue-1;

        /// <summary>
        /// 版本信息
        /// </summary>
        public static string VersionInfo
        {
            get
            {
                return string.Format("{0}，版本号:{1}", Resource.Product_MulitiTitle, VersionNumber);
            }
        }
#elif  PRODUCT_MULTI_10
         /// <summary>
        /// 产品-授权的最大机构数量
        /// </summary>
        public const int SupportMaxOrganizationCount = 10;
         
        /// <summary>
        /// 版本信息
        /// </summary>
        public static string VersionInfo
        {
            get
            {
                return string.Format("{0}（授权机构数{1}），版本号:{2}", Resource.Product_MulitiTitle,SupportMaxOrganizationCount, VersionNumber);
            }
        }
#elif  PRODUCT_MULTI_50
         /// <summary>
        /// 产品-授权的最大机构数量
        /// </summary>
        public const int SupportMaxOrganizationCount = 50;
         
        /// <summary>
        /// 版本信息
        /// </summary>
        public static string VersionInfo
        {
            get
            {
                   return string.Format("{0}（授权机构数{1}），版本号:{2}", Resource.Product_MulitiTitle,SupportMaxOrganizationCount, VersionNumber);
            }
        }
#endif
    }
}
