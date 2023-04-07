using System.Collections.Generic;

namespace ETMS.Components.OnlinePlaying
{
    public class TKRecordResult
    {
        /// <summary>
        ///  0：表示成功   4007 表示没有该教室 
        /// </summary>
        public int result { get; set; } 
        /// <summary>
        /// 一次直播，可能会有多次回放记录
        /// </summary>
        public List<TKRecordList> recordlist { get; set; }
    }

    public class TKRecordList
    {
        public string tempid { get; set; }
        /// <summary>
        /// 录制件 id
        /// </summary>
        public string recordid { get; set; }
        /// <summary>
        ///  教室号
        /// </summary>
        public string serial { get; set; }
        public string recordtitle { get; set; }
        public string recordpath { get; set; }
        public string starttime { get; set; }
        public string duration { get; set; }
        public string recordpwd { get; set; }
        public string ispublish { get; set; }
        public string recorddesc { get; set; }
        public string version { get; set; }
        public string state { get; set; }
        public string size { get; set; }
        /// <summary>
        /// 播放路径
        /// </summary>
        public string playpath { get; set; }
    }
}
