using System;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public partial class CourseLaunchResult
    {
        public string code { get; set; }
        public CourseLaunchResultData data { get; set; }
    }

    [Serializable]
    public partial class CourseLaunchResultData
    {
        public string url { get; set; }
        public string protocol { get; set; }
        public string download { get; set; }
    }
}
