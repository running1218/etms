using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    public class CourseOpenResource
    {
        public int ID { get; set; }
        public int OrgID { get; set; }
        public Guid CourseID { get; set; }
        public Guid ResourceID { get; set; }
        public string LivingID { get; set; }
        public int CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
