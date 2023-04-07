using System;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// 表【sco_e_Organization】
    /// </summary>
    [Serializable]
    public partial class ScormOrganization
    {
        public Guid OrgID { get; set; }
        public Guid CourseWareID { get; set; }
        public string OrgTitle { get; set; }
        public string StructureCode { get; set; }
        public string Identifier { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
