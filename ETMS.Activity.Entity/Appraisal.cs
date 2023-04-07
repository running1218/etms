using System;

namespace ETMS.Activity.Entity
{
    public partial class Appraisal
    {
        public Guid AppraisalID { get; set; }
        public int OrganizationID { get; set; }
        public int TypeID { get; set; }
        public int ShapeID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int LimitNum { get; set; }
        public int Status { get; set; }
        public int Creator { get; set; }
        public int Modifior { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public bool IsTop { get; set; }
        public string AppraisalTitle { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string Abstract { get; set; }
        public string Details { get; set; }
        public string ReviewRule { get; set; }
        public string Region { get; set; }
        public string Group { get; set; }
    }
}
