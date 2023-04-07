using System;

namespace ETMS.Activity.Entity
{
    /// <summary>
    /// 按活动-组别-作品类型分组统计实体
    /// </summary>
    public partial class MarkingDetailStatic
    {
        public Guid AppraisalID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int ProductType { get; set; }
        public string TypeName { get; set; }
        public int SubmitNum { get; set; }
        public int MarkingNum { get; set; }
        public int NoMarkingNum { get { return (SubmitNum - MarkingNum); } }
    }
}
