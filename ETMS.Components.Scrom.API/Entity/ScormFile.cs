using System;

namespace ETMS.Components.Scrom.API.Entity
{
    /// <summary>
    /// 表【sco_e_SourceFile】
    /// </summary>
    [Serializable]
    public partial class ScormFile
    {
        public Guid FileID { get; set; }
        public Guid ResourceID { get; set; }
        public string FileHref { get; set; }
    }
}
