using System;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public partial class Sty_StudentOnlinePlaying
    {
        public string OnlinePlayingID { get; set; }
        public int StudentID { get; set; }
        public DateTime JoinTime { get; set; }
    }
}
