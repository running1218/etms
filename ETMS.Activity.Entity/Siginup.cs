using System;

namespace ETMS.Activity.Entity
{
    public partial class Siginup : Appraisal
    {
        public Guid SiginupID { get; set; }
        public Guid AppraisalID { get; set; }
        public int UserID { get; set; }
        public int SiginupStatus { get; set; }
        public int GroupID { get; set; }
        public int RegionID { get; set; }
        public DateTime SiginupTime { get; set; }
        public string SiginupNo { get; set; }
        public string School { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string Phone { get; set; }
    }
}
