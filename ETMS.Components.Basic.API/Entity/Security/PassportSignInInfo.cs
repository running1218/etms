using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 统一用户认证实体定义
    /// </summary>
    [Serializable]
    public class PassportSignInInfo
    {
        public string SIGNIN_ID { get; set; }
        public int SORT_ID { get; set; }
        public DateTime SIGNIN_TIME { get; set; }
        public DateTime SIGNIN_TIMEOUT { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string DOMAIN { get; set; }
        public string AUTHENTICATE_SERVER { get; set; }
    }
}
