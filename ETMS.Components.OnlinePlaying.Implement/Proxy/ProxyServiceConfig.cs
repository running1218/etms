namespace ETMS.Components.OnlinePlaying.Implement.BLL
{
    public class ProxyServiceConfig
    {
        public static string ProxyUrl
        {
            get
            {
                return ETMS.AppContext.ApplicationContext.Current.AppSettings["OnlinePlayingServiceUrl"] ?? string.Empty;
            }
        }
        public static string OnlinePlayingUser
        {
            get
            {
                return ETMS.AppContext.ApplicationContext.Current.AppSettings["OnlinePlayingUser"] ?? string.Empty;
            }
        }
        public static string OnlinePlayingPassword
        {
            get
            {
                return ETMS.AppContext.ApplicationContext.Current.AppSettings["OnlinePlayingPassword"] ?? string.Empty;
            }
        }
        public static string OnlinePlayingSite
        {
            get
            {
                return ETMS.AppContext.ApplicationContext.Current.AppSettings["OnlinePlayingSite"] ?? string.Empty;
            }
        }
    }
}
