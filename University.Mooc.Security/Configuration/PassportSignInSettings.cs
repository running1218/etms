
using System;
using System.Configuration;
using MCS.Library.Configuration;
namespace University.Mooc.Security
{
    /// <summary>
    /// 单点登录服务配置
    /// </summary>
    public sealed class PassportSignInSettings : ConfigurationSection
    {
        /// <summary>
        /// 读取单点登录服务配置
        /// </summary>
        /// <returns>认证服务器配置信息</returns>
        /// <remarks>
        /// </remarks>
        public static PassportSignInSettings GetConfig()
        {
            PassportSignInSettings result =
                (PassportSignInSettings)ConfigurationManager.GetSection("University.Mooc.Security/passportSignInSettings");

            ConfigurationExceptionHelper.CheckSectionNotNull(result, "University.Mooc.Security/passportSignInSettings");

            return result;
        }

        /// <summary>
        /// 缺省的过期时间
        /// </summary>
        public TimeSpan DefaultTimeout
        {
            get
            {
                return TimeSpan.FromSeconds((double)DefaultTimeoutInt);
            }
        }

        /// <summary>
        /// 缺省的滑动过期时间
        /// </summary>
        public TimeSpan SlidingExpiration
        {
            get
            {
                return TimeSpan.FromSeconds((double)SlidingExpirationInt);
            }
        }
        /// <summary>
        /// 是否Windows集成认证
        /// </summary>
        [ConfigurationProperty("isWindowsIntegrated", DefaultValue = false)]
        public bool IsWindowsIntegrated
        {
            get
            {
                return (bool)this["isWindowsIntegrated"];
            }
        }

        /// <summary>
        /// Windows认证出现异常时，是否抛出，如果不抛出，会显示缺省的认证页面
        /// </summary>
        [ConfigurationProperty("throwWindowsSignInError", DefaultValue = true)]
        public bool ThrowWindowsSignInError
        {
            get
            {
                return (bool)this["throwWindowsSignInError"];
            }
        }

        /// <summary>
        /// 认证信息的保存寿命是否是Session方式的
        /// </summary>
        public bool IsSessionBased
        {
            get
            {
                return DefaultTimeout.TotalSeconds == -2;
            }
        }


        ///// <summary>
        ///// Windows集成环境下允许使用的域环境设置，要求设置长名称[value]和短名称[name]
        ///// </summary>
        //[ConfigurationProperty("domains")]
        //public NameValueConfigurationCollection Domains
        //{
        //    get
        //    {
        //        return (NameValueConfigurationCollection)this["domains"];
        //    }
        //}

        /// <summary>
        /// 是否存在滑动过期时间
        /// </summary>
        public bool HasSlidingExpiration
        {
            get
            {
                return SlidingExpiration != TimeSpan.Zero;
            }
        }

        /// <summary>
        /// 加密Ticket所使用
        /// </summary>
        public string RsaKeyValue
        {
            get
            {
                return RsaKeyValueElement.Value;
            }
        }

        #region 私有属性
        [ConfigurationProperty("slidingExpiration", DefaultValue = 0)]
        private int SlidingExpirationInt
        {
            get
            {
                return (int)this["slidingExpiration"];
            }
        }

        [ConfigurationProperty("defaultTimeout", DefaultValue = -2)]
        private int DefaultTimeoutInt
        {
            get
            {
                return (int)this["defaultTimeout"];
            }
        }

        /// <summary>
        ///  RSA密钥，此配置与rsaKeyContainerName是二选一
        /// 使用场景：多台机器下，方便公钥，私钥的配置
        /// </summary>
        [ConfigurationProperty("rsaKeyValue", IsRequired = false)]
        private SignInRsaKeyValueConfigurationElement RsaKeyValueElement
        {
            get
            {
                return (SignInRsaKeyValueConfigurationElement)this["rsaKeyValue"];
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

        #endregion 私有属性
    }

    /// <summary>
    /// Rsa加密配置信息
    /// </summary>
    public class SignInRsaKeyValueConfigurationElement : ConfigurationElement
    {
        private static string sValue = string.Empty;

        /// <summary>
        /// Rsa配置的string值
        /// </summary>
        public string Value
        {
            get
            {
                return SignInRsaKeyValueConfigurationElement.sValue;
            }
        }
        /// <summary>
        /// 读入Rsa配置信息
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <param name="serializeCollectionKey"></param>
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            lock (typeof(SignInRsaKeyValueConfigurationElement))
            {
                if (SignInRsaKeyValueConfigurationElement.sValue == string.Empty)
                    SignInRsaKeyValueConfigurationElement.sValue = reader.ReadOuterXml();
                else
                    reader.ReadOuterXml();
            }
        }
    }
}
