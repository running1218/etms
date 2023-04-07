using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 应用认证票据实体定义
    /// </summary>
    [Serializable]
    public class PassportTicket 
    {
        public string APP_SIGNIN_ID { get; set; }
        public string SIGNIN_ID { get; set; }
        public string APP_ID { get; set; }
        public DateTime APP_SIGNIN_TIME { get; set; }
        public DateTime APP_SIGNIN_TIMEOUT { get; set; }
        public string APP_SIGNIN_URL { get; set; }
        public string APP_SIGNIN_IP { get; set; }
        public string APP_LOGOFF_URL { get; set; }
        public string DEL_FLAG { get; set; }
    }
}
