using System.Configuration;
using MCS.Library.Configuration;

namespace University.Mooc.Security
{
    /// <summary>
    /// 和Passport有关的加密接口
    /// </summary>
    public class PassportEncryptionSettings : ConfigurationSection
    {
        private ITicketEncryption ticketEncryption = null;
        private IStringEncryption stringEncryption = null;

        /// <summary>
        /// 单点登录加密配置信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        public static PassportEncryptionSettings GetConfig()
        {
            PassportEncryptionSettings result =
                (PassportEncryptionSettings)ConfigurationManager.GetSection("autumn.security/passportEncryptionSettings");

            if (result == null)
                result = new PassportEncryptionSettings();

            return result;
        }

        /// <summary>
        /// Ticket加密器
        /// </summary>
        public ITicketEncryption TicketEncryption
        {
            get
            {
                if (this.ticketEncryption == null)
                    if (TypeFactories.ContainsKey("ticketEncryption"))
                        this.ticketEncryption = (ITicketEncryption)TypeFactories["ticketEncryption"].CreateInstance();
                    else
                        this.ticketEncryption = new TicketEncryption();

                return this.ticketEncryption;
            }
        }

        /// <summary>
        /// 字符串加密器
        /// </summary>
        public IStringEncryption StringEncryption
        {
            get
            {
                if (this.stringEncryption == null)
                    if (TypeFactories.ContainsKey("stringEncryption"))
                        this.stringEncryption = (IStringEncryption)TypeFactories["stringEncryption"].CreateInstance();
                    else
                        this.stringEncryption = new StringEncryption();

                return this.stringEncryption;
            }
        }

        [ConfigurationProperty("typeFactories", IsRequired = true)]
        private TypeConfigurationCollection TypeFactories
        {
            get
            {
                return (TypeConfigurationCollection)this["typeFactories"];
            }
        }
    }
}
