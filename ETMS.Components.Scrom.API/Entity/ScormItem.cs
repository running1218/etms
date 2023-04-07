using System;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// 表【sco_e_Item】
    /// </summary>
    [Serializable]
    public partial class ScormItem
    {
        public Guid ItemID { get; set; }
        public Guid CoursewareID { get; set; }
        public Guid OrgID { get; set; }
        public string ItemTitle { get; set; }
        public Guid ParentItemID { get; set; }
        public int SequenceNo { get; set; }
        public int IsVisible { get; set; }
        public string ItemIdentifier { get; set; }
        public Guid ResourceID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public int Delete { get; set; }
    }
}
