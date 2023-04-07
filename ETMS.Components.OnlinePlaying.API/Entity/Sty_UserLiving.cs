using System;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public class Sty_UserLiving 
    {
        public Guid UserLivingID { get; set; }
        public string UserID { get; set; }
        public string LivingID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
