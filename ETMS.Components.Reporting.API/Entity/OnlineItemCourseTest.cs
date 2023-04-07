using System;

namespace ETMS.Components.Reporting.API.Entity
{
    public partial class OnlineItemCourseTest
    {
        public Guid ItemStudentCourseID { get; set; }
        public Guid OnLineTestID { get; set; }
        public string OnLineTestName { get; set; }
        public Guid StudentOnlineTestID { get; set; }
        public decimal Score { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
