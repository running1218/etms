
using System;
using System.Configuration;
using MCS.Library.Configuration;
namespace University.Mooc.Security
{
    /// <summary>
    /// �����¼��������
    /// </summary>
    public sealed class PassportSignInSettings : ConfigurationSection
    {
        /// <summary>
        /// ��ȡ�����¼��������
        /// </summary>
        /// <returns>��֤������������Ϣ</returns>
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
        /// ȱʡ�Ĺ���ʱ��
        /// </summary>
        public TimeSpan DefaultTimeout
        {
            get
            {
                return TimeSpan.FromSeconds((double)DefaultTimeoutInt);
            }
        }

        /// <summary>
        /// ȱʡ�Ļ�������ʱ��
        /// </summary>
        public TimeSpan SlidingExpiration
        {
            get
            {
                return TimeSpan.FromSeconds((double)SlidingExpirationInt);
            }
        }
        /// <summary>
        /// �Ƿ�Windows������֤
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
        /// Windows��֤�����쳣ʱ���Ƿ��׳���������׳�������ʾȱʡ����֤ҳ��
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
        /// ��֤��Ϣ�ı��������Ƿ���Session��ʽ��
        /// </summary>
        public bool IsSessionBased
        {
            get
            {
                return DefaultTimeout.TotalSeconds == -2;
            }
        }


        ///// <summary>
        ///// Windows���ɻ���������ʹ�õ��򻷾����ã�Ҫ�����ó�����[value]�Ͷ�����[name]
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
        /// �Ƿ���ڻ�������ʱ��
        /// </summary>
        public bool HasSlidingExpiration
        {
            get
            {
                return SlidingExpiration != TimeSpan.Zero;
            }
        }

        /// <summary>
        /// ����Ticket��ʹ��
        /// </summary>
        public string RsaKeyValue
        {
            get
            {
                return RsaKeyValueElement.Value;
            }
        }

        #region ˽������
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
        ///  RSA��Կ����������rsaKeyContainerName�Ƕ�ѡһ
        /// ʹ�ó�������̨�����£����㹫Կ��˽Կ������
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
        /// RSA��Կ���KeyContainerName����������rsaKeyValue�Ƕ�ѡһ
        /// ʹ�ó�����ͬһ̨�����£������û�������Կ��˽�У��洢������ϵͳ��
        /// </summary>
        [ConfigurationProperty("rsaKeyContainerName", DefaultValue = "")]
        public string RSAKeyContainerName
        {
            get
            {
                return (string)this["rsaKeyContainerName"];
            }
        }

        #endregion ˽������
    }

    /// <summary>
    /// Rsa����������Ϣ
    /// </summary>
    public class SignInRsaKeyValueConfigurationElement : ConfigurationElement
    {
        private static string sValue = string.Empty;

        /// <summary>
        /// Rsa���õ�stringֵ
        /// </summary>
        public string Value
        {
            get
            {
                return SignInRsaKeyValueConfigurationElement.sValue;
            }
        }
        /// <summary>
        /// ����Rsa������Ϣ
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
