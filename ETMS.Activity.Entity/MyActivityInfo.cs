using System;

namespace ETMS.Activity.Entity
{
    /// <summary>
    /// 用户参与活动信息
    /// </summary>
    public class MyActivityInfo : Appraisal
    {
        public Guid SiginupID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public int RegionID { get; set; }
        public DateTime SiginupTime { get; set; }
        public string SiginupNo { get; set; }
        public string School { get; set; }
        public string GroupName { get; set; }
        public string RegionName { get; set; }
    }
}
