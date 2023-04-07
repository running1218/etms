using System;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public partial class CourseAccessResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public CourseAccessResultData data { get; set; }
    }

    [Serializable]
    public partial class CourseAccessResultData
    {
        public string access_key { get; set; }
        public string access_token { get; set; }
        public string liveUrl { get; set; }
        public string playbackUrl { get; set; }
    }
}
