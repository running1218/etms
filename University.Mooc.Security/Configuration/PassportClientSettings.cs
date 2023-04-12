
using System;
using System.Configuration;
//using MCS.Library.Principal;
using MCS.Library.Configuration;

namespace University.Mooc.Security
{
    /// <summary>
    /// 单点登录客户端应用配置
    /// </summary>
    public sealed class PassportClientSettings : ConfigurationSection
    {
        /// <summary>
        /// 单点登录客户端应用配置信息
        /// </summary>
        /// <returns>PassportClientSettings对象</returns>
        public static PassportClientSettings GetConfig()
        {
            PassportClientSettings result =
                (PassportClientSettings)ConfigurationManager.GetSection("autumn.security/passportClientSettings");

            ConfigurationExceptionHelper.CheckSectionNotNull(result, "autumn.security/passportClientSettings");

            return result;
        }
        /// <summary>
        /// 应用令牌Cookie存储的域，默认为空标识当前域
        /// </summary>
        [ConfigurationProperty("ticketCookieDomain")]
        public string TicketCookieDomain
        {
            get
            {
                return (string)this["ticketCookieDomain"];
            }
        }
        /// <summary>
        /// 应用的绝对过期时间
        /// </summary>
        [ConfigurationProperty("appSignInTimeout", DefaultValue = -2)]
        public int AppSignInTimeout
        {
            get
            {
                return (int)this["appSignInTimeout"];
            }
        }

        [ConfigurationProperty("appSlidingExpiration", DefaultValue = 0)]
        private int AppSlidingExpirationSeconds
        {
            get
            {
                return (int)this["appSlidingExpiration"];
            }
        }
        /// <summary>
        /// 应用的滑动过期时间
        /// </summary>
        public TimeSpan AppSlidingExpiration
        {
            get
            {
                return TimeSpan.FromSeconds(AppSlidingExpirationSeconds);
            }
        }
        /// <summary>
        /// 应用的ID号
        /// </summary>
        [ConfigurationProperty("appID", IsRequired = true)]
        public string AppID
        {
            get
            {
                return (string)this["appID"];
            }
        }
        /// <summary>
        /// 是否滑动过期
        /// </summary>
        public bool HasSlidingExpiration
        {
            get
            {
                return AppSlidingExpiration != TimeSpan.Zero;
            }
        }
        /// <summary>
        /// Rsa key
        /// </summary>
        public string RsaKeyValue
        {
            get
            {
                return RsaKeyValueElement.Value;
            }
        }
        /// <summary>
        /// 注销回调地址
        /// </summary>
        public Uri LogOffCallBackUrl
        {
            get
            {
                //内部增加对Area逻辑划分应用的支持
                Uri defaultUri = Paths["logOffCallBackUrl"].Uri;
                string url = defaultUri.ToString();
                url = url.Insert(url.LastIndexOf('/'), ("/" + AppID));
                return new Uri(url, UriKind.RelativeOrAbsolute);
            }
        }
        /// <summary>
        /// 认证地址
        /// </summary>
        public Uri SignInUrl
        {
            get
            {
                return Paths["signInUrl"].Uri;
            }
        }
        /// <summary>
        /// Ajax方式认证地址
        /// </summary>
        public Uri AjaxSignInUrl
        {
            get
            {
                return Paths["ajaxSignInUrl"] == null ? null : Paths["ajaxSignInUrl"].Uri;
            }
        }
        /// <summary>
        /// 网站自定义的认证地址
        /// </summary>
        public Uri CustomSignInUrl
        {
            get
            {
                return Paths["customSignInUrl"] == null ? null : Paths["customSignInUrl"].Uri;
            }
        }
        /// <summary>
        /// 注销地址
        /// </summary>
        public Uri LogOffUrl
        {
            get
            {
                return Paths["logOffUrl"].Uri;
            }
        }

        /// <summary>
        /// 是否集成授权（默认：false，仅提供认证集成，不提供授权集成）
        /// 如果集成授权，请设置“/OguPermissionSettings/AppAdminServiceAddressV2”服务！
        /// 修改：薛永波
        /// 原因：提供授权功能集成
        /// 日期：2011-5-30
        /// </summary>
        [ConfigurationProperty("isIntegrationAuthorization", DefaultValue = false)]
        public bool IsIntegrationAuthorization
        {
            get
            {
                return (bool)this["isIntegrationAuthorization"];
            }
        }

        /// <summary>
        /// 是否缓存用户角色列表（默认：缓存）
        /// 缓存位置：ticket中，以cookie存储 
        /// 修改：薛永波
        /// 原因：可控制用户角色列表是否缓存，通常情况下是缓存的，但考虑到部分应用中，同一个
        /// 用户可能角色很多，超出cookie的存储范围，则需要关闭此功能。
        /// 日期：2011-5-30
        /// </summary>
        [ConfigurationProperty("isCacheUserRole", DefaultValue = false)]
        public bool isCacheUserRole
        {
            get
            {
                return (bool)this["isCacheUserRole"];
            }
        }

        /// <summary>
        /// 多机构选择页面地址
        /// 如果当前应用是针对多机构的，当用户未选择机构时，跳转至此
        /// 修改：薛永波
        /// 原因：提供多机构选择支持
        /// 日期：2011-5-30
        /// </summary>
        [ConfigurationProperty("userChooseOrganizationUrl", DefaultValue = "")]
        public string UserChooseOrganizationUrl
        {
            get
            {
                return (string)this["userChooseOrganizationUrl"];
            }
        }

        [ConfigurationProperty("typeFactories", IsRequired = false)]
        private TypeConfigurationCollection TypeFactories
        {
            get
            {
                return (TypeConfigurationCollection)this["typeFactories"];
            }
        }

        /// <summary>
        /// RSA密钥，此配置与rsaKeyContainerName是二选一
        /// 使用场景：多台机器下，方便公钥，私钥的配置
        /// </summary>
        [ConfigurationProperty("rsaKeyValue", IsRequired = false)]
        private ClientRsaKeyValueConfigurationElement RsaKeyValueElement
        {
            get
            {
                return (ClientRsaKeyValueConfigurationElement)this["rsaKeyValue"];
            }
        }

        /// <summary>
        /// RSA密钥存放KeyContainerName，此配置与rsaKeyValue是二选一
        /// 使用场景：同一台机器下，避免用户看到公钥、私有，存储到操作系统中
        /// </summary>
        [ConfigurationProperty("rsaKeyContainerName", DefaultValue = "")]
        public string RSAKeyContainerName
        {
            get
            {
                return (string)this["rsaKeyContainerName"];
            }
        }

        [ConfigurationProperty("paths", IsRequired = true)]
        private UriConfigurationCollection Paths
        {
            get
            {
                return (UriConfigurationCollection)this["paths"];
            }
        }
    }

    /// <summary>
    /// 应用Rsa配置
    /// </summary>
    public class ClientRsaKeyValueConfigurationElement : ConfigurationElement
    {
        private static string sValue = string.Empty;

        /// <summary>
        /// 配置的string 值
        /// </summary>
        public string Value
        {
            get
            {
                return ClientRsaKeyValueConfigurationElement.sValue;
            }
        }
        /// <summary>
        /// 读入配置信息
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <param name="serializeCollectionKey"></param>
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            lock (typeof(ClientRsaKeyValueConfigurationElement))
            {
                if (ClientRsaKeyValueConfigurationElement.sValue == string.Empty)
                    ClientRsaKeyValueConfigurationElement.sValue = reader.ReadOuterXml();
                else
                    reader.ReadOuterXml();
            }
        }
    }
}
