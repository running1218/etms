
using System;
using System.Configuration;
//using MCS.Library.Principal;
using MCS.Library.Configuration;

namespace University.Mooc.Security
{
    /// <summary>
    /// �����¼�ͻ���Ӧ������
    /// </summary>
    public sealed class PassportClientSettings : ConfigurationSection
    {
        /// <summary>
        /// �����¼�ͻ���Ӧ��������Ϣ
        /// </summary>
        /// <returns>PassportClientSettings����</returns>
        public static PassportClientSettings GetConfig()
        {
            PassportClientSettings result =
                (PassportClientSettings)ConfigurationManager.GetSection("autumn.security/passportClientSettings");

            ConfigurationExceptionHelper.CheckSectionNotNull(result, "autumn.security/passportClientSettings");

            return result;
        }
        /// <summary>
        /// Ӧ������Cookie�洢����Ĭ��Ϊ�ձ�ʶ��ǰ��
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
        /// Ӧ�õľ��Թ���ʱ��
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
        /// Ӧ�õĻ�������ʱ��
        /// </summary>
        public TimeSpan AppSlidingExpiration
        {
            get
            {
                return TimeSpan.FromSeconds(AppSlidingExpirationSeconds);
            }
        }
        /// <summary>
        /// Ӧ�õ�ID��
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
        /// �Ƿ񻬶�����
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
        /// ע���ص���ַ
        /// </summary>
        public Uri LogOffCallBackUrl
        {
            get
            {
                //�ڲ����Ӷ�Area�߼�����Ӧ�õ�֧��
                Uri defaultUri = Paths["logOffCallBackUrl"].Uri;
                string url = defaultUri.ToString();
                url = url.Insert(url.LastIndexOf('/'), ("/" + AppID));
                return new Uri(url, UriKind.RelativeOrAbsolute);
            }
        }
        /// <summary>
        /// ��֤��ַ
        /// </summary>
        public Uri SignInUrl
        {
            get
            {
                return Paths["signInUrl"].Uri;
            }
        }
        /// <summary>
        /// Ajax��ʽ��֤��ַ
        /// </summary>
        public Uri AjaxSignInUrl
        {
            get
            {
                return Paths["ajaxSignInUrl"] == null ? null : Paths["ajaxSignInUrl"].Uri;
            }
        }
        /// <summary>
        /// ��վ�Զ������֤��ַ
        /// </summary>
        public Uri CustomSignInUrl
        {
            get
            {
                return Paths["customSignInUrl"] == null ? null : Paths["customSignInUrl"].Uri;
            }
        }
        /// <summary>
        /// ע����ַ
        /// </summary>
        public Uri LogOffUrl
        {
            get
            {
                return Paths["logOffUrl"].Uri;
            }
        }

        /// <summary>
        /// �Ƿ񼯳���Ȩ��Ĭ�ϣ�false�����ṩ��֤���ɣ����ṩ��Ȩ���ɣ�
        /// ���������Ȩ�������á�/OguPermissionSettings/AppAdminServiceAddressV2������
        /// �޸ģ�Ѧ����
        /// ԭ���ṩ��Ȩ���ܼ���
        /// ���ڣ�2011-5-30
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
        /// �Ƿ񻺴��û���ɫ�б�Ĭ�ϣ����棩
        /// ����λ�ã�ticket�У���cookie�洢 
        /// �޸ģ�Ѧ����
        /// ԭ�򣺿ɿ����û���ɫ�б��Ƿ񻺴棬ͨ��������ǻ���ģ������ǵ�����Ӧ���У�ͬһ��
        /// �û����ܽ�ɫ�ܶ࣬����cookie�Ĵ洢��Χ������Ҫ�رմ˹��ܡ�
        /// ���ڣ�2011-5-30
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
        /// �����ѡ��ҳ���ַ
        /// �����ǰӦ������Զ�����ģ����û�δѡ�����ʱ����ת����
        /// �޸ģ�Ѧ����
        /// ԭ���ṩ�����ѡ��֧��
        /// ���ڣ�2011-5-30
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
        /// RSA��Կ����������rsaKeyContainerName�Ƕ�ѡһ
        /// ʹ�ó�������̨�����£����㹫Կ��˽Կ������
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
    /// Ӧ��Rsa����
    /// </summary>
    public class ClientRsaKeyValueConfigurationElement : ConfigurationElement
    {
        private static string sValue = string.Empty;

        /// <summary>
        /// ���õ�string ֵ
        /// </summary>
        public string Value
        {
            get
            {
                return ClientRsaKeyValueConfigurationElement.sValue;
            }
        }
        /// <summary>
        /// ����������Ϣ
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
