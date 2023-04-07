using System;

namespace ETMS.Components.Scrom.API.Entity
{
    [Serializable]
    public partial class Catalog
    {
        // 课件学习目录
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
        public string ResourceHref { get; set; }
        public string StatusCode { get; set; }
    }
}
