using System;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// 表【sco_e_Resource】
    /// </summary>
    [Serializable]
    public partial class ScormResource
    {
        public Guid ResourceID { get; set; }
        public string ScormTypeCode { get; set; }
        public string ResourceName { get; set; }
        public string ResourceHref { get; set; }
        public string Type { get; set; }
        public string Resourceidentifier { get; set; }
        public Guid CoursewareID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
